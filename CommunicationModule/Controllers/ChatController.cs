using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ChatContract;
using CommunicationModule.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommunicationModule.Controllers
{
    //[Route("v1/")]
    //[ApiController]
    [Route("v1/Messages")]
    public class ChatController : Controller
    {

        private readonly IChatServices _chatServices;

        public ChatController(IChatServices services)
        {
            // major release
            _chatServices = services;
        }

        //[Route("Messages")]
        [HttpGet]
        public async Task<IEnumerable<MessageFormat>> GetAllItemsFromDB()
        {
            var results = await _chatServices.AllMessages();
            return results;
        }

        [Route("{senderId}/{datetime}")]
        [HttpGet]
        public async Task<MessageFormat> GetMessage(int senderId, string datetime)
        {
            var result = await _chatServices.GetMessage(senderId, datetime);
            return result;
        }

        [Route("id/{senderId}/message/{message}")]
        [HttpGet]
        public async Task<IEnumerable<MessageFormat>> SearchMessage(int senderId, string message)
        {
            var result = await _chatServices.SearchMessage(senderId, message);
            return result;
        }

        [Route("{senderId}")]
        [HttpPost]
        public async Task<IActionResult> SendMessage(int senderId, [FromBody] SendMessage sendMessage)
        {
            //var datetime = DateTime.UtcNow.ToString().Replace("/", "-");
            var datetime = "1990-03-01 00:00:00";
            var response = await _chatServices.SendMessage(senderId, sendMessage, datetime);
            var result = response.Result;
            if (result.HttpStatusCode.ToString().Equals("OK"))
                return Ok();
            else
                return NotFound();
        }

        [HttpPatch]
        [Route("{senderId}")]
        public async Task<IActionResult> UpdateMessage(int senderId, [FromBody] UpdateMessage updateMessage)
        {
            await _chatServices.UpdateMessage(senderId, updateMessage);
            return Ok();
        }

        [HttpGet]
        [Route("{senderName}/inbox")]
        public async Task<IEnumerable<MessageFormat>> LoadInbox(string senderName)
        {
            var result = await _chatServices.LoadInbox(senderName);
            return result;
        }
    }
}
