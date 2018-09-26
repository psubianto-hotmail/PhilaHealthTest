#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 53581
EXPOSE 44397

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["PhilaHealthTestApi/PhilaHealthTestApi.csproj", "PhilaHealthTestApi/"]
RUN dotnet restore "PhilaHealthTestApi/PhilaHealthTestApi.csproj"
COPY . .
WORKDIR "/src/PhilaHealthTestApi"
RUN dotnet build "PhilaHealthTestApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PhilaHealthTestApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PhilaHealthTestApi.dll"]