using ControleDeContatos.Data;
using ControleDeContatos.Repository;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			/// <summary>
			/// Realiza a conexão com o banco de dados Postgresql 
			/// através do ORM EntityFramework pelo método UseNpgsql passando a connection string em appsettings
			/// </summary>
			builder.Services.AddDbContext<BancoContext>(options =>
			{
				options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

            /// <summary>
			/// Dependecies Injection
            /// toda vez que a interface IContatoRepository for chamada, resolva a classe implementada ContatoRepository
            /// </summary>
            builder.Services.AddScoped<IContatoRepository, ContatoRepository>(); 
		
			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}