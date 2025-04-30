using Kuronai.Api.Extensions;
using Kuronai.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFirebase(options => builder.Configuration.Bind("Firebase", options))
    .AddDepedencies();

if (builder.Environment.IsDevelopment())
    builder.Services.AddEntityFrameworkDev();
else
    builder.Services.AddEntityFramework();

    builder.Services.AddCors(setup =>
    {
        setup.AddPolicy("localhost", policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(5);
        options.Cookie.HttpOnly = true;
        options.LoginPath = string.Empty;
        options.LogoutPath = string.Empty;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<FirebaseUserIdMiddleware>();

app.MapControllers();

app.UseCors("localhost");

app.Run();
