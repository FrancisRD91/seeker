# Seeker

This application is a simple seeker developed basically with DotNet Core, Angular Js and MongoDB in addition to some other frameworks and libraries to enrich the work, like Bootstrap(See package.json file)

## Getting Started

### Prerequisites

To run the application you need
* [.NET Core 2.0.0 SDK or later](https://www.microsoft.com/net/core) - Free and open-source framework.
* [NodeJs v6.11.0 LTS && NPM v3.10.10 or later](https://nodejs.org/en/) - To install angular and their dependencies.
* [MongoDB v3.2 or later](https://www.mongodb.com/download-center#community) - Database server

_See the next section for more details_

### Configuration of the execution environment
#### Create a folder inside the workspace (example: on the Desktop)

Run a console and navigate to the workspace folder

```bash
#cd /path/to/workspace
cd C:\Users\developer\Desktop
```
Create a folder with the name of your preference

```bash
#mkdir <foldername>
mkdir project
```

#### Create folder structure to store the database and database server logs

Navigate to project folder

```bash
#cd <folderproject>
cd project
```
Create folder structure

```bash
mkdir data\db
mkdir data\log
```
#### Create configuration file for the database server

```bash
notepad mongodb.conf
```
Put the following content in the file
    
```
# mongodb.conf
 
# Where to store the data.
dbpath=C:\Users\developer\Desktop\project\data\db
 
#where to log
logpath=C:\Users\developer\Desktop\project\data\log\mongo.log
 
#append log
logappend=true
 
#ip address
bind_ip = 127.0.0.1
port = 27017
 
# Enable journaling, http://www.mongodb.org/display/DOCS/Journaling
journal=true
 
# Don't show mongodb http interface
nohttpinterface=true
 
# Enable mongodb rest interface
rest=false
  
#quiet mode
quiet=true
```
Note: _Change the values of the variables "dbpath" and "logpath" by the corresponding values associated with the previously created folder structure._

#### Download and install MongoDB

* Download [here](https://www.mongodb.com/dr/fastdl.mongodb.org/win32/mongodb-win32-x86_64-2008plus-ssl-3.4.10-signed.msi/download)

Run the installer and follow the instructions of it

#### Install the MongoDB service with custom configuration

Open a console with administration permissions
* Right click on the start menu 
* Select System Symbol (Administrator)

Navigate to the MongoDB installation folder
```bash
cd "C:\Program Files\MongoDB\Server\3.4\bin"
```
Run the following command

```bash
#mongod --config /path/to/mongodb.conf --install
mongod --config C:\Users\developer\Desktop\project\mongodb.conf --install
```
Start the service
```
net start MongoDB
```
#### Configure the user to access the database server

``` bash
mongo admin
```
```m
db.createUser({user: 'seeker', pwd: 'H0m3w0rk', roles: [{role: "userAdminAnyDatabase", db: "admin"}]});
```
```m
exit
```
#### Download and install .Net Core SDK

* Download [here](https://download.microsoft.com/download/7/3/A/73A3E4DC-F019-47D1-9951-0453676E059B/dotnet-sdk-2.0.2-win-gs-x64.exe)

Run the installer and follow the instructions of it

#### Download and install NodeJS

* Download [here](https://nodejs.org/dist/v8.9.1/node-v8.9.1-x64.msi)

Run the installer and follow the instructions of it

**Important:** _Restart the computer_

#### Download the repository project

Navigate to project folder

```bash
#cd /path/to/projectFolder
cd C:\Users\developer\Desktop\project
```
 Clone the repository

```bash
git clone https://github.com/FrancisRD91/seeker.git
```
#### Install project dependencies

Navigate to seeker folder

```bash
cd seeker
```
*  Install dependencies from npm

```bash
npm install
```
*  Install dependencies from nuget

```bash
dotnet restore
```
#### Run the project

```bash
dotnet run
```

Open your web browser an navigate to url
```
http://localhost:5000
```
## Authors

* **Francisco Ruiz Duanys** - *Software Developer*


