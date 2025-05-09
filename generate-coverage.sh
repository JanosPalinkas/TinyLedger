#!/bin/bash

set -e

SOLUTION_DIR=$(pwd)
TEST_PROJECT="TinyLedger.Tests"
COVERAGE_FILE="$TEST_PROJECT/TestResults/**/coverage.cobertura.xml"
REPORT_DIR="coverage-report"

echo "🧹 Cleaning previous builds and test results..."
dotnet clean
rm -rf "$TEST_PROJECT/TestResults"
rm -rf "$REPORT_DIR"

echo "🔨 Building the solution..."
dotnet build

echo "🧪 Running tests with coverage..."
dotnet test $TEST_PROJECT --collect:"XPlat Code Coverage"

echo "📊 Generating coverage report..."
reportgenerator \
  "-reports:$COVERAGE_FILE" \
  "-targetdir:$REPORT_DIR" \
  "-reporttypes:Html"

echo "✅ Coverage report generated at: $REPORT_DIR/index.html"

# Automatically open the report in the default browser (macOS)
if [ -f "$REPORT_DIR/index.html" ]; then
  open "$REPORT_DIR/index.html"
else
  echo "⚠️ Could not find the HTML report to open."
fi