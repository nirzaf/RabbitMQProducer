using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory
{
    Uri =
    new Uri("amqp://guest:guest@localhost:15672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("Demo-Queue", true, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) => { 
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};




//var message = new { name = "Producer", message = "Hello" };
//var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

channel.BasicConsume("Demo-queue", true, consumer);
