# RabbitMQ Samples

[RabbitMQ/docs](https://www.rabbitmq.com/docs/download)

## Run RabbitMQ with Docker

Experimenting with RabbitMQ on your workstation? Try the community Docker image:
```console
# latest RabbitMQ 4.0.x
docker run -it --restart unless-stopped --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4.0-management
```


[Migration/docs](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

## Code first asp.net core with Migrations

You're now ready to add your first migration! Instruct EF Core to create a migration named InitialCreate:
```console
dotnet ef migrations add InitialCreate --output-dir Infrastructure //Created folder

dotnet ef database update
```


## Building a sample

Build any .NET Core sample using the .NET Core CLI, which is installed with [the .NET Core SDK](https://www.microsoft.com/net/download). Then run
these commands from the CLI in the directory of any sample:

```console
dotnet build
dotnet run
```