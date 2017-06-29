# Lykke.Service template #

dotnet cli template for generating a solution for the job Lykke.Job.JobName

### How to use? ###

Clone repo to some directory

Install template:
```sh
$ dotnet new --install ${path}
```
where `${path}` is the path to the clonned directory (where folder .template.config placed) without trailing slash

Now new template can be used in dotnet cli:

```sh
dotnet new lkejob -n ${JobName} -o Lykke.Service.${JobName}
```
This will create a solution in the current folder, where `${JobName}` is the job name without Lykke.Job. prefix.

When temlate has changed, to update installed template run again command:

```sh
$ dotnet new --install ${path}
```

To remove all installed custom temlates run command:

```sh
$ dotnet new --debug:reinit 
```

### Developing notes ###

See this [link](https://github.com/KonstantinRyazantsev/lykke.dotnettemplates/tree/master/Lykke.Job.LykkeJob/src/Lykke.Job.LykkeJob)
