## Installation

### Bootstrap a postgres instance

`docker run --name postgres -e POSTGRES_PASSWORD=pwd -p 5432:5432 -v C:/PersistentDockerVolumes/postgres:/var/lib/postgresql/data -d postgres`

### Apply Db migrations
`Update-Database -Project src\Infrastructure -StartupProject src\Presentation`
