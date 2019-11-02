ESAPrizes
==========

ASP.Net Core application that fetches the list of not yet won prizes from a instance of the GDQ donation tracker and displays it in a grid.

Build
------
To build the application, dotnet core 3.0 with the Microsoft.AspNetCore.App 3.0.0 runtime is necessary.

````sh
$ dotnet publish -c Release -o out
$ dotnet out/ESAPrizes.dll
````

Alternatively to just run it without first publishing using `dotnet run`.

### Docker
To build the docker container, either
````sh
$ docker build -t esamarathon/esaprizes .
```` 
or, with docker-compose, 
````sh
$ docker-compose build
````

Routes
-------

`/`: HomeController.Index - Start page. Lists all prizes.

`/privacy`: HomeController.Privacy - Privacy policy. Nothing right now. Might remove.

`/error`: HomeController.Error - Simple error page.

`/api/prizes`: PrizesController.GetAllPrizes - API to get list of all prizes.

## Configuration ##
Application comes with the following default configuration (found in `/app/appsettings.default.json`)
````json
{
    "Logging": {
        "LogLevel": {
            "Default": "Warning"
        }
    },
    "AllowedHosts": "*",
    "TrackerUrl": "https://donations.esamarathon.com"
}
````
This showcases all possible settings and works fine for most development work.
To override any settings, simply add a appsettings.json file with the overriding settings and it will load during startup.
Additionally, using the rules provided at https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2#environment-variables-configuration-provider, 
you can also override settings using environment variables prefixed with ESAPRIZES_.
Example docker-compose.yml file:

````yaml
version: "3.7"
services:
  app:
    environment:
      - ESAPRIZE_Logging__LogLevel__Default=Debug
      - ESAPRIZE_TrackerUrl=http://localhost:3000
````

Please make note of the double underscores between keys.

Sizing Rules
------
The rules to determine the size classification for a prize is defined in `SizeSelectionService`. Might pull out configuration for the values in the future, but no complex rules engine. If you need a different set of rules, fork the repo and change the service.

- If the prize value is evenly divisiable by 50, it is a size1 (large).
- If the prize value is evenly divisiable by 25, it is a size2 (medium).
- Otherwise it is a size3 (small).

#### NOTICE
Please note that at the time of writing, the rules are not actully correct.
This is due to the values associated with the current list of prizes in production all being either missing or divisiable by 50. So by setting the limits to 100 and 50 respectively, a better spread is achieved for demoing.

