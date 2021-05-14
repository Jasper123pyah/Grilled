using System;
using GrilledCommon.Models;
using GrilledData;
using GrilledData.Data;

namespace GrilledLogic
{
    public class ForYou
    {
        private readonly ForYouData forYouData = new ForYouData();
        private ForYouModel NewForYou(string accId, ProductModel productModel, GrilledContext grilledContext)
        {
            ForYouModel forYouModel = new ForYouModel()
            {
                Id = Guid.NewGuid(),
                ProductId = productModel.Id,
                ClickCount = 1
            };
            forYouData.AddForYou(accId, forYouModel, grilledContext);
            return forYouModel;
        }

        public void UpdateForYou(string accId, ProductModel productModel, GrilledContext grilledContext)
        {
            ForYouModel forYouModel = forYouData.GetForYou(accId, productModel, grilledContext);

            if (forYouModel == null)
                forYouModel = NewForYou(accId, productModel, grilledContext);
            forYouData.UpdateForYou(forYouModel, grilledContext);
        }
    }
}