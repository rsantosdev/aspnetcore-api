#!/bin/sh

# exit on sub-module failure
set -e

# --------

echo "Deploy Started..."

export AZURE_PUBLISH_URL=${AZURE_PUBLISH_URL}
export AZURE_PUBLISH_USERNAME=${AZURE_PUBLISH_USERNAME}
export AZURE_PUBLISH_PASSWORD=${AZURE_PUBLISH_PASSWORD}

# Builds the app
echo "  Publishing"
dotnet publish
echo "  Done Publishing"

# Zip the content
echo "  Zipping"
pushd ./bin/Debug/netcoreapp1.0/publish
zip -r ../publish.zip ./*
popd
echo "  Done Zipping"

# upload and expand the zip file, the If-Match header is to force file update
echo "  Uploading zip file"
curl -H "If-Match: *" --user $AZURE_PUBLISH_USERNAME:$AZURE_PUBLISH_PASSWORD $AZURE_PUBLISH_URL/zip/site/wwwroot --upload-file ./bin/Debug/netcoreapp1.0/publish.zip
echo "  Done Uploading zip file"

# In case you need to execute commands on the web server, npm install for node.js apps for example
# uncomment the following section
# echo "  Sending npm install command"
# curl -H "Content-Type: application/json" --user $AZURE_PUBLISH_USERNAME:$AZURE_PUBLISH_PASSWORD -X POST -d '{"command":"npm install","dir":"site\\wwwroot"}' $AZURE_PUBLISH_URL/command
# echo "  Done Sending npm install command"

echo "Deploy Finished!"

# --------
