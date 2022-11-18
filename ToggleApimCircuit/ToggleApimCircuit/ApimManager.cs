using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.ApiManagement;
using Azure.ResourceManager.Resources;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ToggleApimCircuit
{
    public class ApimManager
    {
        //private static string ClientId = "a5f280a9-a44f-4303-884e-824173e0c360";
        //private static string ClientSecret = "arJ8Q~4.L8-cvNAJA1Vm1dhFrVR83xv_7aSb2bix";
        //private static string TenantId = "16b3c013-d300-468d-ac64-7eda0820b6d3";
        private static string SubscriptionId = "aa27a1b3-530a-4637-a1e6-6855033a65e5";
        private static string ResourceGroupName = "mohamed-apim1";
        private static string ApiManagmentName = "mohamedapim2";
        private static string CicruitKeyName = "CircuitOpen";
        public static async Task GetCircuitState(ILogger log)
        {
            //var azCred = new ClientSecretCredential(TenantId, ClientId, ClientSecret);
            var azCred = new ManagedIdentityCredential();
            ArmClient client = new ArmClient(azCred);

            var subscriptiobRef = SubscriptionResource.CreateResourceIdentifier(SubscriptionId);
            SubscriptionResource subscription = client.GetSubscriptionResource(subscriptiobRef);
            ResourceGroupCollection resourceGroups = subscription.GetResourceGroups();
            ResourceGroupResource resourceGroup = await resourceGroups.GetAsync(ResourceGroupName);
            var apiMangegment = resourceGroup.GetApiManagementService(ApiManagmentName).Value;
            var namedValues = apiMangegment.GetApiManagementNamedValues();
            // I am able to list all Named Values below
            foreach (var namedValue in namedValues)
            {
                log.LogInformation($" named Value name is {namedValue.Data.Name} and value is {namedValue.Data.Value}");
            }
            // I am also able to get a particular Named Value
            var circuitNamedVal = apiMangegment.GetApiManagementNamedValue(CicruitKeyName).Value;

            // I am able to `set` the value inb the long below, But
            //I can't find the updateNamedValue function or any other similar function
            // Any idea how to do that?
            //circuitNamedVal.Data.Value = circuitState.ToString(); // Does not actually update the value on the Azure resource

        }
        public static async Task<string> UpdateAPImanagment(bool cicruitState, ILogger log)
        {
            var circuitStateString = cicruitState ? "true" : "false";
            var token = await GetAccessToken();
            log.LogInformation($"access token is {token}");
            var payLoad = new
            {
                properties = new
                {
                    displayName = CicruitKeyName,
                    value = circuitStateString,
                    secret = false
                }
            };
            var payLoadString = JsonConvert.SerializeObject(payLoad);
            log.LogInformation(payLoadString);

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://management.azure.com/subscriptions/")
            };

            string URI = $"{SubscriptionId}/resourceGroups/{ResourceGroupName}/providers/Microsoft.ApiManagement/service/{ApiManagmentName}/namedValues/{CicruitKeyName}?api-version=2020-12-01";
            // https://management.azure.com/subscriptions/aa27a1b3-530a-4637-a1e6-6855033a65e5/resourceGroups/mohamed-apim1/providers/Microsoft.ApiManagement/service/mohamedapim2/namedValues/testprop2?api-version=2020-12-01
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var httpContnet = new StringContent(payLoadString, encoding: Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync(URI, httpContnet);
            var httpsResponse = await response.Content.ReadAsStringAsync();
            var JSONObject = JsonConvert.DeserializeObject<object>(httpsResponse);

            log.LogInformation(JSONObject.ToString());
            log.LogInformation($"httpRes is {httpsResponse}");
            //var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(JSONObject);
            return response.StatusCode.ToString();
        }
        private static async Task<string> GetAccessToken()
        {
            var cred = new ManagedIdentityCredential();
            var toeknRequestContext = new TokenRequestContext(
                scopes: new[] { "https://management.azure.com/.default" })
            {

            };
            var token = await cred.GetTokenAsync(toeknRequestContext);
            return token.Token;
        }
    }
}
