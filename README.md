## Blog Api

# Overview 

This project is the api for a dynamic bloc application. It is built using C# and ASP.NET Core, with Entity Framework Core for database interactions.
The project provides a robust API for managing blog posts user authentication, comments and other blog related functionalities

# Technologies Used 

Backend: ASP.NET Core,

Database: Entity Framework Core and Sql Server,

Authentication: JSON Web Tokens (JWT),

API Documentation: Swagger

# Test Users
admin { username: Admin@gmail.com
        password: admin@123
        }
user { username: User@gmail.com
        password: user@123
        }        
        

# To run

1. Open NuGet terminal and type ```Update-Database -Context "DataContext" ```

2. ```Update-Database -Context "AuthDataContext" ```
