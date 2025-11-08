# 10-Day Kubernetes DevOps Training Agenda

**Training Duration:** 10 Days  
**Daily Schedule:** 8:30 AM - 5:00 PM  
**Lunch Break:** 12:30 PM - 2:00 PM (1.5 hours)  
**Effective Training Time:** 7 hours per day | 70 hours total

## Training Schedule Template

**Morning Session:** 8:30 AM - 12:30 PM (4 hours)  
**Lunch Break:** 12:30 PM - 2:00 PM (1.5 hours)  
**Afternoon Session:** 2:00 PM - 5:00 PM (3 hours)

---

## Day 1: Introduction to DevOps and Containerization

**Focus:** Understanding the WHY before the HOW

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 9:00 | 30 min | Welcome & Introductions | Interactive | Icebreaker activities |
| 9:00 - 10:10 | 70 min | **Introduction Presentation** | Lecture | [00-introduction-presentation.md](00-introduction-presentation.md) |
| | | - Developer pain points | | |
| | | - DevOps transformation story | | |
| | | - Real-world case studies (Netflix, Spotify, Airbnb) | | |
| | | - Kubernetes architecture overview | | |
| 10:10 - 10:20 | 10 min | Break | - | - |
| 10:20 - 11:30 | 70 min | **Why Containerize?** | Lecture & Demo | [01-why-containerize.md](01-why-containerize.md) |
| | | - Docker fundamentals | | |
| | | - Container vs VM comparison | | |
| | | - Docker commands demo | | |
| | | - Building first Docker image | | |
| 11:30 - 12:30 | 60 min | **Hands-On: Docker Basics** | Exercise | [01-why-containerize.md#exercises](01-why-containerize.md) |
| | | - Exercise 1: Run hello-world container | | |
| | | - Exercise 2: Build custom web app | | |
| | | - Exercise 3: Docker Compose multi-container | | |

**Morning Objectives:**
- ✅ Understand DevOps principles and benefits
- ✅ Grasp containerization advantages
- ✅ Run first Docker containers
- ✅ Build custom Docker images

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:00 | 60 min | **Why Kubernetes?** | Lecture | [02-why-kubernetes-orchestration.md](02-why-kubernetes-orchestration.md) |
| | | - Container orchestration challenges | | |
| | | - Kubernetes core concepts | | |
| | | - Industry adoption and ecosystem | | |
| 3:00 - 3:10 | 10 min | Break | - | - |
| 3:10 - 4:10 | 60 min | **Kubernetes Architecture** | Lecture | [03-kubernetes-architecture.md](03-kubernetes-architecture.md) |
| | | - Control plane components | | |
| | | - Worker node components | | |
| | | - How components interact | | |
| 4:10 - 5:00 | 50 min | **Environment Setup** | Hands-On | [00-prerequisites.md](00-prerequisites.md) |
| | | - Install kubectl, k3d, Docker Desktop | | |
| | | - Verify installations | | |
| | | - Create first Kubernetes cluster | | |
| | | - kubectl basic commands | | |

**Afternoon Objectives:**
- ✅ Understand need for orchestration
- ✅ Learn Kubernetes architecture
- ✅ Set up local development environment
- ✅ Create first Kubernetes cluster

**Day 1 Homework:**
- Review presentation slides
- Complete any unfinished Docker exercises
- Ensure development environment is working

---

## Day 2: Kubernetes Fundamentals

**Focus:** Core Kubernetes concepts and components

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Day 1 Recap & Q&A | Discussion | - |
| 8:45 - 10:00 | 75 min | **Kubernetes Components Deep Dive** | Lecture | [05-kubernetes-components.md](05-kubernetes-components.md) |
| | | - Pods: The atomic unit | | |
| | | - Services: Network abstraction | | |
| | | - Deployments: Declarative updates | | |
| | | - ConfigMaps and Secrets | | |
| 10:00 - 10:10 | 10 min | Break | - | - |
| 10:10 - 11:00 | 50 min | **Hands-On: Pods and Services** | Exercise | [05-kubernetes-components.md#exercises](05-kubernetes-components.md) |
| | | - Exercise 1: Create and manage pods | | |
| | | - Exercise 2: Create services (ClusterIP, NodePort) | | |
| 11:00 - 12:30 | 90 min | **Hands-On: ConfigMaps** | Exercise | [05-kubernetes-components.md#exercise-3](05-kubernetes-components.md) |
| | | - Exercise 3: ConfigMap for Nginx HTML | | |
| | | - Mount ConfigMap as files | | |
| | | - Update content without rebuilding images | | |

**Morning Objectives:**
- ✅ Master Pods, Services, Deployments
- ✅ Understand ConfigMaps and Secrets
- ✅ Deploy first applications to Kubernetes

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:00 | 60 min | **Persistent Storage** | Lecture & Exercise | [05-kubernetes-components.md#exercise-5](05-kubernetes-components.md) |
| | | - PersistentVolumes and PersistentVolumeClaims | | |
| | | - Storage Classes | | |
| | | - Exercise 5: Working with persistent storage | | |
| 3:00 - 3:10 | 10 min | Break | - | - |
| 3:10 - 4:30 | 80 min | **Local Setup with K3d** | Hands-On | [04-local-setup-k3d.md](04-local-setup-k3d.md) |
| | | - Install and configure K3d | | |
| | | - Multi-node cluster setup | | |
| | | - Deploy sample applications | | |
| 4:30 - 5:00 | 30 min | **Day 2 Review & Q&A** | Discussion | - |

**Afternoon Objectives:**
- ✅ Understand persistent storage
- ✅ Set up multi-node local cluster
- ✅ Deploy applications to local K3d cluster

---

## Day 3: Hands-On Workshops - Part 1 (.NET & Node.js)

**Focus:** Deploy real applications to Kubernetes

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Day 2 Recap & Setup Verification | Discussion | - |
| 8:45 - 10:15 | 90 min | **Workshop: ASP.NET Core API** | Hands-On | [workshop/aspnet-core/](workshop/aspnet-core/) |
| | | - Review application code | | |
| | | - Create multi-stage Dockerfile | | |
| | | - Build Docker image | | |
| | | - Deploy to Kubernetes | | |
| | | - Test API endpoints | | |
| | | - Scale deployment | | |
| 10:15 - 10:25 | 10 min | Break | - | - |
| 10:25 - 12:30 | 125 min | **Workshop: Node.js Application** | Hands-On | [workshop/nodejs/](workshop/nodejs/) |
| | | - Review Express.js application | | |
| | | - Multi-stage Docker build | | |
| | | - Deploy MongoDB StatefulSet | | |
| | | - Configure ConfigMaps and Secrets | | |
| | | - Deploy Node.js application | | |
| | | - Test service-to-service communication | | |

**Morning Objectives:**
- ✅ Deploy production-ready ASP.NET Core API
- ✅ Deploy full-stack Node.js application with database
- ✅ Understand StatefulSets for databases

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:30 | 90 min | **Continue: Node.js Workshop** | Hands-On | [workshop/nodejs/](workshop/nodejs/) |
| | | - Test data persistence | | |
| | | - Scale application pods | | |
| | | - View logs and monitor | | |
| | | - Troubleshooting exercises | | |
| 3:30 - 3:40 | 10 min | Break | - | - |
| 3:40 - 4:50 | 70 min | **Scaling and Updates Practice** | Hands-On | Custom exercises |
| | | - Manual scaling demonstrations | | |
| | | - Rolling updates | | |
| | | - Rollback procedures | | |
| | | - Zero-downtime deployment verification | | |
| 4:50 - 5:00 | 10 min | **Day 3 Wrap-up** | Discussion | - |

**Afternoon Objectives:**
- ✅ Complete Node.js workshop
- ✅ Master scaling strategies
- ✅ Perform rolling updates
- ✅ Practice rollback procedures

---

## Day 4: Hands-On Workshops - Part 2 (Laravel) & Advanced Concepts

**Focus:** PHP ecosystem and advanced Kubernetes features

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Day 3 Recap | Discussion | - |
| 8:45 - 11:00 | 135 min | **Workshop: Laravel Application** | Hands-On | [workshop/laravel/](workshop/laravel/) |
| | | - Review Laravel Blog API | | |
| | | - Build multi-stage Dockerfile (Nginx+PHP-FPM) | | |
| | | - Deploy MySQL StatefulSet | | |
| | | - Deploy Redis for caching | | |
| | | - Configure init containers for migrations | | |
| | | - Deploy Laravel application | | |
| | | - Test API endpoints | | |
| 11:00 - 11:10 | 10 min | Break | - | - |
| 11:10 - 12:30 | 80 min | **Continue: Laravel Workshop** | Hands-On | [workshop/laravel/](workshop/laravel/) |
| | | - Verify Redis caching | | |
| | | - Test data persistence | | |
| | | - Scale application | | |
| | | - Monitor multi-container pods | | |

**Morning Objectives:**
- ✅ Deploy Laravel application with MySQL and Redis
- ✅ Understand init containers
- ✅ Configure multi-container pods
- ✅ Implement caching strategies

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:30 | 90 min | **Horizontal Pod Autoscaler (HPA)** | Lecture & Hands-On | [05-kubernetes-components.md#hpa](05-kubernetes-components.md) |
| | | - HPA architecture and concepts | | |
| | | - Metrics Server setup | | |
| | | - Create HPA for workshop apps | | |
| | | - Load testing and observing scaling | | |
| 3:30 - 3:40 | 10 min | Break | - | - |
| 3:40 - 5:00 | 80 min | **Ingress Controllers** | Lecture & Hands-On | [05-kubernetes-components.md#ingress](05-kubernetes-components.md) |
| | | - Ingress concepts | | |
| | | - Install NGINX Ingress Controller | | |
| | | - Create Ingress resources | | |
| | | - Path-based and host-based routing | | |

**Afternoon Objectives:**
- ✅ Implement auto-scaling
- ✅ Set up ingress controller
- ✅ Configure HTTP routing

---

## Day 5: Kubernetes Administration & Security

**Focus:** RBAC, security, and cluster management

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Week 1 Recap | Discussion | - |
| 8:45 - 10:30 | 105 min | **Kubernetes Administration & Security** | Lecture | [06-kubernetes-administration-security.md](06-kubernetes-administration-security.md) |
| | | - RBAC (Role-Based Access Control) | | |
| | | - Service Accounts | | |
| | | - Network Policies | | |
| | | - Pod Security Standards | | |
| | | - Security best practices | | |
| 10:30 - 10:40 | 10 min | Break | - | - |
| 10:40 - 12:30 | 110 min | **Hands-On: Security Configuration** | Exercise | [06-kubernetes-administration-security.md#exercises](06-kubernetes-administration-security.md) |
| | | - Create service accounts | | |
| | | - Configure RBAC roles and bindings | | |
| | | - Implement network policies | | |
| | | - Set pod security contexts | | |

**Morning Objectives:**
- ✅ Understand Kubernetes security model
- ✅ Implement RBAC
- ✅ Configure network policies
- ✅ Apply security best practices

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:30 | 90 min | **Rancher Cluster Management** | Lecture & Demo | [07-rancher-management.md](07-rancher-management.md) |
| | | - Rancher overview and architecture | | |
| | | - Install Rancher on K3d | | |
| | | - Explore Rancher UI | | |
| | | - Multi-cluster management | | |
| | | - Deploy apps via Rancher | | |
| 3:30 - 3:40 | 10 min | Break | - | - |
| 3:40 - 5:00 | 80 min | **Hands-On: Rancher Management** | Exercise | [07-rancher-management.md#exercises](07-rancher-management.md) |
| | | - Set up Rancher | | |
| | | - Import existing clusters | | |
| | | - Deploy applications via UI | | |
| | | - Manage cluster resources | | |

**Afternoon Objectives:**
- ✅ Install and configure Rancher
- ✅ Manage clusters via Rancher UI
- ✅ Deploy applications through Rancher

---

## Day 6: High Availability Cluster Setup

**Focus:** Production-grade cluster deployment

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Previous Days Recap | Discussion | - |
| 8:45 - 10:30 | 105 min | **High Availability Concepts** | Lecture | [08-ha-cluster-rke2.md](08-ha-cluster-rke2.md) |
| | | - HA architecture patterns | | |
| | | - RKE2 overview | | |
| | | - Control plane HA | | |
| | | - etcd clustering | | |
| | | - Load balancer requirements | | |
| 10:30 - 10:40 | 10 min | Break | - | - |
| 10:40 - 12:30 | 110 min | **Hands-On: RKE2 Installation** | Exercise | [08-ha-cluster-rke2.md#installation](08-ha-cluster-rke2.md) |
| | | - Prepare infrastructure | | |
| | | - Install first control plane node | | |
| | | - Join additional control plane nodes | | |
| | | - Verify HA configuration | | |

**Morning Objectives:**
- ✅ Understand HA architecture
- ✅ Begin RKE2 cluster setup
- ✅ Configure control plane HA

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:30 | 90 min | **Continue: RKE2 Setup** | Hands-On | [08-ha-cluster-rke2.md](08-ha-cluster-rke2.md) |
| | | - Add worker nodes | | |
| | | - Verify cluster health | | |
| | | - Deploy test applications | | |
| 3:30 - 3:40 | 10 min | Break | - | - |
| 3:40 - 5:00 | 80 min | **Cluster Validation** | Exercise | [08-ha-cluster-rke2.md#validation](08-ha-cluster-rke2.md) |
| | | - Test node failure scenarios | | |
| | | - Verify etcd replication | | |
| | | - Check service continuity | | |
| | | - Performance testing | | |

**Afternoon Objectives:**
- ✅ Complete HA cluster setup
- ✅ Validate cluster resilience
- ✅ Test failover scenarios

---

## Day 7: Load Balancing & Networking

**Focus:** Production networking and load balancing

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Cluster Health Check | Hands-On | - |
| 8:45 - 10:30 | 105 min | **Load Balancing with MetalLB** | Lecture | [09-loadbalancing-metallb.md](09-loadbalancing-metallb.md) |
| | | - Load balancing concepts | | |
| | | - MetalLB architecture | | |
| | | - Layer 2 vs BGP mode | | |
| | | - IP address management | | |
| 10:30 - 10:40 | 10 min | Break | - | - |
| 10:40 - 12:30 | 110 min | **Hands-On: MetalLB Installation** | Exercise | [09-loadbalancing-metallb.md#installation](09-loadbalancing-metallb.md) |
| | | - Install MetalLB | | |
| | | - Configure IP address pool | | |
| | | - Create LoadBalancer services | | |
| | | - Test external access | | |

**Morning Objectives:**
- ✅ Understand load balancing in Kubernetes
- ✅ Install MetalLB
- ✅ Configure LoadBalancer services

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:30 | 90 min | **Advanced Networking** | Lecture & Exercise | [05-kubernetes-components.md#network-policies](05-kubernetes-components.md) |
| | | - Network policies | | |
| | | - Service mesh concepts (brief intro) | | |
| | | - DNS in Kubernetes | | |
| | | - Network troubleshooting | | |
| 3:30 - 3:40 | 10 min | Break | - | - |
| 3:40 - 5:00 | 80 min | **Deploy Workshop Apps to HA Cluster** | Exercise | Workshop apps |
| | | - Deploy ASP.NET Core API | | |
| | | - Deploy Node.js app | | |
| | | - Deploy Laravel app | | |
| | | - Configure LoadBalancer services | | |
| | | - Test high availability | | |

**Afternoon Objectives:**
- ✅ Configure network policies
- ✅ Deploy all workshop apps to HA cluster
- ✅ Test production-like setup

---

## Day 8: Security & Web Application Firewall

**Focus:** Application security and WAF implementation

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Security Review | Discussion | - |
| 8:45 - 10:30 | 105 min | **Web Application Firewall (WAF)** | Lecture | [10-waf-security.md](10-waf-security.md) |
| | | - WAF concepts and importance | | |
| | | - ModSecurity overview | | |
| | | - OWASP Core Rule Set | | |
| | | - Common attack patterns | | |
| 10:30 - 10:40 | 10 min | Break | - | - |
| 10:40 - 12:30 | 110 min | **Hands-On: WAF Setup** | Exercise | [10-waf-security.md#setup](10-waf-security.md) |
| | | - Install WAF (ModSecurity with Nginx) | | |
| | | - Configure OWASP rules | | |
| | | - Deploy WAF as Ingress | | |
| | | - Test protection against attacks | | |

**Morning Objectives:**
- ✅ Understand WAF architecture
- ✅ Install and configure WAF
- ✅ Protect applications with OWASP rules

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:30 | 90 min | **Security Hardening** | Lecture & Hands-On | [06-kubernetes-administration-security.md](06-kubernetes-administration-security.md) |
| | | - Pod security admission | | |
| | | - Image scanning | | |
| | | - Secrets encryption | | |
| | | - Audit logging | | |
| 3:30 - 3:40 | 10 min | Break | - | - |
| 3:40 - 5:00 | 80 min | **Security Testing** | Exercise | Custom exercises |
| | | - Penetration testing basics | | |
| | | - Security audit of deployed apps | | |
| | | - Implement security fixes | | |
| | | - Document security policies | | |

**Afternoon Objectives:**
- ✅ Harden cluster security
- ✅ Test security configurations
- ✅ Implement security best practices

---

## Day 9: DevOps, GitOps & CI/CD

**Focus:** Modern deployment practices and automation

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 8:45 | 15 min | Week 2 Overview | Discussion | - |
| 8:45 - 10:30 | 105 min | **DevOps with Kubernetes** | Lecture | [11-devops-kubernetes-argocd.md](11-devops-kubernetes-argocd.md) |
| | | - DevOps principles and practices | | |
| | | - GitOps methodology | | |
| | | - CI/CD pipeline design | | |
| | | - ArgoCD architecture | | |
| 10:30 - 10:40 | 10 min | Break | - | - |
| 10:40 - 12:30 | 110 min | **Hands-On: ArgoCD Installation** | Exercise | [11-devops-kubernetes-argocd.md#setup](11-devops-kubernetes-argocd.md) |
| | | - Install ArgoCD | | |
| | | - Access ArgoCD UI | | |
| | | - Configure Git repositories | | |
| | | - Create first application | | |

**Morning Objectives:**
- ✅ Understand DevOps and GitOps
- ✅ Install ArgoCD
- ✅ Configure GitOps workflow

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 3:30 | 90 min | **CI/CD Pipeline Setup** | Hands-On | [11-devops-kubernetes-argocd.md#cicd](11-devops-kubernetes-argocd.md) |
| | | - GitHub Actions configuration | | |
| | | - Docker image build automation | | |
| | | - Automated testing | | |
| | | - GitOps manifest updates | | |
| 3:30 - 3:40 | 10 min | Break | - | - |
| 3:40 - 5:00 | 80 min | **Deployment Strategies** | Hands-On | [11-devops-kubernetes-argocd.md#strategies](11-devops-kubernetes-argocd.md) |
| | | - Blue-Green deployments | | |
| | | - Canary deployments | | |
| | | - Progressive delivery | | |
| | | - Automated rollbacks | | |

**Afternoon Objectives:**
- ✅ Build complete CI/CD pipeline
- ✅ Implement GitOps workflow
- ✅ Practice deployment strategies

---

## Day 10: Integration, Best Practices & Final Project

**Focus:** Putting it all together

### Morning Session (8:30 AM - 12:30 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 8:30 - 9:00 | 30 min | **Course Review** | Discussion | - |
| | | - Key concepts recap | | |
| | | - Q&A session | | |
| 9:00 - 10:30 | 90 min | **Monitoring & Observability** | Lecture & Demo | Custom materials |
| | | - Prometheus installation | | |
| | | - Grafana dashboards | | |
| | | - Application metrics | | |
| | | - Logging with Loki/EFK | | |
| 10:30 - 10:40 | 10 min | Break | - | - |
| 10:40 - 12:30 | 110 min | **Best Practices Workshop** | Discussion & Hands-On | All materials |
| | | - Production readiness checklist | | |
| | | - Disaster recovery planning | | |
| | | - Cost optimization | | |
| | | - Documentation practices | | |

**Morning Objectives:**
- ✅ Set up monitoring stack
- ✅ Learn production best practices
- ✅ Understand disaster recovery

### Afternoon Session (2:00 PM - 5:00 PM)

| Time | Duration | Topic | Type | Materials |
|------|----------|-------|------|-----------|
| 2:00 - 4:00 | 120 min | **Final Project** | Hands-On | All workshop materials |
| | | - Deploy complete application stack: | | |
| | | &nbsp;&nbsp;• Frontend (React/Vue) | | |
| | | &nbsp;&nbsp;• Backend API (choice of .NET/Node.js/Laravel) | | |
| | | &nbsp;&nbsp;• Database (MySQL/MongoDB/PostgreSQL) | | |
| | | &nbsp;&nbsp;• Cache (Redis) | | |
| | | - Configure CI/CD with ArgoCD | | |
| | | - Set up monitoring | | |
| | | - Implement security policies | | |
| | | - Configure auto-scaling | | |
| | | - Test high availability | | |
| 4:00 - 4:10 | 10 min | Break | - | - |
| 4:10 - 4:50 | 40 min | **Project Presentations** | Presentation | - |
| | | - Each participant presents their deployment | | |
| | | - Peer feedback | | |
| 4:50 - 5:00 | 10 min | **Course Completion & Certificates** | Closing | - |

**Afternoon Objectives:**
- ✅ Build complete production-ready application
- ✅ Demonstrate learned skills
- ✅ Receive feedback and certification

---

## Daily Breakdown Summary

| Day | Focus Area | Key Topics | Hands-On Time |
|-----|------------|------------|---------------|
| **1** | Introduction | DevOps, Docker, K8s basics | 2.5 hours |
| **2** | Fundamentals | Components, Storage, K3d | 4 hours |
| **3** | Workshops 1 | ASP.NET Core, Node.js | 5 hours |
| **4** | Workshops 2 | Laravel, HPA, Ingress | 5 hours |
| **5** | Security | RBAC, Rancher | 3.5 hours |
| **6** | Production | HA cluster setup | 5 hours |
| **7** | Networking | MetalLB, LoadBalancer | 4 hours |
| **8** | Security | WAF, Hardening | 3.5 hours |
| **9** | DevOps | ArgoCD, CI/CD | 4.5 hours |
| **10** | Integration | Best practices, Final project | 4 hours |

**Total Hands-On Time:** ~41 hours (58% of training)  
**Total Lecture Time:** ~29 hours (42% of training)

---

## Pre-Training Checklist

**1 Week Before Training:**
- [ ] Send prerequisites email to participants
- [ ] Share installation guides
- [ ] Provide pre-reading materials
- [ ] Set up training environment (if virtual)
- [ ] Prepare lab infrastructure

**3 Days Before Training:**
- [ ] Verify all participants completed prerequisites
- [ ] Test all exercises in clean environment
- [ ] Prepare backup USB drives with installers
- [ ] Set up screen sharing/recording (if virtual)
- [ ] Print handouts (optional)

**Day Before Training:**
- [ ] Test presentation slides
- [ ] Verify internet connectivity
- [ ] Prepare demo environments
- [ ] Review all exercise solutions
- [ ] Charge all devices

---

## Training Delivery Tips

### Daily Routine:
1. **Start with Recap** (15 min): Review previous day's content
2. **Theory First**: Explain concepts before hands-on
3. **Frequent Breaks**: 10-minute breaks every 90-120 minutes
4. **Hands-On Focus**: 60% practical exercises
5. **End with Q&A**: Reserve last 10-15 minutes for questions

### Engagement Strategies:
- 🎯 **Interactive Polls**: Use during presentations
- 🤝 **Pair Programming**: Partner participants for exercises
- 💬 **Discussion Time**: Share real-world experiences
- 🏆 **Gamification**: Award points for completed exercises
- 📸 **Screenshot Success**: Celebrate deployment wins

### Troubleshooting Support:
- 🆘 **Sticky Notes**: Red = need help, Green = completed
- 👥 **TA Support**: Have 1-2 assistants for 10+ participants
- 💻 **Backup Environments**: Cloud-based labs if local fails
- 📱 **Chat Channel**: Slack/Teams for quick questions

---

## Materials Checklist

### Digital Materials:
- [x] Introduction presentation
- [x] All documentation in docs/
- [x] Workshop exercises source code
- [x] Kubernetes manifests
- [x] Dockerfiles
- [x] Setup scripts

### Physical Materials (Optional):
- [ ] Printed quick reference cards
- [ ] USB drives with installers
- [ ] Notebooks for participants
- [ ] Certificates of completion
- [ ] Feedback forms

---

## Post-Training Follow-Up

**Day After Training:**
- Email thank you and survey link
- Share recording links (if recorded)
- Provide additional resources
- Set up office hours schedule

**Week After Training:**
- Send advanced learning resources
- Share job opportunities
- Create alumni Slack channel
- Schedule follow-up Q&A session

**Month After Training:**
- Send certification (if applicable)
- Request testimonials
- Share success stories
- Offer continued mentorship

---

## Assessment & Certification

### Daily Assessments:
- End of Day 2: Quiz on Kubernetes basics
- End of Day 5: Security concepts test
- End of Day 9: CI/CD pipeline knowledge check

### Final Assessment:
- **Day 10 Final Project** (scored on):
  - Application deployment (30%)
  - Security implementation (25%)
  - High availability configuration (20%)
  - CI/CD setup (15%)
  - Documentation (10%)

### Certification Criteria:
- ✅ 80% attendance
- ✅ Complete all workshop exercises
- ✅ Pass final project assessment
- ✅ Demonstrate understanding of core concepts

---

## Trainer Notes

### Time Management:
- **Buffer Time**: Each session has 5-10 min buffer for overruns
- **Flexible Topics**: Days 6-8 can be reordered based on pace
- **Optional Content**: Advanced topics can be homework
- **Catch-Up Time**: Use Day 10 morning for any missed topics

### Common Pitfalls to Watch:
- ⚠️ Day 1: Environment setup issues
- ⚠️ Day 3-4: Docker build problems
- ⚠️ Day 6: HA cluster complexity
- ⚠️ Day 9: Git/GitHub configuration

### Success Indicators:
- ✅ All participants deploy first pod by Day 2
- ✅ Complete at least 2 workshop exercises by Day 4
- ✅ HA cluster operational by end of Day 6
- ✅ CI/CD pipeline working by Day 9
- ✅ Final project deployed by Day 10

---

## Training Resources

**Provided to Participants:**
- Complete course repository access
- Slack workspace invitation
- Cloud credits (if applicable)
- Kubernetes cheat sheet
- kubectl quick reference
- Access to recorded sessions

**Recommended Reading:**
- Kubernetes: Up & Running (O'Reilly)
- The DevOps Handbook
- Site Reliability Engineering (Google)

**Online Resources:**
- Kubernetes official docs
- CNCF YouTube channel
- Kubernetes Slack community
- GitHub repositories with examples

---

## Emergency Contingency Plans

### Internet Outage:
- Pre-downloaded Docker images
- Local documentation copies
- Offline exercises
- Theory sessions backup

### Participant Issues:
- Backup laptops available
- Cloud-based environments ready
- Pair programming option
- After-hours help sessions

### Content Adjustment:
- Skip optional advanced topics
- Combine related sessions
- Extend to Day 11 if needed
- Provide homework for catch-up

---

**This agenda is designed to be flexible and can be adjusted based on participant pace and needs.**

📅 **Total Training Time:** 70 hours  
🎯 **Hands-On Focus:** 58%  
👥 **Recommended Class Size:** 10-15 participants  
🏆 **Expected Outcome:** Production-ready Kubernetes skills