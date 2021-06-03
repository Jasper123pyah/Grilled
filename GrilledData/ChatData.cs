using GrilledCommon.Models;
using GrilledData.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrilledData
{
    public class ChatData
    {
        public void AddChat(string messagetext, string receivername, Guid productId, string accId, GrilledContext context)
        {
            AccountModel account = context.Account.FirstOrDefault(a => a.Id == accId);
            AccountModel raccount = context.Account.FirstOrDefault(a => a.Username == receivername);

            ProductModel product = context.Product.FirstOrDefault(p => p.Id == productId);
            ChatModel chat = context.Chat.Include(a => a.Messages).FirstOrDefault(c => c.Users == $"{account.Username}_{raccount.Username}"
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
        public ChatModel SendMessage(string message, Guid messageId, string accId, GrilledContext context)
        {
            MessageModel searchmessage = context.Message.FirstOrDefault(a => a.Id == messageId);
            ChatModel chat = context.Chat.Include(a => a.Messages).FirstOrDefault(a => a.Messages.Contains(searchmessage));

            if (chat != null)
            {
                chat.Messages.Add(new MessageModel()
                {
                    Message = message,
                    SenderName = new AccountData().GetAccount(accId, context).Username,
                    Time = DateTime.Now
                });
            }
            context.SaveChanges();
            return chat;
        }
        public List<ChatModel> GetChats (string accId, GrilledContext context, string chatType)
        {
            if(chatType == "Buy")
            {
                AccountModel account = context.Account.Include(a => a.BuyChats)
                                      .ThenInclude(a => a.Messages)
                                      .Include(a => a.BuyChats)
                                      .ThenInclude(a => a.Product)
                                      .ThenInclude(a => a.Images)
                                      .FirstOrDefault(a => a.Id == accId);
                return account.BuyChats;
            }
            if(chatType == "Sell")
            {
                AccountModel account = context.Account.Include(a => a.SellChats)
                                      .ThenInclude(a => a.Messages)
                                      .Include(a => a.SellChats)
                                      .ThenInclude(a => a.Product)
                                      .ThenInclude(a => a.Images)
                                      .FirstOrDefault(a => a.Id == accId);
                return account.SellChats;
            }
            return new List<ChatModel>();
        }
        public ChatModel SelectChat(ChatModel chat, GrilledContext context)
        {
            if (chat != null)
            {
                return context.Chat.Include(c => c.Messages).FirstOrDefault(c => c.Id == chat.Id);
            }
            return new ChatModel();
        }
    }
}
