using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

/// <summary>
/// 发送信息
/// </summary>
namespace SendMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("生产者开始运行");

            var factory = new ConnectionFactory() { HostName = "localhost", Port = 32850, UserName = "test", Password = "test123" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //模拟生成者
                Random rd = new Random();
                while (true)
                {
                    string message = $"你好，{rd.Next(10)}。";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                            routingKey: "hello",
                                            basicProperties: null,
                                            body: body);

                    Console.WriteLine("发送信息：{0}", message);

                    //随机休眠
                    Thread.Sleep(TimeSpan.FromSeconds(rd.Next(5)));
                }
            }

        }
    }
}
