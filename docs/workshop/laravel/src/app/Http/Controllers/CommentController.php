<?php

namespace App\Http\Controllers;

use App\Models\Comment;
use App\Models\Post;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Log;

class CommentController extends Controller
{
    public function index($postId)
    {
        $post = Post::find($postId);

        if (!$post) {
            return response()->json([
                'success' => false,
                'message' => 'Post not found'
            ], 404);
        }

        Log::info("Fetching comments for post {$postId}", ['pod' => gethostname()]);

        $comments = $post->comments()->orderBy('created_at', 'desc')->get();

        return response()->json([
            'success' => true,
            'count' => $comments->count(),
            'pod' => gethostname(),
            'data' => $comments
        ]);
    }

    public function store(Request $request, $postId)
    {
        $post = Post::find($postId);

        if (!$post) {
            return response()->json([
                'success' => false,
                'message' => 'Post not found'
            ], 404);
        }

        $validated = $request->validate([
            'author' => 'required|string|max:100',
            'content' => 'required|string|max:500',
        ]);

        Log::info("Creating comment for post {$postId}", ['pod' => gethostname()]);

        $comment = $post->comments()->create([
            'author' => $validated['author'],
            'content' => $validated['content'],
        ]);

        return response()->json([
            'success' => true,
            'pod' => gethostname(),
            'message' => 'Comment added successfully',
            'data' => $comment
        ], 201);
    }
}