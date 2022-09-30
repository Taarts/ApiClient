using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

namespace ApiClient
{
 class Program    
 {
     static void DisplayGreeting()
      {
        Console.WriteLine();
        Console.WriteLine("Welcome to Ghibli API interface");
        Console.WriteLine();
        }
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Invalid input,0 your answer shall be.");
                return 0;
            }
        }
        
        static async Task Main(string[] args)
        {
        var keepGoing  = true;
            DisplayGreeting();
            while (keepGoing)
            {
                Console.WriteLine();
                Console.WriteLine("Choose from the menu");
                Console.WriteLine("Show (A)ll movies");
                Console.WriteLine("Show Movie & (Y)ear");
                Console.WriteLine("Show the Movie & it's (R)otten Tomatoes Score");
                Console.WriteLine("(Q)uit to exit");
                var choice = Console.ReadLine().ToUpper();

            var url = $"https://ghibliapi.herokuapp.com/films";

            switch (choice)
            {
            case "A":
                await AllMovies();
                break;
            case "Y":
                await MovieReleaseDate();
                break;
            case "R":
                await RottenTomatoes();
                break;
            case "Q":
                Console.WriteLine($"Goodbye");
                keepGoing = false;
                break;
            default:
                Console.WriteLine($"Invalid selection, Try again.");
                break;
            }
        }
        static async Task MovieReleaseDate(string url)
        {
            var client = new  HttpClient();
            var responseAsStream = await client.GetStreamAsync(url);
            var movies = await JsonSerializer.DeserializeAsync<List<movies>>(responseAsStream);
            var years = movies.Where(movie => movies.ReleaseDate);

            var table = new ConsoleTable("Title", "ReleaseDate");
            foreach (var movie in years)
            {
                table.AddRow(movie.Title, movie.ReleaseDate);
            }
            table.Write();
            
        }
        static async Task AllMovies()
        {
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://ghibliapi.herokuapp.com/films/");
            var movies = await JsonSerializer.DeserializeAsync<List<movies>>(responseAsStream);

            // Console.WriteLine(responseAsString);
            
            var table = new ConsoleTable("Title", "Director", "release date", "Rotten Tomato Score" );

            foreach (var movie in movies)
            {
                // Console.WriteLine($"The movie {item.Title} was directed by {item.Director} and released in {item.ReleaseDate}.");
                
                table.AddRow(
                    movie.Title, 
                    movie.Director, 
                    movie.ReleaseDate, 
                    movie.RottenTomatoScore 
                );
                table.Write();
               
            }
        }
    }
}

