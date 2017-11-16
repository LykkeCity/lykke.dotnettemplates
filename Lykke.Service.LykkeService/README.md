# Lykke.Service template #

dotnet cli template for generating a solution for the service Lykke.Service.ServiceName

## How to use? ##

Clone repo to some directory

Install template:
```sh
$ dotnet new --install ${path}
```
where `${path}` is the **full** path to the clonned directory (where folder .template.config placed) **without trailing slash**

Now new template can be used in dotnet cli:

```sh
dotnet new lkeservice -n ${ServiceName} -o Lykke.Service.${ServiceName}
```
This will create a solution in the current folder, where `${ServiceName}` is the service name without Lykke.Service. prefix. 

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
