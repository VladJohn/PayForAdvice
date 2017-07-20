using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Mappings;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UploadService
    {
        public UploadModel Register(UploadModel upload)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<Upload>();
                repo.Add(UploadMapper.MapUploadDataModel(upload));
                uw.Save();
                return upload;
            }
        }


    }
}
