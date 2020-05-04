using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using ChatMiddleware.Libs.Models;

namespace ChatMiddleware.Libs.Repositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<Message>> AllMessages();
        Task<Message> GetMessage(int senderId, string senderName);
        Task<IEnumerable<Message>> SearchMessage(int senderId, string message);
        Task UpdateMovie(Message updatedMessage);
        Task<IEnumerable<Message>> LoadInbox(string senderName);
        Task<Task<PutItemResponse>> AddMessage(Message messageDb);
    }
}
