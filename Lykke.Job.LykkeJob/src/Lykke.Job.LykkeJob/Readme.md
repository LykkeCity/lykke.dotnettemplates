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

## Health monitoring

Job should provides it's health status by responding to HTTP /api/isAlive request. 
If job health is ok, it should respond IsAliveResponse with status code 200, if job health is bad, it should respond ErrorResponse with status code 500.
You can extend IsAliveResponse to  provide all necessary job health information.

*Job shouldn't receive any other HTTP requests*

Typical IsAlive action looks like:

```cs
    public IActionResult Get()
    {
        if (!IsHealthy())
        {
            return StatusCode((int) HttpStatusCode.InternalServerError, new ErrorResponse
            {
                ErrorMessage = GetHealthProblemDescription()
            });
        }

        return Ok(new IsAliveResponse
        {
            Version = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion,
            Env = Environment.GetEnvironmentVariable("Env")
        });
    }
```
