using Newtonsoft.Json.Linq;
using System;
using System.Text;
using RabbitMQ.Client;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using SystemNetHttpJsonSamples;

namespace RabbitMQConsumer
{
    public class MessageReceiver : DefaultBasicConsumer

    {

        private readonly IModel _channel;
        private static byte[] data = new byte[100];

        private static readonly HttpClient httpClient = new HttpClient();

        public static object URL { get; private set; }

        public MessageReceiver(IModel channel)

        {

            _channel = channel;

        }

        public override async void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)

        {

            Console.WriteLine($"Consuming Message");

            Console.WriteLine(string.Concat("Message received from the exchange ", exchange));

            Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));

            Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));

            Console.WriteLine(string.Concat("Routing tag: ", routingKey));

            Console.WriteLine(string.Concat("Message: ", Encoding.UTF8.GetString(body.ToArray())));

            await readintext(body.ToArray());

            _channel.BasicAck(deliveryTag, false);

        }

        public static async Task readintext(byte[] data)
        {
            string input = Encoding.UTF8.GetString(data, 0, data.Length);
            string text = System.IO.File.ReadAllText(input);
            await CreateJSON(text, httpClient);
            await PostJsonContent("https://ene4n1texfj7m85.m.pipedream.net", httpClient);
        }

        private static async Task CreateJSON(String name, HttpClient httpClient1)
        {
            
            string read_in_line;
            JObject Jobject = new JObject();
            System.IO.StreamReader file = new System.IO.StreamReader("BoxingExportBox#.58189J01.1.csv");

            //String to read in category names
            String[] words = new String[11];

            if ((read_in_line = file.ReadLine()) != null)
            {
                words = read_in_line.Split(',');
                Console.WriteLine(words[11]);
            }

            int i;
            int index = 0;
            while ((read_in_line = file.ReadLine()) != null)
            {
                String[] input = new String[11];
                input = read_in_line.Split(",");

                for (i = 0; i < words.Length; i++)
                {
                    Jobject.Add(words[i] + index.ToString(), input[i]);
                    index++;
                }
            }
           
            await File.WriteAllTextAsync("file.json", Jobject.ToString());
            file.Close();

        }
        private static async Task PostJsonContent(string uri, HttpClient httpClient)
        {

            System.IO.StreamReader file = new System.IO.StreamReader("file.json");
            String text = file.ReadToEnd();

            var postUser = new User { input = text };
            var postRequest = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create(postUser)
            };
            
            var postResponse = await httpClient.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
