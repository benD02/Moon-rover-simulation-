using robot_controller_api.Persistence;
using Microsoft.Extensions.Logging;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// 3.1P:
// builder.Services.AddScoped<IRobotCommandDataAccess, RobotCommandADO>();
// builder.Services.AddScoped<IMapDataAccess, MapADO>();
// 3.2C:
// builder.Services.AddScoped<IRobotCommandDataAccess,
//RobotCommandRepository > ();
// builder.Services.AddScoped<IMapDataAccess, MapRepository>();
// 3.3D:
builder.Services.AddScoped<IRobotCommandDataAccess, RobotCommandEF>();
builder.Services.AddScoped<IMapDataAccess, MapEF>();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
