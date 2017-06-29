# Lykke.Service template #

dotnet cli template for generating a solution for the service Lykke.Service.ServiceName

### How to use? ###

Clone repo to some directory

Install template:
```sh
$ dotnet new --install ${path}
```
where `${path}` is the path to the clonned directory (where folder .template.config placed) without trailing slash

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
