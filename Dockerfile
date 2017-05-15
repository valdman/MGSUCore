FROM microsoft/aspnetcore-build
RUN mkdir /src
COPY . /src
WORKDIR /app
RUN ls
RUN cd /src && dotnet restore MGSUCore.sln && dotnet publish MGSUCore.sln -o:/app
EXPOSE 5000
CMD ["dotnet", "MGSUCore.dll"]