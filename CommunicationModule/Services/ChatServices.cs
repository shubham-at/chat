using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatContract;
using ChatMiddleware.Libs.Mapper;
using ChatMiddleware.Libs.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationModule.Services
{
    public class ChatServices : IChatServices
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatServices(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageFormat>> AllMessages()
        {
            var response = await _chatRepository.AllMessages();
            return _mapper.ToMessageContract(response);
        }

        public async Task<MessageFormat> GetMessage(int senderId, string datetime)
        {
            var response = await _chatRepository.GetMessage(senderId, datetime);
            return _mapper.ToMessageContract(response);
            //return null;
        }

        public async Task<IEnumerable<MessageFormat>> LoadInbox(string senderName)
        {
            var response = await _chatRepository.LoadInbox(senderName);
            return _mapper.ToMessageContract(response);
        }

        public async Task<IEnumerable<MessageFormat>> SearchMessage(int senderId, string message)
        {
            var response = await _chatRepository.SearchMessage(senderId, message);
            return _mapper.ToMessageContract(response);
        }

        public async Task<Task<Amazon.DynamoDBv2.Model.PutItemResponse>> SendMessage(int senderId, SendMessage sendMessage, string datetime)
        {
            var messageDb = _mapper.ToMessageDbModel(senderId, sendMessage, datetime);
            var response = await _chatRepository.AddMessage(messageDb);
            return response;
        }

        public async Task UpdateMessage(int senderId, UpdateMessage updateMessage)
        {
            var response = await _chatRepository.GetMessage(senderId, updateMessage.datetime);
            var updatedMessage = _mapper.ToMessageDbModel(senderId, response, updateMessage);
            await _chatRepository.UpdateMovie(updatedMessage);
        }
    }
}
