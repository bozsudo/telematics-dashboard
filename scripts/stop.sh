#!/bin/bash

echo "Stopping local development environment..."
docker-compose down -v
echo "Services stopped and volumes removed!"
