using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome Martian !!");
            Console.WriteLine();
            Console.WriteLine("Please enter your top-right coordinates of turf.");

            var createCanvasSuccess = false;
            Canvas canvas = new Canvas();
            do
            {
                try
                {
                    string canvasXY = Console.ReadLine();
                    canvas = new Canvas(canvasXY);
                    createCanvasSuccess = true;
                }
                catch (MyCustomException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again to enter your top-right coordinates of turf.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("!! Exception !!");
                }
            }
            while (!createCanvasSuccess);

            Console.WriteLine();
            Console.WriteLine("Time to deploy Rovers.");
            Console.WriteLine("----------------------");


            string choice;
            do
            {
                DeployRover(canvas);

                Console.WriteLine();
                Console.WriteLine("Press Enter to deploy another rover or 'Q' to quit");

                choice = Console.ReadLine();
            }
            while (choice != "Q" && choice != "q");
        }

        private static void DeployRover(Canvas canvas)
        {
            var rover = new Rover(canvas);

            var roverDropSuccess = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter Rover's drop off point.");

                try
                {
                    string dropPosition = Console.ReadLine();
                    rover.DropAt(dropPosition);
                    roverDropSuccess = true;
                }
                catch (MyCustomException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again to drop your Rover.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("!! Exception !!");
                }
            }
            while (!roverDropSuccess);



            Console.WriteLine();
            Console.WriteLine("Enter Rover's path to explore.");

            var roverExploreSuccess = false;
            do
            {
                try
                {
                    string explorationPath = Console.ReadLine();
                    rover.Explore(explorationPath);
                    Console.WriteLine();
                    Console.WriteLine(string.Format("Rover Position - {0}", rover.Position.ToString()));

                    roverExploreSuccess = true;
                }
                catch (OutOfBoundException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ooops !! Rover went too far from the Canvas. It is on the ledge");
                    Console.WriteLine(string.Format("Rover Position - {0}", rover.Position.ToString()));
                    roverExploreSuccess = true;
                }
                catch (MyCustomException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Rover couldn't explore. Try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("!! Exception !!");
                }
            }
            while (!roverExploreSuccess);
        }
    }
}
