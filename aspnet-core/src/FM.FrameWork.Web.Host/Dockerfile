FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/FM.FrameWork.Web.Host/FM.FrameWork.Web.Host.csproj", "src/FM.FrameWork.Web.Host/"]
COPY ["src/FM.FrameWork.Web.Core/FM.FrameWork.Web.Core.csproj", "src/FM.FrameWork.Web.Core/"]
COPY ["src/FM.FrameWork.Application/FM.FrameWork.Application.csproj", "src/FM.FrameWork.Application/"]
COPY ["src/FM.FrameWork.Core/FM.FrameWork.Core.csproj", "src/FM.FrameWork.Core/"]
COPY ["src/FM.FrameWork.EntityFrameworkCore/FM.FrameWork.EntityFrameworkCore.csproj", "src/FM.FrameWork.EntityFrameworkCore/"]
WORKDIR "/src/src/FM.FrameWork.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/FM.FrameWork.Web.Host", "src/FM.FrameWork.Web.Host"]
COPY ["src/FM.FrameWork.Web.Core", "src/FM.FrameWork.Web.Core"]
COPY ["src/FM.FrameWork.Application", "src/FM.FrameWork.Application"]
COPY ["src/FM.FrameWork.Core", "src/FM.FrameWork.Core"]
COPY ["src/FM.FrameWork.EntityFrameworkCore", "src/FM.FrameWork.EntityFrameworkCore"]
WORKDIR "/src/src/FM.FrameWork.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "FM.FrameWork.Web.Host.dll"]
