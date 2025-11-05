# Laravel Workshop Setup Guide

## Complete File Structure

The Laravel workshop requires the following files to be fully functional. The core files have been created in this repository. For a complete working application, you would need:

### ✅ Created Files

**Source Code:**
- `src/composer.json` - Dependencies configuration
- `src/app/Models/Post.php` - Post model
- `src/app/Models/Comment.php` - Comment model
- `src/app/Http/Controllers/PostController.php` - Post API controller
- `src/app/Http/Controllers/CommentController.php` - Comment API controller
- `src/app/Http/Controllers/HealthController.php` - Health check controller
- `src/routes/api.php` - API routes
- `src/database/migrations/2024_01_01_000001_create_posts_table.php` - Posts table migration
- `src/database/migrations/2024_01_01_000002_create_comments_table.php` - Comments table migration
- `src/database/seeders/DatabaseSeeder.php` - Sample data seeder

**Kubernetes:**
- `k8s/redis-deployment.yaml` - Redis cache deployment

**Documentation:**
- `README.md` - Complete workshop guide with step-by-step instructions

### 📝 Additional Files Needed

To make this a complete working Laravel application, you would need to add:

**Laravel Core Files** (can be generated with `laravel new` or `composer create-project`):
- `artisan` - Laravel command-line tool
- `bootstrap/` - Application bootstrap files
- `config/` - Configuration files
- `public/index.php` - Application entry point
- `storage/` - Logs and cache directories
- `.env.example` - Environment template

**Docker Configuration:**
- `src/Dockerfile` - Multi-stage Docker build (see README for complete example)
- `src/docker/nginx.conf` - Nginx configuration
- `src/docker/supervisord.conf` - Supervisor configuration
- `src/docker/php.ini` - PHP settings

**Kubernetes Manifests:**
- `k8s/configmap.yaml` - Application configuration
- `k8s/secret.yaml` - Secrets (DB credentials, APP_KEY)
- `k8s/mysql-statefulset.yaml` - MySQL database
- `k8s/deployment.yaml` - Laravel application deployment
- `k8s/service.yaml` - Service exposure
- `k8s/storage-pvc.yaml` - Persistent storage claim

## Quick Setup Instructions

### Option 1: Start from Scratch (Recommended for Learning)

```bash
# 1. Create new Laravel project
composer create-project laravel/laravel blog-api
cd blog-api

# 2. Copy workshop files
cp -r ../src/app/Models/* app/Models/
cp -r ../src/app/Http/Controllers/* app/Http/Controllers/
cp ../src/routes/api.php routes/
cp -r ../src/database/migrations/* database/migrations/
cp ../src/database/seeders/DatabaseSeeder.php database/seeders/

# 3. Update composer.json with Redis dependency
composer require predis/predis

# 4. Create Docker and Kubernetes files
# Follow the README.md instructions for each file

# 5. Build and deploy
docker build -t blog-api:v1.0 .
kind load docker-image blog-api:v1.0 --name workshop
kubectl apply -f k8s/
```

### Option 2: Use Complete Template

For a complete ready-to-use template, the workshop instructor can provide a full Laravel application with all necessary files, or you can:

1. Clone a Laravel starter template
2. Replace the API code with the workshop files
3. Follow the deployment steps in README.md

## Environment Configuration

The application requires these environment variables (see `k8s/configmap.yaml` and `k8s/secret.yaml`):

**Required:**
- `APP_KEY` - Laravel application key
- `DB_HOST`, `DB_DATABASE`, `DB_USERNAME`, `DB_PASSWORD` - Database connection
- `REDIS_HOST` - Redis connection

**Optional:**
- `CACHE_DRIVER=redis`
- `SESSION_DRIVER=redis`
- `QUEUE_CONNECTION=redis`

## Deployment Checklist

- [ ] Laravel dependencies installed (`composer install`)
- [ ] Environment files configured
- [ ] Docker image built and loaded to KIND
- [ ] MySQL StatefulSet deployed and running
- [ ] Redis deployed and running
- [ ] ConfigMap and Secret created
- [ ] Application deployment successful
- [ ] Service accessible via NodePort or port-forward
- [ ] Health check endpoint responding
- [ ] Database migrations completed
- [ ] Sample data seeded

## Troubleshooting

### Cannot connect to database
```bash
# Check MySQL pod
kubectl logs mysql-0

# Verify service
kubectl get svc mysql-service

# Test connection from app pod
kubectl exec -it <app-pod> -- php artisan tinker
>>> DB::connection()->getPdo();
```

### Migrations not running
```bash
# Check init container logs
kubectl logs <pod-name> -c run-migrations

# Manually run migrations
kubectl exec -it <pod-name> -- php artisan migrate --force
```

### Redis connection issues
```bash
# Check Redis pod
kubectl get pods -l app=redis

# Test Redis
kubectl exec -it <redis-pod> -- redis-cli ping
```

## Additional Resources

- [Laravel Documentation](https://laravel.com/docs)
- [Laravel Kubernetes Best Practices](https://laravel.com/docs/deployment#optimization)
- [PHP-FPM Configuration](https://www.php.net/manual/en/install.fpm.php)

## Workshop Notes

This workshop demonstrates:
- ✅ Laravel API development
- ✅ Multi-stage Docker builds with Nginx + PHP-FPM
- ✅ Database migrations in Kubernetes
- ✅ Redis caching integration
- ✅ Init containers for setup tasks
- ✅ StatefulSets for databases
- ✅ ConfigMaps and Secrets management
- ✅ Health checks and readiness probes

Estimated completion time: 30-40 minutes