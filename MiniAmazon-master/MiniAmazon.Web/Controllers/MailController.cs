using ActionMailer.Net.Mvc;
using MiniAmazon.Domain.Entities;

namespace MiniAmazon.Web.Controllers
{
    public class MailController : MailerBase
    {
        public EmailResult VerificationEmail(Account model)
        {
            To.Add(model.Email);
            From = "info@miniamazon.com";
            Subject = "Welcome to MiniAmazon!";
            return Email("VerificationEmail", model);
        }

        public EmailResult ResetPasswordEmail(PasswordRecovery model)
        {
            To.Add(model.Account.Email);
            From = "info@miniamazon.com";
            Subject = "You have to reset your password";
            return Email("ResetPasswordEmail", model);
        }
    }
}