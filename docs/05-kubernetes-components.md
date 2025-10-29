# Kubernetes Components

Kubernetes is composed of several key components that work together to manage containerized applications. These components can be categorized into control plane components (responsible for cluster management) and worker node components (responsible for running applications). Additionally, there are workload resources that define how applications run.

## Core Concepts Overview

Before diving into specific components, let's understand the fundamental building blocks:

- **Pods**: The smallest deployable units in Kubernetes, containing one or more containers
- **Services**: Abstractions that define a logical set of pods and enable network access
- **Deployments**: Controllers that manage pod replicas and provide declarative updates
- **Namespaces**: Virtual clusters within a physical cluster for resource isolation

## Control Plane Components

Control plane components are responsible for managing the cluster as a whole. They include:

*   **kube-apiserver:** The API server is the central point of contact for all communication with the cluster.
*   **kube-controller-manager:** The controller manager is responsible for ensuring that the desired state of the cluster is maintained.
*   **kube-scheduler:** The scheduler is responsible for scheduling pods to worker nodes.
*   **etcd:** etcd is a distributed key-value store that is used to store the state of the cluster.

## Worker Node Components

Worker node components are responsible for running the applications. They include:

*   **kubelet:** The kubelet is responsible for communicating with the master node and managing the pods on the worker node.
*   **kube-proxy:** The kube-proxy is responsible for networking between pods.
*   **Container runtime:** The container runtime is responsible for running the containers.

## Other Components

In addition to the control plane and worker node components, there are a number of other components that are used in Kubernetes. These components include:

*   **Pods:** A pod is the smallest and simplest unit in the Kubernetes object model. It represents a single instance of a running process in a cluster.
*   **Services:** A service is an abstraction that defines a logical set of pods and a policy by which to access them.
*   **Deployments:** A deployment is a controller that provides declarative updates for pods and replica sets.
*   **ReplicaSets:** A replica set is a controller that ensures that a specified number of pod replicas are running at any given time.
*   **Namespaces:** A namespace is a way to divide cluster resources between multiple users.

## Component Relationships Diagram

```mermaid
graph TB
    subgraph "Control Plane"
        API[kube-apiserver<br/>API Gateway]
        CM[kube-controller-manager<br/>Controller Logic]
        SCH[kube-scheduler<br/>Pod Scheduling]
        ETCD[(etcd<br/>Cluster State Store)]
    end

    subgraph "Worker Node"
        KUBELET[kubelet<br/>Pod Lifecycle]
        PROXY[kube-proxy<br/>Network Proxy]
        CR[Container Runtime<br/>Docker/Containerd]
    end

    subgraph "Workload Resources"
        POD[Pod<br/>Container Group]
        SVC[Service<br/>Network Abstraction]
        DEP[Deployment<br/>Replica Management]
        RS[ReplicaSet<br/>Pod Scaling]
    end

    API --> CM
    API --> SCH
    API --> ETCD
    CM --> ETCD
    SCH --> ETCD

    API --> KUBELET
    KUBELET --> POD
    KUBELET --> CR
    KUBELET --> PROXY

    DEP --> RS
    RS --> POD
    SVC --> POD
```

## Detailed Component Explanations

### Pods: The Atomic Unit

A Pod is the smallest and most basic deployable object in Kubernetes. It represents a single instance of a running process in your cluster and can contain one or more containers that share storage, network, and specifications about how to run them.

**Key Characteristics:**
- **Ephemeral**: Pods are not designed to be long-lived
- **Shared Context**: Containers in a pod share the same network namespace, IPC namespace, and can share volumes
- **Atomic Scheduling**: All containers in a pod are scheduled together on the same node

**Pod Lifecycle:**
1. **Pending**: Pod accepted by Kubernetes but containers not yet created
2. **Running**: All containers in the pod are running
3. **Succeeded**: All containers terminated successfully
4. **Failed**: At least one container terminated with failure
5. **Unknown**: State cannot be determined

### Services: Network Abstraction

A Service is an abstraction that defines a logical set of pods and a policy by which to access them. Services enable loose coupling between dependent pods and provide a stable endpoint for accessing applications.

**Service Types:**

#### 1. ClusterIP (Default)
The default service type that exposes the service on an internal IP within the cluster.

**Use Case:** Internal communication between services within the cluster.

```mermaid
graph LR
    subgraph "Kubernetes Cluster"
        POD1[Pod A<br/>app: web]
        POD2[Pod B<br/>app: web]
        POD3[Pod C<br/>app: web]
        SVC[Service<br/>Type: ClusterIP<br/>IP: 10.43.0.1]
        CLIENT[Internal Client<br/>Pod/Service]
    end

    CLIENT --> SVC
    SVC --> POD1
    SVC --> POD2
    SVC --> POD3
```

