<?php

namespace App\Http\Controllers;

use App\Models\Post;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Cache;
use Illuminate\Support\Facades\Log;

class PostController extends Controller
{
    public function index(Request $request)
    {
        Log::info('Fetching all posts', [
            'pod' => gethostname(),
            'ip' => $request->ip()
        ]);

        $cacheKey = 'posts.all';
        
        $posts = Cache::remember($cacheKey, 300, function () {
            return Post::with('comments')
                ->published()
                ->recent()
                ->get();
        });

        return response()->json([
            'success' => true,
            'count' => $posts->count(),
            'pod' => gethostname(),
            'data' => $posts
        ]);
    }

    public function show($id)
    {
        Log::info("Fetching post {$id}", ['pod' => gethostname()]);

        $post = Post::with('comments')->find($id);

        if (!$post) {
            return response()->json([
                'success' => false,
                'message' => 'Post not found'
            ], 404);
        }

        return response()->json([
            'success' => true,
            'pod' => gethostname(),
            'data' => $post
        ]);
    }

    public function store(Request $request)
    {
        $validated = $request->validate([
            'title' => 'required|string|max:255',
            'content' => 'required|string',
            'author' => 'required|string|max:100',
            'published' => 'boolean',
        ]);

        Log::info('Creating new post', [
            'title' => $validated['title'],
            'pod' => gethostname()
        ]);

        $post = Post::create([
            'title' => $validated['title'],
            'content' => $validated['content'],
            'author' => $validated['author'],
            'published' => $validated['published'] ?? false,
            'published_at' => ($validated['published'] ?? false) ? now() : null,
        ]);

        // Clear cache
        Cache::forget('posts.all');

        return response()->json([
            'success' => true,
            'pod' => gethostname(),
            'message' => 'Post created successfully',
            'data' => $post
        ], 201);
    }

    public function update(Request $request, $id)
    {
        $post = Post::find($id);

        if (!$post) {
            return response()->json([
                'success' => false,
                'message' => 'Post not found'
            ], 404);
        }

        $validated = $request->validate([
            'title' => 'sometimes|string|max:255',
            'content' => 'sometimes|string',
            'author' => 'sometimes|string|max:100',
            'published' => 'sometimes|boolean',
        ]);

        Log::info("Updating post {$id}", ['pod' => gethostname()]);

        $post->update($validated);

        // Clear cache
        Cache::forget('posts.all');

        return response()->json([
            'success' => true,
            'pod' => gethostname(),
            'message' => 'Post updated successfully',
            'data' => $post
        ]);
    }

    public function destroy($id)
    {
        $post = Post::find($id);

        if (!$post) {
            return response()->json([
                'success' => false,
                'message' => 'Post not found'
            ], 404);
        }

        Log::info("Deleting post {$id}", ['pod' => gethostname()]);

        $post->delete();

        // Clear cache
        Cache::forget('posts.all');

        return response()->json([
            'success' => true,
            'pod' => gethostname(),
            'message' => 'Post deleted successfully'
        ]);
    }
}