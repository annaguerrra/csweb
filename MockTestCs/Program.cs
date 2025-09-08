using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;
using MockTestCs.Features.AddHistory;
using MockTestCs.Features.AddToList;
using MockTestCs.Features.CreateList;
using MockTestCs.Features.CreateUser;
using MockTestCs.Features.DeleteFromList;
using MockTestCs.Features.DeleteHistory;
using MockTestCs.Features.Login;
using MockTestCs.Services.JWT;
using MockTestCs.Services.Password;

var builder = WebApplication.CreateBuilder(args);

var strConnection = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=History;Trust Server Certificate=True;Integrated Security=True";
builder.Services.AddDbContext<MockTestCsDbContext>(
    opt => opt.UseSqlServer(strConnection)
);

var app = builder.Build();


// Use cases: Scoped (Mexem com DbContext): AddHistory, AddToList, CreateUserUseCase, DeleteFromList, DeleteHistory, Login, CreateList

builder.Services.AddScoped<AddHistoryUseCase>();
builder.Services.AddScoped<AddToListUseCase>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<CreateListUseCase>();
builder.Services.AddScoped<DeleteFromListUseCase>();
builder.Services.AddScoped<DeleteHistoryUseCase>();
builder.Services.AddScoped<LoginUseCase>();

// Serviços: Singleton (mas poderiam ser transient) não armazenam estado interno entre chamadas, só recebem e transformam dados

builder.Services.AddSingleton<IPasswordServices, PBKDF2PasswordServices>();
builder.Services.AddSingleton<IJWTService, EFJWTService>();

app.Run();
 