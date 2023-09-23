# Use the official .NET 6 SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj and .sln files to the container
COPY *.csproj ./

# Restore the NuGet packages
RUN dotnet restore

# Copy the rest of the application source code to the container
COPY . .

# Build the application
RUN dotnet build -c Release -o out

# Publish the application
RUN dotnet publish -c Release -o /app/out

# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the runtime image
WORKDIR /app

# Copy the published application from the build image
COPY --from=build /app/out .

# Expose the port your application will listen on (change this as needed)
EXPOSE 80

# Define the entry point for your application
ENTRYPOINT ["dotnet", "./POC.Integracao.dll"]
