# Setting up a Local Environment with K3d

K3d is a lightweight wrapper to run K3s (a certified Kubernetes distribution) in Docker. It creates local Kubernetes clusters using Docker containers as nodes, making it perfect for development, testing, and learning. K3d is faster and more resource-efficient than traditional Kubernetes distributions while maintaining full Kubernetes API compatibility.

## Why K3d for Local Development?

- **Lightning Fast**: Create clusters in seconds (vs minutes with other tools)
- **Resource Efficient**: Uses minimal resources with K3s' optimized binary
- **Full Kubernetes API**: Compatible with all Kubernetes tools and features
- **Easy Cleanup**: Delete clusters instantly
- **Multi-Cluster Support**: Run multiple clusters simultaneously
- **CI/CD Ready**: Perfect for automated testing and development pipelines

## Installation

1.  **Install Docker:** If you don't already have Docker installed, follow the instructions for your operating system: [https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/)

2.  **Install K3d:** Follow the installation instructions for your operating system: [https://k3d.io/v5.4.6/#installation](https://k3d.io/v5.4.6/#installation)

   **Quick install for Linux/macOS:**
   ```bash
   curl -s https://raw.githubusercontent.com/k3d-io/k3d/main/install.sh | bash
   ```

   **For Windows (using Chocolatey):**
   ```powershell
   choco install k3d
   ```

## Creating a Cluster

1.  **Create a cluster:** To create a new cluster, run the following command:

    ```bash
   k3d cluster create mycluster --agents 2 --port "8080:80@loadbalancer" --port "8443:443@loadbalancer"
    ```

    This creates a single-node cluster with K3s. The cluster will be available immediately.

2.  **Verify cluster:** Check that the cluster is running and kubectl is configured:

    ```bash
    kubectl cluster-info
    kubectl get nodes
    ```

3.  **Delete a cluster:** To delete your cluster, run:

    ```bash
    k3d cluster delete mycluster
    ```

## Exercise: Deploy an Application

1.  **Create a deployment:** Create a file named `deployment.yaml` with the following content:

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
            image: nginx:1.14.2
            ports:
            - containerPort: 80
    ```

2.  **Apply the deployment:** Run the following command to apply the deployment:

    ```bash
    kubectl apply -f deployment.yaml
    ```

3.  **Check the deployment:** To check the status of your deployment, run the following command:

    ```bash
    kubectl get deployments
    ```

4.  **Expose the deployment:** To expose the deployment, run the following command:

    ```bash
    kubectl expose deployment nginx-deployment --port=80 --type=LoadBalancer
    ```

    ### Use Ingress
    ```bash
    apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: nginx-ingress
  annotations:
    ingress.kubernetes.io/ssl-redirect: "false"
spec:
  rules:
  - http:
      paths:
      - path: /
        pathType: Prefix
        backend:
          service:
            name: nginx-deployment
            port:
              number: 80
```

5.  **Access the application:** To access the application, you will need to get the port that the service is running on. Run the following command to get the port:

    ```bash
    kubectl get services
    ```

    You can then access the application by opening a web browser and navigating to `http://localhost:<port>`.

## Advanced K3d Configuration

### Multi-Node Cluster Setup

Create a multi-node cluster for more realistic testing:

```bash
# Create cluster with 1 control-plane and 2 worker nodes
k3d cluster create multinode \
  --servers 1 \
  --agents 2 \
  --port "8080:80@loadbalancer" \
  --port "8443:443@loadbalancer"
```

### Custom Configuration

Create a `k3d-config.yaml` for advanced cluster configuration:

```yaml
apiVersion: k3d.io/v1alpha4
kind: Simple
metadata:
  name: my-cluster
servers: 1
agents: 2
image: rancher/k3s:v1.26.0-k3s1
ports:
  - port: 8080:80
    nodeFilters:
      - loadbalancer
  - port: 8443:443
    nodeFilters:
      - loadbalancer
options:
  k3d:
    wait: true
    timeout: "60s"
  k3s:
    extraArgs:
      - arg: --disable=traefik
        nodeFilters:
          - server:*
  kubeconfig:
    updateDefaultKubeconfig: true
    switchCurrentContext: true
```

Create the cluster with configuration:

```bash
k3d cluster create --config k3d-config.yaml
```

### Integrating with Rancher

After creating your K3d cluster, you can import it into Rancher for management:

1. **Get kubeconfig**: `k3d kubeconfig get mycluster > kubeconfig.yaml`
2. **Import into Rancher**: Use Rancher's UI to import the cluster using the generated kubeconfig

### Volume Mounting and Data Persistence

```bash
# Create cluster with persistent volumes
k3d cluster create dev-cluster \
  --volume "/tmp/k3dvol:/tmp/k3dvol@server:0" \
  --volume "/tmp/k3dvol:/tmp/k3dvol@agent:0" \
  --volume "/tmp/k3dvol:/tmp/k3dvol@agent:1"
```

### Registry Integration

```bash
# Create local registry
k3d registry create myregistry.localhost --port 5000

# Create cluster with registry
k3d cluster create dev-cluster --registry-use k3d-myregistry.localhost:5000

# Push images to local registry
docker tag myapp:latest k3d-myregistry.localhost:5000/myapp:latest
docker push k3d-myregistry.localhost:5000/myapp:latest
```

## Troubleshooting Common Issues

- **Port conflicts**: Check if ports are already in use: `netstat -tulpn | grep :8080`
- **Resource limits**: K3d uses fewer resources than KIND, but ensure Docker has enough memory
- **DNS issues**: Test with: `kubectl run test --image=busybox --rm -it -- nslookup kubernetes.default`
- **Cleanup**: List all clusters: `k3d cluster list`, then delete: `k3d cluster delete CLUSTER_NAME`
- **Kubeconfig issues**: Regenerate with: `k3d kubeconfig get CLUSTER_NAME > ~/.kube/config`