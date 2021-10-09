## Database (optional)
- Run your own MSSQL instance 
- Run docker image: 
```
  docker-compose -f docker/compose.yaml up -d 
```
## Build and run
###Build and run in your system:
- `path_to_dotnet build`
- `path_to_dotnet run`
### Build and run with docker
```
docker build -f Dockerfile -t parser
docker run -t parser
```
or combine into single command
```
docker run -t $(docker build -q .)
```