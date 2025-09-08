using Microsoft.EntityFrameworkCore;
using MockTestCs.Entities;

var builder = WebApplication.CreateBuilder(args);

var strConnection = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=History;Trust Server Certificate=True;Integrated Security=True";
builder.Services.AddDbContext<MockTestCsDbContext>(
    opt => opt.UseSqlServer(strConnection)
);

var app = builder.Build();


app.Run();
 