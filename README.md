# Request system for YouTube chat
[![Build Status](https://travis-ci.org/Creeperman007/request-yt.svg?branch=master)](https://travis-ci.org/Creeperman007/request-yt) [![Build status](https://ci.appveyor.com/api/projects/status/de9mvns9hegqrlrw?svg=true)](https://ci.appveyor.com/project/Creeperman007/request-yt)


[Download latest](https://github.com/Creeperman007/requests-yt/releases/latest)
## Overview
This app can be used when you are doing radio show or any kind of music stream and you don't want to have auto-queue.<br>
## Basic logic
> To Do: explain logic

## Requirements
* SQL database with public access
* Web page (can be hosted)
* YouTube chat
* Installed this app
## Future plans
* Responding system
## Setup
### SQL database
1. Create a database with name you want
1. Create table called *requests*
1. Create these columns
    * id - INT; A_I;
    * session - INT;
    * name - TEXT; utf8_bin;
    * state - TEXT; utf8_bin;
> To Do: Add SQL command

# For Developers
## Notice
If you want to edit this app or use it for yourself, I recommend not to download the updater with the source. It can cause troubles when you are playing with the code.
## Used NuGets
* EntityFramework (by *Microsoft*)
* Google.Protobuf (by *Google Inc.*)
* MySql.Data (by *Oracle*)
* Newtonsoft.Json (by *James Newton-King*)
* System.Data.SQLite (by *SQLite Development Team*)
* System.Data.SQLite.Core (by *SQLite Development Team*)
* System.Data.SQLite.EF6 (by *SQLite Development Team*)
* System.Data.SQLite.Linq (by *SQLite Development Team*)
* System.Runtime.InteropServices.RuntimeInformation (by *Microsoft*)
