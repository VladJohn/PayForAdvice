using Domain;
using Repository;
using WebAPI.Mappings;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UploadService
    {
        public UploadModel Register(UploadModel upload)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var uploadRepository = unitOfWork.GetRepository<Upload>();
                uploadRepository.Add(UploadMapper.MapUploadDataModel(upload));
                unitOfWork.Save();
                return upload;
            }
        }


    }
}
