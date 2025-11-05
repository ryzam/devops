<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use App\Models\Post;
use App\Models\Comment;

class DatabaseSeeder extends Seeder
{
    public function run(): void
    {
        // Create sample posts
        $post1 = Post::create([
            'title' => 'Getting Started with Kubernetes',
            'content' => 'Kubernetes is a powerful container orchestration platform that automates deployment, scaling, and management of containerized applications. In this guide, we will explore the fundamentals of Kubernetes and how it can transform your DevOps practices.',
            'author' => 'DevOps Engineer',
            'published' => true,
            'published_at' => now(),
        ]);

        $post2 = Post::create([
            'title' => 'Laravel Best Practices',
            'content' => 'Laravel provides an elegant syntax and powerful features for modern PHP development. Learn about routing, Eloquent ORM, middleware, and more in this comprehensive guide.',
            'author' => 'PHP Developer',
            'published' => true,
            'published_at' => now()->subDays(1),
        ]);

        $post3 = Post::create([
            'title' => 'Microservices Architecture',
            'content' => 'Microservices architecture is a design approach where applications are built as a collection of small, independent services. Each service runs in its own process and communicates through well-defined APIs.',
            'author' => 'Solution Architect',
            'published' => true,
            'published_at' => now()->subDays(2),
        ]);

        // Create comments
        Comment::create([
            'post_id' => $post1->id,
            'author' => 'John Doe',
            'content' => 'Great article! Very informative and well-written.',
        ]);

        Comment::create([
            'post_id' => $post1->id,
            'author' => 'Jane Smith',
            'content' => 'Thanks for sharing this. Helped me understand Kubernetes better.',
        ]);

        Comment::create([
            'post_id' => $post2->id,
            'author' => 'Bob Wilson',
            'content' => 'Laravel is amazing! These tips are very useful.',
        ]);
    }
}