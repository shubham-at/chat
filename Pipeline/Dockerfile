#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY Pipeline/Pipeline.csproj Pipeline/
COPY CommunicationModule/CommunicationModule.csproj CommunicationModule/
COPY ChatContract/ChatContract.csproj ChatContract/
COPY ChatMiddleware.Libs/ChatMiddleware.Libs.csproj ChatMiddleware.Libs/
RUN dotnet restore "Pipeline/Pipeline.csproj"
COPY . .
WORKDIR "/src/Pipeline"
RUN dotnet build "Pipeline.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pipeline.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pipeline.dll"]
