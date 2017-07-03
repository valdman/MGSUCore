FROM microsoft/aspnetcore-build 
LABEL Name=mgsucore

COPY . /src
WORKDIR /src

RUN ["dotnet", "restore"]
RUN ["dotnet", "publish", "-o", "/app"]

WORKDIR /app

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

CMD ["dotnet", "run", "MGSUCore.dll"]