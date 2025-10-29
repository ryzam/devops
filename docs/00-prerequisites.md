# Prerequisites and Installation

Before starting the hands-on exercises in this Kubernetes tutorial, ensure you have the following prerequisites and install the required tools on your system.

## Prerequisites
- Basic knowledge of Linux, containers (Docker), and networking.
- Access to a computer with internet connection for hands-on exercises.
- Familiarity with command-line interfaces.

## Installation of Required Tools

### Package Manager Setup (Recommended)

For Windows users, we recommend using Chocolatey (choco) as a package manager to simplify installations:

1. **Install Chocolatey:**
   - Open PowerShell as Administrator
   - Run the following command:
     ```powershell
     Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
     ```
   - Restart PowerShell and verify: `choco --version`

2. **Install essential tools with Chocolatey:**
   ```powershell
   # Install Git
   choco install git -y

   # Install Kubernetes CLI
   choco install kubernetes-cli -y

   # Install Helm
   choco install kubernetes-helm -y

   # Install Docker Desktop
   choco install docker-desktop -y

   # Install other tools
   choco install k3d -y
   choco install argocd-cli -y
   ```

### Manual Installation (Alternative)

If you prefer manual installation or are on a different platform:

1. **Docker**: Required for containerization and running KIND clusters.
   - Download and install from [docker.com](https://www.docker.com/get-started).
   - Verify installation: `docker --version`

2. **kubectl**: Kubernetes command-line tool for interacting with clusters.
   - Install via package manager or download from [kubernetes.io](https://kubernetes.io/docs/tasks/tools/).
   - Example for Linux: `curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl" && chmod +x kubectl && sudo mv kubectl /usr/local/bin/`
   - Verify installation: `kubectl version --client`

3. **K3d (K3s in Docker)**: For creating lightweight local Kubernetes clusters.
   - Install with: `curl -s https://raw.githubusercontent.com/k3d-io/k3d/main/install.sh | bash`
   - Or download from [k3d.io](https://k3d.io/v5.4.6/)
   - Verify installation: `k3d version`

4. **Rancher CLI**: For managing Rancher and RKE2 clusters.
   - Download from [rancher.com](https://rancher.com/docs/rancher/v2.6/en/cli/).
   - Or install via: `wget https://releases.rancher.com/cli2/v2.6.9/rancher-linux-amd64-v2.6.9.tar.gz && tar -xzf rancher-linux-amd64-v2.6.9.tar.gz && sudo mv rancher /usr/local/bin/`
   - Verify installation: `rancher --version`

5. **RKE2**: Rancher Kubernetes Engine 2 for high-availability clusters.
   - Follow installation guide from [rke2.io](https://docs.rke2.io/install/quickstart).
   - Typically involves downloading the installer script and running it on target nodes.

6. **MetalLB**: Load balancer for bare-metal Kubernetes clusters.
   - Will be installed as part of the exercises using kubectl or Helm.
   - Ensure Helm is installed if using Helm: `curl https://raw.githubusercontent.com/helm/helm/main/scripts/get-helm-3 | bash`
   - Verify Helm: `helm version`

7. **Helm**: Package manager for Kubernetes (optional but recommended).
   - Install with: `curl https://raw.githubusercontent.com/helm/helm/main/scripts/get-helm-3 | bash`
   - Verify installation: `helm version`

Note: Installation commands may vary based on your operating system (Linux, macOS, Windows). Refer to the official documentation for the most up-to-date instructions.