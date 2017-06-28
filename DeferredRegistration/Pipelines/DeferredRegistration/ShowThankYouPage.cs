namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    using Sitecore.Web;

    public class ShowThankYouPage
    {
        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            var thankYouPageId = Settings.GetSetting("DeferredRegistration.ThankYouPage");
            Assert.ArgumentNotNullOrEmpty(thankYouPageId, "thankYouPageId");

            var thankYouPageItem = Sitecore.Context.Database.GetItem(thankYouPageId);
            Assert.ArgumentNotNull(thankYouPageItem, "thankYouPageItem");

            var thankYouPageUrl = LinkManager.GetItemUrl(thankYouPageItem);
            WebUtil.Redirect(thankYouPageUrl);
        }
    }
}