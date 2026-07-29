#!/bin/bash
RESPONSE=$(curl -s -X POST http://localhost:5127/connect/token -d "grant_type=password&username=emir&password=1234&client_id=test-client&client_secret=test-secret&scope=offline_access")
echo "$RESPONSE"
REFRESH_TOKEN=$(echo "$RESPONSE" | python3 -c "import sys, json; print(json.load(sys.stdin)['refresh_token'])")
echo "Refresh token uzunlugu: ${#REFRESH_TOKEN}"
curl -s -X POST http://localhost:5127/connect/token -d "grant_type=refresh_token&refresh_token=${REFRESH_TOKEN}&client_id=test-client&client_secret=test-secret"

