#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/1 - Presentation/Challenge.RealEstates.API/Challenge.RealEstates.API.csproj", "src/1 - Presentation/Challenge.RealEstates.API/"]
COPY ["src/1 - Presentation/Challenge.RealEstates.Gateways/Challenge.RealEstates.Gateways.csproj", "src/1 - Presentation/Challenge.RealEstates.Gateways/"]
COPY ["src/3 - Domain/Challenge.RealEtates.Domain/Challenge.RealEtates.Domain.csproj", "src/3 - Domain/Challenge.RealEtates.Domain/"]
COPY ["src/4 - Infrastructure/Challenge.RealStates.Infrastructure/Challenge.RealStates.Infrastructure.csproj", "src/4 - Infrastructure/Challenge.RealStates.Infrastructure/"]
COPY ["src/3 - Domain/Challenge.RealEtates.Core/Challenge.RealEtates.Core.csproj", "src/3 - Domain/Challenge.RealEtates.Core/"]
COPY ["src/2 - Application/Challenge.RealEstates.Application/Challenge.RealEstates.Application.csproj", "src/2 - Application/Challenge.RealEstates.Application/"]
COPY ["src/3 - Domain/Challenge.RealEtates.Services/Challenge.RealEtates.Services.csproj", "src/3 - Domain/Challenge.RealEtates.Services/"]
RUN dotnet restore "src/1 - Presentation/Challenge.RealEstates.API/Challenge.RealEstates.API.csproj"
COPY . .
WORKDIR "/src/src/1 - Presentation/Challenge.RealEstates.API"
RUN dotnet build "Challenge.RealEstates.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Challenge.RealEstates.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge.RealEstates.API.dll"]