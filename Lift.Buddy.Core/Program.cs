using Lift.Buddy.Core.DB;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Text;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<DBContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("TestDatabase"));
    });

    var app = builder.Build();

    var context = app.Services.GetRequiredService<DBContext>();

    context.Database.EnsureCreated();
    context.Database.Migrate();

    app.Run();
}
catch (Exception)
{
    throw;
}

