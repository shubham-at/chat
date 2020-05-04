using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using ChatContract;
using ChatMiddleware.Libs.Models;

namespace ChatMiddleware.Libs.Repositories
{
    public class ChatRepository : IChatRepository
    {

        private readonly DynamoDBContext _context;
        private const string TableName = "chat_service";

        public ChatRepository(IAmazonDynamoDB dynamoClient)
        {
            _context = new DynamoDBContext(dynamoClient);
        }

        public async Task<Task<PutItemResponse>> AddMessage(Message messageDb)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>()
                  {
                      { "sender_id", new AttributeValue { N = messageDb.sender_id.ToString() }},
                      { "datetime", new AttributeValue { S = messageDb.datetime.Replace("/","-") }},
                      { "sender_name", new AttributeValue { S = messageDb.sender_name }},
                      { "message", new AttributeValue { S = messageDb.message }},
                      {
                        "info",
                            new AttributeValue
                            { SS = messageDb.info }
                      }
                  }
            };
            return client.PutItemAsync(request);
            //await _context.SaveAsync(messageDb);
        }

        public async Task<IEnumerable<Message>> AllMessages()
        {
            return await _context.ScanAsync<Message>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<Message> GetMessage(int senderId, string datetime)
        {
            return await _context.LoadAsync<Message>(senderId, datetime);
        }

        public async Task<IEnumerable<Message>> LoadInbox(string senderName)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "sender_name-index"
            };
            return await _context.QueryAsync<Message>(senderName, config).GetRemainingAsync();
        }

        public async Task<IEnumerable<Message>> SearchMessage(int senderId, string message)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("message", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Contains, message)
                    //new ScanCondition("message", Amazon.DynamoDBv2.DocumentModel.ScanOperator.BeginsWith,message)
                }
            };

            return await _context.QueryAsync<Message>(senderId, config).GetRemainingAsync();
        }

        public async Task UpdateMovie(Message updatedMessage)
        {
            await _context.SaveAsync(updatedMessage);
        }
    }
}
