isolated_pulsar_docker_compose_file = ./docker/isolated-pulsar.docker-compose.yaml
mounted_pulsar_docker_compose_file = ./docker/mounted-pulsar.docker-compose.yaml
preloaded_docker_compose_file = ./docker/preloaded.docker-compose.yaml

image_name = preloaded-pulsar
tag := $(shell date +%F_%H-%m)

message_count = 25

consumer = src/Consumer/Consumer.csproj
producer = src/Producer/Producer.csproj
test_script = src/bash/write-messages.sh

pulsar_delay = 30

host=localhost:6650
topic=persistent://public/default/msg-topic
subscription=Consumer

.PHONY: test

clear:
	clear

start-isolated-pulsar-server:
	@echo "Starting isolated Pulsar server..."
	@docker compose -f $(isolated_pulsar_docker_compose_file) up -d

stop-isolated-pulsar-server:
	@echo "Stopping isolated Pulsar server..."
	@docker compose -f $(isolated_pulsar_docker_compose_file) down

start-mounted-pulsar-server:
	@echo "Starting mounted Pulsar server..."
	@docker compose -f $(mounted_pulsar_docker_compose_file) up -d

stop-mounted-pulsar-server:
	@echo "Stopping mounted Pulsar server..."
	@docker compose -f $(mounted_pulsar_docker_compose_file) down

start-preloaded-server:
	@echo "Starting pre-loaded Pulsar server..."
	@docker compose -f $(preloaded_docker_compose_file) up -d
	@echo "Sleeping $(pulsar_delay) seconds for Pulsar to warm up..."
	@sleep $(pulsar_delay)

stop-preloaded-server:
	@echo "Stopping pre-loaded Pulsar server..."
	@docker compose -f $(preloaded_docker_compose_file) down

build: build-producer build-consumer

build-producer:
	@dotnet build $(producer)

build-consumer:
	@dotnet build $(consumer)

build-container: clear start-mounted-pulsar-server clean build generate-messages stop-mounted-pulsar-server
	@docker build -f ./docker/Dockerfile -t $(image_name):latest -t $(image_name):$(tag) .
	@rm -Rf data/

clean: clean-producer clean-consumer

clean-producer:
	@dotnet clean $(producer)

clean-consumer:
	@dotnet clean $(consumer)

generate-messages:
	@echo "Sleeping $(pulsar_delay) seconds for Pulsar to warm up..."
	@sleep $(pulsar_delay)
	@echo "Producing $(message_count) messages..."
	@$(test_script) $(message_count) $(host) $(topic)

consume-messages:
	@echo "Starting consumer..."
	@dotnet run --project $(consumer) $(host) $(topic) $(subscription) $(message_count)

sure: clear start-isolated-pulsar-server clean build generate-messages consume-messages stop-isolated-pulsar-server

test-preload: clear start-preloaded-server clean-consumer build-consumer consume-messages stop-preloaded-server