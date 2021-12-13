FROM node:15.14.0-alpine AS frontPatientBuild
WORKDIR /src
COPY /WebClients/PatientWebClient ./PatientWebClient
RUN cd PatientWebClient && \
    npm install
COPY ./Stacks/Build/Gateway/Files/Front/environment.ts.template ./PatientWebClient
COPY ./Stacks/Build/Gateway/Files/Scripts/build-app.sh .
ARG HOSPITAL_API_URL
RUN chmod +x ./build-app.sh && \
    ./build-app.sh ${HOSPITAL_API_URL}

FROM node:15.14.0-alpine AS frontManagerBuild
WORKDIR /src
COPY /WebClients/ManagerWebClient ./ManagerWebClient
RUN cd ManagerWebClient && \
    npm install
COPY ./Stacks/Build/Gateway/Files/Front/environment.ts.template ./ManagerWebClient
COPY ./Stacks/Build/Gateway/Files/Scripts/build-app-manager.sh .
ARG HOSPITAL_API_URL
RUN chmod +x ./build-app-manager.sh && \
    ./build-app-manager.sh ${HOSPITAL_API_URL}

FROM nginx:1.19.8-alpine AS gatewayWithFront
COPY --from=frontPatientBuild /PatientWebClient /usr/share/nginx/html/app/patient
COPY --from=frontManagerBuild /ManagerWebClient /usr/share/nginx/html/app/manager
COPY ./Stacks/Build/Gateway/Files/Config/nginx.conf /etc/nginx/nginx.conf
COPY ./Stacks/Build/Gateway/Files/Config/api_gateway.conf /etc/nginx/api_gateway.conf

FROM nginx:1.19.8-alpine AS gateway
COPY ./Stacks/Build/Gateway/Files/Config/nginx.conf /etc/nginx/nginx.conf
COPY ./Stacks/Build/Gateway/Files/Config/api_gateway.conf /etc/nginx/api_gateway.conf