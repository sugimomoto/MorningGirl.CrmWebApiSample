using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MorningGirl.CrmWebApiSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientId = ConfigurationManager.AppSettings["clientId"];
            var clientSecret = ConfigurationManager.AppSettings["clientSecret"];
            var serverUrl = ConfigurationManager.AppSettings["serverUri"];
            var tenantId = ConfigurationManager.AppSettings["tenantId"];

            var azureAd = new AzureAdTokenManager(serverUrl,tenantId,clientId,clientSecret);

            var token = azureAd.GetAccessToken();
            token.Wait();

            var crud = new CrmCrudCequest(token.Result, serverUrl);

            var accounts = crud.RetrieveMultiple<CrmWebApiRoot<Accounts>>("accounts", "?$select=name");
            accounts.Wait();

            foreach (var account in accounts.Result.value)
            {
                Console.WriteLine(account.name);
            }

            Console.ReadKey();
        }
    }
}
