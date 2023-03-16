using MartianNavigator.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MartianNavigator
{
    public class Program
    {
        static void Main()
        {
            try
            {
                var services = ConfigureServices();
                var serviceProvider = services.BuildServiceProvider();

                serviceProvider.GetService<ConsoleApplication>().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex is FormatException || ex is ArgumentOutOfRangeException || ex is ArgumentNullException
                    ? ex.Message
                    : "Something went wrong.");

                return;
            }
            
            Console.ReadKey();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IValidationService, ValidationService>();
            services.AddSingleton<INavigationService, NavigationService>();
            
            services.AddSingleton<ConsoleApplication>();

            return services;
        }
    }
}
