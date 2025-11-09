# Module 12: Kubernetes Autoscaling with HPA and KEDA

This module demonstrates Horizontal Pod Autoscaling (HPA) and Kubernetes Event-driven Autoscaling (KEDA) using an ASP.NET Core API that can generate CPU load for testing autoscaling behavior.

## Overview

The module covers:
- **Horizontal Pod Autoscaler (HPA)** - CPU/memory-based autoscaling
- **KEDA (Kubernetes Event-driven Autoscaling)** - Event-driven autoscaling
- **Load testing** - Generating CPU load to trigger scaling
- **Monitoring** - Observing autoscaling in action

## Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Load Tester   │───▶│   HPA/KEDA      │───▶│   Deployment     │
│   (curl/http)   │    │   (Metrics)     │    │   (Pods)         │
└─────────────────┘    └─────────────────┘    └─────────────────┘
                              │
                              ▼
                       ┌─────────────────┐
                       │ Metrics Server  │
                       │ (for HPA)       │
                       └─────────────────┘
```

## Files

- `Program.cs` - ASP.NET Core minimal API with CPU load generation
- `AutoScaleApi.csproj` - .NET 8.0 project file
- `Dockerfile` - Multi-stage Docker build
- `deployment.yaml` - Kubernetes deployment (2 replicas)
- `service.yaml` - ClusterIP service
- `hpa.yaml` - Horizontal Pod Autoscaler configuration
- `keda-scaledobject.yaml` - KEDA ScaledObject for event-driven scaling

## API Endpoints

### Basic Endpoints
- `GET /` - Welcome message with pod info
- `GET /info` - Detailed pod information (JSON)
- `GET /health` - Health check

### Load Testing Endpoints
- `GET /cpu-load?duration=10&intensity=50` - Generate CPU load
  - `duration`: seconds to run (1-60, default: 10)
  - `intensity`: CPU intensity (1-100, default: 50)

- `GET /memory-load?size=10` - Allocate memory
  - `size`: MB to allocate (1-100, default: 10)

## Prerequisites

### For HPA (Basic Autoscaling)
```bash
# Metrics Server (required for HPA)
kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml

# Verify Metrics Server
kubectl get apiservices | grep metrics
```

### For KEDA (Advanced Autoscaling)
```bash
# Install KEDA
kubectl apply -f https://github.com/kedacore/keda/releases/download/v2.12.0/keda-2.12.0.yaml

# Install Prometheus (for metrics)
# Follow your Prometheus installation guide
# Or use kube-prometheus-stack

# Verify KEDA
kubectl get pods -n keda
```

## Part 1: Horizontal Pod Autoscaler (HPA)

### Step 1: Build and Deploy

```bash
cd docs/excercise/12

# Build Docker image
docker build -t autoscale-api:latest .

# Deploy to Kubernetes
kubectl apply -f deployment.yaml
kubectl apply -f service.yaml

# Verify deployment
kubectl get deployments,pods,services
```

### Step 2: Deploy HPA

```bash
# Apply HPA configuration
kubectl apply -f hpa.yaml

# Check HPA status
kubectl get hpa
kubectl describe hpa autoscale-api-hpa
```

### Step 3: Test Autoscaling

```bash
# Check initial state
kubectl get hpa
kubectl get pods

# Generate CPU load (run multiple times in parallel)
for i in {1..5}; do
  kubectl run load-test-$i --rm -it --image=curlimages/curl -- \
    curl "http://autoscale-api-service/cpu-load?duration=30&intensity=80" &
done

# Monitor scaling
kubectl get hpa -w
kubectl get pods -w

# Check pod resource usage
kubectl top pods
```

### Step 4: Observe Scaling Behavior

```bash
# Watch HPA decisions
kubectl describe hpa autoscale-api-hpa

# Check scaling events
kubectl get events --sort-by=.metadata.creationTimestamp | grep autoscale-api

# Monitor pod metrics
kubectl top pods
kubectl top nodes
```

## Part 2: KEDA Event-Driven Autoscaling

### Step 1: Install Prerequisites

```bash
# Install KEDA (if not already installed)
kubectl apply -f https://github.com/kedacore/keda/releases/download/v2.12.0/keda-2.12.0.yaml

# Install Prometheus (required for the example)
# Use kube-prometheus-stack or your preferred method
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
helm install prometheus prometheus-community/kube-prometheus-stack
```

### Step 2: Deploy KEDA ScaledObject

```bash
# Remove HPA first (can't have both)
kubectl delete hpa autoscale-api-hpa

# Apply KEDA ScaledObject
kubectl apply -f keda-scaledobject.yaml

# Check KEDA status
kubectl get scaledobject
kubectl describe scaledobject autoscale-api-keda
```

### Step 3: Test Event-Driven Scaling

```bash
# Generate sustained load to trigger scaling
while true; do
  kubectl run load-test --rm -it --image=curlimages/curl -- \
    curl "http://autoscale-api-service/cpu-load?duration=10&intensity=60"
  sleep 2
done

# Monitor KEDA scaling
kubectl get scaledobject -w
kubectl get pods -w

# Check Prometheus metrics (if available)
kubectl port-forward svc/prometheus-kube-prometheus-prometheus 9090:9090
# Open http://localhost:9090 and query metrics
```

## Load Testing Strategies

### CPU Load Testing

```bash
# Single request
kubectl run test --rm -it --image=curlimages/curl -- \
  curl "http://autoscale-api-service/cpu-load?duration=30&intensity=80"

# Multiple concurrent requests
for i in {1..10}; do
  kubectl run load-test-$i --rm -it --image=curlimages/curl -- \
    curl "http://autoscale-api-service/cpu-load?duration=30&intensity=80" &
done

