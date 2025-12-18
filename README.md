# Projects SPA

**Projects SPA** is a web application built with **.NET and Blazor Server**.  
It is designed to support internal company workflows, while not being limited to the company’s internal network.

The main objective of the application is to **simplify the creation, management, and tracking of company projects and offers**.  
The system allows users to register and manage **commercial offers**, store basic project information, and track project status through an intuitive **Kanban board**.

The application is based on a **user and role management system**, ensuring controlled access to features according to user permissions.

Additionally, the application provides:
- Storage of manager comments and project-related notes
- Registration and management of billing and financial data associated with each project
- **Basic statistics and indicators** to support decision-making and project monitoring

In the **second version**, the application also includes:
- Project scheduling and timeline management
- Employee assignment and workload tracking for each project
## Authors

- **Steven Gazo** ([@stevengazo](https://github.com/stevengazo))  
  *Developer & Project Creator*

---

## License

This project is licensed under the **MIT License**.

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/stevengazo/ProjectsMecsaSPA/blob/main/LICENSE.txt)
## Features

- **Kanban board** for project management and task tracking.
- **Calendar view** (weekly and monthly) to visualize project schedules and timelines.
- **Integration with the Central Bank of Costa Rica API** for financial data.
- **Bitrix24 integration** to post comments and upload files directly from the application.
- **Web-based application** accessible from modern browsers.
## Environment Variables

To run this project, you must configure the following environment variables in your `.appsettings.json` file or application settings. These variables are required for external integrations, email services, database connections, and authentication.

---

### Banco Central
```env
BANCOCENTRAL_NOMBRE=Your Name
BANCOCENTRAL_CORREO=your.email@domain.com
BANCOCENTRAL_TOKEN=YourBancoCentralToken
```
### Bitrix Intergration
```env
BITRIX_URL_TASK_ADD=https://your-bitrix-url/task.commentitem.add.json
BITRIX_URL_CREATE_FOLDER=https://your-bitrix-url/disk.folder.addsubfolder.json
BITRIX_URL_UPLOAD_FILE=https://your-bitrix-url/disk.folder.uploadfile.json
```
### Telegram integration
```env
TELEGRAM_BOT_TOKEN=YourTelegramBotToken
```
### SMTP Email Configuration
``` env
SMTP_HOST=smtp.gmail.com
SMTP_PORT=587
SMTP_USERNAME=your.email@domain.com
SMTP_PASSWORD=YourEmailPassword
SMTP_ENABLE_SSL=true
SMTP_FROM=your.email@domain.com
```
### Database connections
``` env
USERS_DB_CONNECTION=Data Source=SERVER_IP;Initial Catalog=UsersDatabase;User ID=DB_USER;Password=DB_PASSWORD
PROJECTS_DB_CONNECTION=Data Source=SERVER;Initial Catalog=ProjectsDatabase;User ID=DB_USER;Password=DB_PASSWORD
```
### Application Settings 
``` env
LOG_LEVEL_DEFAULT=Information
LOG_LEVEL_MICROSOFT=Warning
ALLOWED_HOSTS=*
```

## Deployment - Docker Configuration (.NET 6)

This project uses **Docker** to build and run an ASP.NET Core 6 application.  
Visual Studio leverages this `Dockerfile` to speed up debugging and container builds.

For more information, see:  
https://aka.ms/customizecontainer



### Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjectsMecsaSPA.csproj", "."]
RUN dotnet restore "./ProjectsMecsaSPA.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ProjectsMecsaSPA.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProjectsMecsaSPA.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectsMecsaSPA.dll"]
```
## FAQ

### What is required to run the web application?
Only the .NET 6 Runtime is required on the target device.

### Where is the database schema defined?
The application automatically creates and updates the database schema on the server by applying Entity Framework migrations at startup.

### Can I use Docker?
Yes. The project includes a Dockerfile that allows you to build and run the application as a Docker image.

### What type of SQL database does the application use?
The web application uses a SQL-based relational database.

### Do I need to configure Telegram?
No. Telegram integration is optional and not required for the application to function.
## Contributing

Contributions are welcome and highly appreciated.

If you would like to contribute, please review the guidelines in the `CONTRIBUTING.md` file before getting started. This document explains the development workflow, coding standards, and the process for submitting pull requests.

All contributors are expected to follow the project’s `Code of Conduct` to maintain a respectful, inclusive, and collaborative environment.

If you find this project useful and would like to support its ongoing development, you can do so here:  
https://buymeacoffee.com/stevengazo
