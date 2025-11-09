<?php

use Illuminate\Support\Facades\Route;

Route::get('/info', function () {
    return response()->json([
        'machine_name' => gethostname(),
        'instance_name' => gethostname(),
        'web_server' => 'nginx',
        'timestamp' => now()->toIso8601String()
    ]);
});

Route::get('/health', function () {
    return response()->json([
        'status' => 'ok',
        'machine_name' => gethostname(),
        'web_server' => 'nginx'
    ]);
});