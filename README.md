# Hot Chocolate - Pure Code First - Entity Framework

An example of a Hot Chocolate GraphQL service using the Pure Code First model and Entity Framework Core.

It's meant to serve as an example for someone who is new to GraphQL, Hot Chocolate, .NET Core, and Entity Framework Core - hence all the comments on things that might be ovious to more seasoned devs.

Feedbeck is welcomed and appreciated.

## Prerequisites

1.  Install either [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio Community 2019](https://visualstudio.microsoft.com/downloads/) if you don't have them already.  You can use other IDEs, but these are recommended.
2. If you installed Visual Studio Code, then install [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) if you don't already have it installed.
    - You do not have to install .NET Core SDK 3.1 if you installed Visual Studio Commmunity 2019 because it is installed alongside it.
    - Not sure what version of .NET SDK you have installed?  [Follow these instructions](https://docs.microsoft.com/en-us/dotnet/core/install/how-to-detect-installed-versions?pivots=os-windows) to find out (be sure to select the instructions using the OS of your dev env).

## How to recreate this solution structure from scratch using .NET Core CLI

I included this because I think it's helpful for new devs who might not understand how to read a typical .NET solution's various config files.

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
3. `cd` to the new directory and run `mkdir src` to create another directory inside it called `src` to hold your projects.
4. Run `dotnet new sln` to add a solution (.sln) file.
5. `cd` to `src` and run `mkdir api`.  This will house the project for the API.
6. `cd` to `api` and run `dotnet new empty` to create an empty .NET Core project.  This will be the project for the API.
    - Run `dotnet add package HotChocolate` to add the base HotChocolate GraphQL server dependencies.  This app uses `--version 10.3.5`.
    - Run `dotnet add package Microsoft.EntityFrameworkCore` to add Entity Framework Core dependencies. This app uses `--version 3.1.1`.
    - Run `dotnet add package HotChocolate.AspNetCore.Authorization` to add Hot Chocolate's authorization dependencies. This app uses `--version 11.0.0-preview.95`.
7. `cd ../` to go up a directory and `mkdir api-tests`.
8. `cd api-tests` and run `dotnet new xunit` to create a new xUnit project to house the unit tests for the API.
9. `cd ../../` and then run the following two commands to add the api and api-tests projects to the solution:
    - dotnet sln hc-pcf.sln add src/api/api.csproj
    - dotnet sln hc-pcf.sln add src/api-tests/api-tests.csproj
10. Run `dotnet build`.  If everything was setup properly, your solution should build both projects successfully.