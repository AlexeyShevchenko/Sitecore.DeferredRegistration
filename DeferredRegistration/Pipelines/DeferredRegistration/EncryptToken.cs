namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Diagnostics;

    public class EncryptToken
    {
        private readonly StringCipher cipher = new StringCipher();

        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.UserData, "args.UserData");
            Assert.ArgumentNotNullOrEmpty(args.UserData.UserName, "args.UserData.UserName");

            args.EncryptedToken = cipher.Encrypt(args.UserData.UserName);
        }
    }
}