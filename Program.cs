using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Game game = new();
            game.InitGame();
            //game.PlaceShipsRandomOnBoard();
            //game.PlaceShipsRandomOnBoard();
            //CreateHostBuilder(args).Build().Run();
            //Console.WriteLine("test");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

       
    }
}
