# Usage

## Environment variables

To define your own environment variables, see [Working with multiple environments](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments)

* *ASPNETCORE_ENVIRONMENT* - defines environment name, the value can be: Development, Staging, Production. When value is Development, 
AppSettings will be loaded from appsettings.Development.json (which overrides appsettings.json), 
otherwise, AppSettings will be loaded from URL defined by SettingsUrl env variable.
* *SettingsUrl* - defines URL of remote settings. 

Default launchSettings.json is:

```json
{
  "profiles": {
    "LykkeJob Development": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "LykkeJob Remote Settings": {
      "commandName": "Project",
      "environmentVariables": {
        "SettingsUrl": ""
      }
    }
  }
}
```

## Job triggers

See [Job Triggers read me](https://github.com/LykkeCity/JobTriggers/blob/master/readme.md)