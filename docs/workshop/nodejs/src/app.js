const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const cors = require('cors');
const path = require('path');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 3000;
const MONGO_URI = process.env.MONGO_URI || 'mongodb://localhost:27017/taskmanager';

// Middleware
app.use(cors());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.static(path.join(__dirname, 'public')));

// MongoDB connection with retry logic
const connectWithRetry = () => {
    console.log('Attempting to connect to MongoDB...');
    mongoose.connect(MONGO_URI, {
        useNewUrlParser: true,
        useUnifiedTopology: true,
        serverSelectionTimeoutMS: 5000,
    })
    .then(() => {
        console.log('✅ Successfully connected to MongoDB');
        console.log('Database:', mongoose.connection.db.databaseName);
    })
    .catch(err => {
        console.error('❌ MongoDB connection error:', err.message);
        console.log('Retrying connection in 5 seconds...');
        setTimeout(connectWithRetry, 5000);
    });
};

connectWithRetry();

// Handle MongoDB connection events
mongoose.connection.on('disconnected', () => {
    console.log('MongoDB disconnected. Attempting to reconnect...');
    setTimeout(connectWithRetry, 5000);
});

mongoose.connection.on('error', err => {
    console.error('MongoDB error:', err);
});

// Routes
app.use('/api/tasks', require('./routes/tasks'));

// Health check endpoint
app.get('/health', (req, res) => {
    const health = {
        status: 'healthy',
        timestamp: new Date().toISOString(),
        hostname: process.env.HOSTNAME || require('os').hostname(),
        uptime: process.uptime(),
        mongodb: mongoose.connection.readyState === 1 ? 'connected' : 'disconnected',
        environment: process.env.NODE_ENV || 'development'
    };
    
    if (mongoose.connection.readyState !== 1) {
        res.status(503).json({ ...health, status: 'unhealthy' });
    } else {
        res.status(200).json(health);
    }
});

// Root endpoint
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

// API info endpoint
app.get('/api', (req, res) => {
    res.json({
        name: 'Task Manager API',
        version: '1.0.0',
        endpoints: {
            health: '/health',
            tasks: '/api/tasks',
            ui: '/'
        },
        pod: process.env.HOSTNAME || 'unknown'
    });
});

// Error handling middleware
app.use((err, req, res, next) => {
    console.error('Error:', err.message);
    res.status(500).json({
        error: 'Internal Server Error',
        message: err.message,
        pod: process.env.HOSTNAME || 'unknown'
    });
});

// 404 handler
app.use((req, res) => {
    res.status(404).json({
        error: 'Not Found',
        path: req.path,
        pod: process.env.HOSTNAME || 'unknown'
    });
});

// Start server
app.listen(PORT, '0.0.0.0', () => {
    console.log('=================================');
    console.log(`🚀 Task Manager Server Started`);
    console.log(`📍 Port: ${PORT}`);
    console.log(`🌍 Environment: ${process.env.NODE_ENV || 'development'}`);
    console.log(`📦 Pod: ${process.env.HOSTNAME || 'local'}`);
    console.log(`🗄️  MongoDB: ${MONGO_URI.replace(/\/\/.*@/, '//<credentials>@')}`);
    console.log('=================================');
});

// Graceful shutdown
process.on('SIGTERM', () => {
    console.log('SIGTERM signal received: closing HTTP server');
    mongoose.connection.close(false, () => {
        console.log('MongoDB connection closed');
        process.exit(0);
    });
});

module.exports = app;