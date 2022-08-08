pulsar_docker_compose_file = ./docker/pulsar.docker-compose.yaml
preloaded_docker_compose_file = ./docker/preloaded.docker-compose.yaml

tag := $(shell date +%F_%H-%m)

message_count = 25

consumer = src/Consumer/Consumer.csproj
producer = src/Producer/Producer.csproj
test_script = ./test/bash/test.sh

pulsar_delay = 30

host=localhost:6650
topic=persistent://public/default/msg-topic
subscription=Consumer

.PHONY: test

clear:
	clear

start-pulsar-server:
	@echo "Starting Pulsar server..."
	@docker compose -f $(pulsar_docker_compose_file) up -d

stop-pulsar-server:
	@echo "Stopping Pulsar server..."
	@docker compose -f $(pulsar_docker_compose_file) down

start-preloaded-server:
	@echo "Starting preloaded Pulsar server..."
	@docker compose -f $(preloaded_docker_compose_file) up -d
	@echo "Sleeping $(pulsar_delay) seconds for Pulsar to warm up..."
	@sleep $(pulsar_delay)

stop-preloaded-server:
	@echo "Stopping preloaded Pulsar server..."
	@docker compose -f $(preloaded_docker_compose_file) down

build: build-producer build-consumer

build-producer:
	@dotnet build $(producer)

build-consumer:
	@dotnet build $(consumer)

build-container: clear start-pulsar-server clean build generate-messages stop-pulsar-server
	@docker build -f ./docker/Dockerfile -t preloaded-pulsar:latest -t preloaded-pulsar:$(tag) .

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

test: clear start-pulsar-server clean build generate-messages consume-messages stop-pulsar-server

test-preload: clear start-preloaded-server clean-consumer build-consumer consume-messages stop-preloaded-server