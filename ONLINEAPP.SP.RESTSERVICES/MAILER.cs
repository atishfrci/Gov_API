using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace ONLINEAPP.SP.RESTSERVICES
{
    public static class MAILER
    {
        static string SMTPSERVER = Convert.ToString(WebConfigurationManager.AppSettings["SMTPSERVER"]);
        static int SMTPPort = Convert.ToInt32(Convert.ToString(WebConfigurationManager.AppSettings["SMTPPort"]));

        static string FromAddress = Convert.ToString(WebConfigurationManager.AppSettings["FromAddress"]);
        static string FromAddressPassword = Convert.ToString(WebConfigurationManager.AppSettings["FromAddressPassword"]);

        static string BccMailIDs = Convert.ToString(WebConfigurationManager.AppSettings["BccMailIDs"]);

        #region << Overloaded SendMail Method >>

        /// <summary>
        /// Send Mail with attachment. Pass attachment as full path with filename
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static bool SendEmail(string emailTo, string emailCC, string msgSubject, string msgBody, string attachment)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(FromAddress);


                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    mail.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        mail.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (!string.IsNullOrEmpty(attachment))
                        {
                            //Add the attachment
                            // mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/image.jpg")));
                            Attachment attach = new Attachment(attachment);

                            FileInfo attachmentinfo = new FileInfo(attachment);
                            attach.Name = attachmentinfo.Name;
                            mail.Attachments.Add(attach);

                        }

                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send Mail with attachment. Pass attachment as full path with filename
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static bool SendEmailWithFromAddress(string emailFrom, string emailTo, string emailCC, string msgSubject, string msgBody, string attachment)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);


                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (!string.IsNullOrEmpty(attachment))
                        {
                            //Add the attachment
                            // mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/image.jpg")));
                            Attachment attach = new Attachment(attachment);

                            FileInfo attachmentinfo = new FileInfo(attachment);
                            attach.Name = attachmentinfo.Name;
                            mail.Attachments.Add(attach);

                        }

                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send Mail with attachment. Pass attachment as full path with filename
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static bool SendEmail(string emailTo, string emailCC, string msgSubject, string msgBody, HttpPostedFile postedFile)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(FromAddress);

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (postedFile.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(postedFile.FileName);
                            mail.Attachments.Add(new Attachment(postedFile.InputStream, fileName, postedFile.ContentType));
                        }
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);

            }
            return IsSend;

        }

        /// <summary>
        /// Send mail by accepting mail from address
        /// </summary>
        /// <param name="emailFrom"></param>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        public static bool SendEmailWithFromAddress(string emailFrom, string emailTo, string emailCC, string msgSubject, string msgBody, HttpPostedFile postedFile)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (postedFile.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(postedFile.FileName);
                            mail.Attachments.Add(new Attachment(postedFile.InputStream, fileName));
                        }
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send Mail with attachment. Pass attachment as byte array with filename
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool SendEmail(string emailTo, string emailCC, string msgSubject, string msgBody, byte[] data, string filename)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(FromAddress);

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (data.Length > 0)
                        {
                            //string fileName = Path.GetFileName(postedFile.FileName);
                            mail.Attachments.Add(new Attachment(new MemoryStream(data), filename));
                        }
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send Mail with attachment. Pass attachment as byte array with filename
        /// </summary>
        /// <param name="emailFrom"></param>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool SendEmailWithFromAddress(string emailFrom, string emailTo, string emailCC, string msgSubject, string msgBody, byte[] data, string filename)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (data.Length > 0)
                        {
                            mail.Attachments.Add(new Attachment(new MemoryStream(data), filename));
                        }
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send Mail 
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static bool SendEmail(string emailTo, string emailCC, string msgSubject, string msgBody)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(FromAddress);
                        // mail.To.Add(new MailAddress(email));

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject only with 100 char and body of the message

                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        //smtpClient.UseDefaultCredentials = true;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send mail by accepting mail from address 
        /// </summary>
        /// <param name="emailFrom"></param>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static bool SendEmailWithFromAddress(string emailFrom, string emailTo, string emailCC, string msgSubject, string msgBody)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        // mail.To.Add(new MailAddress(email));

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail))
                                {
                                    if (!ToEmail.Equals("&nbsp;"))
                                        //Adding Multiple To email Id
                                        mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail))
                                    {
                                        if (!CCEmail.Equals("&nbsp;"))
                                            //Adding Multiple CC email Id
                                            mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail))
                                    {
                                        if (!BCCEmail.Equals("&nbsp;"))
                                            //Adding Multiple CC email Id
                                            mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject only with 100 char and body of the message

                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send Mail 
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static bool SendEmail(string emailTo, string msgSubject, string msgBody)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(FromAddress);
                        // mail.To.Add(new MailAddress(email));

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject only with 100 char and body of the message

                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;

                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send mail by accepting mail from address
        /// </summary>
        /// <param name="emailFrom"></param>
        /// <param name="emailTo"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public static bool SendEmailWithFromAddress(string emailFrom, string emailTo, string msgSubject, string msgBody)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);
                        // mail.To.Add(new MailAddress(email));

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject only with 100 char and body of the message

                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;

                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);
                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }


        /// <summary>
        /// Send Mail asynchronous 
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static void SendAsyncEmail(string emailTo, string emailCC, string msgSubject, string msgBody)
        {

            Thread email = new Thread(delegate()
            {
                try
                {
                    if (!string.IsNullOrEmpty(emailTo))
                    {
                        //Create the mail message and supply it with from and to info
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(FromAddress);
                            // mail.To.Add(new MailAddress(email));

                            if (emailTo.Contains(";"))
                            {
                                string[] ToId = emailTo.Split(';');

                                foreach (string ToEmail in ToId)
                                {
                                    if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple To email Id
                                        mail.To.Add(new MailAddress(ToEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.To.Add(new MailAddress(emailTo));
                            }

                            if (!string.IsNullOrEmpty(emailCC))
                            {
                                if (emailCC.Contains(";"))
                                {
                                    string[] CCId = emailCC.Split(';');

                                    foreach (string CCEmail in CCId)
                                    {
                                        if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                        {
                                            //Adding Multiple CC email Id
                                            mail.CC.Add(new MailAddress(CCEmail));
                                        }
                                    }
                                }
                                else
                                {
                                    mail.CC.Add(new MailAddress(emailCC));
                                }
                            }

                            if (!string.IsNullOrEmpty(BccMailIDs))
                            {
                                if (BccMailIDs.Contains(";"))
                                {
                                    string[] BCCId = BccMailIDs.Split(';');

                                    foreach (string BCCEmail in BCCId)
                                    {
                                        if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                        {
                                            //Adding Multiple CC email Id
                                            mail.Bcc.Add(new MailAddress(BCCEmail));
                                        }
                                    }
                                }
                                else
                                {
                                    mail.Bcc.Add(new MailAddress(BccMailIDs));
                                }
                            }

                            //Set the subject only with 100 char and body of the message
                            string msgSub = msgSubject.Replace('\r', ' ').Replace('\n', ' ');
                            if (msgSubject.Length > 100)
                            {
                                msgSubject = msgSub.Substring(0, 100);
                            }
                            mail.Subject = msgSub;

                            mail.Body = msgBody;
                            mail.BodyEncoding = System.Text.Encoding.ASCII;
                            mail.IsBodyHtml = true;
                            //Create the SMTP client object and send the message
                            SmtpClient smtpClient = new SmtpClient();
                            smtpClient.Host = SMTPSERVER;
                            smtpClient.Port = SMTPPort;
                            //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                            //smtpClient.Credentials = cred;
                            try
                            {
                                smtpClient.Send(mail);

                            }
                            catch (Exception ex)
                            {
                                CommonMethods.AddExceptionInEventViewer(ex);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonMethods.AddExceptionInEventViewer(ex);
                }

            });

            email.IsBackground = true;
            email.Start();

        }

        /// <summary>
        /// Send mail by accepting mail from address
        /// </summary>
        /// <param name="emailFrom"></param>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        public static void SendAsyncWithFromAddress(string emailFrom, string emailTo, string emailCC, string msgSubject, string msgBody)
        {

            Thread email = new Thread(delegate()
            {
                try
                {
                    if (!string.IsNullOrEmpty(emailTo))
                    {
                        //Create the mail message and supply it with from and to info
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            // mail.To.Add(new MailAddress(email));

                            if (emailTo.Contains(";"))
                            {
                                string[] ToId = emailTo.Split(';');

                                foreach (string ToEmail in ToId)
                                {
                                    if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple To email Id
                                        mail.To.Add(new MailAddress(ToEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.To.Add(new MailAddress(emailTo));
                            }

                            if (!string.IsNullOrEmpty(emailCC))
                            {
                                if (emailCC.Contains(";"))
                                {
                                    string[] CCId = emailCC.Split(';');

                                    foreach (string CCEmail in CCId)
                                    {
                                        if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                        {
                                            //Adding Multiple CC email Id
                                            mail.CC.Add(new MailAddress(CCEmail));
                                        }
                                    }
                                }
                                else
                                {
                                    mail.CC.Add(new MailAddress(emailCC));
                                }
                            }

                            if (!string.IsNullOrEmpty(BccMailIDs))
                            {
                                if (BccMailIDs.Contains(";"))
                                {
                                    string[] BCCId = BccMailIDs.Split(';');

                                    foreach (string BCCEmail in BCCId)
                                    {
                                        if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                        {
                                            //Adding Multiple CC email Id
                                            mail.Bcc.Add(new MailAddress(BCCEmail));
                                        }
                                    }
                                }
                                else
                                {
                                    mail.Bcc.Add(new MailAddress(BccMailIDs));
                                }
                            }

                            //Set the subject only with 100 char and body of the message
                            string msgSub = msgSubject.Replace('\r', ' ').Replace('\n', ' ');
                            if (msgSubject.Length > 100)
                            {
                                msgSubject = msgSub.Substring(0, 100);
                            }
                            mail.Subject = msgSub;

                            mail.Body = msgBody;
                            mail.BodyEncoding = System.Text.Encoding.ASCII;
                            mail.IsBodyHtml = true;
                            //Create the SMTP client object and send the message
                            SmtpClient smtpClient = new SmtpClient();
                            smtpClient.Host = SMTPSERVER;
                            smtpClient.Port = SMTPPort;
                            //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                            //smtpClient.Credentials = cred;
                            try
                            {
                                smtpClient.Send(mail);
                            }
                            catch (Exception ex)
                            {
                                CommonMethods.AddExceptionInEventViewer(ex);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonMethods.AddExceptionInEventViewer(ex);
                }

            });

            email.IsBackground = true;
            email.Start();
        }

        public static bool SendEmail(string emailTo, string emailCC, string msgSubject, string msgBody, List<string> attachments)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(FromAddress);

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (attachments.Count > 0)
                        {
                            //Download the content of the file with a WebClient
                            WebClient webClient = new WebClient();

                            //Supply the WebClient with the network credentials of our user
                            webClient.Credentials = CredentialCache.DefaultNetworkCredentials;

                            foreach (var attachment in attachments)
                            {
                                ////Add the attachment
                                Attachment attach = new Attachment(attachment);
                                // mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/image.jpg")));
                                FileInfo attachmentinfo = new FileInfo(attachment);
                                attach.Name = attachmentinfo.Name;
                                mail.Attachments.Add(attach);
                            }

                            //mail.IsBodyHtml = true;
                        }
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);

                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        /// <summary>
        /// Send mail by accepting mail from address
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="emailCC"></param>
        /// <param name="msgSubject"></param>
        /// <param name="msgBody"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public static bool SendEmailWithFromAddress(string emailFrom, string emailTo, string emailCC, string msgSubject, string msgBody, List<string> attachments)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    //Create the mail message and supply it with from and to info
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom);

                        if (emailTo.Contains(";"))
                        {
                            string[] ToId = emailTo.Split(';');

                            foreach (string ToEmail in ToId)
                            {
                                if (!string.IsNullOrEmpty(ToEmail) && !ToEmail.Equals("&nbsp;"))
                                {
                                    //Adding Multiple To email Id
                                    mail.To.Add(new MailAddress(ToEmail));
                                }
                            }
                        }
                        else
                        {
                            mail.To.Add(new MailAddress(emailTo));
                        }

                        if (!string.IsNullOrEmpty(emailCC))
                        {
                            if (emailCC.Contains(";"))
                            {
                                string[] CCId = emailCC.Split(';');

                                foreach (string CCEmail in CCId)
                                {
                                    if (!string.IsNullOrEmpty(CCEmail) && !CCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.CC.Add(new MailAddress(CCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.CC.Add(new MailAddress(emailCC));
                            }
                        }

                        if (!string.IsNullOrEmpty(BccMailIDs))
                        {
                            if (BccMailIDs.Contains(";"))
                            {
                                string[] BCCId = BccMailIDs.Split(';');

                                foreach (string BCCEmail in BCCId)
                                {
                                    if (!string.IsNullOrEmpty(BCCEmail) && !BCCEmail.Equals("&nbsp;"))
                                    {
                                        //Adding Multiple CC email Id
                                        mail.Bcc.Add(new MailAddress(BCCEmail));
                                    }
                                }
                            }
                            else
                            {
                                mail.Bcc.Add(new MailAddress(BccMailIDs));
                            }
                        }

                        //Set the subject and body of the message
                        mail.Subject = msgSubject;
                        mail.Body = msgBody;
                        mail.BodyEncoding = System.Text.Encoding.ASCII;
                        mail.IsBodyHtml = true;
                        if (attachments.Count > 0)
                        {
                            //Download the content of the file with a WebClient
                            WebClient webClient = new WebClient();

                            //Supply the WebClient with the network credentials of our user
                            webClient.Credentials = CredentialCache.DefaultNetworkCredentials;

                            foreach (var attachment in attachments)
                            {
                                ////Add the attachment
                                Attachment attach = new Attachment(attachment);
                                FileInfo attachmentinfo = new FileInfo(attachment);
                                attach.Name = attachmentinfo.Name;
                                mail.Attachments.Add(attach);
                            }
                        }
                        //Create the SMTP client object and send the message
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = SMTPSERVER;
                        smtpClient.Port = SMTPPort;
                        //NetworkCredential cred = new NetworkCredential(FromAddress, FromAddressPassword);
                        //smtpClient.Credentials = cred;
                        try
                        {
                            smtpClient.Send(mail);

                            IsSend = true;
                        }
                        catch (Exception ex)
                        {
                            CommonMethods.AddExceptionInEventViewer(ex);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                IsSend = false;
                CommonMethods.AddExceptionInEventViewer(ex);
            }
            return IsSend;

        }

        #endregion
    }

    public static class CommonMethods
    {
        public static void AddExceptionInEventViewer(Exception ex)
        {
            using (EventLog eventLog = new EventLog())
            {
                eventLog.Source = "Mailer";
                // Write an entry in the event log.
                StringBuilder sb = new StringBuilder();
                sb.Append("There was an error in Mailer");
                sb.Append(Environment.NewLine);
                sb.Append(string.Concat("Message:", ex.Message));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(string.Concat("Stack Trace:", ex.StackTrace));
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(string.Concat("Inner Exception:", ex.InnerException));
                eventLog.WriteEntry(Convert.ToString(sb), EventLogEntryType.Error, 101);
            }
        }
    }
}
