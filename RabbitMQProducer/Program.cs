// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory { Uri= 
    new Uri("amqp://guest:guest@localhost:5672") 
    };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("Demo Queue", true, false, false, null);
var message = new { name = "Producer", message = "Hello" };
var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

channel.BasicPublish("", "Demo-queue", null, body);




