# Lykke.Service template #

dotnet cli template for generating a solution for the service Lykke.Service.ServiceName

### How to use? ###

Clone repo to some directory

Install template:
```sh
$ dotnet new --install ${path}
```
where `${path}` is the path to the clonned directory (where folder .template.config)

Now new template can be used in dotnet cli:

```sh
dotnet new lkeservice -n ${ServiceName}
```
This will create a solution in the current folder, where `${ServiceName}` is the service name without Lykke.Service. prefix. 
Optionally -o output-folder parameter can be used to create solution in provided folder

When temlate has changed, to update installed template run again command:

```sh
$ dotnet new --install ${path}
```

To remove installed custom temlates run command:

```sh
$ dotnet new --debug:reinit 
```
