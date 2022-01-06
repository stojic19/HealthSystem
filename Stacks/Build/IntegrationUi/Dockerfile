FROM node:14.15.0-alpine as buildIntegration
COPY /WebClients/IntegrationWebClient ./WebClient
RUN cd WebClient && \
    npm install
COPY ./Stacks/Build/Gateway/Files/Front/environment.ts.template ./WebClient
COPY ./Stacks/Build/Gateway/Files/Scripts/build-app.sh .
ARG HOSPITAL_API_URL
RUN chmod +x ./build-app.sh && \
    ./build-app.sh ${HOSPITAL_API_URL}

FROM node:14.15.0-alpine as executer
COPY --from=buildIntegration /WebClient /usr/src/server
EXPOSE 8083

CMD ["node", "/usr/src/server/server.js"]