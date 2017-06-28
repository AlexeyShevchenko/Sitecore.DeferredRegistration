namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    using Sitecore.Web;

    public class ReplaceTokensInMailMessage
    {
        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(args.EncryptedToken, "args.EncryptedToken");
            Assert.ArgumentNotNullOrEmpty(args.UserData.FirstName, "args.UserData.FirstName");
            Assert.ArgumentNotNullOrEmpty(args.UserData.LastName, "args.UserData.LastName");
            Assert.ArgumentNotNull(args.MailMessage, "args.MailMessage");
            Assert.ArgumentNotNullOrEmpty(args.MailMessage.Body, "args.MailMessage.Body");

            var processActivationLinkPageId = Settings.GetSetting("DeferredRegistration.PageToProcessActivationLink");
            Assert.ArgumentNotNullOrEmpty(processActivationLinkPageId, "processActivationLinkPageId");

            var processActivationLinkPageItem = Sitecore.Context.Database.GetItem(processActivationLinkPageId);
            Assert.ArgumentNotNull(processActivationLinkPageItem, "processActivationLinkPageItem");

            var urlOptions = new UrlOptions
            {
                AlwaysIncludeServerUrl = true,
                LanguageEmbedding = LanguageEmbedding.Never
            };
            var deferredRegistrationPageUrl = LinkManager.GetItemUrl(processActivationLinkPageItem, urlOptions);

            var activationLink = WebUtil.AddQueryString(deferredRegistrationPageUrl, "hash", args.EncryptedToken);
            args.MailMessage.Body = args.MailMessage.Body.Replace("{activation-link}", activationLink);
            args.MailMessage.Body = args.MailMessage.Body.Replace("{name}", args.UserData.FirstName);
        }
    }
}