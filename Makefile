docker_compose = docker compose -f ./docker/server.docker-compose.yaml

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
	$(docker_compose) up -d

stop-pulsar-server:
	@echo "Stopping Pulsar server..."
	$(docker_compose) down

build: build-producer build-consumer

build-producer:
	dotnet build $(producer)

build-consumer:
	dotnet build $(consumer)

clean: clean-producer clean-consumer

clean-producer:
	dotnet clean $(producer)

clean-consumer:
	dotnet clean $(consumer)

generate-messages:
	@echo "Sleeping $(pulsar_delay) seconds for Pulsar to warm up..."
	@sleep $(pulsar_delay)
	@echo "Producing $(message_count) messages..."
	@$(test_script) $(message_count) $(host) $(topic)

consume-messages:
	@echo "Starting consumer..."
	@dotnet run --project $(consumer) $(host) $(topic) $(subscription) $(message_count)

test: clear start-pulsar-server clean build generate-messages consume-messages stop-pulsar-server