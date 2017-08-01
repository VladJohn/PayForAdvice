using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.FacebookIntegration.Models
{

        public class PictureData
    {
            public int height { get; set; }
            public bool is_silhouette { get; set; }
            public string url { get; set; }
            public int width { get; set; }
        }

        public class Picture
        {
            public PictureData data { get; set; }
        }

        public class UserFacebookModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public Picture picture { get; set; }
            public string Id { get; set; }
        }
  
}