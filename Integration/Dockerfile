ARG ASPNET_VERSION=5.0
ARG SDK_VERSION=5.0

FROM mcr.microsoft.com/dotnet/aspnet:${ASPNET_VERSION} AS base
RUN groupadd -g 5000 appuser && \
    useradd -u 5000 -g appuser appuser

USER appuser:appuser
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:7001
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:${SDK_VERSION} AS build
ENV PROJECT=IntegrationApi
WORKDIR /src
COPY ./ .
RUN dotnet restore "${PROJECT}/${PROJECT}.csproj" && \
    dotnet build "${PROJECT}/${PROJECT}.csproj" -c Release

FROM build AS publish
ENV PROJECT=IntegrationApi
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef --version 5.0.11 && \
    dotnet publish "${PROJECT}/${PROJECT}.csproj" -c Release -o /app/publish

FROM base AS final
USER appuser:appuser
WORKDIR /app
COPY --from=publish /app .
WORKDIR /app/publish
ENTRYPOINT ["dotnet", "IntegrationApi.dll"]