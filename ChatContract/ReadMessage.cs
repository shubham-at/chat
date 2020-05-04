using System;
using System.Collections.Generic;

namespace ChatContract
{
    public class ReadMessage
    {
        public int sender_id { get; set; }

        public string sender_name { get; set; }

        public List<string> info { get; set; }

        public string message { get; set; }
    }
}
