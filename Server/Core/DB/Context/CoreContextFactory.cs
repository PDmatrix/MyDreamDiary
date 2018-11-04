using System;
using DB.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace DB.Context
{
    public class CoreContextFactory : ICoreContextFactory
    {
	    private static readonly LoggerFactory LoggerFactory
		    = new LoggerFactory(new[] {new ConsoleLoggerProvider((category, level) =>
			    category == DbLoggerCategory.Database.Command.Name
				    && level == LogLevel.Information
		    , true)});
	    
        public CoreContext CreateDbContext(string connectionString)
        {
	        var isDevelopment = string.Equals(
		        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Development", 
		        StringComparison.OrdinalIgnoreCase);
	        
            var optionsBuilder = new DbContextOptionsBuilder<CoreContext>();

            optionsBuilder.UseNpgsql(connectionString);
	        
	        if(isDevelopment)
				optionsBuilder.UseLoggerFactory(LoggerFactory);
	        
            return new CoreContext(optionsBuilder.Options);
        }
    }
}