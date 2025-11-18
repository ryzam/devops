using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on port 8080
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8081);
});

var app = builder.Build();

// Get pod information from environment variables
var podName = Environment.GetEnvironmentVariable("HOSTNAME") ?? "unknown-pod";
var nodeName = Environment.GetEnvironmentVariable("NODE_NAME") ?? Environment.MachineName;
var podIP = Environment.GetEnvironmentVariable("POD_IP") ?? "unknown-ip";
var namespaceName = Environment.GetEnvironmentVariable("NAMESPACE") ?? "default";

// Generate a unique instance ID for this pod
var instanceId = Guid.NewGuid().ToString().Substring(0, 8);


app.MapGet("/info", () =>
{
    var podInfo = new
    {
        environment = Environment.GetEnvironmentVariable("APP_TITTLE") ?? "DotNetCore Minimal API",
        instanceId = instanceId,
        podName = podName,
        nodeName = nodeName,
        podIP = podIP,
        namespaceName = namespaceName,
        timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC"),
        machineName = Environment.MachineName,
        processId = Environment.ProcessId,
        dotnetVersion = Environment.Version.ToString()
    };
    return Results.Json(podInfo);
});

// New endpoint: /save?data=yourtext
app.MapGet("/save", (string data) =>
{
    var dirPath = "public";
    Directory.CreateDirectory(dirPath);
    var filePath = Path.Combine(dirPath, "saved_data.txt");
    File.AppendAllText(filePath, $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss UTC}: {data}\n");
    return Results.Ok($"Saved: {data}");
});

app.MapGet("/health", () => Results.Ok("Healthy"));

app.MapGet("/", () => Results.Ok($"Pod Load Balancer Demo API - Instance {instanceId} {nodeName}"));

app.Run();