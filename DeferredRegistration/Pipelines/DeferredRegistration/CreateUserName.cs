namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using System;

    public class CreateUserName
    {
        private readonly string deferredRegistrationDomain = Settings.GetSetting("DeferredRegistration.Domain");

        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(deferredRegistrationDomain, "deferredRegistration");
            Assert.ArgumentNotNullOrEmpty(args.UserData.FirstName, "args.UserData.FirstName");
            Assert.ArgumentNotNullOrEmpty(args.UserData.LastName, "args.UserData.LastName");

            args.UserData.UserName = $@"{deferredRegistrationDomain}\{args.UserData.FirstName}{args.UserData.LastName}{Guid.NewGuid()}";
        }
    }
}