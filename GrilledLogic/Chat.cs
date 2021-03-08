using GrilledCommon.Models;
using GrilledData;
using GrilledData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrilledLogic
{
    public class Chat
    {
        ChatData chatData = new ChatData();
        CommonFunctions functions = new CommonFunctions();

        public void StartChat(string messagetext, string receivername, Guid productId, HttpContext httpContext, GrilledContext context)
        {
            chatData.AddChat(messagetext, receivername, productId, httpContext.Request.Cookies["Login"], context);
        }
        public ChatModel Send(string message, Guid messageId, HttpContext httpContext, GrilledContext context)
        {    
            return chatData.SendMessage(message, messageId, httpContext.Request.Cookies["Login"], context);
        }
        public DisplayMessagesModel BuyMessages(ChatModel chat, HttpContext httpContext, GrilledContext context)
        {
            DisplayMessagesModel mdisplay = new DisplayMessagesModel
            {
                Chats = chatData.GetChats(httpContext.Request.Cookies["Login"],context,"Buy"),
                Messages = new List<MessageModel>()
            };

            foreach (ChatModel _chat in mdisplay.Chats)
            {
                _chat.Product.Images[0].Source = functions.GetImagePath(httpContext) + _chat.Product.Images[0].Name;
            }

            if (chat != null) // chat only if chat  is selected
            {
                foreach (MessageModel message in chatData.SelectChat(chat, context).Messages)
                {
                    mdisplay.Messages.Add(message);
                }
                mdisplay.Messages = mdisplay.Messages.OrderBy(x => x.Time).ToList();
            } 

            return mdisplay;
        }
        public DisplayMessagesModel SellMessages(ChatModel chat, HttpContext httpContext, GrilledContext context)
        {

            DisplayMessagesModel mdisplay = new DisplayMessagesModel
            {
                Chats = chatData.GetChats(httpContext.Request.Cookies["Login"], context, "Sell"),
                Messages = new List<MessageModel>()
            };

            foreach (ChatModel _chat in mdisplay.Chats)
            {
                _chat.Product.Images[0].Source = functions.GetImagePath(httpContext) + _chat.Product.Images[0].Name;
            }

            if (chat != null) // chat only if chat  is selected
            {
                foreach (MessageModel message in chatData.SelectChat(chat, context).Messages)
                {
                    mdisplay.Messages.Add(message);
                }
                mdisplay.Messages = mdisplay.Messages.OrderBy(x => x.Time).ToList();
            }

            return mdisplay;
        }
    }
}

