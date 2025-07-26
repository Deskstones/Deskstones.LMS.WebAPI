# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /app

# Copy solution and project files
COPY Deskstones.LMS.WebAPI.sln ./

COPY src/Deskstones.LMS.WebAPI/Deskstones.LMS.WebAPI.csproj ./src/Deskstones.LMS.WebAPI/
COPY src/Deskstones.LMS.BusinessLogic/Deskstones.LMS.BusinessLogic.csproj ./src/Deskstones.LMS.BusinessLogic/
COPY src/Deskstones.LMS.Domain/Deskstones.LMS.Domain.csproj ./src/Deskstones.LMS.Domain/
COPY src/Deskstones.LMS.Infrastructure/Deskstones.LMS.Infrastructure.csproj ./src/Deskstones.LMS.Infrastructure/
COPY src/Software.DataContracts/Software.DataContracts.csproj ./src/Software.DataContracts/
COPY src/Software.Helper/Software.Helper.csproj ./src/Software.Helper/

# Restore NuGet packages
RUN dotnet restore

# Copy all source files
COPY . .

# Build and publish the WebAPI
RUN dotnet publish src/Deskstones.LMS.WebAPI/Deskstones.LMS.WebAPI.csproj -c Release -o /app/publish

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose default HTTP port
EXPOSE 80

# Run the WebAPI
ENTRYPOINT ["dotnet", "Deskstones.LMS.WebAPI.dll"]
