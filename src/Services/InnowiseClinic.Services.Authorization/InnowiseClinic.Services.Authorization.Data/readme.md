# EF Core scaffolding/reverse-engineering tools notes

This project uses a custom DB context design time factory. It requires at least a single command-line argument, which is the connection string to your database.

Any EF Core scaffolding/reverse-engineering tool command will require you to specify the connection string. Example: `dotnet ef database update -- "Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=InnowiseClinic.Services.Authorization.Data-localdb"`. Before executing the command, make sure you are in the data layer project directory (that is also where this file is located).