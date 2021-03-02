using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            

            logger.LogInfo("Log initialized");

            
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            
            var parser = new TacoParser();

            
            var locations = lines.Select(parser.Parse).ToArray();

            
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            double distance = 0;


            for (int i=0; i <locations.Length; i++)
            {
                var locA = locations[i];
               
                var corA = new GeoCoordinate();
                
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (int j=0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    if (corA.GetDistanceTo(corB)> distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }

               

            }
            Console.WriteLine();
            Console.WriteLine($"The furthest TacoBells are {tacoBell1.Name} and {tacoBell2.Name}");
            Console.WriteLine();
            Console.WriteLine($"They are {Math.Round(distance * 0.000621371192, 4)} Miles from each other.");



            



        }
    }
}
