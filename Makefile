start-pulsar-server:
	docker compose -f ./docker/server.docker-compose.yaml up -d

stop-pulsar-server:
	docker compose -f ./docker/server.docker-compose.yaml down