ARG ASPNET_VERSION=5.0
ARG SDK_VERSION=5.0

FROM mcr.microsoft.com/dotnet/aspnet:${ASPNET_VERSION} AS base
WORKDIR /app
 

FROM mcr.microsoft.com/dotnet/sdk:${SDK_VERSION} AS build
ENV PROJECT=HospitalApi
WORKDIR /src
COPY . .
RUN dotnet restore "${PROJECT}.csproj" && \
    dotnet build "${PROJECT}.csproj" -c Release
 

FROM build AS publish
ENV PROJECT=HospitalApi
RUN dotnet publish "${PROJECT}.csproj" -c Release -o /app/publish 


FROM base AS final
ENV ASPNETCORE_URLS=http://*:$PORT
COPY --from=publish /app .
WORKDIR /app/publish
CMD ["dotnet", "HospitalApi.dll"]


# FROM build AS migration-base
# RUN PATH="$PATH:/root/.dotnet/tools"; \
#     dotnet tool install --global dotnet-ef

# FROM migration-base AS generate-schema
# ENV PROJECT=HospitalApi
# RUN PATH="$PATH:/root/.dotnet/tools"; \
#     dotnet-ef dbcontext script \
#         -p "${PROJECT}.csproj" \
#         -o sql/init.sql