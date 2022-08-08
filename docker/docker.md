# Docker Files

There are 4 Docker files that are used to drive the execution of this solution:

- [`isolated-pulsar.docker-compose.yaml`](./isolated-pulsar.docker-compose.yaml)
- [`mounted-pulsar.docker-compose.yaml`](./mounted-pulsar.docker-compose.yaml)
- [`preloaded.docker-compose.yaml`](./preloaded.docker-compose.yaml)
- [`Dockerfile`](./Dockerfile)

## `isolated-pulsar.docker-compose.yaml`

This file runs the `apachepulsar/pulsar` Docker image, exposing the following port mappings:

- Host `6650` -> Container `6650`
- Host `8080` -> Container `8080`

The command `bin/pulsar standalone` is invoked when started up so that the server is run in standalone mode, rather than clustered mode.

This file is generally only used for testing plumbing, so every time the container is restarted, all messages are lost.

## `mounted-pulsar.docker-compose.yaml`

This file runs the `apachepulsar/pulsar` Docker image, exposing the following port mappings:

- Host `6650` -> Container `6650`
- Host `8080` -> Container `8080`

The command `bin/pulsar standalone` is invoked when started up so that the server is run in standalone mode, rather than clustered mode.

It also binds the volume located at `data/pulsar/data` into the `/pulsar/data` location in the image so that the persisted topic messages can be written to disk and applied to the new pre-loaded container image.

## `preloaded.docker-compose.yaml`

This file runs the built `preloaded-pulsar:latest` Docker image, exposing the following port mappings:

- Host `6650` -> Container `6650`
- Host `8080` -> Container `8080`

The command `bin/pulsar standalone` is invoked when started up so that the server is run in standalone mode, rather than clustered mode.

This image doesn't need any volume mountings because the data necessary for message processing was bound to the image during the image build process. Any messages read from the container will be rehydrated when the container is restarted; analogously, any messages written to the container will be lost on restart.

## `Dockerfile`

This is the file that generates the pre-loaded Pulsar image. The only additional step it performs is to copy the `data/pulsar/data` directory from the host into the `/pulsar/data` directory in the container, forcing the message consumption immutability.