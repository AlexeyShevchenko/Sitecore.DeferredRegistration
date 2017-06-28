namespace DeferredRegistration
{
    using Pipelines.DeferredRegistration;
    using Sitecore.Pipelines;
    using Sitecore.Security.Accounts;
    using Sitecore.Web;

    public static class Test
    {
        public static void InitDeferredRegistration()
        {
            var args = new DeferredRegistrationArgs
            {
                UserData = new DeferredRegistrationUserData
                {
                    EmailAddress = "me@me.com",
                    FirstName = "Aleksey",
                    LastName = "Shevchenko"
                }
            };
            CorePipeline.Run("deferredRegistration", args);
        }

        public static void ProcessActivationLink()
        {
            var hash = WebUtil.GetQueryString("hash");
            var userName = new StringCipher().Decrypt(hash);

            if (User.Exists(userName))
            {
                var user = User.FromName(userName, true);

                // Do what you need with activated user
            }
        }
    }
}