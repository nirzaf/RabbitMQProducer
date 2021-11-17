using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var factory = new ConnectionFactory
{
    Uri =
    new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("Demo-queue", true, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) => { 
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};


channel.BasicConsume("Demo-queue", true, consumer);
Console.ReadLine();
