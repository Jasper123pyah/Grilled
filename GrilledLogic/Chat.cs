using GrilledCommon.Models;
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
        AccountModel account = new AccountModel();
        ChatModel chat = new ChatModel();
        CommonFunctions functions = new CommonFunctions();

        public void StartChat(string messagetext, string receivername, Guid productId, HttpContext httpContext, GrilledContext context)
        {
            account = context.Account.FirstOrDefault(a => a.Username == httpContext.Request.Cookies["Login"]);
            AccountModel raccount = context.Account.FirstOrDefault(a => a.Username == receivername);

            ProductModel product = context.Product.FirstOrDefault(p => p.Id == productId);
            chat = context.Chat.Include(a => a.Messages).FirstOrDefault(c => c.Users == $"{account.Username}_{raccount.Username}"
                                        && c.Product == product);

            if (chat != null)
            {
                chat.Messages.Add(new MessageModel()
                {
                    Message = messagetext,
                    SenderName = account.Username,
                    Time = DateTime.Now
                });
            }
            else
            {
                chat = new ChatModel()
                {
                    Product = product,
                    Users = $"{account.Username}_{raccount.Username}",
                    Messages = new List<MessageModel>()
                };

                chat.Messages.Add(new MessageModel()
                {
                    Message = messagetext,
                    SenderName = account.Username,
                    Time = DateTime.Now
                });

                if (account.BuyChats != null)
                    account.BuyChats.Add(chat);
                else
                    account.BuyChats = new List<ChatModel>() { chat };

                if (raccount.SellChats != null)
                    raccount.SellChats.Add(chat);
                else
                    raccount.SellChats = new List<ChatModel> { chat };
                context.Chat.Add(chat);
            }
            context.SaveChanges();
        }
        public ChatModel Send(string message, Guid messageId, HttpContext httpContext, GrilledContext context)
        {
            MessageModel searchmessage = context.Message.FirstOrDefault(a => a.Id == messageId);
            ChatModel chat = context.Chat.Include(a => a.Messages).FirstOrDefault(a => a.Messages.Contains(searchmessage));

            if (chat != null)
            {
                chat.Messages.Add(new MessageModel()
                {
                    Message = message,
                    SenderName = httpContext.Request.Cookies["Login"],
                    Time = DateTime.Now
                });
            }
            context.SaveChanges();
            return chat;
        }
        public DisplayMessagesModel BuyMessages(ChatModel chat, HttpContext httpContext, GrilledContext context)
        {
            account = context.Account.Include(a => a.BuyChats).ThenInclude(a => a.Messages).Include(a => a.BuyChats)
                                     .ThenInclude(a => a.Product).ThenInclude(a => a.Images)
                                     .FirstOrDefault(a => a.Username == httpContext.Request.Cookies["Login"]);

            foreach (ChatModel _chat in account.BuyChats)
            {
                _chat.Product.Images[0].Source = functions.GetImagePath(httpContext) + _chat.Product.Images[0].Name;
            }

            DisplayMessagesModel mdisplay = new DisplayMessagesModel
            {
                Chats = account.BuyChats,
                Messages = new List<MessageModel>()
            };

            if (chat != null)
            {
                ChatModel selectedchat = context.Chat.Include(c => c.Messages).FirstOrDefault(c => c.Id == chat.Id);
                if (selectedchat != null)
                {
                    foreach (MessageModel message in selectedchat.Messages)
                    {
                        mdisplay.Messages.Add(message);
                    }
                    mdisplay.Messages = mdisplay.Messages.OrderBy(x => x.Time).ToList();
                }
            }
            return mdisplay;
        }
        public DisplayMessagesModel SellMessages(ChatModel chat, HttpContext httpContext, GrilledContext context)
        {
            account = context.Account.Include(a => a.SellChats).ThenInclude(a => a.Messages).Include(a => a.SellChats)
                                     .ThenInclude(a => a.Product).ThenInclude(a => a.Images)
                                     .FirstOrDefault(a => a.Username == httpContext.Request.Cookies["Login"]);

            foreach (ChatModel _chat in account.SellChats)
            {               
                _chat.Product.Images[0].Source = functions.GetImagePath(httpContext) + _chat.Product.Images[0].Name;
            }

            DisplayMessagesModel mdisplay = new DisplayMessagesModel
            {
                Chats = account.SellChats,
                Messages = new List<MessageModel>()
            };


            if (chat != null)
            {
                ChatModel selectedchat = context.Chat.Include(c => c.Messages).FirstOrDefault(c => c.Id == chat.Id);
                if (selectedchat != null)
                {
                    foreach (MessageModel message in selectedchat.Messages)
                    {
                        mdisplay.Messages.Add(message);
                    }
                    mdisplay.Messages = mdisplay.Messages.OrderBy(x => x.Time).ToList();
                }
            }
            return mdisplay;
        }
    }
}

