# Using EF Core design tools

EF Core design tools require a database connection string to function. 

When using them to work with this project, by default the connection string is looked up in the API layer project's appsettings.Development.json file. The configuration key used for the lookup is `ConnectionStrings:Default`.

You can override the file to search for a connection string by supplying the `--file <filename>` argument after the command, separated from it by a single `--` token.