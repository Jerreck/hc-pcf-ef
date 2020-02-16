# Hot Chocolate - Pure Code First - Entity Framework

An example of a Hot Chocolate GraphQL service using the Pure Code First model and Entity Framework Core.

This project is meant to serve as an example for someone who is new to GraphQL, Hot Chocolate, .NET Core, and Entity Framework Core - hence all the comments on things that might be ovious to more seasoned devs.

Feedbeck is welcomed and appreciated.

## Prerequisites

1.  Install either [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio Community 2019](https://visualstudio.microsoft.com/downloads/) if you don't have them already.  You can use other IDEs, but these are recommended.
2. If you installed Visual Studio Code, then install [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) if you don't already have it installed.
    - You do not have to install .NET Core SDK 3.1 if you installed Visual Studio Commmunity 2019 because the SDK is installed alongside the IDE.
    - Not sure what version of .NET SDK you have installed?  [Follow these instructions](https://docs.microsoft.com/en-us/dotnet/core/install/how-to-detect-installed-versions?pivots=os-windows) to find out (be sure to select the instructions using the OS of your dev env).

## How to recreate this solution structure from scratch using .NET Core CLI

**You don't have to do any of this if you're cloning this repository - this is just for educational purposes.**  

I included the next couple sections because I think it's helpful for new devs who might not understand how to read a typical .NET solution's various config files.

.NET Core CLI reference: 

https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x 

### Commands at a glance

Order of commands for creating this project template (I've tested this on Windows 10, macOS Cataline 10.15.3, and Ubuntu 18.04 LTS).

```
mkdir hc-ef
cd hc-ef
dotnet new sln
mkdir src
cd src
mkdir api
mkdir api-tests
cd api
dotnet new empty
dotnet add package HotChocolate
dotnet add package HotChocoalte.AspNetCore.Authorization
dotnet add package Microsoft.EntityFrameworkCore
cd ../api-tests
dotnet new xunit
cd ../../
dotnet sln hc-pcf.sln add src/api/api.csproj
dotnet sln hc-pcf.sln add src/api-tests/api-tests.csproj
```

### Commands in detail

One way run these commands is be opening Visual Studio Code and pressing `` CTRL+` `` (Windows/Linux) or ``CMD+` `` (macOS) to bring up the VS Code terminal.

Note that these commands are just a little bit out of order from the commands at a glance for narration's sake.

1. Run `mkdir hc-ef` to create a new directory to hold your solution.  You can change `hc-ef` to whatever you want the directory name to be.
2. `cd hc-ef` and run `mkdir src` to create another directory inside it called `src` to hold your projects.
3. Run `dotnet new sln` to add a solution (.sln) file.
4. `cd src` and run `mkdir api`.  This will house the project for the API.
5. `cd api` and run `dotnet new empty` to create an empty .NET Core project.  This will be the project for the API.
    - Run `dotnet add package HotChocolate` to add the base HotChocolate GraphQL server dependencies.  This app uses `--version 10.3.5`.
    - Run `dotnet add package Microsoft.EntityFrameworkCore` to add Entity Framework Core dependencies. This app uses `--version 3.1.1`.
    - Run `dotnet add package HotChocolate.AspNetCore.Authorization` to add Hot Chocolate's authorization dependencies. This app uses `--version 11.0.0-preview.95`.
6. `cd ../` to go up a directory and `mkdir api-tests`.  This will house the unit testing project for the API.
7. `cd api-tests` and run `dotnet new xunit` to create a new xUnit project to house the unit tests for the API.
8. `cd ../../` and then run the following two commands to add the api and api-tests projects to the solution:
    - dotnet sln hc-pcf.sln add src/api/api.csproj
    - dotnet sln hc-pcf.sln add src/api-tests/api-tests.csproj
9. Run `dotnet build`.  If everything was setup properly, your solution should build both projects successfully.