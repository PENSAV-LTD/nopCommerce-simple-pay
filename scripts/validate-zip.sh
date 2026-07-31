#!/bin/bash
set -e

PLUGIN_NAME="Nop.Plugin.Payments.SimplePay"
ROOT="validate"
ZIP_FILE="Nop.Plugin.Payments.SimplePay.zip"

echo "Validating plugin ZIP structure..."

# Extract ZIP
mkdir -p "$ROOT"
unzip -q "$ZIP_FILE" -d "$ROOT"

PLUGIN_PATH="$ROOT/Presentation/Nop.Web/Plugins/$PLUGIN_NAME"

# Check folder exists
if [ ! -d "$PLUGIN_PATH" ]; then
  echo "❌ ERROR: Plugin folder not found at:"
  echo "   Presentation/Nop.Web/Plugins/$PLUGIN_NAME"
  exit 1
fi

# Required files
REQUIRED_FILES=(
  "plugin.json"
  "$PLUGIN_NAME.dll"
)

for file in "${REQUIRED_FILES[@]}"; do
  if [ ! -f "$PLUGIN_PATH/$file" ]; then
    echo "❌ ERROR: Missing required file: $file"
    exit 1
  fi
done

# Required directories
REQUIRED_DIRS=(
  "Views"
)

for dir in "${REQUIRED_DIRS[@]}"; do
  if [ ! -d "$PLUGIN_PATH/$dir" ]; then
    echo "❌ ERROR: Missing required directory: $dir"
    exit 1
  fi
done

echo "✅ Plugin ZIP structure is valid."
