namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    public interface IRatingsDataLogic
    {
        /// <summary>
        /// Returns all ratings for all content
        /// </summary>
        /// <returns>all ratings for all content</returns>
        IEnumerable<Rating> GetRatings();

        /// <summary>
        /// Updates a content rating
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="rating"></param>
        void UpdateRating(int contentId, int rating);

        /// <summary>
        /// Deletes all ratings records for a specified content ID
        /// Required to faciliate content deletion
        /// </summary>
        /// <param name="contentId">Content ID</param>
        void DeleteRatings(int contentId);
    }
}