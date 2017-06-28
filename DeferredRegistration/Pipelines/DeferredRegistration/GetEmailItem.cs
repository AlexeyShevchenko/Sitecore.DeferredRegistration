namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Diagnostics;

    public class GetEmailItem
    {
        public string DefaultActivationFlowEmailItemId { get; set; }

        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(DefaultActivationFlowEmailItemId, "DefaultActivationFlowEmailItemId");

            var emailItem = Sitecore.Context.Database.GetItem(DefaultActivationFlowEmailItemId);
            Assert.ArgumentNotNull(emailItem, "emailItem");

            args.EmailItem = emailItem;
        }
    }
}