**Example:**
```yaml
apiVersion: v1
kind: Service
metadata:
  name: my-service
spec:
  selector:
    app: my-app
  ports:
  - port: 80
    targetPort: 8080
  type: ClusterIP  # Default
```

**Access:** Only accessible within the cluster via `my-service:80` or `10.43.0.1:80`

#### 2. NodePort
Exposes the service on each node's IP at a static port (range: 30000-32767).

**Use Case:** External access to services, development environments, or when LoadBalancer is not available.

```mermaid
graph LR
    subgraph "Kubernetes Cluster"
        NODE1[Node 1<br/>IP: 192.168.1.10]
        NODE2[Node 2<br/>IP: 192.168.1.11]
        POD1[Pod A<br/>app: web]
        POD2[Pod B<br/>app: web]
        SVC[Service<br/>Type: NodePort<br/>Port: 30080]
    end

    subgraph "External"
        CLIENT[External Client]
    end

    CLIENT --> NODE1
    CLIENT --> NODE2
    NODE1 --> SVC
    NODE2 --> SVC
    SVC --> POD1
    SVC --> POD2
```

**Example:**
```yaml
apiVersion: v1
kind: Service
metadata:
  name: my-service
spec:
  selector:
    app: my-app
  ports:
  - port: 80
    targetPort: 8080
    nodePort: 30080  # Optional: specify port, otherwise auto-assigned
  type: NodePort
```

**Access:** Via any node IP and the NodePort: `http://192.168.1.10:30080`

#### 3. LoadBalancer
Creates an external load balancer in supported cloud environments (AWS ELB, GCP LoadBalancer, Azure LB).

**Use Case:** Production applications requiring external load balancing and SSL termination.

```mermaid
graph LR
    subgraph "Cloud Provider"
        LB[Load Balancer<br/>External IP: 203.0.113.1]
    end

    subgraph "Kubernetes Cluster"
        NODE1[Node 1]
        NODE2[Node 2]
        POD1[Pod A<br/>app: web]
        POD2[Pod B<br/>app: web]
        SVC[Service<br/>Type: LoadBalancer]
    end

    subgraph "External"
        CLIENT[External Client]
    end

    CLIENT --> LB
    LB --> NODE1
    LB --> NODE2
    NODE1 --> SVC
    NODE2 --> SVC
    SVC --> POD1
    SVC --> POD2
```

**Example:**
```yaml
apiVersion: v1
kind: Service
metadata:
  name: my-service
spec:
  selector:
    app: my-app
  ports:
  - port: 80
    targetPort: 8080
  type: LoadBalancer
```

**Access:** Via the load balancer's external IP (automatically assigned by cloud provider)

#### 4. ExternalName
Maps a service to an external DNS name without creating endpoints.

**Use Case:** Accessing external services or legacy systems via DNS names.

```mermaid
graph LR
    subgraph "Kubernetes Cluster"
        POD[Pod<br/>app: client]
        SVC[Service<br/>Type: ExternalName<br/>externalName: api.external.com]
    end

    subgraph "External"
        EXT[External Service<br/>api.external.com]
    end

    POD --> SVC
    SVC -.->|"DNS Resolution"| EXT
```

**Example:**
```yaml
apiVersion: v1
kind: Service
metadata:
  name: external-api
spec:
  type: ExternalName
  externalName: api.external-service.com
```

**Access:** Pods access via `external-api` which resolves to `api.external-service.com`

### Deployments: Declarative Updates

A Deployment provides declarative updates for Pods and ReplicaSets. It describes a desired state and the Deployment controller changes the actual state to the desired state at a controlled rate.

**Key Features:**
- **Rolling Updates**: Update pods without downtime
- **Rollback**: Revert to previous versions
- **Scaling**: Change number of replicas
- **Pause/Resume**: Control update process

**Understanding `kubectl get deployments` Output:**

When you run `kubectl get deployments`, you'll see output like this:

```
NAME               READY   UP-TO-DATE   AVAILABLE   AGE
nginx-deployment   3/3     3            3           2m
```

**Column Explanations:**
- **NAME**: The name of the deployment
- **READY**: Shows `current/desired` replicas (e.g., 3/3 means 3 out of 3 desired pods are ready)
- **UP-TO-DATE**: Number of replicas that have been updated to the latest pod spec
- **AVAILABLE**: Number of replicas available to users (ready and able to serve traffic)
- **AGE**: How long ago the deployment was created

**Example Scenarios:**
- `3/3  3  3  2m`: Perfect state - all desired pods are ready, up-to-date, and available
- `2/3  3  2  2m`: Rolling update in progress - 3 pods updated but only 2 are ready yet
- `3/3  0  3  2m`: Deployment created but pods not updated (paused deployment)
- `1/3  1  1  2m`: Only 1 pod ready, likely due to resource constraints or failures

