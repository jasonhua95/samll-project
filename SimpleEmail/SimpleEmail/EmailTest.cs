using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEmail
{
    /// <summary>
    /// 发送邮件测试
    /// </summary>
    public class EmailTest
    {
        static string EmailFrom = ConfigurationManager.AppSettings["from"].ToString();
        static string EmailPassword = ConfigurationManager.AppSettings["key"].ToString();
        static string EmailTo = ConfigurationManager.AppSettings["to"].ToString();

        /// <summary>
        /// 微软System.Net.Mail中的邮件发送
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SendEmailForOriginal()
        {
            MailMessage mail = new MailMessage();
            //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
            MailAddress fromAddr = new MailAddress(EmailFrom, "系统邮件");
            mail.From = fromAddr;

            //设置收件人,可添加多个,添加方法与下面的一样
            mail.To.Add(EmailTo);

            //设置邮件标题
            mail.Subject = "主题";

            //设置邮件内容
            mail.Body = "内容";

            //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是163的;
            //设置发送人的邮箱账号和密码，POP3/SMTP服务要开启, 密码要是POP3/SMTP等服务的授权码
            var smtp = new SmtpClient("smtp.163.com");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(EmailFrom, EmailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            smtp.Send(mail);
            smtp.Dispose();
            return true;
        }

        /// <summary>
        /// 封装之后的邮件发送
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SendEmail(string content)
        {
            //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的;
            //设置发送人的邮箱账号和密码，POP3/SMTP服务要开启, 密码要是POP3/SMTP等服务的授权码
            var smtp = new SmtpClient("smtp.163.com");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(EmailFrom, EmailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            SimpleEmail.DefaultSender = smtp;
            SimpleEmail.From(EmailFrom, "系统邮件").To(EmailTo).Subject("主题").Body(content).Send();

            smtp.Dispose();
            return true;
        }
    }
}