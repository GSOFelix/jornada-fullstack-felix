using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Handler;
using Fina.Core.Requests.Categories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = 
    "Server=ALTOPADRAO03;Database=Fina;User ID=user_access_sql;Password=duzentos;Trusted_Connection=False;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandle,TransactionHandler>();


var app = builder.Build();

app.MapGet("/", (GetCategoryByIdRequest request,ICategoryHandler handler) => handler.GetByIdAsync(request));

app.Run();
