FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 5000
COPY . .

# Use dotnet watch to enable hot reload
CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000", "--non-interactive"]
