# Seeker

This application is a simple seeker developed basically with DotNet Core, Angular Js and MongoDB in addition to some other frameworks and libraries to enrich the work, like Bootstrap(See package.json file)

## Getting Started

### Prerequisites

To run the application you need
* [.NET Core 2.0.0 SDK or later](https://www.microsoft.com/net/core) - Free and open-source framework.
* [NodeJs v6.11.0 LTS && NPM v3.10.10](https://nodejs.org/en/) - To install angular and their dependencies.
* [MongoDB v3.2.1](https://www.mongodb.com/download-center#community) - Database server

### Configuring MongoDB

Launch a terminal and run the database server

```
mongod [--config /path/to/mongodb.conf]
```
In the terminal configure user to access the database

```
mongo admin

db.createUser({user: 'seeker', pwd: 'H0m3w0rk', roles: [{role: "userAdminAnyDatabase", db: "admin"}]});
```
### Installing

First install all the dependencies of the project

```
npm install
dotnet restore
```

## Running the app

``` bash
dotnet run
```

## Authors

* **Francisco Ruiz Duanys** - *Software Developer*


