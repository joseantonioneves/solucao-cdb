# Etapa 1: build da aplicação Angular
FROM node:23-alpine AS build
WORKDIR /app

# Copia os arquivos do projeto Angular
COPY package*.json ./
RUN npm install --legacy-peer-deps

COPY . .
RUN npm run build

# Etapa 2: imagem de produção com Nginx
FROM nginx:stable-alpine
COPY --from=build /app/dist/solucao-cdb-frontend /usr/share/nginx/html

# Substitui a configuração default do nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]