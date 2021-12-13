FROM node:15.14.0-alpine AS frontBuild
WORKDIR /src
# COPY ./Files/Scripts/install-dependencies.sh .
# ARG FRONTEND_APP_SRC_URL=https://github.com/Clean-CaDET/platform-tutor-ui-web/archive/refs/heads/master.tar.gz
# SHELL ["/bin/ash", "-o", "pipefail", "-c"]
# RUN chmod +x ./install-dependencies.sh && \
#     ./install-dependencies.sh ${FRONTEND_APP_SRC_URL}
COPY /WebClients/PatientWebClient ./PatientWebClient
RUN cd PatientWebClient && \
    npm install
COPY ./Stacks/Build/Gateway/Files/Front/environment.ts.template ./PatientWebClient
COPY ./Stacks/Build/Gateway/Files/Scripts/build-app.sh .
ARG HOSPITAL_API_URL
RUN chmod +x ./build-app.sh && \
    ./build-app.sh ${HOSPITAL_API_URL}

FROM nginx:1.19.8-alpine AS gatewayWithFront
COPY --from=frontBuild /PatientWebClient /usr/share/nginx/html/app
COPY ./Stacks/Build/Gateway/Files/Config/nginx.conf /etc/nginx/nginx.conf
COPY ./Stacks/Build/Gateway/Files/Config/api_gateway.conf /etc/nginx/api_gateway.conf

FROM nginx:1.19.8-alpine AS gateway
COPY ./Stacks/Build/Gateway/Files/Config/nginx.conf /etc/nginx/nginx.conf
COPY ./Stacks/Build/Gateway/Files/Config/api_gateway.conf /etc/nginx/api_gateway.conf