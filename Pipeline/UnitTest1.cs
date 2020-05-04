using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ChatContract;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Pipeline
{
    public class Tests
    {

        private Helper _client;
        private String _baseURL;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Initializing Test Setup");
            _client = new Helper();
            _baseURL = "https://localhost:5001/v1/messages";
        }

        //Test 1: To read all the messages from DynamoDB

        [TestCase]
        public void TestListMessage()
        {
            HttpResponseMessage result = _client.TestListMessage(_baseURL);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        //Test 2: To send message to DynamoDB

        [TestCase]
        public void TestSendMessage()
        {
            SendMessage message = new SendMessage
            {
                sender_id = 99,
                sender_name = "pipeline",
                datetime = DateTime.UtcNow.ToString().Replace("/", "-"),
                message = "test from pipeline",
                info = new List<string> { "Hello", "World" }
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            HttpResponseMessage result = _client.TestSendMessage(_baseURL + "/99", content);
            Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            Console.WriteLine(result);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        //Test 3: DynamoDB Primary Key integrity test

        [TestCase]
        public void TestNegativeSendMessage()
        {
            SendMessage message = new SendMessage
            {
                sender_name = "pipeline",
                sender_id = null,
                message = "test negative from pipeline",
                info = new List<string> { "Hello", "World" }
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            HttpResponseMessage result = _client.TestSendMessage(_baseURL + "/998", content);
            
            Assert.IsFalse(result.IsSuccessStatusCode);
        }

        //Test 4: Searching Message in DynamoDB

        [TestCase]
        public void TestGetMessage()
        {
            int sender_id = 99;
            string datetime = "04-25-2020 15:50:49";
            HttpResponseMessage result = _client.TestGetMessage(_baseURL + "/" + sender_id + "/" + datetime);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        //Test 5: Searching based on sender_name in DynamoDB

        [TestCase]
        public void TestInbox()
        {
            string sender_name = "pipeline";
            HttpResponseMessage result = _client.TestInbox(_baseURL + "/" + sender_name + "/inbox");
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        //Test 6: Updating message in DynamoDB

        [TestCase]
        public void TestUpdateMessage()
        {
            int sender_id = 99;
            UpdateMessage updatedMessage = new UpdateMessage
            {
                datetime = "04-25-2020 15:50:49",
                message = "Updated from pipeline"
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(updatedMessage), Encoding.UTF8, "application/json");

            HttpResponseMessage result = _client.TestUpdateMessage(_baseURL + "/" + sender_id, content);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        //Test 7: Get Message Negative testing in DynamoDB

        [TestCase]
        public void TestNegativeGetMessage()
        {
            int sender_id = 99;
            string datetime = "";
            //string datetime = "04-25-2020 15:50:49";
            HttpResponseMessage result = _client.TestGetMessage(_baseURL + "/" + sender_id + "/" + datetime);
            Assert.IsFalse(result.IsSuccessStatusCode);
        }

        //Test 8: Get Message Negative testing in DynamoDB

        [TestCase]
        public void TestNegative2GetMessage()
        {
            Nullable<int> sender_id = null;
            string datetime = "04-25-2020 15:50:49";
            HttpResponseMessage result = _client.TestGetMessage(_baseURL + "/" + sender_id + "/" + datetime);
            Assert.IsFalse(result.IsSuccessStatusCode);
        }

        //Test 9: Updating message negtaive test in DynamoDB

        [TestCase]
        public void TestNegativeUpdateMessage()
        {
            Nullable<int> sender_id = null;
            UpdateMessage updatedMessage = new UpdateMessage
            {
                datetime = "04-25-2020 15:50:49",
                message = "Updated from pipeline"
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(updatedMessage), Encoding.UTF8, "application/json");

            HttpResponseMessage result = _client.TestUpdateMessage(_baseURL + "/" + sender_id, content);
            Assert.IsFalse(result.IsSuccessStatusCode);
        }

        //Test 10: Updating message negative test in DynamoDB

        [TestCase]
        public void TestNegative2UpdateMessage()
        {
            int sender_id = 99;
            UpdateMessage updatedMessage = new UpdateMessage
            {
                //datetime = "04-25-2020 15:50:49",
                message = "Updated from pipeline"
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(updatedMessage), Encoding.UTF8, "application/json");

            HttpResponseMessage result = _client.TestUpdateMessage(_baseURL + "/" + sender_id, content);
            Assert.IsFalse(result.IsSuccessStatusCode);
        }
    }
}