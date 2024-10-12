# Use the official .NET 8 SDK image from Microsoft for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS develop
COPY . /app
WORKDIR /app

EXPOSE 5219

CMD dotnet watch --no-launch-profile