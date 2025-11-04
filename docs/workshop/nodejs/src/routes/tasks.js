const express = require('express');
const router = express.Router();
const Task = require('../models/Task');

// GET /api/tasks - Get all tasks
router.get('/', async (req, res) => {
    try {
        const { category, priority, completed } = req.query;
        const filter = {};
        
        if (category) filter.category = category;
        if (priority) filter.priority = priority;
        if (completed !== undefined) filter.completed = completed === 'true';
        
        const tasks = await Task.find(filter).sort({ createdAt: -1 });
        
        res.json({
            success: true,
            count: tasks.length,
            pod: process.env.HOSTNAME || 'unknown',
            data: tasks
        });
    } catch (error) {
        res.status(500).json({
            success: false,
            error: error.message,
            pod: process.env.HOSTNAME || 'unknown'
        });
    }
});

// GET /api/tasks/:id - Get single task
router.get('/:id', async (req, res) => {
    try {
        const task = await Task.findById(req.params.id);
        
        if (!task) {
            return res.status(404).json({
                success: false,
                error: 'Task not found'
            });
        }
        
        res.json({
            success: true,
            pod: process.env.HOSTNAME || 'unknown',
            data: task
        });
    } catch (error) {
        res.status(500).json({
            success: false,
            error: error.message,
            pod: process.env.HOSTNAME || 'unknown'
        });
    }
});

// POST /api/tasks - Create new task
router.post('/', async (req, res) => {
    try {
        const task = await Task.create(req.body);
        
        res.status(201).json({
            success: true,
            pod: process.env.HOSTNAME || 'unknown',
            data: task
        });
    } catch (error) {
        res.status(400).json({
            success: false,
            error: error.message,
            pod: process.env.HOSTNAME || 'unknown'
        });
    }
});

// PUT /api/tasks/:id - Update task
router.put('/:id', async (req, res) => {
    try {
        const task = await Task.findByIdAndUpdate(
            req.params.id,
            req.body,
            { new: true, runValidators: true }
        );
        
        if (!task) {
            return res.status(404).json({
                success: false,
                error: 'Task not found'
            });
        }
        
        res.json({
            success: true,
            pod: process.env.HOSTNAME || 'unknown',
            data: task
        });
    } catch (error) {
        res.status(400).json({
            success: false,
            error: error.message,
            pod: process.env.HOSTNAME || 'unknown'
        });
    }
});

// DELETE /api/tasks/:id - Delete task
router.delete('/:id', async (req, res) => {
    try {
        const task = await Task.findByIdAndDelete(req.params.id);
        
        if (!task) {
            return res.status(404).json({
                success: false,
                error: 'Task not found'
            });
        }
        
        res.json({
            success: true,
            pod: process.env.HOSTNAME || 'unknown',
            message: 'Task deleted successfully'
        });
    } catch (error) {
        res.status(500).json({
            success: false,
            error: error.message,
            pod: process.env.HOSTNAME || 'unknown'
        });
    }
});

// GET /api/tasks/stats/summary - Get statistics
router.get('/stats/summary', async (req, res) => {
    try {
        const total = await Task.countDocuments();
        const completed = await Task.countDocuments({ completed: true });
        const pending = total - completed;
        
        const byCategory = await Task.aggregate([
            { $group: { _id: '$category', count: { $sum: 1 } } }
        ]);
        
        const byPriority = await Task.aggregate([
            { $group: { _id: '$priority', count: { $sum: 1 } } }
        ]);
        
        res.json({
            success: true,
            pod: process.env.HOSTNAME || 'unknown',
            data: {
                total,
                completed,
                pending,
                byCategory,
                byPriority
            }
        });
    } catch (error) {
        res.status(500).json({
            success: false,
            error: error.message
        });
    }
});

module.exports = router;