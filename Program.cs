using AmaselBE.Services;
using Microsoft.AspNetCore.Http.Json;
using VendolaCore;
using VendolaCore.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
var setting = builder.Configuration.GetSection("Setting").Get<Setting>();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddScoped<Setting>((s) =>
{
    return setting;
});
builder.Services.AddTransient<AttachmentService>();
builder.Services.AddTransient<PlatformService>();
builder.Services.AddTransient<AdvertisementRequestService>();
builder.Services.AddTransient<AdvertisementService>();
builder.Services.AddTransient<FeedbackService>();
builder.Services.AddTransient<GiftCardService>();
builder.Services.AddTransient<PromoRequestService>();
builder.Services.AddTransient<PromoService>();
builder.Services.AddTransient<RatingService>();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddHttpContextAccessor();
User user = new User();
builder.Services.AddTransient<User>((s) =>
{
    return user;
});
var app = builder.Build();
setting.IsProduction = app.Environment.IsDevelopment() ? false : true;
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//validate
app.Use(async (httpContext, next) =>
{
    var token = httpContext.Request.Headers["Authorization"].Count > 0 ? httpContext.Request.Headers["Authorization"][0] : "";

    using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole());

    ILogger logger = loggerFactory.CreateLogger<Program>();
    var u = new VendolaCore.VendolaCore().GetUserFromJWT(token);
    if (httpContext.Request.Path == "/" || u != null || (token == VendolaCore.VendolaCore.DefaultToken))
    {
        if (token == VendolaCore.VendolaCore.DefaultToken)
        {
            user.Tenant = VendolaCore.VendolaCore.DefaultTenant;
        }
        logger.LogInformation(user.Tenant);
        logger.LogInformation(token);

        if (u != null)
        {
            user.SetUserFromJWT(u);
        }

        await next();
    }

});
PlatformService.SeedDB(setting, null);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();