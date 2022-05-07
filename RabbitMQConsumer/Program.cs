using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using static System.Console;


ConnectionFactory factory = new ConnectionFactory
{
    Uri =
    new Uri("amqp://guest:guest@localhost:5672")
};

using IConnection? connection = factory.CreateConnection();
using IModel? channel = connection.CreateModel();
channel.QueueDeclare("Demo-queue", true, false, false, null);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) => { 
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    WriteLine(message);
    WriteLine(sender);
};


channel.BasicConsume("Demo-queue", true, consumer);
ReadLine();
