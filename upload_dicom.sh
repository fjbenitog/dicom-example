#!/bin/bash
# Upload all DICOM files in dicom-files/ to Orthanc using curl
ORTHANC_URL="http://localhost:8042/instances"
ORTHANC_USER="admin"
ORTHANC_PASS="password123"
DICOM_DIR="dicom-files"

for file in "$DICOM_DIR"/*.dcm; do
  echo "Uploading $file..."
  curl -u "$ORTHANC_USER:$ORTHANC_PASS" -X POST "$ORTHANC_URL" \
    -H "Content-Type: application/dicom" \
    --data-binary "@$file"
done
