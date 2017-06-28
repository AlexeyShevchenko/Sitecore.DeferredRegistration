namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore;
    using Sitecore.Diagnostics;

    public class SendActivationEmail
    {
        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.MailMessage, "args.MailMessage");

            MainUtil.SendMail(args.MailMessage);
        }
    }
}