namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Data.Items;
    using Sitecore.Pipelines;
    using System.Net.Mail;
	
    public class DeferredRegistrationArgs : PipelineArgs
    {
        public DeferredRegistrationUserData UserData { get; set; }
        public Item EmailItem { get; set; }
        public MailMessage MailMessage { get; set; }
        public string EncryptedToken { get; set; }
    }
}