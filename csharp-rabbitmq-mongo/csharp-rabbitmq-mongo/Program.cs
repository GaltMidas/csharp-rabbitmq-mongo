using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client.Events;
using System.Configuration;
using System.Collections.Specialized;

namespace csharp_rabbitmq_mongo
{
    class Program
    {
        static void Main(string[] args)
        {
            var addressqueue = ConfigurationManager.AppSettings.Get("addressqueue");
            var portqueue = ConfigurationManager.AppSettings.Get("portqueue");
            var factory = new ConnectionFactory() { HostName = addressqueue };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            var addressdb = ConfigurationManager.AppSettings.Get("addressdb");
            var portdb = ConfigurationManager.AppSettings.Get("portdb");
            var client = new MongoClient("mongodb://" + addressdb + ":" + portdb);
            var database = client.GetDatabase("foo");
        }
    }
}
