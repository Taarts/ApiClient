using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Linq;
using ConsoleTables;

namespace ApiClient
{
    class Program
    {
        class Film
        {
            [JsonPropertyName("id")]
            public string id { get; set; }

            [JsonPropertyName("title")]
            public string filmTitle { get; set; }

            [JsonPropertyName("original_title")]
            public string japaneseKanji { get; set; }

            [JsonPropertyName("original_title_romanised")]
            public string phoneticTitle { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("director")]
            public string director { get; set; }

            [JsonPropertyName("producer")]
            public string producer { get; set; }

            [JsonPropertyName("release_date")]
            public string yearReleased { get; set; }

            [JsonPropertyName("running_time")]
            public string RunningTimeInMinutes { get; set; }

            [JsonPropertyName("rt_score")]
            public string rottenTomatoScore { get; set; }
        }
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://ghibliapi.herokuapp.com/films");

            var films = await JsonSerializer.DeserializeAsync<List<Film>>(responseAsStream);
            var table = new ConsoleTable("Title", "Kanji");

            foreach (var film in films)
            {
                table.AddRow(film.filmTitle, film.japaneseKanji);
            }

            table.Write();
            var keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine();
                Console.WriteLine($"Would you like to find out more about your favorite Studio Ghibli film? (y/n)");

                Console.WriteLine();
                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "Y":
                        NewMethod(films);
                        break;
                    case "N":
                        Console.WriteLine("Thanks! Goodbye!");
                        keepGoing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        private static void NewMethod(List<Film> films)
        {
            Console.WriteLine($"Pick a number between 1 - {films.Count} to find out more about your favorite Studio Ghibli film.");
            var filmIndex = Int32.Parse(Console.ReadLine()) - 1;
            var selectedFilm = films[filmIndex];
            Console.WriteLine($"The film {selectedFilm.filmTitle}, {selectedFilm.japaneseKanji}, pronounced {selectedFilm.phoneticTitle} was released in {selectedFilm.yearReleased}. ");
            Console.WriteLine($"{selectedFilm.filmTitle} is about {selectedFilm.Description}.");
            Console.WriteLine();
            Console.WriteLine($"Directed by {selectedFilm.director} and produce by {selectedFilm.producer}");
        }
    }
}