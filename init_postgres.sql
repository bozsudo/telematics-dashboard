CREATE TABLE uptime_data (
    id SERIAL PRIMARY KEY,
    machine_id VARCHAR(255) NOT NULL,
    uptime_hours INT NOT NULL,
    recorded_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
