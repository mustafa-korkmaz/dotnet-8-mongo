# dotnet-mongo
My boilerplate .NET8 &amp; mongo solution including generic repository pattern using domain driven design approach

### MongoDB replica set setup
1. Change your directory  
`cd MongoDBSetup`  
2. Bootstrap replica set with docker-compose  
`docker-compose up`  
3. Configure and initiate the replica set  
`docker exec -it mongo1 bin/sh /scripts/setup.sh`
