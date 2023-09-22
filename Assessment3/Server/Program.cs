using Assessment3.Server.Api.Extensions;
using Assessment3.Server.Application.Common.Services;
using Assessment3.Server.Application.Data;
using Assessment3.Server.Application.Extensions;
using Assessment3.Server.Infrastructure.Common.Services;
using Assessment3.Server.Infrastructure.Data;
using Assessment3.Server.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Assessment API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", b => 
    { 
        b.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod(); 
    });
});

builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IDataSeeder, DataSeeder>();

builder.Services
    .AddMappings()
    .AddAuthenticationService(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddApplication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();
await app.SeedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.UseCors("EnableCORS");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();