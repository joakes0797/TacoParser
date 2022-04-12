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
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command:
            var locations = lines.Select(parser.Parse).ToArray();
            //for each line in lines, parse it, array it, store it in locations

            // DON'T FORGET TO LOG YOUR STEPS - don't worry about this says Jeremy

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to
            //      store your two taco bells that are the farthest from each other.        --done
            // Create a `double` variable to store the distance     --done
            ITrackable firstBell = null;
            ITrackable secondBell = null;
            double distance = 0;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`  --already done

            // NESTED LOOPS SECTION
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)    --done
            // Create a new corA Coordinate with your locA's lat and long                   --done

            // Now, do another loop on the locations within the scope of your first loop, so you can grab the
            //      "destination" location (perhaps: `locB`)                                --done
            // Create a new Coordinate with your locB's lat and long                        --done

            for (int x = 0; x < locations.Length; x++)
            {
                var locA = locations[x];
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (int y = 0; y < locations.Length; y++)
                {
                    var locB = locations[y];
                    var corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        firstBell = locA;
                        secondBell = locB;
                    }
                }
            }

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and
            // the two `ITrackable` variables you set above                     --done

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.  --done
            Console.WriteLine($"The first Taco Bell is {firstBell.Name}.");
            Console.WriteLine($"The second Taco Bell is {secondBell.Name}.");
            Console.WriteLine($"The distance between these two restaurants is: {distance}");            
        }
    }
}
