# CarRego Demonstration

Implementation of [Chris Klug's Stop Using Entity Framework as DTO Provider](https://github.com/ChrisKlug/efcore-dto-demo) as presented in this [NDC Talk](https://www.youtube.com/watch?v=ZYfdjszs8sU)


Further details in his [blog](https://www.fearofoblivion.com/dont-let-ef-call-the-shots)

## Running the code

The code is built around using tests to verify that everything works. This means, that to "run" the code, you just run the tests.

However, the tests use a database. So, you will need to set up a SQL Server instance and make sure that the connection string in the appSettings.json files in the different test projects are correct.

## Setup Local Docker Instance

```
docker volume create sqlserverdata
docker-compose up -d; docker-compose logs -f
```

## Create Database 

```
sqlcmd -S localhost -U sa -P MyP@ssword -d master -i ./scripts/create_db.sql
cd ./src/CarRego.Domain
dotnet ef migrations script --idempotent --context MigrationContext --output ../../scripts/migrations.sql
cd ../..
sqlcmd -S localhost -U sa -P MyP@ssword -d CareRego.Domain.Tests -i .\scripts\migrations.sql
```

## Run the Tests

`dotnet test`

## Cleanup

```
docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)
docker volume rm sqlserverdata
```


