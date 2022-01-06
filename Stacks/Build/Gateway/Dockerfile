FROM nginx:1.19.8-alpine AS gateway
COPY ./Stacks/Build/Gateway/Files/Config/nginx.conf /etc/nginx/nginx.conf
COPY ./Stacks/Build/Gateway/Files/Config/api_gateway.conf /etc/nginx/api_gateway.conf