# Sustained load (run in background)
while true; do
  curl "http://autoscale-api-service/cpu-load?duration=10&intensity=60"
  sleep 1
done
```

### Memory Load Testing

```bash
# Allocate memory to test memory-based scaling (if configured)
kubectl run mem-test --rm -it --image=curlimages/curl -- \
  curl "http://autoscale-api-service/memory-load?size=50"
```

### External Load Testing

```bash
# Port forward for external access
kubectl port-forward svc/autoscale-api-service 8080:80

# Use external tools
hey -n 1000 -c 10 http://localhost:8080/cpu-load?duration=5&intensity=50
# or
ab -n 1000 -c 10 http://localhost:8080/cpu-load?duration=5&intensity=50
```

## Monitoring and Troubleshooting

### HPA Monitoring

```bash
# Check HPA status
kubectl get hpa autoscale-api-hpa
kubectl describe hpa autoscale-api-hpa

# View scaling events
kubectl get events | grep autoscale-api

# Check metrics
kubectl top pods
kubectl top nodes

# Debug HPA
kubectl logs -n kube-system deployment/metrics-server
```

### KEDA Monitoring

```bash
# Check ScaledObject
kubectl get scaledobject
kubectl describe scaledobject autoscale-api-keda

# Check KEDA operator logs
kubectl logs -n keda deployment/keda-operator

# Check Prometheus metrics
kubectl port-forward svc/prometheus-kube-prometheus-prometheus 9090:9090
# Query: rate(http_requests_total[5m])
```

### Common Issues

#### HPA Not Scaling
```bash
# Check Metrics Server
kubectl get apiservices | grep metrics

# Check pod metrics
kubectl top pods

# Verify resource requests/limits
kubectl describe deployment autoscale-api
```

#### KEDA Not Scaling
```bash
# Check KEDA installation
kubectl get pods -n keda

# Check ScaledObject events
kubectl describe scaledobject autoscale-api-keda

# Verify Prometheus connectivity
kubectl logs -n keda deployment/keda-operator | grep prometheus
```

## Advanced Configurations

### Custom HPA Metrics

```yaml
# Memory-based scaling
apiVersion: autoscaling/v2
kind: HorizontalPodAutoscaler
metadata:
  name: autoscale-api-memory-hpa
spec:
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: autoscale-api
  minReplicas: 2
  maxReplicas: 10
  metrics:
  - type: Resource
    resource:
      name: memory
      target:
        type: Utilization
        averageUtilization: 70
```

### Multiple KEDA Triggers

```yaml
apiVersion: keda.sh/v1alpha1
kind: ScaledObject
metadata:
  name: autoscale-api-multi-trigger
spec:
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: autoscale-api
  minReplicaCount: 2
  maxReplicaCount: 20
  triggers:
  - type: cpu
    metadata:
      type: Utilization
      value: "60"
  - type: memory
    metadata:
      type: Utilization
      value: "70"
  - type: prometheus
    metadata:
      serverAddress: http://prometheus:9090
      metricName: http_requests_per_second
      query: sum(rate(http_requests_total[1m]))
      threshold: "50"
```

## Performance Tuning

### HPA Behavior Tuning

```yaml
behavior:
  scaleDown:
    stabilizationWindowSeconds: 300  # Wait 5min before scaling down
    policies:
    - type: Percent
      value: 50  # Scale down by max 50% per minute
      periodSeconds: 60
  scaleUp:
    stabilizationWindowSeconds: 60   # Wait 1min before scaling up
    policies:
    - type: Percent
      value: 100  # Scale up by max 100% per minute
      periodSeconds: 60
```

### Resource Optimization

```yaml
resources:
  requests:
    cpu: "200m"     # Higher requests for stable metrics
    memory: "256Mi"
  limits:
    cpu: "1000m"    # Allow bursting for load testing
    memory: "512Mi"
```

## Cleanup

```bash
# Remove all resources
kubectl delete -f hpa.yaml
kubectl delete -f keda-scaledobject.yaml
kubectl delete -f service.yaml
kubectl delete -f deployment.yaml

# Remove test pods
kubectl delete pods -l run=load-test
kubectl delete pods -l run=test
```

## Key Concepts Demonstrated

1. **Horizontal Pod Autoscaler (HPA)**
   - CPU utilization-based scaling
   - Resource metrics collection
   - Automatic pod scaling

2. **KEDA (Kubernetes Event-driven Autoscaling)**
   - Event-driven scaling
   - Custom metrics integration
   - Prometheus metrics consumption

3. **Load Testing**
   - CPU load generation
   - Memory allocation testing
   - Concurrent request simulation

4. **Monitoring & Observability**
   - Metrics Server integration
   - Prometheus metrics
   - Scaling event observation

5. **Production Considerations**
   - Resource requests/limits
   - Scaling policies
   - Stabilization windows

## Next Steps

- Experiment with different scaling thresholds
- Try memory-based scaling
- Implement custom metrics with Prometheus
- Explore Cluster Autoscaler for node scaling
- Integrate with CI/CD pipelines for automated scaling

## Troubleshooting Guide

### HPA Not Working
1. Verify Metrics Server is installed and running
2. Check pod resource requests/limits
3. Ensure CPU utilization exceeds threshold
4. Check HPA events and conditions

### KEDA Not Working
1. Verify KEDA operator is installed
2. Check ScaledObject status
3. Verify Prometheus connectivity
4. Check metric queries in Prometheus

### Load Testing Issues
1. Ensure pods have sufficient resources
2. Check network connectivity
3. Verify service endpoints
4. Monitor pod logs for errors

This module provides hands-on experience with both traditional HPA and modern event-driven autoscaling, preparing you for production Kubernetes deployments with automatic scaling capabilities.