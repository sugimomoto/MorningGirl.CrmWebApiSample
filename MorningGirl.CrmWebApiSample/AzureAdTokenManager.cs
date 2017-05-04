using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MorningGirl.CrmWebApiSample
{
    public class AzureAdTokenManager
    {
        /// <summary>
        /// 
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// Azure AD Client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Azure AD Client Secret
        /// </summary>
        public string ClientSecret{ get; set; }

        /// <summary>
        /// Office365 の Tenant ID
        /// ***.onmicrosoft.com
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string AuthUrl {
            get { return $"https://login.windows.net/{TenantId}"; }
        }

        private AuthenticationContext authContext { get; }

        /// <summary>
        /// ClientCledenti認証の場合
        /// </summary>
        public AzureAdTokenManager(string serverUrl, string tenantId, string clientId, string clientSecret)
        {
            ServerUrl = serverUrl;
            ClientId = clientId;
            ClientSecret = clientSecret;
            TenantId = tenantId;

            authContext = new AuthenticationContext(AuthUrl);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessToken()
        {

            var clientCledential = new ClientCredential(ClientId, ClientSecret);

            var result = await authContext.AcquireTokenAsync(ServerUrl, clientCledential);
            return result.AccessToken;
        }
    }
}
