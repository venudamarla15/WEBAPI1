using Microsoft.AspNetCore.HttpOverrides;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.Use(async( context, next)=>{
    Console.WriteLine($"Logic befor the nwxt delegate in the Use Method");
    await next.Invoke(context);
    Console.WriteLine($"Logic After the nwxt delegate in the Use Method");
});

app.Map("/usingmapbranch", builder =>
{
    app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), builder =>
    {
        builder.Run(async context =>
        {
            await context.Response.WriteAsync("hello from mapwhen branch");
        });
    });

    app.Run(async context =>
    {

    });
    builder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        Console.WriteLine("Map branch logic in the Use method before the nextdelegate");
        await next.Invoke(context);
        Console.WriteLine("Map branch logic in the Use method after the nextdelegate");

    });
    builder.Run(async context =>
    {
        Console.WriteLine($"Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the map branch.");

    });
});

app.Run(async context =>
{
    Console.WriteLine($"Writing the response to the client in the Run method");
    await context.Response.WriteAsync("Hello from the middleware");
});

app.MapControllers();

app.Run();
