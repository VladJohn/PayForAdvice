using System;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;
using Repository;
using WebAPI.Mappings;

public class PriceService
{
	public PriceService()
	{
	}

    public PriceModel Add(PriceModel newPrice)
    {
        using (var uw = new UnitOfWork())
        {
            var repo = uw.GetRepository<Price>();
            repo.Add(PriceMapper.MapPriceDataModel(newPrice));
            uw.Save();
        }
        return newPrice;
    }

    public PriceModelForPublicProfile GetAllPricesByUser(int idUser)
    {
        PriceModelForPublicProfile res = new PriceModelForPublicProfile();
        using (var uw = new UnitOfWork())
        {
            var repo = uw.GetRepository<Price>();
            var listUser = repo.GetAll().ToList().Where(x => x.UserId == idUser).ToList();
            var listPrice = new List<PriceModel>();
            foreach (var price in listUser)
            {
                if (price.Order.Equals("base"))
                {
                    res.Base = price.Amount;
                    res.DetailBase = price.Details;
                }
                if (price.Order.Equals("normal"))
                {
                    res.Normal = price.Amount;
                    res.DetailNormal = price.Details;
                }
                if (price.Order.Equals("premium"))
                {
                    res.Premium = price.Amount;
                    res.DetailPremium = price.Details;
                }
            }
            return res;
        }
    }

    public PriceModel UpdatePrice(PriceModel updatePrice)
    {
        using (var uw = new UnitOfWork())
        {
            var repo = uw.GetRepository<Price>();
            var found =  repo.GetAll().Where(x => x.Order == updatePrice.Order && x.UserId == updatePrice.UserId).FirstOrDefault();
            if (found != null)
            {
                found.Amount = updatePrice.Amount;
                found.Order = updatePrice.Order;
                found.Details = updatePrice.Details;
                found.UserId = updatePrice.UserId;
                repo.Update(found);
            }
            else
            {
                var price = new Price { Amount = updatePrice.Amount, Details = updatePrice.Details, Order = updatePrice.Order, UserId = updatePrice.UserId};
                repo.Add(price);
            }
            uw.Save();
            return updatePrice;
        }
    }
}
