# Lykke.Service template #

dotnet cli template for generating a solution for the service Lykke.Service.ServiceName and/or job Lykke.Job.JobName

## How to use? ##

Clone repo to some directory

Install template:
```sh
$ dotnet new --install ${path}
```
where `${path}` is the **full** path to the clonned directory (where folder .template.config placed) **without trailing slash**

Now new template can be used in dotnet cli:
to generate new service:
```sh
dotnet new lkeservice -n ${ServiceName} -o Lykke.Service.${ServiceName} [-az {true|false} -rpub {true|false} -rsub {true|false} -t {true|false}]  
```

to generate solution with only the job projects:
```sh
dotnet new lkeservice -n ${JobName} -o Lykke.Job.${JobName} -type Job [-az {true|false} -rpub {true|false} -rsub {true|false} -t {true|false}] 
```

to generate solution with service and job projects:
```sh
dotnet new lkeservice -n ${ServiceName} -o Lykke.Service.${ServiceName} -type ServiceJob [-az {true|false} -rpub {true|false} -rsub {true|false} -t {true|false}] 
```

This will create a solution in the current folder, where `${ServiceName}` or `${JobName}` is the service/job name without Lykke.Service./Lykke.Job. prefix. Switches:

-   **-n|--name**: Service/Job name
-   **-o|--output**: Output directory name
-   **-type**: Type of the project. Available values: 
Service - will create a solution named Lykke.Service.{ServiceName} containing service, client and service contracts.
ServiceJob - will create a solution named Lykke.Service.{ServiceName} containing service, client, job, service and job contracts.
Job - will create a solution named Lykke.Job.{JobName} containing only job related projects (no client, service contracts, and service host). 
Default is **Service**
-   **-az|--azurequeuesub**: Enables incoming Azure Queue messages processing, using Lykke.JobTriggers package. Default is  **false**
-   **-rsub|--rabbitsub**: Enables incoming RabbitMQ messages processing. Default is  **false**
-   **-rpub|--rabbitpub**: Enables outcoming RabbitMQ messages sending. Default is  **false**
-   **-t|--timeperiod**: Enables periodical work execution, using TimerPeriod class from Lykke.Common package. Default is  **false**

When temlate has changed, to update installed template run again command:

```sh
$ dotnet new --install ${path}
```

To remove all installed custom temlates run command:

```sh
$ dotnet new --debug:reinit 
```

## Developing ##

### Environment variables ###

To define your own environment variables, see [Working with multiple environments](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments)

* *ASPNETCORE_ENVIRONMENT* - defines environment name, the value can be: Development, Staging, Production.
* *SettingsUrl* - defines URL of remote settings or path for local settings.
* *APP_INFO* - define your name. This will help another developers to determine who runs service locally. Consider to define this variable in machine-wide or user-wide configuration to spread out this variable across all of the projects.

Reflect your settings structure in appsettings.json - leave all of field blank, or just show value's format. Fill appsettings.XXX.json with real settings data. Ensure that appsettings.XXX.json is ignored in git.

Default launchSettings.json is:

```json
{
  "profiles": {
    "LykkeService local dev settings": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "SettingsUrl": "appsettings.Development.json"
      }
    },
    "LykkeService local test settings": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging",
        "SettingsUrl": "appsettings.Testing.json"
      }
    },
    "LykkeService remote settings": {
      "commandName": "Project",
      "environmentVariables": {
        "SettingsUrl": "http://settings.lykke-settings.svc.cluster.local/your_token_LykkeServiceJob"
      }
    }
  }
}
```
