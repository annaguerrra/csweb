using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MockTestCs.Endpoints;
using MockTestCs.Entities;
using MockTestCs.Features.AddHistory;
using MockTestCs.Features.AddToList;
using MockTestCs.Features.CreateList;
using MockTestCs.Features.CreateUser;
using MockTestCs.Features.DeleteFromList;
using MockTestCs.Features.DeleteHistory;
using MockTestCs.Features.Login;
using MockTestCs.Features.ShowHistory;
using MockTestCs.Services.JWT;
using MockTestCs.Services.Password;

var builder = WebApplication.CreateBuilder(args);

var strConnection = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=History;Trust Server Certificate=True;Integrated Security=True";
builder.Services.AddDbContext<MockTestCsDbContext>(
    opt => opt.UseSqlServer(strConnection)
);


// Use cases: Scoped (Mexem com DbContext): AddHistory, AddToList, CreateUserUseCase, DeleteFromList, DeleteHistory, Login, CreateList

builder.Services.AddScoped<AddHistoryUseCase>();     // history enpoint
builder.Services.AddScoped<AddToListUseCase>();      // list endpoint
builder.Services.AddScoped<CreateUserUseCase>();     // user endpoint
builder.Services.AddScoped<CreateListUseCase>();     // list endpoint
builder.Services.AddScoped<DeleteFromListUseCase>(); // list endpoint
builder.Services.AddScoped<DeleteHistoryUseCase>();  // history endpoint
builder.Services.AddScoped<LoginUseCase>();          // login endpoint
builder.Services.AddScoped<ShowHistoryUseCase>();    // history endpoint

// Serviços: Singleton (mas poderiam ser transient) não armazenam estado interno entre chamadas, só recebem e transformam dados

builder.Services.AddSingleton<IPasswordServices, PBKDF2PasswordServices>();
builder.Services.AddSingleton<IJWTService, EFJWTService>();

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);
var key = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {// dhcsdfcirlfiuwhelvlfhlhl
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "mock-test-app",
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = key,
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureHistory();
app.ConfigureLoginEnpoints();
app.ConfigureReadingListEndpoints();
app.ConfigureUserEndpoints();

app.MapGet("/", () => "API rodando ✅");


app.Run();
 