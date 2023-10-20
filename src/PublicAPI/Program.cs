using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using PublicAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movies",
        Description = "Demo API - Clean Architecture Solution in .NET 6",
    });

    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Description = "api key.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "basic"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                },
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var scopeProvider = scope.ServiceProvider;
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-ui/custom.css");
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

