using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class PriceMapper
    {
        public static PriceModel MapPrice (Price price)
        {
            return new PriceModel { Id = price.Id, Amount = price.Amount, Details = price.Details, Order = price.Order, UserId = price.UserId };
        }

        public static Price MapPriceDataModel (PriceModel price)
        {
            return new Price { Id = price.Id, UserId = price.UserId, Order = price.Order, Amount = price.Amount, Details = price.Details };
        }
    }
}