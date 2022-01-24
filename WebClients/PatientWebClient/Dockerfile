FROM node:14.15.0-alpine as buildPatient
COPY ./ ./WebClient
RUN cd WebClient && \
    npm install
COPY ./Files/Front/environment.ts.template ./WebClient
COPY ./Files/Scripts/build-app.sh .
ARG HOSPITAL_API_URL
RUN chmod +x ./build-app.sh && \
    ./build-app.sh ${HOSPITAL_API_URL}


FROM node:14.15.0-alpine as executer
COPY --from=buildPatient /WebClient /usr/src/server
EXPOSE 8081

CMD ["node", "/usr/src/server/server.js"]