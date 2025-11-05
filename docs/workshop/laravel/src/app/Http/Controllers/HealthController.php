<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Cache;

class HealthController extends Controller
{
    public function check(Request $request)
    {
        $health = [
            'status' => 'healthy',
            'timestamp' => now()->toIso8601String(),
            'hostname' => gethostname(),
            'pod' => env('HOSTNAME', gethostname()),
            'environment' => app()->environment(),
            'php_version' => PHP_VERSION,
            'laravel_version' => app()->version(),
        ];

        // Check database connection
        try {
            DB::connection()->getPdo();
            $health['database'] = 'connected';
        } catch (\Exception $e) {
            $health['database'] = 'disconnected';
            $health['database_error'] = $e->getMessage();
            $health['status'] = 'unhealthy';
        }

        // Check Redis connection
        try {
            Cache::store('redis')->put('health_check', 'ok', 5);
            Cache::store('redis')->get('health_check');
            $health['redis'] = 'connected';
        } catch (\Exception $e) {
            $health['redis'] = 'disconnected';
            $health['redis_error'] = $e->getMessage();
        }

        $statusCode = $health['status'] === 'healthy' ? 200 : 503;

        return response()->json($health, $statusCode);
    }
}