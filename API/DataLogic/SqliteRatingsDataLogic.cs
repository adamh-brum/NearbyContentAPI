namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Contains the SQLite implementation of the IRatingsDataLogic
    /// </summary>
    public class SqliteRatingsDataLogic : IRatingsDataLogic
    {
        /// <summary>
        /// Deletes all ratings where the content ID matches the given content ID
        /// </summary>
        /// <param name="contentId">The ID of the ratings content to delete</param>
        public void DeleteRatings(int contentId)
        {
            using (var db = new ApplicationDbContext())
            {
                var criteria = db.Ratings.Where(rating => rating.ContentId == contentId);
                db.Ratings.RemoveRange(criteria);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns every single rating
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Rating> GetRatings()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Ratings.ToList();
            }
        }

        /// <summary>
        /// Updates a given rating 
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="rating"></param>
        public void UpdateRating(int contentId, int rating)
        {
            using (var db = new ApplicationDbContext())
            {
                var contentRating = db.Ratings.FirstOrDefault(r => r.ContentId == contentId);
                if (contentRating == null)
                {
                    contentRating = new Rating()
                    {
                        ContentId = contentId
                    };

                    db.Ratings.Add(contentRating);
                }

                // Now update the content rating
                contentRating.RatingCount += rating;

                // Save the changes
                db.SaveChanges();
            }
        }
    }
}