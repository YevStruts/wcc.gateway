# wcc.gateway

# dotnet ef migrations add InitialCreate --project wcc.gateway.data --startup-project wcc.gateway.api
# dotnet ef migrations add AddedTestNewsData --project wcc.gateway.data --startup-project wcc.gateway.api
# dotnet ef database update --project wcc.gateway.data --startup-project wcc.gateway.api

# docker-compose up -d
# docker-compose -f "docker-compose.yml" up -d

# add tag
# docker tag wccgateway_wcc_gateway:latest yevstruts/wcc.gateway:2023.3.5.1
# push
# docker push yevstruts/wcc.gateway:2023.3.5.1

# -p <host_port>:<container_port>

# Server
# docker run --name wcc.ui -p 80:80 -p 433:433 -d yevstruts/wcc.ui:2023.3.5.7
# docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=A&VeryComplex123Password' --name sql_server2022 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
# docker run --name wcc.gateway -p 80:80 -p 433:433 -d yevstruts/wcc.gateway:2023.3.5.7

# docker run --name wcc.gateway -p 5001:5001 -d yevstruts/wcc.gateway:2023.3.5.2

# -------------------------
# docker tag wccgateway_wcc_gateway:latest yevstruts/wcc.gateway:2023.3.5.7
# docker tag wcc.ui.i:latest yevstruts/wcc.ui:2023.3.5.7

# docker push yevstruts/wcc.gateway:2023.3.5.7
# docker push yevstruts/wcc.ui:2023.3.5.7

# docker stop wcc.ui;docker rm wcc.ui;docker stop wcc.gateway;docker rm wcc.gateway;
# docker run --name wcc.ui -p 80:80 -d yevstruts/wcc.ui:2023.3.5.7
# docker run --name wcc.gateway -p 5001:80 -p 5002:433 -d yevstruts/wcc.gateway:2023.3.5.7
