FROM 192.168.59.159/2.1-sdk-nanoserver-sac2016-nugetconfigproxy-pfx:002 AS base
WORKDIR /app
EXPOSE 80

FROM 192.168.59.159/2.1-sdk-nanoserver-sac2016-nugetconfigproxy-pfx:002 AS build
WORKDIR /src
COPY src/IT2media.IdentityServer/. IT2media.IdentityServer/.
WORKDIR /src/IT2media.IdentityServer

RUN dotnet restore IT2media.IdentityServer.csproj
RUN dotnet build IT2media.IdentityServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish IT2media.IdentityServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IT2media.IdentityServer.dll"]
