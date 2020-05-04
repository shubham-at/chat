using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace ChatMiddleware.Libs.Models
{
    [DynamoDBTable("chat_service")]
    public class Message
    {
        [DynamoDBHashKey]
        public Nullable<int> sender_id { get; set; }

        public string datetime { get; set; }

        public List<string> info { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string sender_name { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string message { get; set; }

        public static implicit operator Dictionary<object, object>(Message v)
        {
            throw new NotImplementedException();
        }
    }
}
