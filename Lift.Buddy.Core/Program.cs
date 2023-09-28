using Lift.Buddy.Core.Database;
using Microsoft.EntityFrameworkCore;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<LiftBuddyContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("TestDatabase"));
    });

    var app = builder.Build();

    // var dataContext = app.Services.GetService<DBContext>();
    // if (dataContext == null) throw new NullReferenceException("Null datacontext");
    // dataContext.Database.Migrate();

    app.Run();
}
catch (Exception)
{
    throw;
}

