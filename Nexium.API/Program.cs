using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Nexium.API.Configuration;
using Nexium.API.Data;
using Nexium.API.Data.Repositories;
using Nexium.API.Data.Repositories.Implementation;
using Nexium.API.Services;
using Nexium.API.Services.Implementation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
ConfigureServices(builder.Services);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<SelectedLanguageMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NexiumDbContext>();
    var pendingMigrations = dbContext.Database.GetPendingMigrations();

    if (pendingMigrations.Any()) dbContext.Database.Migrate();
}

app.Run();
return;

void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<NexiumDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("NexiumConnection"))
            .UseSnakeCaseNamingConvention().UseExceptionProcessor());
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddScoped<IIndustriesService, IndustriesService>();
    services.AddScoped<IIndustriesRepository, IndustriesRepository>();
    services.AddScoped<IBusinessTypesService, BusinessTypesService>();
    services.AddScoped<IBusinessTypesRepository, BusinessTypesRepository>();
    // Registering Mappers
    services.AddSingleton<IndustryMapper>();
    services.AddSingleton<IndustryTranslationMapper>();
    services.AddSingleton<BusinessTypeMapper>();
    services.AddSingleton<BusinessTypeTranslationMapper>();
    services.AddHttpContextAccessor();
    services.AddScoped<SelectedLanguageService>();
}