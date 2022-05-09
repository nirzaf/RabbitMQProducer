using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using static System.Console;

//WriteLine("Message Producer Running");

ConnectionFactory factory = new()
{ 
    Uri= new Uri("amqp://guest:guest@localhost:5672") 
};

using IConnection? connection = factory.CreateConnection();
using IModel? channel = connection.CreateModel();
channel.QueueDeclare("my-rabbit-queue", true, false, false, null);


for(int i=0; i<1000;i++)
{
    var message = new
    {
        name = Faker.Name.FullName() , message = Faker.Internet.DomainName(), count = i.ToString()
    };
    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
    WriteLine(message);
    channel.BasicPublish("", "my-rabbit-queue", null, body);
    Thread.Sleep(50);
}






