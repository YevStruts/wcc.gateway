# wcc.gateway

# dotnet ef migrations add InitialCreate --project wcc.gateway.data --startup-project wcc.gateway.api
# dotnet ef migrations add AddedTestNewsData --project wcc.gateway.data --startup-project wcc.gateway.api
# dotnet ef database update --project wcc.gateway.data --startup-project wcc.gateway.api

# docker-compose up -d
# docker-compose -f "docker-compose.yml" up -d

# add tag
# docker tag wccgateway_wcc_gateway:latest yevstruts/wcc.gateway:2023.11.28.1
# push
# docker push yevstruts/wcc.gateway:2023.11.28.1

# -p <host_port>:<container_port>

# Server
# docker run --name wcc.ui -p 80:80 -p 433:433 -d yevstruts/wcc.ui:2023.11.28.1
# docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=A&VeryComplex123Password' --name mssql -p 1433:1433 -v /home/ubuntu/database:/media/Database --cpus="4.0" --memory="6G" -d mcr.microsoft.com/mssql/server:2022-latest
# docker run --rm --name ravendb -d -p 8080:8080 -p 38888:38888 -v /home/ubuntu/database/ravendb:/opt/RavenDB/Server/RavenData --name RavenDb-WithData -e RAVEN_Setup_Mode=None -e RAVEN_License_Eula_Accepted=true -e RAVEN_Security_UnsecuredAccessAllowed=PrivateNetwork ravendb/ravendb
# docker run --name wcc.gateway -p 80:80 -p 433:433 -d yevstruts/wcc.gateway:2023.11.28.1

# docker run --name ravendb -d -p 8080:8080 -p 38888:38888 -v d:/database/wcc/ravendb:/opt/RavenDB/Server/RavenData -e RAVEN_Setup_Mode=None -e RAVEN_License_Eula_Accepted=true -e RAVEN_Security_UnsecuredAccessAllowed=PrivateNetwork ravendb/ravendb
# docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=A&VeryComplex123Password' --name mssql -p 1433:1433 -v d:/database/wcc/mssql:/media/database --cpus="4.0" --memory="6G" -d mcr.microsoft.com/mssql/server:2022-latest

# docker run --name wcc.gateway -p 5001:5001 -d yevstruts/wcc.gateway:2023.3.5.2

# -------------------------
# docker tag wcc.ui.i:latest yevstruts/wcc.ui:2023.11.28.1
# docker tag wcc.gateway.i:latest yevstruts/wcc.gateway:2023.11.28.1
# docker tag wcc.rating.i:latest yevstruts/wcc.rating:2023.11.28.1
# docker tag wcc.lanserver.i:latest yevstruts/wcc.lanserver:2023.11.28.1

# docker push yevstruts/wcc.gateway:2023.11.28.1
# docker push yevstruts/wcc.ui:2023.11.28.1
# docker push yevstruts/wcc.rating:2023.11.28.1
# docker push yevstruts/wcc.lanserver:2023.11.28.1

# docker stop wcc.ui;docker rm wcc.ui;docker stop wcc.gateway;docker rm wcc.gateway;docker stop wcc.rating;docker rm wcc.rating;
# docker run --name wcc.ui -p 3000:80 -d yevstruts/wcc.ui:2023.11.28.1
# docker run --name wcc.gateway -p 3001:80 -p 5002:433 -d yevstruts/wcc.gateway:2023.11.28.1
# docker run --name wcc.rating -p 6001:80 -p 6002:433 -d yevstruts/wcc.rating:2023.11.28.1
# docker run --name wcc.lanserver -p 31523:31523 -d yevstruts/wcc.lanserver:2023.11.28.1

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
# docker cp mssql:/media/Database/wcc.bak .

# INSERT INTO [dbo].[Games] VALUES ( 1 ,'none' ,'2023-05-13 00:00:00.0000000' ,0 ,0 ,0 ,0 ,4);
#
# copy from vps to local
# scp root@217.160.240.169:/home/ubuntu/database/wcc.bak D:/Database/WCC/mssql 

# ------------------------
# docker run --name teamcity-server -v D:/Projects/wcc/wcc.teamcity/server/data:/data/teamcity_server/datadir -v D:/Projects/wcc/wcc.teamcity/server/logs:/opt/teamcity/logs -p 8111:8111 jetbrains/teamcity-server
# docker run --name teamcity-agent -e SERVER_URL="http://192.168.2.27:8111/" --link teamcity-server -v D:/Projects/wcc/wcc.teamcity/agent:/data/teamcity_agent/conf jetbrains/teamcity-agent

# https://apisero.com/dockerised-installation-of-teamcity-server-and-agent/

docker run --name teamcity-server -p 8111:8111 jetbrains/teamcity-server

docker run -d --name teamcity-server-instance -v /opt/docker/teamCity/teamcity_server/datadir:/data/teamcity_server/datadir -v /opt/docker/teamCity/teamcity_server/logs:/opt/teamcity/logs -p 9111:8111 jetbrains/teamcity-server

# dotnet cake --target=PushAll --version=2023.11.28.1