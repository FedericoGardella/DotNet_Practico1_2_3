using DAL;
using DAL.IDALs;

try
{

    var builder = WebApplication.CreateBuilder(args);

    //For Entity Framework

    builder.Services.AddDbContext<DBContext>();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    #region

    builder.Services.AddTransient<IDAL_Personas, DAL_Personas_EF>();

    #endregion

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

    DBContext.UpdateDatabase();

    app.Run();

}
catch ( Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}


