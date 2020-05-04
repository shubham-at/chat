using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatContract;
using ChatMiddleware.Libs.Models;

namespace ChatMiddleware.Libs.Mapper
{
    public class Mapper : IMapper
    {
        public Mapper()
        {
        }

        public IEnumerable<MessageFormat> ToMessageContract(IEnumerable<Message> items)
        {
            return items.Select(ToMessageContract);
        }

        public MessageFormat ToMessageContract(Message item)
        {
            return new MessageFormat
            {
                sender_id = item.sender_id,
                sender_name = item.sender_name,
                info = item.info,
                message = item.message,
                datetime = item.datetime
            };
        }

        public Message ToMessageDbModel(int senderId, SendMessage sendMessage, string date)
        {
            return new Message
            {
                sender_id = sendMessage.sender_id,
                sender_name = sendMessage.sender_name,
                message = sendMessage.message,
                datetime = date,
                info = sendMessage.info
            };
        }

        public Message ToMessageDbModel(int senderId, Message response, UpdateMessage updateMessage)
        {
            return new Message
            {
                sender_id = response.sender_id,
                sender_name = response.sender_name,
                message = updateMessage.message,
                datetime = response.datetime,
                info = response.info
            };
        }
    }
}
