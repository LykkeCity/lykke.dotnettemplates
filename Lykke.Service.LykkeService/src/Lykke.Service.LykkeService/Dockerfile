FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY . .
COPY /runtimes/linux-x64/native/libuv.so /app
ENTRYPOINT ["dotnet", "Lykke.Service.LykkeService.dll"]
