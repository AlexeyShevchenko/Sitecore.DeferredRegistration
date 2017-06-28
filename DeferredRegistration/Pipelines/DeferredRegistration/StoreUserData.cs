namespace DeferredRegistration.Pipelines.DeferredRegistration
{
    using Sitecore.Diagnostics;
    using Sitecore.Security.Accounts;
    using System;
    using System.Web.Security;

    public class StoreUserData
    {
        public string ProfileItemId { get; set; }

        public void Process(DeferredRegistrationArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.UserData, "args.UserData");
            Assert.ArgumentNotNullOrEmpty(args.UserData.FirstName, "args.UserData.FirstName");
            Assert.ArgumentNotNullOrEmpty(args.UserData.LastName, "args.UserData.LastName");
            Assert.ArgumentNotNullOrEmpty(ProfileItemId, "ProfileItemId");

            if (User.Exists(args.UserData.UserName))
            {
                args.AbortPipeline();
                return;
            }

            CreateUser(args.UserData);
        }

        protected void CreateUser(DeferredRegistrationUserData userData)
        {
            var password = Membership.GeneratePassword(10, 3);

            try
            {
                Membership.CreateUser(userData.UserName, password, userData.EmailAddress);
            }
            catch (Exception ex)
            {
                Log.Error($"Error in DeferredRegistration.Pipelines.DeferredRegistration.StoreUserData (CreateUser): Message: {ex.Message}; Source:{ex.Source}", this);
            }

            var user = User.FromName(userData.UserName, true);
            var userProfile = user.Profile;
            userProfile.FullName = $"{userData.FirstName} {userData.LastName}";

            try
            {
                userProfile.SetPropertyValue("ProfileItemId", ProfileItemId);
            }
            catch (Exception ex)
            {
                Log.Error($"Error in DeferredRegistration.Pipelines.DeferredRegistration.StoreUserData (SetPropertyValue): Message: {ex.Message}; Source:{ex.Source}", this);
            }

            userProfile.SetCustomProperty("First Name", userData.FirstName);
            userProfile.SetCustomProperty("Last Name", userData.LastName);
            userProfile.Save();
        }
    }
}