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

    public List<PriceModel> GetAllPricesByUser(int idUser)
    {
        using (var uw = new UnitOfWork())
        {
            var repo = uw.GetRepository<Price>();
            var listUser = repo.GetAll().ToList().Where(x => x.UserId == idUser).ToList();
            var listPrice = new List<PriceModel>();
            foreach (var user in listUser)
            {
                var userPrice = PriceMapper.MapPrice(user);
                listPrice.Add(userPrice);
            }
            return listPrice;
        }
    }

    public PriceModel UpdatePrice(PriceModel updatePrice)
    {
        using (var uw = new UnitOfWork())
        {
            var repo = uw.GetRepository<Price>();
            repo.Update(PriceMapper.MapPriceDataModel(updatePrice));
            uw.Save();
            return updatePrice;
        }
    }
}
