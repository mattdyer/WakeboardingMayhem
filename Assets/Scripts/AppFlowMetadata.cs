using System;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;

namespace Google.Apis.Sample.MVC4
{
    public class AppFlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
            new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = "656522469414-4bvr6p7mhu5p627t0bijjvugrfv8a21a.apps.googleusercontent.com",
                        ClientSecret = "Q3W6eF1oge_6jqR1nBVAi9FK"
                    },
                    Scopes = new[] { DriveService.Scope.Drive },
                    DataStore = new FileDataStore("Drive.Api.Auth.Store")
                });

        public IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}