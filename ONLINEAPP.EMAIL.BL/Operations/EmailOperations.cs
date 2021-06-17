using ONLINEAPP.EMAIL.INTERFACE;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.EMAIL.BL.Operations
{
    public class EmailOperations : IEmailOperations
    {
        public Result Send(EMAIL.MODEL.Email email)
        {
            Result res = new Result();

            try
            {
                string _SmtpServerName = "172.16.10.55";//System.Configuration.ConfigurationManager.AppSettings["SmtpServerName"];
                int _SmtpServerPort = 25;/*System.Configuration.ConfigurationManager.AppSettings["SmtpServerPort"] != null ?
                    int.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpServerPort"]) : 0;*/

                if (_SmtpServerName != null && _SmtpServerPort != 0)
                {
                    using (SmtpClient SmtpServer = new SmtpClient(_SmtpServerName, _SmtpServerPort))
                    {
                        MailMessage mail = new MailMessage();
                        mail.Subject = email.Subject;
                        //System.Configuration.ConfigurationManager.AppSettings["SmtpServerName"],
                        //int.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpServerPort"])
                        //mail.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["MailSender"]);

                        string From = "atisht@frci.net";/*System.Configuration.ConfigurationManager.AppSettings["MailSender"] != null ?
                            System.Configuration.ConfigurationManager.AppSettings["MailSender"] : "";*/

                        mail.From = new MailAddress(From);
                        foreach (string receiver in email.ListTo)
                        {
                            mail.To.Add(new MailAddress(receiver));
                        }

                        if (email.ListCc != null)
                        {
                            foreach (string cc in email.ListCc)
                            {
                                mail.CC.Add(new MailAddress(cc));
                            }
                        }

                        mail.IsBodyHtml = email.IsBodyHtml;
                        mail.Body = email.Body;
                        SmtpServer.Send(mail);

                        res.StatusCode = StatusCode.Success;
                        res.Message = Messages.EmailSentSuccessfully;

                    }
                }
                else
                {
                    throw new Exception("Server name not initialized");
                }
            }
            catch (Exception ex)
            {
                res.StatusCode = StatusCode.Error;
                res.Message = Messages.EmailSentFailed;
                res.ErrorMsg = ex.Message;
            }

            return res;

        }
    }
}
