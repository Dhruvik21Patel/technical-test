using ProductManagement.API.Extensions;
using ProductManagement.API.Middlewares;
using ProductManagement.BusinessAccess.Utilities;
using ProductManagement.Common.Constants;
using ProductManagement.Entities.DataContext;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

var config = builder.Configuration;

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
     {
         options.SuppressModelStateInvalidFilter = true;
     });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.RegisterDatabaseConnection(builder.Configuration);

builder.Services.RegisterJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddScoped<JwtTokenHelper>();

builder.Services.ConfigureSwagger();

builder.Services.RegisterHelperService(builder.Configuration);

builder.Services.RegisterUnitOfWork();

builder.Services.RegisterServices();

builder.Services.RegisterAutoMapper();

builder.Services.SetRequestBodySize();

builder.Services.ConfigureCors();

builder.Services.AddTransient<ErrorHandlerMiddleware>();

WebApplication? app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();
});

app.UseCors(SystemConstants.CORS_POLICY);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();