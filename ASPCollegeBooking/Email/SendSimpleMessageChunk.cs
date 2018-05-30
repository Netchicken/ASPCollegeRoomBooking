using System;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;

//https://documentation.mailgun.com/en/latest/quickstart-sending.html#send-via-api
namespace ASPCollegeBooking.Email
{
    public static class SendSimpleMessageChunk
    {

        public static IRestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", "dacfc8d465fc94794cf50028c6834218-115fe3a6-47557102");

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox84efe81bdff848469fe85bdae5d4ecdc.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@mg.go-cycle-touring.com>");
            request.AddParameter("to", "gary.d@visioncollege.ac.nz");
            request.AddParameter("to", "intelproof@gmail.com");
            request.AddParameter("subject", "Test of Email sent From SimpleMessage");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            return client.Execute(request);
        }

    }
}
