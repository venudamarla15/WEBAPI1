using Microsoft.AspNetCore.HttpOverrides;
using WebAPI.Extensions;
using NLog;
using Contracts;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters().
AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

//builder.Services.AddControllers().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthorization();


//app.Use(async( context, next)=>{
//    Console.WriteLine($"Logic befor the nwxt delegate in the Use Method");
//    await next.Invoke(context);
//    Console.WriteLine($"Logic After the nwxt delegate in the Use Method");
//});

//app.Map("/usingmapbranch", builder =>
//{
//    app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), builder =>
//    {
//        builder.Run(async context =>
//        {
//            await context.Response.WriteAsync("hello from mapwhen branch");
//        });
//    });

    //app.Run(async context =>
    //{

    //});
//    builder.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        Console.WriteLine("Map branch logic in the Use method before the nextdelegate");
//        await next.Invoke(context);
//        Console.WriteLine("Map branch logic in the Use method after the nextdelegate");

//    });
//    builder.Run(async context =>
//    {
//        Console.WriteLine($"Map branch response to the client in the Run method");
//        await context.Response.WriteAsync("Hello from the map branch.");

//    });
//});

//app.Run(async context =>
//{
//    Console.WriteLine($"Writing the response to the client in the Run method");
//    await context.Response.WriteAsync("Hello from the middleware");
//});

app.MapControllers();

app.Run();
