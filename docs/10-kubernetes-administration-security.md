# Kubernetes Administration & Security

This guide covers essential administrative tasks and security best practices for managing Kubernetes clusters in production environments. Topics include user management, RBAC, network policies, security contexts, and compliance.

## Role-Based Access Control (RBAC)

RBAC is Kubernetes' authorization mechanism that regulates access to cluster resources based on roles assigned to users or service accounts.

### Core RBAC Concepts

- **Subjects**: Users, groups, or service accounts
- **Resources**: Kubernetes API objects (pods, services, etc.)
- **Verbs**: Actions allowed on resources (get, list, create, update, delete)
- **Roles**: Collections of permissions
- **RoleBindings**: Assignments of roles to subjects

### Creating RBAC Resources

1. **Create a Service Account:**

```yaml
apiVersion: v1
kind: ServiceAccount
metadata:
  name: app-service-account
  namespace: default
```

2. **Create a Role:**

```yaml
apiVersion: rbac.authorization.k8s.io/v1
kind: Role
metadata:
  namespace: default
  name: pod-reader
rules:
- apiGroups: [""]
  resources: ["pods"]
  verbs: ["get", "watch", "list"]
- apiGroups: [""]
  resources: ["pods/log"]
  verbs: ["get"]
```

3. **Create a RoleBinding:**

```yaml
apiVersion: rbac.authorization.k8s.io/v1
kind: RoleBinding
metadata:
  name: read-pods
  namespace: default
subjects:
- kind: ServiceAccount
  name: app-service-account
  namespace: default
roleRef:
  kind: Role
  name: pod-reader
  apiGroup: rbac.authorization.k8s.io
```

### Cluster-Level RBAC

For cluster-wide permissions, use ClusterRole and ClusterRoleBinding:

```yaml
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRole
metadata:
  name: cluster-admin
rules:
- apiGroups: ["*"]
  resources: ["*"]
  verbs: ["*"]
---
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRoleBinding
metadata:
  name: cluster-admin-binding
subjects:
- kind: User
  name: admin-user
  apiGroup: rbac.authorization.k8s.io
roleRef:
  kind: ClusterRole
  name: cluster-admin
  apiGroup: rbac.authorization.k8s.io
```

## Network Policies

Network policies control traffic flow between pods and external endpoints, providing network segmentation and security.

### Default Deny Policy

```yaml
apiVersion: networking.k8s.io/v1
kind: NetworkPolicy
metadata:
  name: default-deny-all
  namespace: default
spec:
  podSelector: {}
  policyTypes:
  - Ingress
  - Egress
```

### Allow Specific Traffic

```yaml
apiVersion: networking.k8s.io/v1
kind: NetworkPolicy
metadata:
  name: allow-web-traffic
  namespace: default
spec:
  podSelector:
    matchLabels:
      app: web
  policyTypes:
  - Ingress
  ingress:
  - from:
    - podSelector:
        matchLabels:
          app: api
    ports:
    - protocol: TCP
      port: 80
  - from: []
    ports:
    - protocol: TCP
      port: 443
```

## Pod Security Standards

Pod Security Standards define security contexts for pods to prevent privilege escalation and ensure secure configurations.

### Security Context Configuration

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: secure-pod
spec:
  securityContext:
    runAsUser: 1000
    runAsGroup: 1000
    fsGroup: 1000
    runAsNonRoot: true
  containers:
  - name: app
    image: nginx
    securityContext:
      allowPrivilegeEscalation: false
      readOnlyRootFilesystem: true
      runAsNonRoot: true
      capabilities:
        drop:
        - ALL
    volumeMounts:
    - name: tmp
      mountPath: /tmp
  volumes:
  - name: tmp
    emptyDir: {}
```

### Pod Security Admission

Enable Pod Security Admission controller:

```yaml
# In kube-apiserver configuration
--enable-admission-plugins=PodSecurity
```

Apply security standards:

```yaml
apiVersion: policy/v1beta1
kind: PodSecurityPolicy
metadata:
  name: restricted
spec:
  privileged: false
  allowPrivilegeEscalation: false
  requiredDropCapabilities:
  - ALL
  runAsUser:
    rule: MustRunAsNonRoot
  seLinux:
    rule: RunAsAny
  supplementalGroups:
    rule: MustRunAs
    ranges:
    - min: 1
      max: 65535
  fsGroup:
    rule: MustRunAs
    ranges:
    - min: 1
      max: 65535
  readOnlyRootFilesystem: true
```

## Secrets Management

### Best Practices for Secrets

1. **Use External Secret Management**: Integrate with tools like Vault, AWS Secrets Manager, or Azure Key Vault
2. **Rotate Secrets Regularly**: Implement automated rotation
3. **Limit Secret Access**: Use RBAC to control who can access secrets
4. **Encrypt Secrets at Rest**: Enable encryption for etcd

### External Secrets Operator

```yaml
apiVersion: external-secrets.io/v1beta1
kind: ExternalSecret
metadata:
  name: example-secret
spec:
  refreshInterval: 15s
  secretStoreRef:
    name: vault-backend
    kind: SecretStore
  target:
    name: example-secret
    creationPolicy: Owner
  data:
  - secretKey: password
    remoteRef:
      key: secret/data/database
      property: password
