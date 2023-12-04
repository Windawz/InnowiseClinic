# Configuring

The service needs the following settings specified in `appsettings.json`, starting from the top level:
- `Auth:Jwt:Audience` (url)
- `Auth:Jwt:Issuer` (url)
- `Auth:Jwt:SigningKey` (16 characters-long string)
- `Auth:Jwt:Generation:AccessTokenExpirationSeconds` (positive int)
- `Auth:Jwt:Generation:RefreshTokenExpirationSeconds` (positive int)
- `ConnectionStrings:Default` (database connection string)
- `ConnectionStrings:Local` (database connection string; only read in development if no default is provided)
