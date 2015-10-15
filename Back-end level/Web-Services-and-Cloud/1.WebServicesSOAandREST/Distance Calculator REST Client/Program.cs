using System;
using RestSharp;

namespace Distance_Calculator_REST_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("http://localhost:51041");
            var request = new RestRequest();
            request.Resource = "/api/points/distance?startX=3&startY=4&endX=11&endY=122";
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
