using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using TetrisAdvanced.Interfaces;

namespace TetrisAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            CompositionRoot.AddServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var engineService = serviceProvider.GetService<IEngineService>();

            ConsoleKey choice;

            do
            {
                engineService.Run();

                Console.WriteLine("\nAnother Round? (y/n): ");
                choice = Console.ReadKey().Key;
            }
            while (choice == ConsoleKey.Y);
        }
    }
}
