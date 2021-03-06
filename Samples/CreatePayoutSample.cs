using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Samples;
using PayoutsSdk.Payouts;
using PayoutsSdk.Core;
using PayPalHttp;

using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
namespace Samples
{
    public class CreatePayoutSample
    {
        private static CreatePayoutRequest buildRequestBody()
        {
            var request = new CreatePayoutRequest()
            {

                SenderBatchHeader = new SenderBatchHeader()
                {
                    EmailMessage = "Congrats on recieving 1$",
                    EmailSubject = "You recieved a payout!!"
                },
                Items = new List<PayoutItem>(){
            new PayoutItem(){
                RecipientType="EMAIL",

                Amount=new Currency(){
                    CurrencyCode="USD",
                    Value="1",
                 },
                Receiver="payouts-simulator23@paypal.com",

            }
            }
            };
            return request;

        }
        public async static Task<HttpResponse> CreatePayout(bool debug = false)
        {
            Console.WriteLine("Creating payout with complete payload");
            PayoutsPostRequest request = new PayoutsPostRequest()
               .PayPalPartnerAttributionId("agSzCOx4Ab9pHxgawSO")
               .PayPalRequestId("M6a5KDUiH6-u6E3D");
            request.RequestBody(buildRequestBody());


            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<CreatePayoutResponse>();
            if (debug)
            {
                Console.WriteLine("Status: {0}", result.BatchHeader.BatchStatus);
                Console.WriteLine("Batch Id: {0}", result.BatchHeader.PayoutBatchId);
                Console.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }

            }
            return response;
        }

        /* static void Main(string[] args)
          {
             CreatePayout(true).Wait();
          }*/
    }
}