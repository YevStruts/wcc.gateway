# wcc.gateway

# dotnet ef migrations add InitialCreate --project wcc.gateway.data --startup-project wcc.gateway.api
# dotnet ef migrations add AddedTestNewsData --project wcc.gateway.data --startup-project wcc.gateway.api
# dotnet ef database update --project wcc.gateway.data --startup-project wcc.gateway.api

# docker-compose up -d
# docker-compose -f "docker-compose.yml" up -d

# add tag
# docker tag wccgateway_wcc_gateway:latest yevstruts/wcc.gateway:2023.7.5.1
# push
# docker push yevstruts/wcc.gateway:2023.7.5.1

# -p <host_port>:<container_port>

# Server
# docker run --name wcc.ui -p 80:80 -p 433:433 -d yevstruts/wcc.ui:2023.7.5.1
# docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=A&VeryComplex123Password' --name mssql -p 1433:1433 -v /home/ubuntu/database:/media/Database --cpus="4.0" --memory="6G" -d mcr.microsoft.com/mssql/server:2022-latest
# docker run --name wcc.gateway -p 80:80 -p 433:433 -d yevstruts/wcc.gateway:2023.7.5.1

# docker run --name wcc.gateway -p 5001:5001 -d yevstruts/wcc.gateway:2023.3.5.2

# -------------------------
# docker tag wcc.gateway.i:latest yevstruts/wcc.gateway:2023.7.5.1
# docker tag wcc.ui.i:latest yevstruts/wcc.ui:2023.7.5.1

# docker push yevstruts/wcc.gateway:2023.7.5.1
# docker push yevstruts/wcc.ui:2023.7.5.1

# docker stop wcc.ui;docker rm wcc.ui;docker stop wcc.gateway;docker rm wcc.gateway;
# docker run --name wcc.ui -p 3000:80 -d yevstruts/wcc.ui:2023.7.5.1
# docker run --name wcc.gateway -p 3001:80 -p 5002:433 -d yevstruts/wcc.gateway:2023.7.5.1

# -------------------------
# delete everything in docker
# docker system prune -a --volumes

# docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=A&VeryComplex123Password' --name mssql -p 1433:1433 -v database:/media/Database -d mcr.microsoft.com/mssql/server:2022-latest
# The solution was to change the permission of the file. I needed to do:
# docker exec -it -u root mssql "bash"
# Then change the user (whoami returns mssql when I run the docker container not as root)
# chown mssql /media/Database

# explore files system docker
# docker exec -t -i wcc.gateway /bin/bash
# docker cp wcc.gateway:/logs/webapi-20230311.log .

# INSERT INTO [dbo].[Games] VALUES ( 1 ,'none' ,'2023-05-13 00:00:00.0000000' ,0 ,0 ,0 ,0 ,4);