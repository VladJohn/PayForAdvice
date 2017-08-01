using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAPI.FacebookIntegration.Models;
using WebAPI.Models;

namespace WebAPI.FacebookIntegration.Service
{
    public class UserFacebookDetails
    {

        private FacebookService fs;

        public UserFacebookDetails()
        {
            fs = new FacebookService();
        }


    }
}