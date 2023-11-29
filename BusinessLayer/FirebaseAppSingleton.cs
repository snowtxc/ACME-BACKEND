using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FirebaseAppSingleton
    {
        public static FirebaseApp app;
        public static FirebaseAuth authentication;


        public static FirebaseApp GetFirebaseApp(string url)
        {
            if (app == null || authentication == null)
            {
                string jsonKey = File.ReadAllText(url + "/googlePrivateKey.json");

                var credential = GoogleCredential.FromJson(jsonKey)
                .CreateScoped("https://www.googleapis.com/auth/firebase");

                var firebaseApp = FirebaseApp.Create(new AppOptions
                {
                    Credential = credential,
                    ProjectId = "acme-536c1"
                });
                var auth = FirebaseAuth.GetAuth(firebaseApp);
                app = firebaseApp;
                authentication = auth;
            }

            return app;
        }

    }
}
