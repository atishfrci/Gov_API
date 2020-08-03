using Microsoft.Owin.Security.OAuth;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ONLINEAPP.API.Providers
{
    public class ADAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            string UsernameContext = DecryptStringAES(context.UserName);

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, Constants.DomainName))
            {
                // validate the credentials
                //bool isValid = pc.ValidateCredentials(context.UserName, context.Password);
                //Get user information from AD

                //decrypt username 

               

                UserPrincipal UserInfoFromAD = UserPrincipal.FindByIdentity(pc, UsernameContext);
                if (UserInfoFromAD == null)
                {
                    context.SetError("invalid_grant", "The user name and/or password is incorrect.");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, UsernameContext));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

                identity.AddClaim(new Claim("LoginName", Convert.ToString(UsernameContext)));

                if (UserInfoFromAD.GivenName == null)
                    identity.AddClaim(new Claim("FirstName", ""));
                else
                    identity.AddClaim(new Claim("FirstName", Convert.ToString(UserInfoFromAD.GivenName)));

                if (UserInfoFromAD.MiddleName == null)
                    identity.AddClaim(new Claim("MiddleName", ""));
                else
                    identity.AddClaim(new Claim("MiddleName", Convert.ToString(UserInfoFromAD.MiddleName)));

                if (UserInfoFromAD.Surname == null)
                    identity.AddClaim(new Claim("LastName", ""));
                else
                    identity.AddClaim(new Claim("LastName", Convert.ToString(UserInfoFromAD.Surname)));

                if (UserInfoFromAD.EmailAddress == null)
                    identity.AddClaim(new Claim("Email", ""));
                else
                    identity.AddClaim(new Claim("Email", Convert.ToString(UserInfoFromAD.EmailAddress)));

                if (UserInfoFromAD.Name == null)
                    identity.AddClaim(new Claim("UserName", ""));
                else
                    identity.AddClaim(new Claim("UserName", Convert.ToString(UserInfoFromAD.DisplayName)));

                //if (string.IsNullOrEmpty(userInfo.EmployeeId))
                //    identity.AddClaim(new Claim("EmployeeID", ""));
                //else
                //    identity.AddClaim(new Claim("EmployeeID", userInfo.EmployeeId));
                //if (string.IsNullOrEmpty(userInfo.Department))
                //    identity.AddClaim(new Claim("Department", ""));
                //else
                //    identity.AddClaim(new Claim("Department", userInfo.Department));
                //identity.AddClaim(new Claim("UserName", Convert.ToString(UserInfoFromAD.Name)));
                //if (userInfo.ID == 0)
                //    identity.AddClaim(new Claim("UserID", ""));
                //else
                //    identity.AddClaim(new Claim("UserID", Convert.ToString(userInfo.ID)));

                //identity.AddClaim(new Claim("UserRoles", userInfo.UserRole));

                context.Validated(identity);


            }
        }

        public static string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes(Constants.DecryptKey);
            var iv = Encoding.UTF8.GetBytes(Constants.DecryptKey);
            string stringToDecrypt = cipherText.Replace(" ", "+");
            int len = stringToDecrypt.Length;
            var encrypted = Convert.FromBase64String(stringToDecrypt);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check for null arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext = null;

            // Create an RijndaelManaged object  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
    }
}