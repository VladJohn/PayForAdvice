using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class UploadMapper
    {
        public static UploadModel MapUpdate (Upload upload)
        {
            return new UploadModel { Id = upload.Id, AnswerId = upload.AnswerId, Name = upload.Name, UploadURL = upload.UploadURL };
        }

        public static Upload MapUploadDataModel (UploadModel upload)
        {
            return new Upload { Id = upload.Id, UploadURL = upload.UploadURL, Name = upload.Name, AnswerId = upload.AnswerId };
        }
    }
}