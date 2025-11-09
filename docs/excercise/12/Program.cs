using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on port 8080
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

var app = builder.Build();

// Get pod information from environment variables
var podName = Environment.GetEnvironmentVariable("HOSTNAME") ?? "unknown-pod";
var nodeName = Environment.GetEnvironmentVariable("NODE_NAME") ?? Environment.MachineName;
var podIP = Environment.GetEnvironmentVariable("POD_IP") ?? "unknown-ip";
var namespaceName = Environment.GetEnvironmentVariable("NAMESPACE") ?? "default";

// Generate a unique instance ID for this pod
var instanceId = Guid.NewGuid().ToString().Substring(0, 8);

app.MapGet("/", () => Results.Ok(new
{
    message = "CPU Load Testing API",
    instanceId = instanceId,
    podName = podName,
    timestamp = DateTime.UtcNow
}));

app.MapGet("/info", () =>
{
    var process = Process.GetCurrentProcess();
    var podInfo = new
    {
        instanceId = instanceId,
        podName = podName,
        nodeName = nodeName,
        podIP = podIP,
        namespaceName = namespaceName,
        timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC"),
        machineName = Environment.MachineName,
        processId = Environment.ProcessId,
        dotnetVersion = Environment.Version.ToString(),
        cpuTime = process.TotalProcessorTime.TotalMilliseconds,
        memoryUsage = process.WorkingSet64 / 1024 / 1024, // MB
        threadCount = process.Threads.Count
    };

    return Results.Json(podInfo);
});

// CPU load endpoint - generates CPU load for testing autoscaling
app.MapGet("/cpu-load", async (int duration = 10, int intensity = 50) =>
{
    if (duration < 1 || duration > 60) duration = 10;
    if (intensity < 1 || intensity > 100) intensity = 50;

    var startTime = DateTime.UtcNow;
    var endTime = startTime.AddSeconds(duration);

    // Generate CPU load by performing calculations
    long operations = 0;
    while (DateTime.UtcNow < endTime)
    {
        // Perform CPU-intensive operations
        for (int i = 0; i < intensity * 1000; i++)
        {
            Math.Sqrt(i);
            Math.Pow(i, 2);
            operations++;
        }

        // Small delay to prevent 100% CPU
        await Task.Delay(1);
    }

    return Results.Json(new
    {
        instanceId = instanceId,
        podName = podName,
        duration = duration,
        intensity = intensity,
        operations = operations,
        completedAt = DateTime.UtcNow,
        cpuLoadGenerated = true
    });
});

// Memory load endpoint - allocates memory for testing
app.MapGet("/memory-load", (int size = 10) =>
{
    if (size < 1 || size > 100) size = 10;

    // Allocate memory (in MB)
    var memoryBlocks = new List<byte[]>();
    for (int i = 0; i < size; i++)
    {
        memoryBlocks.Add(new byte[1024 * 1024]); // 1MB each
    }

    var process = Process.GetCurrentProcess();
    return Results.Json(new
    {
        instanceId = instanceId,
        podName = podName,
        memoryAllocated = size,
        totalMemoryMB = process.WorkingSet64 / 1024 / 1024,
        memoryBlocks = memoryBlocks.Count,
        timestamp = DateTime.UtcNow
    });
});

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    instanceId = instanceId,
    timestamp = DateTime.UtcNow
}));

app.Run();