**Deployment Status Details:**

For more detailed information, use `kubectl describe deployment <name>`:

```bash
kubectl describe deployment nginx-deployment
```

This shows:
- **Replicas**: Desired, current, ready, available, and unavailable counts
- **Strategy**: Rolling update configuration (max unavailable, max surge)
- **Conditions**: Status conditions like Available, Progressing, ReplicaFailure
- **Events**: Timeline of deployment events and status changes
- **Pod Template**: The pod specification being deployed

### ConfigMaps and Secrets: Configuration Management

**ConfigMaps** store non-confidential configuration data in key-value pairs that can be consumed by pods.

**Secrets** store sensitive information like passwords, tokens, and keys, encoded in base64.

### Persistent Volumes: Storage Abstraction

Persistent Volumes (PV) provide API for users and administrators to abstract details of how storage is provided from how it is consumed.

**Key Concepts:**
- **PersistentVolume (PV)**: Storage resource in the cluster
- **PersistentVolumeClaim (PVC)**: Request for storage by a user
- **StorageClass**: Defines different classes of storage

## Hands-on Exercises

### Exercise 1: Working with Pods

1. **Create a simple pod:**

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: nginx-pod
  labels:
    app: nginx
spec:
  containers:
  - name: nginx
    image: nginx:1.21
    ports:
    - containerPort: 80
```

```bash
kubectl apply -f pod.yaml
kubectl get pods
kubectl describe pod nginx-pod
```

2. **Access the pod:**

```bash
kubectl port-forward pod/nginx-pod 8080:80
# Access http://localhost:8080
```

### Exercise 2: Creating Services

1. **Create a deployment and service:**

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: nginx-deployment
spec:
  replicas: 3
  selector:
    matchLabels:
      app: nginx
  template:
    metadata:
      labels:
        app: nginx
    spec:
      containers:
      - name: nginx
        image: nginx:1.21
        ports:
        - containerPort: 80
---
apiVersion: v1
kind: Service
metadata:
  name: nginx-service
spec:
  selector:
    app: nginx
  ports:
  - port: 80
    targetPort: 80
  type: ClusterIP
```

```bash
kubectl apply -f deployment-service.yaml
kubectl get services
kubectl get endpoints nginx-service
```

### Exercise 3: Using ConfigMaps

1. **Create a ConfigMap:**

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: app-config
data:
  APP_ENV: "production"
  LOG_LEVEL: "info"
  DATABASE_URL: "postgres://db:5432/myapp"
```

2. **Use ConfigMap in a pod:**

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: config-pod
spec:
  containers:
  - name: app
    image: busybox
    command: ["env"]
    envFrom:
    - configMapRef:
        name: app-config
```

```bash
kubectl apply -f configmap.yaml
kubectl apply -f config-pod.yaml
kubectl logs config-pod
```

### Exercise 4: Working with Secrets

1. **Create a Secret:**

```bash
kubectl create secret generic db-secret \
  --from-literal=username=admin \
  --from-literal=password=mypassword
```

2. **Use Secret in a pod:**

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: secret-pod
spec:
  containers:
  - name: app
    image: busybox
    command: ["sh", "-c", "echo Username: $DB_USER && echo Password: $DB_PASS"]
    env:
    - name: DB_USER
      valueFrom:
        secretKeyRef:
          name: db-secret
          key: username
    - name: DB_PASS
      valueFrom:
        secretKeyRef:
          name: db-secret
          key: password
```

### Exercise 5: Persistent Storage

1. **Create a PersistentVolumeClaim:**

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: app-storage
spec:
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 1Gi
```

2. **Use PVC in a pod:**

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: storage-pod
spec:
  containers:
  - name: app
    image: busybox
    command: ["sh", "-c", "echo 'Hello World' > /data/hello.txt && sleep 3600"]
    volumeMounts:
    - name: storage
      mountPath: /data
  volumes:
  - name: storage
    persistentVolumeClaim:
      claimName: app-storage
