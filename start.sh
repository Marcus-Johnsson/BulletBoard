#!/bin/bash

# Start both backend and frontend concurrently

# Colors for output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

echo -e "${GREEN}Starting Backend (C#)...${NC}"
cd BulletBoard && dotnet run &
BACKEND_PID=$!

echo -e "${BLUE}Starting Frontend (Svelte)...${NC}"
cd frontend && npm run dev &
FRONTEND_PID=$!

# Trap Ctrl+C to kill both processes
trap "echo 'Stopping...'; kill $BACKEND_PID $FRONTEND_PID 2>/dev/null; exit" INT TERM

# Wait for both processes
wait
