# Hands-On Kubernetes Workshop

Welcome to the practical Kubernetes workshop! This hands-on session will guide you through deploying real applications to Kubernetes, giving you practical experience with container orchestration.

## Workshop Overview

**Duration:** 90 minutes  
**Format:** Hands-on, instructor-guided  
**Prerequisites:** Docker, kubectl, and KIND installed (see [prerequisites](../00-prerequisites.md))

## What You'll Build

By the end of this workshop, you'll have:
- ✅ Deployed an ASP.NET Core API to Kubernetes
- ✅ Deployed a Node.js web application to Kubernetes
- ✅ Deployed a Laravel PHP application to Kubernetes
- ✅ Scaled applications dynamically
- ✅ Performed rolling updates with zero downtime
- ✅ Exposed services to external traffic
- ✅ Monitored application health

## Workshop Structure

### Exercise 1: Deploy ASP.NET Core API (30 minutes)
**Location:** [`aspnet-core/`](aspnet-core/)

Build and deploy a RESTful API that manages a product catalog:
- Create Dockerfile for .NET 8 application
- Build and push container image
- Deploy to Kubernetes cluster
- Test API endpoints
- View logs and troubleshoot

**Skills Gained:**
- Containerizing .NET applications
- Kubernetes Deployment manifests
- Service exposure (ClusterIP, NodePort)
- kubectl commands for management

### Exercise 2: Deploy Node.js Web Application (30 minutes)
**Location:** [`nodejs/`](nodejs/)

Deploy a web application with database backend:
- Multi-stage Docker build for efficiency
- Deploy MongoDB as StatefulSet
- Deploy Node.js web app
- Configure service communication
- Access web interface

**Skills Gained:**
- Multi-stage Docker builds
- StatefulSets for databases
- ConfigMaps and Secrets
- Service discovery
- Persistent storage

### Exercise 3: Deploy Laravel PHP Application (30 minutes)
**Location:** [`laravel/`](laravel/)

Deploy a Blog API with PHP Laravel framework:
- Build Laravel application with Nginx + PHP-FPM
- Deploy MySQL StatefulSet for database
- Deploy Redis for caching and sessions
- Run database migrations in init containers
- Configure environment with ConfigMaps and Secrets
- Test RESTful API endpoints

**Skills Gained:**
- Multi-container pods (Nginx + PHP-FPM)
- Init containers for database migrations
- Redis integration for caching
- Laravel-specific deployment patterns
- PHP application containerization
- Advanced StatefulSet configurations

### Exercise 4: Scaling and Updates (20 minutes)
**Location:** [`scaling-updates/`](scaling-updates/)

Experience Kubernetes automation:
- Manual scaling of deployments
- Configure Horizontal Pod Autoscaler
- Perform rolling updates
- Rollback failed deployments
- Zero-downtime deployment verification

**Skills Gained:**
- Scaling strategies
- Rolling update configuration
- Health checks (readiness/liveness probes)
- Rollback procedures
- Update strategies

### Bonus Exercise: GitOps with ArgoCD (Optional)
**Location:** [`gitops/`](gitops/)

If time permits, experience automated deployment:
- Install ArgoCD
- Create Application manifest
- Auto-sync from Git repository
- Observe deployment automation

## Pre-Workshop Checklist

Before starting, verify your environment:

```bash
# Check Docker
docker --version
# Expected: Docker version 20.10+ or higher

# Check kubectl
kubectl version --client
# Expected: v1.27+ or higher

# Check KIND
kind version
# Expected: kind v0.20.0+ or higher

# Verify Docker is running
docker ps
# Should show running containers or empty list (no errors)
```

If any commands fail, see [Prerequisites & Installation](../00-prerequisites.md).

## Workshop Resources

Each exercise includes:
- `README.md` - Step-by-step instructions
- `src/` - Application source code
- `k8s/` - Kubernetes manifests
- `Dockerfile` - Container build instructions
- `solution/` - Complete working solution

## Getting Started

1. **Create local Kubernetes cluster:**
```bash
# Create cluster with 2 worker nodes
kind create cluster --name workshop --config=cluster-config.yaml

# Verify cluster is ready
kubectl cluster-info --context kind-workshop
kubectl get nodes
```

2. **Navigate to Exercise 1:**
```bash
cd aspnet-core/
cat README.md  # Read instructions
```

3. **Follow step-by-step instructions** in each exercise

4. **Ask questions!** No question is too basic

## Workshop Tips

