namespace DeferredRegistration.Pipelines.Initialize
{
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;
    using Sitecore.Security.Accounts;
    using Sitecore.SecurityModel;

    public class CreateCustomDomains
    {
        public void Process(PipelineArgs args)
        {
            var domainName = Settings.GetSetting("DeferredRegistration.Domain");
            Assert.IsNotNullOrEmpty(domainName, "domainName");

            if (DomainManager.GetDomain(domainName) != null)
            {
                return;
            }

            using (new UserSwitcher("sitecore\\admin", true))
            {
                DomainManager.AddDomain(domainName);
            }
        }
    }
}