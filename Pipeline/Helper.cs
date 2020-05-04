using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pipeline
{
    public interface IHttpHandler
    {
        HttpResponseMessage TestListMessage(string url);
        Task<HttpResponseMessage> ListMessagesAsync(string url);
        HttpResponseMessage TestSendMessage(string url, HttpContent content);
        Task<HttpResponseMessage> SendMessageAsync(string url, HttpContent content);
        HttpResponseMessage TestGetMessage(string url);
        HttpResponseMessage TestInbox(string url);
        HttpResponseMessage TestUpdateMessage(string url, HttpContent content);
        Task<HttpResponseMessage> UpdateMessageAsync(string url, HttpContent content);
    }

    public class Helper : IHttpHandler
    {
        private HttpClient _client = new HttpClient();

        //Test Case 1: Listing all the mesages

        public HttpResponseMessage TestListMessage(string url)
        {
            return ListMessagesAsync(url).Result;
        }

        public async Task<HttpResponseMessage> ListMessagesAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        //Test Case 2: Sending message to DynamoDB

        public HttpResponseMessage TestSendMessage(string url, HttpContent content)
        {
            return SendMessageAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> SendMessageAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }

        public HttpResponseMessage TestGetMessage(string url)
        {
            Console.WriteLine(url);
            return ListMessagesAsync(url).Result;
        }

        public HttpResponseMessage TestInbox(string url)
        {
            return ListMessagesAsync(url).Result;
        }

        public HttpResponseMessage TestUpdateMessage(string url, HttpContent content)
        {
            return UpdateMessageAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> UpdateMessageAsync(string url, HttpContent content)
        {
            return await _client.PatchAsync(url, content);
        }
    }
}
