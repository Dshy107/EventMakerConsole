using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EventMakerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverUrl = "http://localhost:6318/";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(serverUrl);
                string urlString = "api/begivenheds";
                HttpResponseMessage response = client.GetAsync(urlString).Result;
                if (response.IsSuccessStatusCode)
                {
                    var EventList = response.Content.ReadAsAsync<List<Begivenhed>>().Result;
                    foreach (var begivenhed in EventList)
                    {
                        Console.WriteLine("Event No : " + begivenhed.Event_Id + " Name : " + begivenhed.Name + " Description : " + begivenhed.Description + " Place : " + begivenhed.Place 
                            + " Date : " + begivenhed.DateTime);
                    }
                }
                //Http Put
                var UpdateEvent = new Begivenhed()
                {
                    Event_Id = 3,
                    Name = "Sakura Fest",
                    Description = "Se sakura træerne springe ud",
                    Place = "Langelinje København",
                    DateTime = "28-04-2017"

                };
                UpdateEvent.Name = "Sakura Party";
                try
                {
                    var repsonsePut = client.PutAsJsonAsync<Begivenhed>("/api/Begivenheds/3", UpdateEvent).Result;
                    if (repsonsePut.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Du har opdateret en event");
                        Console.WriteLine("Statuskode : " + response.StatusCode);
                    }
                    else
                    {
                        Console.WriteLine("Fejl, eventen blev ikke opdateret");
                        Console.WriteLine("Statuskode : " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Der er sket en fejl : " + e.Message);
                }
                //Http Post
                var NewEvent = new Begivenhed()
                {
                    Event_Id = 4,
                    Name = "Doomsday",
                    Description = "The end times",
                    Place = "The World",
                    DateTime = "22-02-2222"
                };
                try
                {
                    var responsePost = client.PostAsJsonAsync<Begivenhed>("api/Begivenheds", NewEvent).Result;
                    if (responsePost.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Du har indsat en ny event");
                        Console.WriteLine("Post Content: " + response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        Console.WriteLine("Fejl, eventen blev ikke indsat");
                        Console.WriteLine("Statuskode : " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("Der er sket en fejl : " + e.Message);

                }
            }
            Console.ReadLine();
        }
    }
}
