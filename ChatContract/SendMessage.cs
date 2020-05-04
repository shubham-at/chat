using System;
using System.Collections.Generic;

namespace ChatContract
{
    public class SendMessage
    {
        public Nullable<int> sender_id { get; set; }

        public string sender_name { get; set; }

        public string message { get; set; }

        public string datetime { get; set; }

        public List<string> info { get; set; }
    }
}
