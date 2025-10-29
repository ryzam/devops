# Complete Kubernetes DevOps Tutorial

This comprehensive Kubernetes DevOps training tutorial covers everything from containerization fundamentals to advanced production deployments. Whether you're new to Kubernetes or looking to deepen your expertise, this guide provides hands-on exercises, detailed explanations, and production-ready configurations.

## What You'll Learn

- **Containerization Basics**: Understanding Docker and why containers matter
- **Kubernetes Architecture**: Master nodes, worker nodes, and core components
- **Local Development**: Setting up clusters with KIND and Rancher management
- **Production Deployments**: High-availability clusters with RKE2
- **Networking & Load Balancing**: MetalLB for bare-metal environments
- **Security**: Web Application Firewall setup and best practices
- **Hands-on Exercises**: Practical examples throughout each topic

## Prerequisites

Before starting, ensure you have the required tools installed. See [Prerequisites and Installation](docs/00-prerequisites.md) for detailed setup instructions.

## Table of Contents

### Prerequisites
- [Prerequisites and Installation](docs/00-prerequisites.md) - Required tools and setup

### Introduction
- [Why Containerize?](docs/01-why-containerize.md) - Containerization advantages and Docker basics
- [Why Kubernetes? Container Orchestration](docs/03-why-kubernetes-orchestration.md) - Real-world orchestration benefits and use cases
- [Kubernetes Architecture](docs/02-kubernetes-architecture.md) - Master-worker architecture and components

### Getting Started
- [Setting up a Local Environment with K3d](docs/04-local-setup-k3d.md) - Local cluster setup with K3d
- [Kubernetes Components](docs/05-kubernetes-components.md) - Detailed component explanations
- [Kubernetes Administration & Security](docs/06-kubernetes-administration-security.md) - RBAC, network policies, and security best practices
- [Using Rancher for Cluster Management](docs/07-rancher-management.md) - Rancher UI and multi-cluster management

### Advanced Topics
- [High-Availability K8s Cluster with RKE2](docs/08-ha-cluster-rke2.md) - Production HA cluster with RKE2
- [Load Balancing with MetalLB](docs/09-loadbalancing-metallb.md) - Bare-metal load balancing
- [Securing Applications with a WAF](docs/10-waf-security.md) - Web Application Firewall setup

### DevOps & GitOps
- [DevOps with Kubernetes and ArgoCD](docs/11-devops-kubernetes-argocd.md) - DevOps methodology, GitOps practices, and ArgoCD implementation

## Getting Started

1. **Install Prerequisites**: Follow the [prerequisites guide](docs/00-prerequisites.md)
2. **Start with Basics**: Read [Why Containerize?](docs/01-why-containerize.md) and [Kubernetes Architecture](docs/02-kubernetes-architecture.md)
3. **Set Up Local Environment**: Use [KIND setup](docs/03-local-setup-kind.md) for local development
4. **Explore Management**: Learn [Rancher](docs/04-rancher-management.md) for cluster management
5. **Go Production**: Deploy [HA clusters](docs/05-ha-cluster-rke2.md) with RKE2
6. **Add Networking**: Configure [MetalLB](docs/06-loadbalancing-metallb.md) for load balancing
7. **Secure Applications**: Implement [WAF protection](docs/07-waf-security.md)

## Contributing

This tutorial is designed to be beginner-friendly yet comprehensive. If you find errors or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
