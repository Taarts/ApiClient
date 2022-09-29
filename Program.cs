using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

namespace ApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://ghibliapi.herokuapp.com/films/");
            var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseAsStream);

            // Console.WriteLine(responseAsString);
            
            
            // var table = new ConsoleTable("title", "director", "release date" );

            foreach (var item in items)
            {
                Console.WriteLine($"The movie {item.title} was directed by {item.director} and released in {item.release_date}.");
                
            //     table.AddRow(
            //         item.title,
            //         item.director,
            //         item.release_date
            //     );
            //     table.Write();
            //     {
                    
                }
            }
        }
}

