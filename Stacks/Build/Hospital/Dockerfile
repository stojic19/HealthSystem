ARG ASPNET_VERSION=5.0
ARG SDK_VERSION=5.0

FROM mcr.microsoft.com/dotnet/aspnet:${ASPNET_VERSION} AS base
WORKDIR /app
 
FROM mcr.microsoft.com/dotnet/sdk:${SDK_VERSION} AS build
WORKDIR /src
COPY . .
RUN dotnet restore "../../Hospital/HospitalApi/HospitalApi.csproj" && \
    dotnet build "../../Hospital/HospitalApi/HospitalApi.csproj" -c Release -o /app/build
 
FROM build AS publish
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef --version 3.1.0 && \
    dotnet publish "../../Hospital/HospitalApi/HospitalApi.csproj" -c Release -o /app/publish && \
    dotnet-ef migrations script -p "../../Hospital/HospitalApi/HospitalApi.csproj" -o /app/sql/init.sql

 
FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app .
WORKDIR /app/publish
VOLUME /app/sql
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PrimerWebApi.dll