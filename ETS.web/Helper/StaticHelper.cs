using System.Net.Mail;
using System.Net;
using ETSystem.Model;
using MimeKit;

namespace ETSystem.Helper
{
    public static class StaticHelper
    {
        static HttpContextAccessor _accessor;
        static IConfiguration _configuration;
        public static void InitHttpContext()
        {
            _accessor = new HttpContextAccessor();
        }
        public static void InitConfig(IConfiguration config)
        {
            _configuration = config;
        }
        public static void SendEmailVerification(string Email, string ToFullName)
        {
            InitHttpContext();
            try
            {
                var confirmationLink = _accessor.HttpContext.Request.Scheme + "://" + _accessor.HttpContext.Request.Host + "/LOGINREG_/VerifyEmail?email=" + Email;
                var htmltext = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Helper", "EmailTemplate", "VerifyEmail.html"));
                var modifyhtmltext = string.Empty;
                modifyhtmltext += htmltext.Replace("%TokenVerifyURL%", "<a href=\"" + confirmationLink + "\" class=\"btn btn-success\" target=\"_blank\" title=\"Click here to Verify\">User Verfiried!</a>");
                //modifyhtmltext += htmltext.Replace("%TokenVerifyURL%", "<a>User Verfiried!</a>");
                var email = new EmailHelperModel
                {
                    ToEmailAddress = Email,
                    ToFullName = ToFullName,
                    Message = modifyhtmltext,
                    Subject = "Verify Email",
                    FromEmailAddress = _configuration["MailSettings:Mail"].ToString(),
                    FromFullName = _configuration["MailSettings:FromFullName"].ToString(),
                    SmtpUsername = _configuration["MailSettings:Mail"].ToString(),
                    SmtpPassword = _configuration["MailSettings:Password"].ToString(),
                    SmtpPort = _configuration["MailSettings:Port"].ToString(),
                    SmtpServer = _configuration["MailSettings:Host"].ToString(),
                    UseDefaultCredentials = _configuration["MailSettings:UseDefaultCredentials"].ToString(),
                    EnableSsl = _configuration["MailSettings:EnableSsl"].ToString(),
                };
                Task.Run(() =>
                {
                    StaticHelper.SendEmailAsync(email);
                });
            }
            catch (Exception ex)
            {

            }
        }
        public static Response SendEmailAsync(EmailHelperModel mailRequest)
        {
            var response = new Response();
            try
            {
                if (mailRequest != null)
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    MimeMessage mail = new MimeMessage();
                    mail.From.Add(new MailboxAddress(mailRequest.FromFullName, string.IsNullOrEmpty(mailRequest.FromEmailAddress) ? "" : mailRequest.FromEmailAddress));
                    mail.To.Add(new MailboxAddress(mailRequest.ToFullName, string.IsNullOrEmpty(mailRequest.ToEmailAddress) ? "" : mailRequest.ToEmailAddress));
                    if (!string.IsNullOrEmpty(mailRequest.CCEmail))
                    {
                        mail.Cc.Add(new MailboxAddress(mailRequest.CCFullName, string.IsNullOrEmpty(mailRequest.CCEmail) ? "" : mailRequest.CCEmail));
                    }
                    mail.Subject = string.IsNullOrEmpty(mailRequest.Subject) ? "" : mailRequest.Subject;
                    mail.Body = new TextPart("html") { Text = mailRequest.Message };
                    using (MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient())
                    {
                        smtp.Connect(mailRequest.SmtpServer, Convert.ToInt32(mailRequest.SmtpPort), false);
                        smtp.Authenticate(mailRequest.SmtpUsername, mailRequest.SmtpPassword);
                        smtp.Send(mail);
                        smtp.Disconnect(true);
                    }
                    response.StatusCode = 0;
                    response.StatusMessage = "SUCCESS!";
                    return response;
                }
                else
                {
                    response.StatusCode = 1;
                    response.StatusMessage = "Unable send mail!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 0;
                response.StatusMessage = ex.Message;
                return response;
            }
        }
    }
}





