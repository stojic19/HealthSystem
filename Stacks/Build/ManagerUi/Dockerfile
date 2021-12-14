FROM node:15.14.0-alpine as buildManager
COPY /WebClients/ManagerWebClient ./ManagerWebClient
RUN cd ManagerWebClient && \
    npm install
COPY ./Stacks/Build/Gateway/Files/Front/environment.ts.template ./ManagerWebClient
COPY ./Stacks/Build/Gateway/Files/Scripts/build-app-manager.sh .
ARG HOSPITAL_API_URL
RUN chmod +x ./build-app-manager.sh && \
    ./build-app-manager.sh ${HOSPITAL_API_URL}

FROM node:15.14.0-alpine as executer
COPY --from=buildManager /ManagerWebClient /usr/src/server
EXPOSE 8082

CMD ["node", "/usr/src/server/server.js"]