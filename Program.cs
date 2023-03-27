using AmaselBE.Services;
using VendolaCore;

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
builder.Services.AddTransient<RatingService>();

builder.Services.AddControllersWithViews()
    // Maintain property names during serialization. See:
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
setting.ApplicationMode = app.Environment.IsDevelopment() ? "Developement" : "Production";
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
PlatformService.SeedDB(setting, null);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();