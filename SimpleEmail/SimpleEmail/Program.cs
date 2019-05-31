using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始运行");
            EmailTest.SendEmailForOriginal();
            Console.WriteLine("邮件发送完成");
            Console.Read();
        }
    }
}
