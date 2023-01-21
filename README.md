
![Logo](./img/logo.png?raw=true)

# Projeto com .net 7 e Kafka

Proposta de treinamento para Avanade Brasil

## Configuração (preferencialmente utilizar via user secret)

```json
{
  "Events": {
    "BootstrapServers": "localhost:9092"
  },
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "ecommerce"
  }
}
```

## Configuração do kafka para o consumer

```json
"Events": {
    "BootstrapServers": "localhost:9092",
    "GroupId": "sendEmail01",
    "Topic": "send-email"
  },
```

## Comandos Docker

#### Os comandos devem ser executador a partir da pasta root do projeto

> docker-compose -f ./docker/ambiente-local.yml up --force-recreate -d --build
>
> docker-compose -f ./docker/ambiente-local.yml down --remove-orphans
>
> docker system prune

## Tooling e acessos

1. KafDrop
```
http://localhost:19000/
```

2. Mongo-Express
```
http://localhost:8081/
```
