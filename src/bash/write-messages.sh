#!/bin/bash

source $(dirname "$0")/generate-payload.sh

counter=1

while [ $counter -le $1 ]
do
  echo "$(generate_payload)" | \
  $(dirname "$0")/../Producer/bin/Debug/net7.0/Producer $2 $3
    
  ((counter++))
done

echo Message generation complete!