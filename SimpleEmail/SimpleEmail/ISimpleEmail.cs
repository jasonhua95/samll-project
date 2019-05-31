using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SimpleEmail
{
    /// <summary>
    /// 仿照FluentEmail中的功能
    /// 地址：https://github.com/lukencode/FluentEmail
    /// 
    /// </summary>
    public interface ISimpleEmail
    {
        /// <summary>
        /// Adds a reciepient to the email, Splits name and MailAddress on ';'
        /// </summary>
        /// <param name="emailMailAddress">Email MailAddress of recipeient</param>
        /// <param name="name">Name of recipient</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail To(string emailMailAddress, string name = null);

        /// <summary>
        /// Adds a reciepient to the email
        /// </summary>
        /// <param name="emailMailAddress">Email MailAddress of recipeient (allows multiple splitting on ';')</param>
        /// <returns></returns>
        ISimpleEmail To(string emailMailAddress);

        /// <summary>
        /// Adds all reciepients in list to email
        /// </summary>
        /// <param name="mailMailAddresses">List of recipients</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail To(IList<MailAddress> mailMailAddresses);

        /// <summary>
        /// Adds a Carbon Copy to the email
        /// </summary>
        /// <param name="emailMailAddress">Email MailAddress to cc</param>
        /// <param name="name">Name to cc</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail CC(string emailMailAddress, string name = "");

        /// <summary>
        /// Adds all Carbon Copy in list to an email
        /// </summary>
        /// <param name="mailMailAddresses">List of recipients to CC</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail CC(IList<MailAddress> mailMailAddresses);

        /// <summary>
        /// Adds a blind carbon copy to the email
        /// </summary>
        /// <param name="emailMailAddress">Email MailAddress of bcc</param>
        /// <param name="name">Name of bcc</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail BCC(string emailMailAddress, string name = "");

        /// <summary>
        /// Adds all blind carbon copy in list to an email
        /// </summary>
        /// <param name="mailMailAddresses">List of recipients to BCC</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail BCC(IList<MailAddress> mailMailAddresses);

        /// <summary>
        /// Sets the ReplyTo MailAddress on the email
        /// </summary>
        /// <param name="mailAddress">The ReplyTo MailAddress</param>
        /// <returns></returns>
        ISimpleEmail ReplyTo(string mailAddress);

        /// <summary>
        /// Sets the ReplyTo MailAddress on the email
        /// </summary>
        /// <param name="mailAddress">The ReplyTo MailAddress</param>
        /// <param name="name">The Display Name of the ReplyTo</param>
        /// <returns></returns>
        ISimpleEmail ReplyTo(string mailAddress, string name);

        /// <summary>
        /// Sets the subject of the email
        /// </summary>
        /// <param name="subject">email subject</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail Subject(string subject);

        /// <summary>
        /// Adds a Body to the Email
        /// </summary>
        /// <param name="body">The content of the body</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
        ISimpleEmail Body(string body, bool isHtml = false);

        /// <summary>
        /// Marks the email as High Priority
        /// </summary>
        ISimpleEmail HighPriority();

        /// <summary>
        /// Marks the email as Low Priority
        /// </summary>
        ISimpleEmail LowPriority();

        /// <summary>
        /// Adds the template file to the email
        /// </summary>
        /// <param name="filename">The path to the file to load</param>
        /// <param name="model">Model for the template</param>
        /// <param name="isHtml">True if Body is HTML (default), false for plain text</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail UsingTemplateFromFile<T>(string filename, T model, bool isHtml = true);

        /// <summary>
        /// Adds razor template to the email
        /// </summary>
        /// <param name="template">The razor template</param>
        /// <param name="model">Model for the template</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail UsingTemplate<T>(string template, T model, bool isHtml = true);

        /// <summary>
        /// Adds an Attachment to the Email
        /// </summary>
        /// <param name="attachment">The Attachment to add</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail Attach(Attachment attachment);

        /// <summary>
        /// Adds Multiple Attachments to the Email
        /// </summary>
        /// <param name="attachments">The List of Attachments to add</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail Attach(IList<Attachment> attachments);

        ISimpleEmail AttachFromFilename(string filename, string attachmentName = null);

        /// <summary>
        /// Adds header to the Email.
        /// </summary>
        /// <param name="header">Header name, only printable ASCII allowed.</param>
        /// <param name="body">value of the header</param>
        /// <returns>Instance of the Email class</returns>
        ISimpleEmail Header(string header, string body);

        /// <summary>
        /// Sends email synchronously
        /// </summary>
        /// <returns>Instance of the Email class</returns>
        void Send();

        /// <summary>
        /// Sends email asynchronously
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
	    Task SendAsync();
    }
}
