# Lykke.Service template #

dotnet cli template for generating a solution for the service Lykke.Service.ServiceName

### How to use? ###

* Clone repo to some directory
* run dotnet new --install <path to cloned directory (where folder .template.config)>
* new cli can be used like this: 
dotnet new lkeservice -n ServiceName

this will create a solution in the current folder, where ServiceName is the service name without Lykke.Service. prefix. 
Optionally -o output-folder parameter can be used to create solution in provided folder