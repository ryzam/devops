# Setting up a Local Environment with KIND

KIND (Kubernetes in Docker) is a tool for running local Kubernetes clusters using Docker containers as nodes. It's perfect for development, testing, and learning Kubernetes without requiring expensive infrastructure. KIND creates multi-node clusters by running Kubernetes components as containers within Docker.

## Why KIND for Local Development?

- **Fast Setup**: Create a cluster in minutes
- **Resource Efficient**: Runs entirely within Docker containers
- **Multi-Node Support**: Simulate production-like environments
- **Easy Cleanup**: Delete clusters instantly
- **CI/CD Ready**: Perfect for automated testing

## Installation

1.  **Install Docker:** If you don't already have Docker installed, follow the instructions for your operating system: [https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/)

2.  **Install KIND:** Follow the instructions for your operating system to install KIND: [https://kind.sigs.k8s.io/docs/user/quick-start/#installation](https://kind.sigs.k8s.io/docs/user/quick-start/#installation)

## Creating a Cluster

1.  **Create a cluster:** To create a new cluster, run the following command:

    ```bash
    kind create cluster
    ```

    This will create a new cluster with a single control-plane node.

2.  **Get cluster info:** To get information about your new cluster, run the following command:

    ```bash
    kubectl cluster-info --context kind-kind
    ```

3.  **Delete a cluster:** To delete your cluster, run the following command:

    ```bash
    kind delete cluster
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
    kubectl expose deployment nginx-deployment --port=80 --type=NodePort
    ```

5.  **Access the application:** To access the application, you will need to get the port that the service is running on. Run the following command to get the port:

    ```bash
    kubectl get services
    ```

    You can then access the application by opening a web browser and navigating to `http://localhost:<port>`.

## Advanced KIND Configuration

### Multi-Node Cluster Setup

Create a `kind-config.yaml` file for a more realistic cluster:

```yaml
kind: Cluster
apiVersion: kind.x-k8s.io/v1alpha4
nodes:
- role: control-plane
  kubeadmConfigPatches:
  - |
    kind: InitConfiguration
    nodeRegistration:
      kubeletExtraArgs:
        node-labels: "ingress-ready=true"
  extraPortMappings:
  - containerPort: 80
    hostPort: 80
    protocol: TCP
  - containerPort: 443
    hostPort: 443
    protocol: TCP
- role: worker
- role: worker
- role: worker
```

Create the cluster with this configuration:

```bash
kind create cluster --config kind-config.yaml --name my-cluster
```

### Integrating with Rancher

After creating your KIND cluster, you can import it into Rancher for management:

1. **Get kubeconfig**: `kind get kubeconfig --name my-cluster > kubeconfig.yaml`
2. **Import into Rancher**: Use Rancher's UI to import the cluster using the generated kubeconfig

## Troubleshooting Common Issues

- **Port conflicts**: If ports 80/443 are in use, modify the `extraPortMappings` in your config
- **Resource limits**: KIND clusters need sufficient Docker resources (4GB+ RAM recommended)
- **DNS issues**: Ensure Docker DNS is working: `docker run --rm busybox nslookup google.com`
- **Cleanup**: Always delete clusters when done: `kind delete cluster --name my-cluster`