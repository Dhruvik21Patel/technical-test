namespace ProductManagement.API.Extensions
{
    using ProductManagement.BusinessLayer.IService;
    using ProductManagement.BusinessLayer.Profiles;
    using ProductManagement.Common.Constants;
    using ProductManagement.Common.Utils;
    using ProductManagement.DataAccess.HelperService;
    using ProductManagement.DataAccess.IRepository;
    using ProductManagement.DataAccess.Repository;
    using ProductManagement.Entities.DataContext;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class ApplicationBuilderConfiguration
    {
        public static void RegisterUnitOfWork(this IServiceCollection services) => services.AddScoped<IUnitOfWork, UnitOfWork>();

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void SetRequestBodySize(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<UserResolverService>();

            IEnumerable<Type> implementationTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseService<>)));

            foreach (Type implementationType in implementationTypes)
            {
                Type[] implementedInterfaces = implementationType.GetInterfaces();
                foreach (Type implementedInterface in implementedInterfaces)
                {
                    if (implementedInterface.IsGenericType)
                    {
                        Type openGenericInterface = implementedInterface.GetGenericTypeDefinition();
                        if (openGenericInterface == typeof(IBaseService<>))
                        {
                            services.AddScoped(implementedInterface, implementationType);
                        }
                    }
                }
            }

            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(IBaseService<>))
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddHttpContextAccessor();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy(SystemConstants.CORS_POLICY,
                        builder =>
                        {
                            builder
                                .WithOrigins("http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        });
                });
        }
        public static void RegisterHelperService(this IServiceCollection services, IConfiguration config)
        {
            JwtSetting jwtSetting = config.GetSection("JwtSetting").Get<JwtSetting>();
            services.AddSingleton(jwtSetting);
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PSM v1",
                    Version = "v1"
                });

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
            }});
            });
        }

        public static void RegisterJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSetting");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new ArgumentNullException("JwtSetting:Key"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtSettings["Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
                            if (!string.IsNullOrWhiteSpace(token))
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new
                            {
                                status = 401,
                                message = "Unauthorized: Token is missing or invalid."
                            });
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new
                            {
                                status = 403,
                                message = "Forbidden: You do not have permission to access this resource."
                            });
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
        }
    }
}
