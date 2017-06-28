namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Diagnostics;
    using System.Net.Mail;

    public class FillMailMessage
    {
        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.EmailItem, "args.EmailItem");
            Assert.ArgumentNotNullOrEmpty(args.UserData.EmailAddress, "args.UserData.EmailAddress");

            args.MailMessage = new MailMessage(args.EmailItem["From"], args.UserData.EmailAddress, args.EmailItem["Subject"], args.EmailItem["Body"])
            {
                IsBodyHtml = true
            };
        }
    }
}