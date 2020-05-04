using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace ChatMiddleware.Libs.Models
{
    public class MessageOld
    {
        [DynamoDBHashKey]
            public int sender_id { get; set; }

            public List<string> info { get; set; }

            public string sender_name { get; set; }

            public string message { get; set; }
        
    }
}