### For Success:
- ✅ Read each step carefully before executing
- ✅ Understand what each command does
- ✅ Experiment - you can't break anything!
- ✅ Check logs when things don't work: `kubectl logs <pod-name>`
- ✅ Use `kubectl describe` for troubleshooting

### Common Commands You'll Use:
```bash
# View resources
kubectl get pods
kubectl get deployments
kubectl get services

# Detailed information
kubectl describe pod <pod-name>
kubectl describe deployment <deployment-name>

# Logs
kubectl logs <pod-name>
kubectl logs <pod-name> -f  # Follow logs

# Execute commands in pod
kubectl exec -it <pod-name> -- /bin/bash

# Port forwarding (access service locally)
kubectl port-forward service/<service-name> 8080:80

# Delete resources
kubectl delete deployment <deployment-name>
kubectl delete service <service-name>
```

## Troubleshooting

### Pod Not Starting?
```bash
# Check pod status
kubectl get pods

# View pod details
kubectl describe pod <pod-name>

# Check logs
kubectl logs <pod-name>

# Common issues:
# - Image pull errors: Check image name and registry
# - CrashLoopBackOff: Check application logs
# - Pending: Check resource availability
```

### Service Not Accessible?
```bash
# Verify service exists
kubectl get services

# Check service endpoints
kubectl get endpoints <service-name>

# Test from inside cluster
kubectl run test-pod --rm -it --image=curlimages/curl -- sh
# Then: curl http://<service-name>
```

### Cluster Issues?
```bash
# Delete and recreate cluster
kind delete cluster --name workshop
kind create cluster --name workshop --config=cluster-config.yaml
```

## Workshop Support

**During the Workshop:**
- 👋 Raise your hand for help
- 💬 Ask questions anytime
- 🤝 Help your neighbor if they're stuck
- 📸 Take screenshots of your working deployments!

**After the Workshop:**
- 📧 Email questions to instructor
- 💬 Join Kubernetes Slack (slack.kubernetes.io)
- 📚 Review course materials in `docs/`
- 🚀 Practice by deploying your own projects

## Post-Workshop Learning Path

After completing this workshop:

1. **Week 1-2:** Review core concepts
   - Re-do exercises without looking at solutions
   - Try deploying your own applications
   - Read [Kubernetes Components](../05-kubernetes-components.md)

2. **Week 3-4:** Explore advanced topics
   - Set up monitoring with Prometheus
   - Implement ingress controllers
   - Practice with StatefulSets
   - Read [Administration & Security](../06-kubernetes-administration-security.md)

3. **Week 5-6:** Production skills
   - Set up HA cluster with RKE2
   - Implement GitOps with ArgoCD
   - Configure load balancing
   - Read [HA Cluster Setup](../08-ha-cluster-rke2.md)

## Additional Resources

- 📖 **Official Documentation:** [kubernetes.io](https://kubernetes.io)
- 🎓 **Interactive Tutorials:** [kubernetes.io/docs/tutorials](https://kubernetes.io/docs/tutorials/)
- 📺 **Video Content:** CNCF YouTube Channel
- 💬 **Community:** Kubernetes Slack (54k+ members)
- 📚 **Books:** "Kubernetes Up & Running" by Kelsey Hightower

## Feedback

Please provide feedback after the workshop:
- What worked well?
- What was confusing?
- What topics need more coverage?
- Suggestions for improvement?

**Feedback Form:** [Link to feedback form]

---

## Quick Reference

### Cluster Setup
```bash
# Create cluster
kind create cluster --name workshop

# Get cluster info
kubectl cluster-info

# View nodes
kubectl get nodes
```

### Deploy Application
```bash
# Apply manifest
kubectl apply -f deployment.yaml

# Check status
kubectl get deployments
kubectl get pods

# View logs
kubectl logs -l app=myapp
```

### Access Application
```bash
# Port forward
kubectl port-forward deployment/myapp 8080:80

# Access at: http://localhost:8080
```

### Scale Application
```bash
# Manual scaling
kubectl scale deployment myapp --replicas=5

# Auto-scaling
kubectl autoscale deployment myapp --min=2 --max=10 --cpu-percent=70
```

### Update Application
```bash
# Update image
kubectl set image deployment/myapp app=myapp:v2

# Watch rollout
kubectl rollout status deployment/myapp

# Rollback if needed
kubectl rollout undo deployment/myapp
```

### Cleanup
```bash
# Delete resources
kubectl delete -f deployment.yaml

# Delete cluster
kind delete cluster --name workshop
```

---

**Let's get started! → [Exercise 1: ASP.NET Core API](aspnet-core/)**

🚀 Happy Learning!