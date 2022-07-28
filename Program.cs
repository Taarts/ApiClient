using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var responseAsString = await client.GetStringAsync("https://ghibliapi.herokuapp.com/films");

            Console.WriteLine(responseAsString);
        }
    }
}
