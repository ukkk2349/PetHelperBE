using PetHelper.BL.Implements;
using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Core.Repository;
using PetHelper.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBaseBL, BaseBL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IPetBL, PetBL>();
builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<ICartBL, CartBL>();
builder.Services.AddScoped<IAppointmentBL, AppointmentBL>();
builder.Services.AddScoped<IOrderBL, OrderBL>();
builder.Services.AddScoped<IBaseService, BaseService>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(otp => otp.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
