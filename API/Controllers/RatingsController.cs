using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.Models;
using API.DataLogic.Models;
using API.DataLogic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class RatingsController : Controller
    {
        private IRatingsDataLogic dataLogic;

        public RatingsController()
        {
            this.dataLogic = new SqliteRatingsDataLogic();;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Rating> Get()
        {
            return this.dataLogic.GetRatings();
        }

        // POST api/values
        [HttpPut]
        public void Put(int contentId, int rating)
        {
            if(rating != 1 && rating != -1){
                // Users can only submit a rating of 1 or -1
                throw new ArgumentOutOfRangeException("Ratings must fall within the correct range");
            }

            this.dataLogic.UpdateRating(contentId, rating);
        }
    }
}
