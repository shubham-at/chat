using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatContract;
using ChatMiddleware.Libs.Models;

namespace ChatMiddleware.Libs.Mapper
{
    public interface IMapper
    {
        IEnumerable<MessageFormat> ToMessageContract(IEnumerable<Message> response);
        MessageFormat ToMessageContract(Message response);
        Message ToMessageDbModel(int senderId, SendMessage sendMessage, string datetime);
        Message ToMessageDbModel(int senderId, Message response, UpdateMessage updateMessage);
    }
}
