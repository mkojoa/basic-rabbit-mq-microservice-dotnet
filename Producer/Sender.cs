using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            //get configuration info
            var factory = new ConnectionFactory() { HostName = "localhost" };
            
            //connect to the server.
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) //open the channel and send message
            {
                //declare a queue
                channel.QueueDeclare("BasicTest", false, false, false, null);

                //set the message
                string message = "This is a basic test send from the producer";

                //encode the message before you send
                var body = Encoding.UTF8.GetBytes(message);

                //publish the message using routing  key
                channel.BasicPublish("", "BasicTest", null, body);

                Console.WriteLine("Sent message is {0}", message);
            }

            Console.WriteLine("Press [enter] to exist the sender App");
            Console.ReadLine();
        }
    }
}
