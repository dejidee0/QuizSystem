# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source

# Copy the project files
COPY . .

# Restore NuGet packages
RUN dotnet restore "QuizAppSystem/QuizAppSystem.csproj"


# Publish the project
RUN dotnet publish "QuizAppSystem/QuizAppSystem.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS serve
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "QuizAppSystem.dll"]
