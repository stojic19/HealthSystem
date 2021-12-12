ARG ASPNET_VERSION=5.0
ARG SDK_VERSION=5.0

FROM mcr.microsoft.com/dotnet/aspnet:${ASPNET_VERSION} AS base
WORKDIR /app
EXPOSE 80
 

FROM mcr.microsoft.com/dotnet/sdk:${SDK_VERSION} AS build
ENV PROJECT=HospitalApi
WORKDIR /src
COPY /Hospital .
RUN dotnet restore "${PROJECT}/${PROJECT}.csproj" && \
    dotnet build "${PROJECT}/${PROJECT}.csproj" -c Release
 

# FROM build AS publish
# ENV PROJECT=HospitalApi
# RUN dotnet publish "${PROJECT}/${PROJECT}.csproj" -c Release -o /app/publish 

# FROM build as sqlgen
# WORKDIR /HospitalClassLib
# RUN dotnet tool install -g dotnet-ef --version 5.0.11 && \
#     dotnet-ef migrations script -p "Hospital.csproj" -o /app/sql/init.sql
# WORKDIR /src

FROM build AS publish
ENV PROJECT=HospitalApi
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef --version 5.0.11 && \
    dotnet publish "${PROJECT}/${PROJECT}.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
WORKDIR /app/publish
# VOLUME /app/sql
ENTRYPOINT ["dotnet", "HospitalApi.dll"]


# FROM build AS migration-base
# RUN PATH="$PATH:/root/.dotnet/tools"; \
#     dotnet tool install --global dotnet-ef

# FROM migration-base AS generate-schema
# ENV PROJECT=HospitalApi
# RUN PATH="$PATH:/root/.dotnet/tools"; \
#     dotnet-ef dbcontext script \
#         -p "${PROJECT}/${PROJECT}.csproj" \
#         -o sql/init.sql