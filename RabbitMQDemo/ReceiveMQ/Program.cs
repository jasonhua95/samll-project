using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

/// <summary>
/// 接收信息
/// </summary>
namespace ReceiveMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("销售者开始运行");

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 32850, UserName = "test", Password = "test123" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //模拟接受者
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("接收信息：{0}", message);
                };

                while (true) {

                    channel.BasicConsume(queue: "hello",
                                         autoAck: true,
                                         consumer: consumer);

                    Thread.Sleep(TimeSpan.FromSeconds(10));
                    Console.WriteLine("每隔10秒接收一次");
                }
            }

        }
    }
}
