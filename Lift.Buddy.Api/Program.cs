using Lift.Buddy.API.Interfaces;
using Lift.Buddy.API.Services;
using Lift.Buddy.Core;
using Lift.Buddy.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.WebHost.UseUrls("http://localhost:5200");

var anyCors = "anyCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: anyCors,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
builder.Services.AddScoped<IPersonalRecordService, PersonalRecordService>();
builder.Services.AddSingleton<IDatabaseMapper, DatabaseMapper>();

builder.Services.AddDbContext<LiftBuddyContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("TestDatabase"));
});

builder.Services.AddControllers().AddJsonOptions(
    opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

var app = builder.Build();

//var context = app.Services.GetRequiredService<DbContext>();

//context.Database.EnsureCreated();
//context.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(anyCors);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
