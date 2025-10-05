namespace StudentFiles.Api
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using StudentFiles.Api.Configuration;
    using StudentFiles.Api.Services;
    using StudentFiles.Application;
    using StudentFiles.Contracts;
    using StudentFiles.Infrastructure.Data.Repositories.Course;
    using StudentFiles.Infrastructure.Data.Repositories.Grade;
    using StudentFiles.Infrastructure.Data.Repositories.User;
    using StudentFiles.Infrastructure.Database.Context;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? dbConnectionString = builder.Configuration.GetConnectionString(name: "DefaultConnection");

            builder.Services.AddDbContext<StudentFilesReadonlyDbContext>(optionsAction: options => options.UseSqlServer(connectionString: dbConnectionString)
                                                                                                          .UseQueryTrackingBehavior(queryTrackingBehavior: QueryTrackingBehavior.NoTracking));
            builder.Services.AddScoped<IStudentFilesReadonlyDbContext, StudentFilesReadonlyDbContext>();

            builder.Services.AddDbContext<StudentFilesDbContext>(optionsAction: options => options.UseSqlServer(connectionString: dbConnectionString));
            builder.Services.AddScoped<IStudentFilesDbContext, StudentFilesDbContext>();

            builder.Services.Configure<JwtSettings>(config: builder.Configuration.GetSection(key: "JwtSettings"));

            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddMediatR(configuration: cfg =>
            {
                cfg.RegisterGenericHandlers = true;
                cfg.RegisterServicesFromAssemblies(typeof(ContractsAssemblyMarker).Assembly, typeof(ApplicationAssemblyMarker).Assembly);
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "StudentFilesClient", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                                };
                            });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("StudentFilesClient");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
