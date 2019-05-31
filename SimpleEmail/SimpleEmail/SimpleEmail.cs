using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleEmail
{
    public class SimpleEmail : ISimpleEmail
    {
        private MailMessage Data;
        private readonly SmtpClient Sender;
        public static SmtpClient DefaultSender;

        /// <summary>
        ///  Creates a new Email instance with default settings, from a specific mailing address.
        /// </summary>
        /// <param name="emailAddress">Email address to send from</param>
        /// <param name="name">Name to send from</param>
        public SimpleEmail(string emailAddress, string name = "")
            : this(DefaultSender, emailAddress, name) { }

        /// <summary>
        ///  Creates a new Email instance using the given engines and mailing address.
        /// </summary>
        /// <param name="renderer">The template rendering engine</param>
        /// <param name="sender">The email sending implementation</param>
        /// <param name="emailAddress">Email address to send from</param>
        /// <param name="name">Name to send from</param>
        public SimpleEmail(SmtpClient sender, string emailAddress, string name = "")
        {
            Data = new MailMessage()
            {
                From = new MailAddress(emailAddress, name)
            };
            Sender = sender;
        }

        /// <summary>
        /// Creates a new Email instance and sets the from property
        /// </summary>
        /// <param name="emailAddress">Email address to send from</param>
        /// <param name="name">Name to send from</param>
        /// <returns>Instance of the Email class</returns>
        public static ISimpleEmail From(string emailAddress, string name = null)
        {
            return new SimpleEmail(DefaultSender, emailAddress, name ?? "");
        }

        /// <summary>
        /// Adds a reciepient to the email, Splits name and address on ';'
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient</param>
        /// <param name="name">Name of recipient</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail To(string emailAddress, string name = null)
        {
            if (emailAddress.Contains(";"))
            {
                //email address has semi-colon, try split
                var nameSplit = name?.Split(';') ?? new string[0];
                var addressSplit = emailAddress.Split(';');
                for (int i = 0; i < addressSplit.Length; i++)
                {
                    var currentName = string.Empty;
                    if ((nameSplit.Length - 1) >= i)
                    {
                        currentName = nameSplit[i];
                    }
                    Data.To.Add(new MailAddress(addressSplit[i].Trim(), currentName.Trim()));
                }
            }
            else
            {
                Data.To.Add(new MailAddress(emailAddress.Trim(), name.Trim()));
            }
            return this;
        }

        /// <summary>
        /// Adds a reciepient to the email
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient (allows multiple splitting on ';')</param>
        /// <returns></returns>
        public ISimpleEmail To(string emailAddress)
        {
            if (emailAddress.Contains(";"))
            {
                foreach (string address in emailAddress.Split(';'))
                {
                    Data.To.Add(new MailAddress(address));
                }
            }
            else
            {
                Data.To.Add(new MailAddress(emailAddress));
            }

            return this;
        }

        /// <summary>
        /// Adds all reciepients in list to email
        /// </summary>
        /// <param name="mailAddresses">List of recipients</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail To(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Data.To.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Adds a Carbon Copy to the email
        /// </summary>
        /// <param name="emailAddress">Email address to cc</param>
        /// <param name="name">Name to cc</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail CC(string emailAddress, string name = "")
        {
            Data.CC.Add(new MailAddress(emailAddress, name));
            return this;
        }

        /// <summary>
        /// Adds all Carbon Copy in list to an email
        /// </summary>
        /// <param name="mailAddresses">List of recipients to CC</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail CC(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Data.CC.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Adds a blind carbon copy to the email
        /// </summary>
        /// <param name="emailAddress">Email address of bcc</param>
        /// <param name="name">Name of bcc</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail BCC(string emailAddress, string name = "")
        {
            Data.Bcc.Add(new MailAddress(emailAddress, name));
            return this;
        }

        /// <summary>
        /// Adds all blind carbon copy in list to an email
        /// </summary>
        /// <param name="mailAddresses">List of recipients to BCC</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail BCC(IList<MailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
            {
                Data.Bcc.Add(address);
            }
            return this;
        }

        /// <summary>
        /// Sets the ReplyTo address on the email
        /// </summary>
        /// <param name="address">The ReplyTo Address</param>
        /// <returns></returns>
        public ISimpleEmail ReplyTo(string address)
        {
            Data.ReplyToList.Add(new MailAddress(address));

            return this;
        }

        /// <summary>
        /// Sets the ReplyTo address on the email
        /// </summary>
        /// <param name="address">The ReplyTo Address</param>
        /// <param name="name">The Display Name of the ReplyTo</param>
        /// <returns></returns>
        public ISimpleEmail ReplyTo(string address, string name)
        {
            Data.ReplyToList.Add(new MailAddress(address, name));

            return this;
        }

        /// <summary>
        /// Sets the subject of the email
        /// </summary>
        /// <param name="subject">email subject</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail Subject(string subject)
        {
            Data.Subject = subject;
            return this;
        }

        /// <summary>
        /// Adds a Body to the Email
        /// </summary>
        /// <param name="body">The content of the body</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (default)</param>
        public ISimpleEmail Body(string body, bool isHtml = false)
        {
            Data.IsBodyHtml = isHtml;
            Data.Body = body;
            return this;
        }

        /// <summary>
        /// Marks the email as High Priority
        /// </summary>
        public ISimpleEmail HighPriority()
        {
            Data.Priority = MailPriority.High;
            return this;
        }

        /// <summary>
        /// Marks the email as Low Priority
        /// </summary>
        public ISimpleEmail LowPriority()
        {
            Data.Priority = MailPriority.Low;
            return this;
        }

        /// <summary>
        /// Adds the template file to the email
        /// </summary>
        /// <param name="filename">The path to the file to load</param>
        /// <param name="model">Model for the template</param>
        /// <param name="isHtml">True if Body is HTML (default), false for plain text</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail UsingTemplateFromFile<T>(string filename, T model, bool isHtml = true)
        {
            var template = "";

            using (var reader = new StreamReader(File.OpenRead(filename)))
            {
                template = reader.ReadToEnd();
            }

            var result = Parse(template, model);
            Data.IsBodyHtml = isHtml;
            Data.Body = result;

            return this;
        }

        /// <summary>
        /// Adds razor template to the email
        /// </summary>
        /// <param name="template">The razor template</param>
        /// <param name="model">Model for the template</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail UsingTemplate<T>(string template, T model, bool isHtml = true)
        {
            var result = Parse(template, model);
            Data.IsBodyHtml = isHtml;
            Data.Body = result;

            return this;
        }

        /// <summary>
        /// Adds an Attachment to the Email
        /// </summary>
        /// <param name="attachment">The Attachment to add</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail Attach(Attachment attachment)
        {
            if (!Data.Attachments.Contains(attachment))
            {
                Data.Attachments.Add(attachment);
            }

            return this;
        }

        /// <summary>
        /// Adds Multiple Attachments to the Email
        /// </summary>
        /// <param name="attachments">The List of Attachments to add</param>
        /// <returns>Instance of the Email class</returns>
        public ISimpleEmail Attach(IList<Attachment> attachments)
        {
            foreach (var attachment in attachments.Where(attachment => !Data.Attachments.Contains(attachment)))
            {
                Data.Attachments.Add(attachment);
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="attachmentName"></param>
        /// <returns></returns>
        public ISimpleEmail AttachFromFilename(string filename, string attachmentName = null)
        {
            Attach(new Attachment(filename) { Name = attachmentName ?? filename });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public ISimpleEmail Header(string header, string body)
        {
            Data.Headers.Add(header, body);

            return this;
        }

        /// <summary>
        /// Sends email synchronously
        /// </summary>
        /// <returns>Instance of the Email class</returns>
        public void Send()
        {
            if (Sender == null)
            {
                if (DefaultSender == null)
                {
                    throw new Exception("SmtpClient object not null");
                }
                else
                {
                    DefaultSender.Send(Data);
                }
            }
            else
            {
                Sender.Send(Data);
            }

            Data.Dispose();
        }

        public async Task SendAsync()
        {
            if (Sender == null)
            {
                if (DefaultSender == null)
                {
                    throw new Exception("SmtpClient object not null");
                }
                else
                {
                    await DefaultSender.SendMailAsync(Data);
                }
            }
            else
            {
                await Sender.SendMailAsync(Data);
            }

            Data.Dispose();
        }

        private static string Parse<T>(string template, T model)
        {
            foreach (PropertyInfo pi in model.GetType().GetRuntimeProperties())
            {
                template = template.Replace($"@{pi.Name}", pi.GetValue(model)?.ToString());
            }

            return template;
        }

        private static Task<string> ParseAsync<T>(string template, T model)
        {
            return Task.FromResult(Parse(template, model));
        }

    }
}
