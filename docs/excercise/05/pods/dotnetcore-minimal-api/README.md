# ASP.NET Core Pod Info API - Load Balancing Exercise

This exercise demonstrates internal load balancing in Kubernetes using ClusterIP services with multiple pod replicas.

## Overview

The ASP.NET Core minimal API returns pod-specific information including:
- Unique instance ID (generated per pod)
- Pod name (from Kubernetes)
- Node name
- Pod IP address
- Namespace
- Machine name
- Process ID
- .NET version

## Files

- `Program.cs` - ASP.NET Core minimal API
- `PodInfoApi.csproj` - Project file
- `Dockerfile` - Multi-stage Docker build
- `deployment.yaml` - Kubernetes deployment (3 replicas)
- `service.yaml` - ClusterIP service configuration

## API Endpoints

- `GET /` - Welcome message with instance ID
- `GET /info` - Detailed pod information (JSON)
- `GET /health` - Health check endpoint

## Building and Deploying

### 1. Build the Docker Image

```bash
cd docs/excercise/05

# Build the Docker image
docker build -t podinfo-api:latest .

# Verify the image
docker images | grep podinfo-api
```

### 2. Deploy to Kubernetes

```bash
# Apply the deployment
kubectl apply -f deployment.yaml

# Apply the service
kubectl apply -f service.yaml

# Check deployment status
kubectl get deployments
kubectl get pods
kubectl get services
```

### 3. Test Load Balancing

```bash
# Get the service cluster IP
kubectl get svc podinfo-api-service

# Test the service (run multiple times to see load balancing)
for i in {1..10}; do
  echo "Request $i:"
  curl http://<SERVICE-CLUSTER-IP>/info
  echo -e "\n"
done

# Or use the service name directly (from within cluster)
kubectl run test-pod --rm -it --image=curlimages/curl -- sh
# Inside the pod:
curl http://podinfo-api-service/info
# Exit the test pod
```

## Expected Output

Each request should show different pod information, demonstrating that requests are being load balanced across the 3 replicas:

```json
{
  "instanceId": "a1b2c3d4",
  "podName": "podinfo-api-7f8b9c6d5-abc12",
  "nodeName": "k3d-dev-server-0",
  "podIP": "10.42.0.15",
  "namespaceName": "default",
  "timestamp": "2024-01-15 10:30:45 UTC",
  "machineName": "podinfo-api-7f8b9c6d5-abc12",
  "processId": 1,
  "dotnetVersion": "8.0.0"
}
```

## Load Balancing Demonstration

1. **Multiple Requests**: Run the curl command multiple times
2. **Observe Distribution**: You should see requests served by different pods
3. **Unique Identifiers**: Each pod has a unique `instanceId` and `podName`
4. **Round Robin**: Kubernetes distributes requests across healthy pods

## Troubleshooting

### Check Pod Status
```bash
kubectl get pods -l app=podinfo-api
kubectl describe pod <pod-name>
kubectl logs <pod-name>
```

### Check Service
```bash
kubectl get svc podinfo-api-service
kubectl describe svc podinfo-api-service
```

### Test from Within Cluster
```bash
# Create a test pod
kubectl run debug-pod --rm -it --image=curlimages/curl -- sh

# Test service discovery
curl http://podinfo-api-service/info
nslookup podinfo-api-service

# Exit
exit
```

### Port Forwarding (Alternative Testing)
```bash
# Forward service port to localhost
kubectl port-forward svc/podinfo-api-service 8080:80

# Test locally
curl http://localhost:8080/info
```

## Key Concepts Demonstrated

1. **ClusterIP Service**: Internal load balancing within the cluster
2. **Pod Replicas**: Multiple instances for high availability
3. **Service Discovery**: DNS-based service resolution
4. **Load Distribution**: Round-robin across healthy pods
5. **Pod Identity**: Unique information per pod instance
6. **Health Checks**: Liveness and readiness probes
7. **Resource Limits**: CPU and memory constraints

## Cleanup

```bash
kubectl delete -f service.yaml
kubectl delete -f deployment.yaml
```

## Next Steps

- Try scaling the deployment: `kubectl scale deployment podinfo-api --replicas=5`
- Observe how load balancing adapts to more pods
- Experiment with different service types (NodePort, LoadBalancer)
- Add ingress for external access

## Test WebHook ArgoCD