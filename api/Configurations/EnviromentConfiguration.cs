
namespace Api.Configurations;


using System;
using System.IO;
using Core.Messages;
using Microsoft.Extensions.Configuration;

public static class EnvironmentConfiguration
{
    public static void LoadEnvironmentSettings(this IConfiguration configuration)
    {
        var currentDir = Directory.GetCurrentDirectory();
        var parentDir = Directory.GetParent(currentDir)?.FullName;

        DotNetEnv.Env.Load(Path.Combine(currentDir, ".env"));
        if (parentDir != null) DotNetEnv.Env.Load(Path.Combine(parentDir, ".env"));

        var user = Environment.GetEnvironmentVariable("MONGO_ROOT_USER");
        var pass = Environment.GetEnvironmentVariable("MONGO_ROOT_PASSWORD");
        var dbName = Environment.GetEnvironmentVariable("MONGO_DB_NAME");
        var host = Environment.GetEnvironmentVariable("MONGO_HOST");
        var port = Environment.GetEnvironmentVariable("MONGO_PORT");

        string connectionTemplate = configuration["MongoDB:ConnectionString"] ?? "";

        if (!string.IsNullOrEmpty(connectionTemplate))
        {
            string finalConnectionString = connectionTemplate
                .Replace("{USER}", user)
                .Replace("{PASS}", pass)
                .Replace("{HOST}", host)
                .Replace("{PORT}", port);

            if (finalConnectionString.Contains("{"))
            {
                throw new InvalidOperationException($"{Message.ErrorMappingEnviroment} : {finalConnectionString}");
            }

            configuration["MongoDB:ConnectionString"] = finalConnectionString;
            configuration["MongoDB:DatabaseName"] = dbName;
        }
    }
}
