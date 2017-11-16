# Lykke.Job template

dotnet cli template for generating a solution for the job Lykke.Job.JobName

## How install and use template?

Clone repo to some directory

Install template:
```sh
$ dotnet new --install ${path}
```
where `${path}` is the **full** path to the clonned directory (where folder .template.config placed) **without trailing slash**

Now new template can be used in dotnet cli:

```sh
dotnet new lkejob -n ${JobName} -o Lykke.Job.${JobName} [-az {true|false} -r {true|false} -ra {true|false} -t {true|false}]
```
This will create a solution in the current folder, where `${JobName}` is the job name without Lykke.Job. prefix. 
Switches:
* **-n|--name**: JobName
* **-o|--output**: Output directory name
* **-az|--azurequeuesub**: Enables incoming Azure Queue messages processing, using Lykke.JobTriggers package. Default is **false**
* **-r|--rabbitsub**: Enables incoming RabbitMQ messages processing. Default is **false**
* **-ra|---rabbitpub**: Enables outcoming RabbitMQ messages sending. Default is **false**
* **-t|--timeperiod**: Enables periodical work execution, using TimerPeriod class from Lykke.Common package. Default is **false**

When temlate has changed, to update installed template run again command:

```sh
$ dotnet new --install ${path}
```

To remove all installed custom temlates run command:

```sh
$ dotnet new --debug:reinit 
```

## Developing notes

### Environment variables

To define your own environment variables, see [Working with multiple environments](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments)

* *ASPNETCORE_ENVIRONMENT* - defines environment name, the value can be: Development, Staging, Production.
* *SettingsUrl* - defines URL of remote settings or path for local settings.

Reflect your settings structure in appsettings.json - leave all of field blank, or just show value's format. Fill appsettings.XXX.json with real settings data. Ensure that appsettings.XXX.json is ignored in git.

Default launchSettings.json is:

```json
{
  "profiles": {
    "LykkeJob local dev settings": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "SettingsUrl": "appsettings.Development.json"
      }
    },
    "LykkeJob local test settings": {
      "commandName": "Project",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging",
        "SettingsUrl": "appsettings.Testing.json"
      }
    },
    "LykkeJob remote settings": {
      "commandName": "Project",
      "environmentVariables": {
        "SettingsUrl": "http://settings.lykke-settings.svc.cluster.local/your_token_LykkeJobJob"
      }
    }
  }
}
```

### Job triggers

See [Job Triggers read me](https://github.com/LykkeCity/JobTriggers/blob/master/readme.md)

Consider trigger handler class as controller, it responsible to control execution flow, and cross-cutting concerns. Place all business logic in appropriate service classes. Typical trigger handler class looks like:

```cs
    public class MyHandlers
    {
        private readonly IMyFooService _myFooService;
        private readonly IHealthService _healthService;

        // NOTE: The object is instantiated using DI container, so registered dependencies are injects well
        public MyHandlers(IMyFooService myFooService, IHealthService healthService)
        {
            _myFooService = myFooService;
            _healthService = healthService;
        }

        [QueueTrigger("queue-name")]
        public async Task QueueTriggeredHandler(MyMessage msg)
        {
            try
            {
                _healthService.TraceBooStarted();

                await _myFooService.BooAsync();

                _healthService.TraceBooCompleted();
            }
            catch
            {
                _healthService.TraceBooFailed();
            }
        }
    }
```

### Health monitoring

Job should provides it's health status by responding to HTTP `/api/isAlive` request. 
If job health is ok, it should respond IsAliveResponse with status code 200, if job health is bad, it should respond `ErrorResponse` with status code 500.

You should gathers health statistics in `IHealthService` by injecting it into your trigger handlers classes, 
and invoking specific `IHealthService` methods when key events has occures. 
Then you should implement `IHealthService.GetHealthViolationMessage()` method, 
to return job health violation message, if any.

You can extend `IsAliveResponse` to  provide all necessary job health information.

*Job shouldn't receive any other HTTP requests*

Typical IsAlive action looks like:

```cs
    public IActionResult Get()
    {
        var healthViloationMessage = _healthService.GetHealthViolationMessage();
        if (healthViloationMessage != null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponse
            {
                ErrorMessage = $"Job is unhealthy: {healthViloationMessage}"
            });
        }

        // NOTE: Feel free to extend IsAliveResponse, to display job-specific health status
        return Ok(new IsAliveResponse
        {
            Version = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationVersion,
            Env = Environment.GetEnvironmentVariable("ENV_INFO"),

            // NOTE: Health status information example: 
            LastFooStartedMoment = _healthService.LastFooStartedMoment,
            LastFooDuration = _healthService.LastFooDuration,
            MaxHealthyFooDuration = _healthService.MaxHealthyFooDuration
        });
    }
```
