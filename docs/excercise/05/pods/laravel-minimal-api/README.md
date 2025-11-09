# Laravel Pod Info API - Load Balancing Exercise

This exercise demonstrates internal load balancing in Kubernetes using ClusterIP services with multiple Laravel application pod replicas.

## Overview

The Laravel API returns pod-specific information including:
- Machine name (hostname)
- Instance name (pod identifier)
- Web server type
- Timestamp

## Files

- `composer.json` - PHP dependencies and project configuration
- `routes/api.php` - Laravel API route definitions
- `app/` - Laravel application code
- `bootstrap/` - Laravel bootstrap files
- `config/` - Laravel configuration files
- `public/` - Web root directory
- `storage/` - Laravel storage directory
- `artisan` - Laravel command-line interface
- `Dockerfile` - Multi-stage build with PHP 8.2, Nginx, and Composer
- `deployment.yaml` - Kubernetes deployment (3 replicas)
- `service.yaml` - ClusterIP service configuration

## API Endpoints

### Basic Endpoints
- `GET /api/` - Welcome message with pod info
- `GET /api/info` - Detailed pod information (JSON)
- `GET /api/health` - Health check endpoint

## Expected API Response

Each request returns JSON with pod-specific information:

```json
{
  "machine_name": "laravel-pod-info-api-7f8b9c6d5-abc12",
  "instance_name": "laravel-pod-info-api-7f8b9c6d5-abc12",
  "web_server": "nginx",
  "timestamp": "2024-01-15T10:30:45.000000Z"
}
```

## Building and Deploying

### Prerequisites

Ensure you have Docker and Kubernetes (kubectl) installed and configured.

### Step 1: Build the Docker Image

```bash
cd docs/excercise/05/laravel-minimal-api

# Build the Docker image
docker build -t laravel-pod-info-api:latest .

# Verify the image
docker images | grep laravel-pod-info-api
```

### Step 2: Deploy to Kubernetes

```bash
# Apply the deployment
kubectl apply -f deployment.yaml

# Apply the service
kubectl apply -f service.yaml

# Check deployment status
kubectl get deployments,pods,services
```

### Step 3: Test Load Balancing

```bash
# Get the service cluster IP
kubectl get svc laravel-pod-info-api-service

# Test the service (run multiple times to see load balancing)
for i in {1..10}; do
  echo "Request $i:"
  curl http://<SERVICE-CLUSTER-IP>/api/info
  echo -e "\n"
done

# Or use the service name directly (from within cluster)
kubectl run test-pod --rm -it --image=curlimages/curl -- sh
# Inside the pod:
curl http://laravel-pod-info-api-service/api/info
# Exit the test pod
```

## Load Balancing Demonstration

1. **Multiple Requests**: Run the curl command multiple times
2. **Observe Distribution**: You should see requests served by different pods
3. **Unique Identifiers**: Each pod has a unique `instanceId` and `podName`
4. **Round Robin**: Kubernetes distributes requests across healthy pods

## Troubleshooting

### Check Pod Status
```bash
kubectl get pods -l app=laravel-pod-info-api
kubectl describe pod <pod-name>
kubectl logs <pod-name>
```

### Check Service
```bash
kubectl get svc laravel-pod-info-api-service
kubectl describe svc laravel-pod-info-api-service
```

### Test from Within Cluster
```bash
# Create a test pod
kubectl run debug-pod --rm -it --image=curlimages/curl -- sh

# Test service discovery
curl http://laravel-pod-info-api-service/api/info
nslookup laravel-pod-info-api-service

# Exit
exit
```

### Port Forwarding (Alternative Testing)
```bash
# Forward service port to localhost
kubectl port-forward svc/laravel-pod-info-api-service 8080:80

# Test locally
curl http://localhost:8080/api/info
```

## Key Concepts Demonstrated

1. **ClusterIP Service**: Internal load balancing within the cluster
2. **Pod Replicas**: Multiple instances for high availability
3. **Service Discovery**: DNS-based service resolution
4. **Load Distribution**: Round-robin across healthy pods
5. **Pod Identity**: Unique information per pod instance
6. **Health Checks**: Liveness and readiness probes
7. **Laravel in Kubernetes**: Running Laravel applications in containers

## Laravel Configuration

### Environment Variables
The application reads Kubernetes environment variables:
- `HOSTNAME` - Pod name (used for machine_name and instance_name)

### Nginx Configuration
- Runs on port 80 inside the container
- Document root set to `/var/www/html/public`
- FastCGI proxy to PHP-FPM for PHP execution
- Laravel URL rewriting enabled

### PHP Configuration
- PHP 8.2 with required extensions (pdo_mysql, mbstring, exif, pcntl, bcmath, gd)
- Composer for dependency management
- Laravel artisan commands available
- Optimized for production use

## Scaling the Application

### Manual Scaling
```bash
# Scale to 5 replicas
kubectl scale deployment laravel-pod-info-api --replicas=5

# Check scaling
kubectl get pods -l app=laravel-pod-info-api
```

### Automatic Scaling (HPA)
```yaml
# Create hpa.yaml
apiVersion: autoscaling/v2
kind: HorizontalPodAutoscaler
metadata:
  name: php-podinfo-api-hpa
spec:
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: laravel-pod-info-api
  minReplicas: 2
  maxReplicas: 10
  metrics:
  - type: Resource
    resource:
      name: cpu
      target:
        type: Utilization
        averageUtilization: 50
```

## Monitoring

### Check Application Logs
```bash
# View logs from all pods
kubectl logs -l app=laravel-pod-info-api

# Follow logs from a specific pod
kubectl logs -f <pod-name>
```

### Monitor Resource Usage
```bash
# Check pod resource usage
kubectl top pods -l app=laravel-pod-info-api

# Check node resource usage
kubectl top nodes
```

## Cleanup

```bash
kubectl delete -f service.yaml
kubectl delete -f deployment.yaml
```

## Advanced Topics

### Custom Health Checks
The application includes custom health check endpoints that can be extended for:
- Database connectivity checks
- External service availability
- Application-specific health metrics

### Environment-Specific Configuration
For production deployments, consider:
- Database connections
- Cache configurations
- External service integrations
- Logging and monitoring setup

### Security Considerations
- Run containers as non-root user
- Use read-only root filesystem where possible
- Implement proper RBAC for Kubernetes resources
- Scan images for vulnerabilities

## Next Steps

- Add database integration (MySQL/PostgreSQL)
- Implement caching (Redis/Memcached)
- Add authentication and authorization
- Configure ingress for external access
- Set up monitoring with Prometheus/Grafana
- Implement CI/CD pipelines

This exercise provides a solid foundation for running Laravel applications in Kubernetes with proper load balancing and high availability.