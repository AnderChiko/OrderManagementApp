1. Environment Requirements

.NET SDK / Runtime: .NET 9

Web Server: Kestrel (built-in) or IIS / Nginx / Apache reverse proxy

Database: SQLite (file-based, no server required)

OS Support: Windows, Linux, or macOS

Optional: Visual Studio 2022+ or VS Code for local builds

2. Configuration

{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=orders.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}

3. Database Deployment
Migration Setup

dotnet ef migrations add InitialCreate
dotnet ef database update


Production SQLite

Copy the generated orders.db file to the deployment directory

Ensure proper file permissions (read/write for app)

4. Build & Publish

From project root:

dotnet publish -c Release -o ./publish


Creates a self-contained build in ./publish

Optional: include runtime for self-contained deployment

5. Deploy to Server

Copy publish folder to target server

Ensure orders.db exists in the same folder or update connection string

6 Notes / Caveats

SQLite is file-based, so concurrent writes are limited. For heavy production, consider SQL Server or PostgreSQL

Ensure .NET 9 runtime is installed on the server

Regularly backup the database file
