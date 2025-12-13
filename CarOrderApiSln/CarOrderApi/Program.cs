using CarOrderApi.Data;
using CarOrderApi.ExtentionProgramee_cs;
using CarOrderApi.Repositories;
using CarOrderApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ApplicationService(builder.Configuration);

builder.Services.AddControllers();

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
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();
app.MapControllers();

app.UseCors(op=>op.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:7202"));

app.Run();