```

```bash
kubectl apply -f pvc.yaml
kubectl apply -f storage-pod.yaml
kubectl exec storage-pod -- cat /data/hello.txt
kubectl delete pod storage-pod
# Data persists!
kubectl apply -f storage-pod.yaml
kubectl exec storage-pod -- cat /data/hello.txt
```

## Advanced Topics

### Ingress: External Access and Routing

Ingress is an API object that manages external access to services in a cluster, typically HTTP/HTTPS traffic. It provides load balancing, SSL termination, and name-based virtual hosting.

**Key Features:**
- **HTTP/HTTPS Routing**: Route traffic based on host and path
- **SSL/TLS Termination**: Handle SSL certificates and encryption
- **Load Balancing**: Distribute traffic across multiple pods
- **Name-based Virtual Hosting**: Multiple domains on single IP

#### NGINX Ingress Controller Architecture

The NGINX Ingress Controller is a popular implementation that uses NGINX as the underlying load balancer.

**Components:**
- **Ingress Controller**: Watches for Ingress resources and configures NGINX
- **NGINX**: High-performance web server and reverse proxy
- **ConfigMap**: Controller configuration
- **Service Account**: RBAC permissions

**Request Flow Diagram:**

```mermaid
sequenceDiagram
    participant Browser
    participant DNS
    participant LB as Load Balancer (Optional)
    participant IC as Ingress Controller (NGINX)
    participant SVC as Service
    participant POD1 as Pod 1
    participant POD2 as Pod 2

    Browser->>DNS: Resolve myapp.example.com
    DNS-->>Browser: Returns IP (Load Balancer or Node)
    Browser->>LB: HTTPS Request to myapp.example.com
    LB->>IC: Forward request to Ingress Controller
    IC->>IC: Match Ingress rules (host + path)
    IC->>SVC: Route to appropriate Service
    SVC->>POD1: Load balance to Pod 1
    alt Pod 2 available
        SVC->>POD2: Or load balance to Pod 2
    end
    POD1-->>IC: Response
    IC-->>Browser: HTTPS Response
```

**Detailed Flow Explanation:**

1. **DNS Resolution**: Browser resolves domain name to IP address
2. **Load Balancer (Optional)**: External load balancer routes traffic to cluster
3. **Ingress Controller**: NGINX receives request and examines headers
4. **Rule Matching**: Controller matches host (`myapp.example.com`) and path (`/api`)
5. **Service Routing**: Routes to appropriate Kubernetes service
6. **Load Balancing**: Service distributes traffic across healthy pods
7. **Pod Processing**: Application pod processes the request
8. **Response**: Response flows back through the same path

**Example Ingress Configuration:**

```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: myapp-ingress
  annotations:
    nginx.ingress.kubernetes.io/rewrite-target: /
    nginx.ingress.kubernetes.io/ssl-redirect: "true"
spec:
  ingressClassName: nginx
  tls:
  - hosts:
    - myapp.example.com
    secretName: myapp-tls
  rules:
  - host: myapp.example.com
    http:
      paths:
      - path: /api
        pathType: Prefix
        backend:
          service:
            name: api-service
            port:
              number: 80
      - path: /
        pathType: Prefix
        backend:
          service:
            name: web-service
            port:
              number: 80
```

**NGINX Ingress Controller Installation:**

```bash
# Add Helm repository
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo update

# Install NGINX Ingress Controller
helm install nginx-ingress ingress-nginx/ingress-nginx \
  --set controller.replicaCount=2 \
  --set controller.nodeSelector."kubernetes\.io/os"=linux \
  --set defaultBackend.replicaCount=1

# Get the external IP
kubectl get svc nginx-ingress-ingress-nginx-controller
```

**Common Ingress Patterns:**

1. **Simple Routing:**
```yaml
spec:
  rules:
  - host: myapp.com
    http:
      paths:
      - path: /
        pathType: Prefix
        backend:
          service:
            name: frontend
            port:
              number: 80
```

2. **Path-based Routing:**
```yaml
spec:
  rules:
  - host: api.myapp.com
    http:
      paths:
      - path: /v1
        pathType: Prefix
        backend:
          service:
            name: api-v1
            port:
              number: 80
      - path: /v2
        pathType: Prefix
        backend:
          service:
            name: api-v2
            port:
              number: 80
```

3. **Multiple Domains:**
```yaml
spec:
  rules:
  - host: frontend.myapp.com
    http:
      paths:
      - path: /
        backend:
          service:
            name: frontend
            port:
              number: 80
  - host: api.myapp.com
    http:
      paths:
      - path: /
        backend:
          service:
            name: api
            port:
              number: 80
```

### Custom Resource Definitions (CRDs)

CRDs allow you to extend Kubernetes API with your own resource types.

### Operators

Operators are software extensions that use CRDs to manage applications and their components.

### Network Policies

Network policies specify how groups of pods are allowed to communicate with each other and other network endpoints.

## Troubleshooting Common Issues

- **Pod Pending**: Check resource availability and node capacity
- **Pod CrashLoopBackOff**: Examine container logs and exit codes
- **Service Not Accessible**: Verify selectors and endpoint creation
- **PVC Pending**: Check storage class and available PVs
- **Image Pull Errors**: Verify image names and registry access

## Best Practices

1. **Resource Limits**: Always set CPU and memory limits
2. **Health Checks**: Implement readiness and liveness probes
3. **Labels and Selectors**: Use consistent labeling strategy
4. **Security Context**: Run containers with appropriate security contexts
5. **Rolling Updates**: Use deployments for zero-downtime updates
6. **Monitoring**: Implement proper logging and metrics collection