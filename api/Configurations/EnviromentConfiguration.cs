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
        var parentDir  = Directory.GetParent(currentDir)?.FullName;

        DotNetEnv.Env.Load(Path.Combine(currentDir, ".env"));
        if (parentDir != null) DotNetEnv.Env.Load(Path.Combine(parentDir, ".env"));

        LoadMongoSettings(configuration);
        LoadSmtpSettings(configuration);
    }

    private static void LoadMongoSettings(IConfiguration configuration)
    {
        var user   = Environment.GetEnvironmentVariable("MONGO_ROOT_USER");
        var pass   = Environment.GetEnvironmentVariable("MONGO_ROOT_PASSWORD");
        var dbName = Environment.GetEnvironmentVariable("MONGO_DB_NAME");
        var host   = Environment.GetEnvironmentVariable("MONGO_HOST");
        var port   = Environment.GetEnvironmentVariable("MONGO_PORT");

        var connectionTemplate = configuration["MongoDB:ConnectionString"] ?? "";

        if (string.IsNullOrEmpty(connectionTemplate)) return;

        var finalConnectionString = connectionTemplate
            .Replace("{USER}", user)
            .Replace("{PASS}", pass)
            .Replace("{HOST}", host)
            .Replace("{PORT}", port);

        if (finalConnectionString.Contains("{"))
            throw new InvalidOperationException($"{Message.ErrorMappingEnviroment} : {finalConnectionString}");

        configuration["MongoDB:ConnectionString"] = finalConnectionString;
        configuration["MongoDB:DatabaseName"]     = dbName;
    }

    private static void LoadSmtpSettings(IConfiguration configuration)
    {
        var username = Environment.GetEnvironmentVariable("SMTP_USERNAME");
        var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
        var from     = Environment.GetEnvironmentVariable("SMTP_FROM");

        var usernameTemplate = configuration["Smtp:Username"] ?? "";
        var passwordTemplate = configuration["Smtp:Password"] ?? "";
        var fromTemplate     = configuration["Smtp:From"]     ?? "";

        if (string.IsNullOrEmpty(usernameTemplate)) return;

        var finalUsername = usernameTemplate.Replace("{SMTP_USERNAME}", username);
        var finalPassword = passwordTemplate.Replace("{SMTP_PASSWORD}", password);
        var finalFrom     = fromTemplate.Replace("{SMTP_FROM}",     from);

        if (finalUsername.Contains("{") || finalPassword.Contains("{") || finalFrom.Contains("{"))
            throw new InvalidOperationException(Message.ErrorMappingEnviroment);

        configuration["Smtp:Username"] = finalUsername;
        configuration["Smtp:Password"] = finalPassword;
        configuration["Smtp:From"]     = finalFrom;
    }
}