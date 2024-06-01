using Fina.Api;
using Fina.Api.Commom.Api;
using Fina.Api.Data;
using Fina.Api.Endpoint;
using Fina.Api.Handlers;
using Fina.Core.Handler;
using Fina.Core.Requests.Categories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();



var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvarioment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoint();
        

app.Run();
