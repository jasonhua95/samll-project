FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["NetNote/NetNote.csproj", "NetNote/"]
RUN dotnet restore "NetNote/NetNote.csproj"
COPY . .
WORKDIR "/src/NetNote"
RUN dotnet build "NetNote.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NetNote.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetNote.dll"]