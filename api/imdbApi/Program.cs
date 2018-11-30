using ImdbDataRefresher;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace imdbApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataRefresher = new DataLoader ();
            dataRefresher.LoadImdbBasicsData ();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
