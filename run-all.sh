#!/bin/bash

set -e

API_PATH="./TinyLedger.Api"
FRONTEND_PATH="./tinyledger-ui"
API_PORT=5078

# Run API in a new Terminal tab
osascript <<EOF
tell application "Terminal"
    activate
    do script "cd \"$(pwd)/$API_PATH\" && dotnet run"
end tell
EOF

# Wait until the API is actually reachable
echo "⏳ Waiting for API to be reachable..."
until curl -s "http://localhost:$API_PORT" > /dev/null; do
  sleep 1
done
echo "✅ API is up!"

# Run frontend in another Terminal tab
osascript <<EOF
tell application "Terminal"
    activate
    do script "cd \"$(pwd)/$FRONTEND_PATH\" && npm install && npm start"
end tell
EOF

# Open React app
sleep 2
open http://localhost:3000