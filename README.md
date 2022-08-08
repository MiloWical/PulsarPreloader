# Pulsar Preloader

## tl;dr

If you want to get started, follow these 3 easy steps from the repo root:

1. Run `make sure` to make sure (_get it?!?_) that your configuration is good for the entire messaging process.
2. Run `make build-container` to build a pre-loaded Pulsar container image.
3. Run `make test-preload` as often as you like to watch the immutability in action!

## Overview

The Pulsar Preloader is a scaffolded project that allows a user to:

1. Generate test data
2. Push that data into a Pulsar server
3. Save off the data into a new container image

This is useful for testing because the messages are immutable - if you need a fresh copy of the messages, just restart the container!

## Components

There are a number of components used to achieve this behavior:

- **[Docker Files](./docker/docker.md)**: These files are used to run and build Pulsar containers.
- **[Producer](./src/Producer/Producer.md)**: A program that can write a generated message to a Pulsar topic.
- **[Consumer](./src/Consumer/Consumer.md)**: A program that can be used for testing reading from a Pulsar topic.
- **[Bash Scripts](./src/bash/bash-scripts.md)**: Bash scripts that help generate test payloads and write messages to Pulsar.

## Makefile

The execution of the process is driven by a `Makefile` that is expected to be run from a Bash terminal. The following targets are defined:

- `clear`: Clears the terminal.
- `start-isolated-pulsar-server`: Starts the default Pulsar server without volumes mounted.
- `stop-isolated-pulsar-server`: Stops the default Pulsar server without volumes mounted.
- `start-mounted-pulsar-server`: Starts the default Pulsar server with volumes mounted.
- `stop-mounted-pulsar-server`: Stops the default Pulsar server with volumes mounted.
- `start-preloaded-server`: Starts the pre-loaded Pulsar server.
- `stop-preloaded-server`: Stops the pre-loaded Pulsar server.
- `build-producer`: Builds the Producer project. 
- `build-consumer`: Builds the Consumer project.
- `build`: Builds all code-related projects.
  - Dependencies: `build-producer build-consumer`
- `build-container`: Builds the pre-loaded message container.
  - Dependencies: `clear start-mounted-pulsar-server clean build generate-messages stop-mounted-pulsar-server`
- `clean-producer`: Cleans the Producer project.
- `clean-consumer`: Cleans the Consumer project.
- `clean`: Cleans all code-related projects.
  - Dependencies: `clean-producer clean-consumer`
- `generate-messages`: Runs [`write-messages.sh`](./src/bash/write-messages.sh) to write messages into the Pulsar server.
- `consume-messages`: Runs the Consumer to pull messages from the Pulsar server.
- `sure`: Generates a specific number of messages, writes into a default Pulsar server, and pulls the same number back off to verify all connectivity is working as expected.
  - Dependencies: `clear start-isolated-pulsar-server clean build generate-messages consume-messages stop-isolated-pulsar-server`
- `test-preload`: Reads a specific number of messages from a pre-loaded Pulsar container to validate the immutability of the image.
  - Dependencies: `clear start-preloaded-server clean-consumer build-consumer consume-messages stop-preloaded-server`

## Defaults

There are a number of configurations that are set in the Makefile that are used to drive execution:

### Pre-loaded Container Image

- Image Name: `preloaded-pulsar`
- Image Tags: `latest`, the output of `date +%F_%H-%m` (e.g. `2022-08-08_11-08`)

### Messaging

- Message Count: 25

### Pulsar Configurations

- Warmup Delay: 30 seconds
- Host: `localhost:6650`
- Topic: `persistent://public/default/msg-topic`
- Consumer Subscription Name: `Consumer`

## Prerequisites

The following software needs to be installed in order for this project to run correctly:

- Bash
- make
- .NET 7.0+
- Docker