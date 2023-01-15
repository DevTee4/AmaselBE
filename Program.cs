using AmaselBE.Configuration;
using AmaselBE.Services;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddControllers ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

builder.Configuration
    .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
    .AddJsonFile ($"appsettings.{builder.Environment.EnvironmentName}.json", optional : true);
var setting = builder.Configuration.GetSection ("Setting").Get<Setting> ();

builder.Services.AddControllers ().AddJsonOptions (x => x.JsonSerializerOptions.Converters.Add (new System.Text.Json.Serialization.JsonStringEnumConverter ()));
builder.Services.AddScoped<Setting> ((s) => {
    return setting;
});
builder.Services.AddScoped<AttachmentService> ();
builder.Services.AddScoped<AuditTrailService> ();
builder.Services.AddScoped<AuthUserService> ();
builder.Services.AddScoped<ChatService> ();
builder.Services.AddScoped<PlatformService> ();
builder.Services.AddScoped<UserService> ();
builder.Services.AddScoped<AdvertisementRequestService> ();
builder.Services.AddScoped<AdvertisementService> ();
builder.Services.AddScoped<FeedbackService> ();
builder.Services.AddScoped<GiftCardService> ();
builder.Services.AddScoped<MessageService> ();
builder.Services.AddScoped<PromoRequestService> ();
builder.Services.AddScoped<RatingService> ();
builder.Services.AddScoped<SellerManagementService> ();

builder.Services.AddControllersWithViews ()
    // Maintain property names during serialization. See:
    .AddJsonOptions (options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = null);
var app = builder.Build ();
app.UseCors (builder => builder.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ()) {
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseHttpsRedirection ();

app.UseAuthorization ();

app.MapControllers ();

app.Run ();