using System.Collections.Generic;
using System.Linq;
using GrilledCommon.Models;
using GrilledData.Data;

namespace GrilledData
{
    public class ForYouData
    {
        private AccountData _accountData = new AccountData();
        public void AddForYou(string accId, ForYouModel forYouModel, GrilledContext grilledContext)
        {
            AccountModel account = _accountData.GetAccount(accId, grilledContext);
            
            if (account.ForYouData == null || account.ForYouData.Count == 0)
                account.ForYouData = new List<ForYouModel>();
            grilledContext.ForYou.Add(forYouModel);
            account.ForYouData.Add(forYouModel);
            grilledContext.SaveChanges();
        }

        public ForYouModel GetForYou(string accId, ProductModel productModel, GrilledContext grilledContext)
        {
            AccountModel account = _accountData.GetAccount(accId, grilledContext);
            ForYouModel foryou = account.ForYouData.FirstOrDefault(p => p.ProductId == productModel.Id);
            return foryou;
        }
        public void UpdateForYou(ForYouModel forYouModel, GrilledContext grilledContext)
        {
            forYouModel.ClickCount++;
            grilledContext.SaveChanges();
        }
    }
}