```

## Audit Logging

Enable audit logging to track API server requests:

```yaml
# kube-apiserver configuration
--audit-policy-file=/etc/kubernetes/audit-policy.yaml
--audit-log-path=/var/log/kubernetes/audit.log
--audit-log-maxage=30
--audit-log-maxbackup=10
--audit-log-maxsize=100
```

Audit policy example:

```yaml
apiVersion: audit.k8s.io/v1
kind: Policy
rules:
- level: Metadata
  verbs: ["create", "update", "delete"]
  resources:
  - group: ""
    resources: ["secrets"]
- level: RequestResponse
  verbs: ["create", "update"]
  resources:
  - group: ""
    resources: ["configmaps"]
  - group: "rbac.authorization.k8s.io"
    resources: ["clusterroles", "clusterrolebindings"]
```

## Resource Quotas and Limits

### Namespace Resource Quotas

```yaml
apiVersion: v1
kind: ResourceQuota
metadata:
  name: compute-resources
  namespace: default
spec:
  hard:
    requests.cpu: "4"
    requests.memory: 8Gi
    limits.cpu: "8"
    limits.memory: 16Gi
    persistentvolumeclaims: "10"
    pods: "50"
    services: "20"
```

### Limit Ranges

```yaml
apiVersion: v1
kind: LimitRange
metadata:
  name: cpu-memory-limits
  namespace: default
spec:
  limits:
  - default:
      cpu: 500m
      memory: 512Mi
    defaultRequest:
      cpu: 100m
      memory: 128Mi
    type: Container
```

## Cluster Administration Tasks

### Node Management

1. **Cordon a node (prevent scheduling):**

```bash
kubectl cordon node-name
```

2. **Drain a node (evict pods):**

```bash
kubectl drain node-name --ignore-daemonsets --delete-emptydir-data
```

3. **Uncordon a node:**

```bash
kubectl uncordon node-name
```

### Backup and Recovery

1. **etcd Backup:**

```bash
# For kubeadm clusters
kubectl get pods -n kube-system
# Find etcd pod name
kubectl exec -n kube-system etcd-pod-name -- etcdctl snapshot save /tmp/etcd-backup.db
```

2. **Cluster Backup with Velero:**

```bash
velero backup create my-backup --include-namespaces=default,production
velero backup get
```

### Upgrading Clusters

1. **Check upgrade path:**

```bash
kubeadm upgrade plan
```

2. **Upgrade control plane:**

```bash
kubeadm upgrade apply v1.28.0
```

3. **Upgrade kubelet on nodes:**

```bash
kubectl drain node-name
# Upgrade kubelet
kubectl uncordon node-name
```

## Security Best Practices

### 1. API Server Security

- Use `--anonymous-auth=false`
- Enable RBAC: `--authorization-mode=RBAC`
- Use secure TLS certificates
- Enable audit logging
- Set appropriate timeouts

### 2. Network Security

- Use network policies to isolate namespaces
- Implement service mesh (Istio, Linkerd)
- Use TLS everywhere
- Restrict pod-to-pod communication

### 3. Image Security

- Scan images for vulnerabilities
- Use trusted registries
- Implement image signing
- Run containers as non-root
- Use read-only root filesystems

### 4. Monitoring and Compliance

- Implement comprehensive logging
- Set up security monitoring
- Regular security audits
- Keep components updated
- Implement CIS benchmarks

## Troubleshooting Security Issues

### Common Security-Related Problems

1. **RBAC Permission Denied:**

```bash
kubectl auth can-i get pods --as=system:serviceaccount:default:app-sa
kubectl get rolebindings,clusterrolebindings -o wide
```

2. **Network Policy Blocking Traffic:**

```bash
kubectl get networkpolicies
kubectl describe networkpolicy policy-name
# Check pod labels and selectors
```

3. **Pod Security Violations:**

```bash
kubectl get events --field-selector reason=FailedCreate
kubectl describe pod pod-name
```

4. **Certificate Issues:**

```bash
kubectl get certificatesigningrequests
kubectl certificate approve request-name
```

## Compliance and Standards

### CIS Kubernetes Benchmark

The Center for Internet Security (CIS) provides security benchmarks for Kubernetes:

- **Level 1**: Basic security settings
- **Level 2**: Advanced security configurations

### Implementing CIS Controls

1. **Control Plane Configuration:**
   - Disable anonymous auth
   - Enable RBAC
   - Secure etcd communication
   - Use strong TLS ciphers

2. **Worker Node Security:**
   - Secure kubelet configuration
   - Use read-only root filesystems
   - Implement pod security standards

3. **Network Security:**
   - Encrypt cluster traffic
   - Implement network segmentation
   - Use firewalls and security groups

## Hands-on Exercises

### Exercise 1: Implementing RBAC

1. Create a namespace and service account
2. Create roles with specific permissions
3. Bind roles to service accounts
4. Test access controls

### Exercise 2: Network Policies

1. Create a default deny policy
2. Allow specific traffic between pods
3. Test policy enforcement

### Exercise 3: Pod Security

1. Configure security contexts
2. Implement pod security standards
3. Test security enforcement

### Exercise 4: Secrets Management

1. Create and use secrets
2. Implement external secrets
3. Test secret rotation

This comprehensive guide provides the foundation for secure Kubernetes administration. Regular review and updates of security policies are essential for maintaining cluster security.