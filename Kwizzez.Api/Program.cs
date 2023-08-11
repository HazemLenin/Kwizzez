using System.Text;
using System.Text.Json.Serialization;
using Kwizzez.Api.Middlewares;
using Kwizzez.DAL.Data;
using Kwizzez.DAL.Services.Answers;
using Kwizzez.DAL.Services.Auth;
using Kwizzez.DAL.Services.Questions;
using Kwizzez.DAL.Services.Quizzes;
using Kwizzez.DAL.Services.Seeds;
using Kwizzez.DAL.Services.StudentScores;
using Kwizzez.DAL.Services.Users;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        options.User.RequireUniqueEmail = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateIssuerSigningKey"]),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JsonWebTokenKeys:IssuerSigningKey"])),

            ValidateIssuer = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateIssuer"]),
            ValidIssuer = builder.Configuration["JsonWebTokenKeys:ValidIssuer"],

            ValidAudience = builder.Configuration["JsonWebTokenKeys:ValidAudience"],
            ValidateAudience = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateAudience"]),

            RequireExpirationTime = bool.Parse(builder.Configuration["JsonWebTokenKeys:RequireExpirationTime"]),
            ValidateLifetime = bool.Parse(builder.Configuration["JsonWebTokenKeys:ValidateLifetime"]),
        };

        //options.Events = new JwtBearerEvents
        //{
        //    OnMessageReceived = context =>
        //    {
        //        var accessToken = context.Request.Query["access_token"];

        //        // If the request is for our hub...
        //        var path = context.HttpContext.Request.Path;
        //        if (!string.IsNullOrEmpty(accessToken) &&
        //            (path.StartsWithSegments("/hubs")))
        //        {
        //            // Read the token out of the query string
        //            context.Token = accessToken;
        //        }
        //        return Task.CompletedTask;
        //    }
        //};
    });

builder.Services.AddTransient<HttpExceptionMiddleware>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IQuizzesService, QuizzesService>();
builder.Services.AddTransient<IQuestionsService, QuestionsService>();
builder.Services.AddTransient<IAnswersService, AnswersService>();
builder.Services.AddTransient<IStudentScoresService, StudentScoresService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseCors(options =>
//     options
//         .AllowAnyOrigin()
//         .AllowAnyHeader()
//         .AllowAnyMethod()
//         .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseDefaultFiles();

//app.MapFallbackToFile("index.html");

app.UseMiddleware<HttpExceptionMiddleware>();

var scope = app.Services.CreateScope();
var seeder = new SeedData(scope.ServiceProvider);
await seeder.Seed();

app.Run();
