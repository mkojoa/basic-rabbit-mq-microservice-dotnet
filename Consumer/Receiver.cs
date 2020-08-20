using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //declare the queue you want to consume
                channel.QueueDeclare("BasicTest", false, false, false, null);

                //create a consumer object
                var consumer = new EventingBasicConsumer(channel);

                //listin to event
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Received messsage is : {0}", message);
                };

                channel.BasicConsume("BasicTest", true, consumer);

                Console.WriteLine("Press [Enter] to exist the consumer App");
                Console.ReadLine();
            }
        }
    }
}
