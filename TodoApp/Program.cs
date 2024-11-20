namespace TodoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();


            var app = builder.Build();


            //app.MapGet("/bdu", () => "12312312312");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Show}/{id?}");

            app.Run();
        }
    }
}
