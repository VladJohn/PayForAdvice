using Domain;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;
using Repository;
using WebAPI.Mappings;

public class PriceService
{
	public PriceService()
	{
	}

    //add a new price for an user
    public PriceModel AddNewPrice(PriceModel newPrice)
    {
        using (var unitOfWork = new UnitOfWork())
        {
            var priceRepository = unitOfWork.GetRepository<Price>();
            priceRepository.Add(PriceMapper.MapPriceDataModel(newPrice));
            unitOfWork.Save();
        }
        return newPrice;
    }

    //it will return an entity with the prices for a user's category
    public PriceModelForPublicProfile GetAllPricesByUser(int idUser)
    {
        PriceModelForPublicProfile userPrices = new PriceModelForPublicProfile();
        using (var unitOfWork = new UnitOfWork())
        {
            var priceRepository = unitOfWork.GetRepository<Price>();
            var listUser = priceRepository.GetAll().ToList().Where(x => x.UserId == idUser).ToList();
            var listPrice = new List<PriceModel>();
            foreach (var price in listUser)
            {
                if (price.Order.Equals("base"))
                {
                    userPrices.Base = price.Amount;
                    userPrices.DetailBase = price.Details;
                }
                if (price.Order.Equals("normal"))
                {
                    userPrices.Normal = price.Amount;
                    userPrices.DetailNormal = price.Details;
                }
                if (price.Order.Equals("premium"))
                {
                    userPrices.Premium = price.Amount;
                    userPrices.DetailPremium = price.Details;
                }
            }
            return userPrices;
        }
    }

    //it will update a price. In case that you don't have what to update, it will create a new instance and add it
    public PriceModel UpdatePrice(PriceModel updatePrice)
    {
        using (var unitOfWork = new UnitOfWork())
        {
            var priceRepository = unitOfWork.GetRepository<Price>();
            var foundPrice =  priceRepository.GetAll().Where(x => x.Order == updatePrice.Order && x.UserId == updatePrice.UserId).FirstOrDefault();
            if (foundPrice != null)
            {
                foundPrice.Amount = updatePrice.Amount;
                foundPrice.Order = updatePrice.Order;
                foundPrice.Details = updatePrice.Details;
                foundPrice.UserId = updatePrice.UserId;
                priceRepository.Update(foundPrice);
            }
            else
            {
                var price = new Price { Amount = updatePrice.Amount, Details = updatePrice.Details, Order = updatePrice.Order, UserId = updatePrice.UserId};
                priceRepository.Add(price);
            }
            unitOfWork.Save();
            return updatePrice;
        }
    }
}
