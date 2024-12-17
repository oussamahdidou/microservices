using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);
var redis =builder.AddRedis("redis", port: 6379)
    .WithImage("redis",tag:"latest");
var sqlserver = builder.AddSqlServer("sqlserver", port: 1433)
    .WithVolume("microservices_sqlserver_data", "/var/opt/mssql")
    .WithEnvironment("SA_PASSWORD", "Coding@1234?")
    .WithEnvironment("ACCEPT_EULA","Y")
    .WithHealthCheck();
var postgres = builder.AddPostgres("postgresql", port: 5432)
    .WithVolume("microservices_postgres_data", "/var/lib/postgresql/data")
    .WithImage("postgres", tag: "15")
    .WithEnvironment("POSTGRES_USER","stockuser")
    .WithEnvironment("POSTGRES_PASSWORD","stockpassword")
    .WithEnvironment("POSTGRES_DB", "stock")
    .WithHealthCheck();
var rabbitmq = builder.AddRabbitMQ("rabbitmq", port: 5672)
    .WithManagementPlugin(port: 15672)
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest")
    .WithImage("rabbitmq", tag: "3-management")
    .WithHealthCheck();
var usersService=builder.AddProject<Projects.UsersService>("usersservice")
    .WithReference(rabbitmq)
    .WithReference(sqlserver)
    .WithReference(redis)
    .WaitFor(rabbitmq)
    .WaitFor(sqlserver);
var gateway=builder.AddProject<Projects.Gateway>("gateway");
var marketplaceservice = builder.AddProject<Projects.MarketplaceService_API>("marketplaceservice-api")
    .WithReference(rabbitmq)
    .WithReference(sqlserver)
    .WithReference(redis)
    .WaitFor(rabbitmq)
    .WaitFor(sqlserver);
var stockservice = builder.AddSpringApp("stockservice",
    "../../services/stockservice",
    new JavaAppExecutableResourceOptions()
    {
        
        ApplicationName = "../../services/stockservice/build/libs/stockservice-0.0.1-SNAPSHOT.jar",
        OtelAgentPath = "../../services/stockservice/agents"
    })
    .WithEnvironment("DB_HOST", "127.0.0.1")
    .WithReference(rabbitmq)
    .WithReference(postgres)
    .WaitFor(rabbitmq)
    .WaitFor(postgres);
builder.AddViteApp("dashboard", workingDirectory: "../../../client/adminpanel/free-react-tailwind-admin-dashboard")
        .WithHttpEndpoint(name: "dashboard-http", port: 3100, targetPort: 3100, isProxied: false)
        .WithReference(gateway)
        .WaitForCompletion(marketplaceservice)
        .WaitForCompletion(usersService)
        .WaitForCompletion(stockservice);
builder.AddNpmApp("marketplace", workingDirectory: "../../../client/marketplace/web")
        .WithHttpEndpoint(name: "marketplace-http", port: 3000, targetPort: 3000, isProxied: false)
        .WithReference(gateway)
        .WaitForCompletion(marketplaceservice)
        .WaitForCompletion(usersService)
        .WaitForCompletion(stockservice);
builder.Build().Run();
