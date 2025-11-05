<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\PostController;
use App\Http\Controllers\CommentController;
use App\Http\Controllers\HealthController;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
*/

// Health check endpoint
Route::get('/health', [HealthController::class, 'check']);

// Posts routes
Route::prefix('posts')->group(function () {
    Route::get('/', [PostController::class, 'index']);
    Route::post('/', [PostController::class, 'store']);
    Route::get('/{id}', [PostController::class, 'show']);
    Route::put('/{id}', [PostController::class, 'update']);
    Route::delete('/{id}', [PostController::class, 'destroy']);
    
    // Comments for a specific post
    Route::get('/{id}/comments', [CommentController::class, 'index']);
    Route::post('/{id}/comments', [CommentController::class, 'store']);
});

// API information
Route::get('/', function () {
    return response()->json([
        'name' => 'Blog API',
        'version' => '1.0.0',
        'endpoints' => [
            'health' => '/health',
            'posts' => '/api/posts',
            'comments' => '/api/posts/{id}/comments',
        ],
        'pod' => gethostname(),
    ]);
});