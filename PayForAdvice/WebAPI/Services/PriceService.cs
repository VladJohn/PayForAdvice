using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Enums;
using Repository;
using WebAPI.Mappings;
using WebAPI.Models;
using WebAPI.ValidatorsModel;

namespace WebAPI.Services
{
    public class PriceService
    {

        //add a new price for an user
        public PriceModel AddNewPrice(PriceModel newPrice)
        {
            var validator = new PriceValidator();
            var errors = validator.Check(newPrice);
            if (errors.Count != 0)
            {
                throw new ModelException(string.Join(Environment.NewLine, errors));
            }
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
            var userPrices = new PriceModelForPublicProfile();
            using (var unitOfWork = new UnitOfWork())
            {
                var priceRepository = unitOfWork.GetRepository<Price>();
                var listUser = priceRepository.GetAll().ToList().Where(x => x.UserId == idUser).ToList();
                foreach (var price in listUser)
                {
                    if (price.Order == (int)PriceOrderEnum.Basic)
                    {
                        userPrices.Base = price.Amount;
                        userPrices.DetailBase = price.Details;
                    }
                    if (price.Order == (int)PriceOrderEnum.Standard)
                    {
                        userPrices.Normal = price.Amount;
                        userPrices.DetailNormal = price.Details;
                    }
                    if (price.Order != (int) PriceOrderEnum.Premium) continue;
                    userPrices.Premium = price.Amount;
                    userPrices.DetailPremium = price.Details;
                }
                return userPrices;
            }
        }

        //it will update a price. In case that you don't have what to update, it will create a new instance and add it
        public PriceModel UpdatePrice(PriceModel updatePrice)
        {
            var validator = new PriceValidator();
            var errors = validator.Check(updatePrice);
            if (errors.Count != 0)
            {
                throw new ModelException(string.Join(Environment.NewLine, errors));
            }
            using (var unitOfWork = new UnitOfWork())
            {
                var priceRepository = unitOfWork.GetRepository<Price>();
                var foundPrice =  priceRepository.GetAll().FirstOrDefault(x => x.Order == updatePrice.Order && x.UserId == updatePrice.UserId);
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
}
