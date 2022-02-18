using EntityFrameworkExample;
using EntityFrameworkExample.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServer => sqlServer.UseNetTopologySuite());
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //options.UseModel(ApplicationDbContextModel.Instance);
    //options.UseLazyLoadingProxies();
});

builder.Services.AddScoped<IUpdaterObservableCollection, UpdaterObservableCollection>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventsDbContext, EventsDbContext>();
builder.Services.AddSingleton<Singleton>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
