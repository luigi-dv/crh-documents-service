using ClearReadHub.Documents.Application;
using ClearReadHub.Documents.Infrastructure;
using ClearReadHub.Documents.Service;

var builder = WebApplication.CreateBuilder(args);
{
    // Add private app settings if exists
    var privateAppSettingsPath = Path.Combine(builder.Environment.ContentRootPath, "appsettings.Private.json");
    if (File.Exists(privateAppSettingsPath))
    {
        builder.Configuration.AddJsonFile(privateAppSettingsPath);
    }

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.UseInfrastructure();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}