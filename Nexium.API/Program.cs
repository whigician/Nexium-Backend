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
builder.Services.AddCors(options =>
{
    options.AddPolicy("_allowedOrigins", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
ConfigureServices(builder.Services);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCors("_allowedOrigins");
app.UseMiddleware<SelectedLanguageMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
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
    services.AddScoped<IBusinessStatusesService, BusinessStatusesService>();
    services.AddScoped<IBusinessStatusesRepository, BusinessStatusesRepository>();
    services.AddScoped<ITargetMarketsService, TargetMarketsService>();
    services.AddScoped<ITargetMarketsRepository, TargetMarketsRepository>();
    services.AddScoped<ICurrenciesService, CurrenciesService>();
    services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
    services.AddScoped<IAddressTypesService, AddressTypesService>();
    services.AddScoped<IAddressTypesRepository, AddressTypesRepository>();
    services.AddScoped<IContactTypesService, ContactTypesService>();
    services.AddScoped<IContactTypesRepository, ContactTypesRepository>();
    services.AddScoped<IIdentifierTypesService, IdentifierTypesService>();
    services.AddScoped<IIdentifierTypesRepository, IdentifierTypesRepository>();
    services.AddScoped<ILanguagesService, LanguagesService>();
    services.AddScoped<ILanguagesRepository, LanguagesRepository>();
    services.AddScoped<ITranslationMappingRepository, TranslationMappingRepository>();
    services.AddScoped<ITranslationMappingService, TranslationMappingService>();
    // Registering Mappers
    services.AddSingleton<IndustryMapper>();
    services.AddSingleton<TranslationMapper>();
    services.AddSingleton<BusinessTypeMapper>();
    services.AddSingleton<BusinessStatusMapper>();
    services.AddSingleton<IdentifierTypeMapper>();
    services.AddSingleton<ContactTypeMapper>();
    services.AddSingleton<AddressTypeMapper>();
    services.AddSingleton<LanguageMapper>();
    services.AddSingleton<CurrencyMapper>();
    services.AddSingleton<TargetMarketMapper>();
    services.AddHttpContextAccessor();
    services.AddScoped<SelectedLanguageService>();
}