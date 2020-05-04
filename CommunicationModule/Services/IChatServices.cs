using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatContract;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationModule.Services
{
    public interface IChatServices
    {
        Task<IEnumerable<MessageFormat>> AllMessages();
        Task<MessageFormat> GetMessage(int senderId, string datetime);
        Task<IEnumerable<MessageFormat>> SearchMessage(int senderId, string message);
        Task<Task<Amazon.DynamoDBv2.Model.PutItemResponse>> SendMessage(int senderId, SendMessage sendMessage, string datetime);
        Task UpdateMessage(int senderId, UpdateMessage updateMessage);
        Task<IEnumerable<MessageFormat>> LoadInbox(string senderName);
    }
}
