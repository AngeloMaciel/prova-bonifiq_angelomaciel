using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;
using ProvaPub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*AddSingleton: adota o padr�o singleton que faz com que a classe seja instanciada uma �nica vez
 * para todo o ciclo de vida da aplica��o. 
AddTransient: faz com que o servi�o seja transiente, sendo a classe novamente instanciada a cada vez que for utilizada. */
builder.Services.AddTransient<RandomService>();
builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

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

app.Run();
