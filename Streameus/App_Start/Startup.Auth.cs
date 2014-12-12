//using Microsoft.Owin.Security.Cookies;

using System;
using System.Configuration;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Streameus.DataAbstractionLayer;
using Streameus.Models;
using Streameus.Providers;

namespace Streameus
{
    /// <summary>
    /// This class is used to initialize Owin. It's called only once when the app starts
    /// </summary>
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        /// <summary>
        /// Oauth Options
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        /// The public client id
        /// </summary>
        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// <summary>
        /// This method sets up the Auth for the whole API
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            app.CreatePerOwinContext<StreameusContext>(StreameusContext.Create);
            app.CreatePerOwinContext<StreameusUserManager>(StreameusUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            var facebookOptions = new FacebookAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings.Get("facebookAppId"),
                AppSecret = ConfigurationManager.AppSettings.Get("facebookAppSecret"),
            };
            facebookOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookOptions);

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings.Get("googleClientId"),
                ClientSecret = ConfigurationManager.AppSettings.Get("googleClientSecret"),
                CallbackPath = new PathString("/signin-google"),
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
#pragma warning disable 1998
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new Claim("picture", context.User.GetValue("picture").ToString()));
                        context.Identity.AddClaim(new Claim("profile", context.User.GetValue("profile").ToString()));
                    }
#pragma warning restore 1998
                }
            };
            googleOAuth2AuthenticationOptions.Scope.Add("email");
            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
        }
    }
}