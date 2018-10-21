using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Core
{
    public static class Init
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder<Startup>(args);
    }
}