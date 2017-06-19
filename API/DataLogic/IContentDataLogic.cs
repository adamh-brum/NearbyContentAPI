namespace API.DataLogic
{
    using API.DataLogic.ViewModels;
    using API.DataLogic.Models;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Manages content data
    /// </summary>
    public interface IContentDataLogic
    {
        /// <summary>
        /// Saves content to the DB
        /// </summary>
        /// <param name="title">The title for the content</param>
        /// <param name="content">Content</param>
        /// <returns>Content ID</returns>
        int AddContent(string title, string content);

        /// <summary>
        /// Returns the content that matches the given ID
        /// </summary>
        /// <param name="contentId">Content ID</param>
        /// <returns>Content data model</returns>
        Content GetContent(int contentId);

        /// <summary>
        /// Returns all content
        /// </summary>
        /// <returns>All content</returns>
        IEnumerable<Content> GetContent();

        /// <summary>
        /// Deletes the content with the given ID
        /// </summary>
        /// <param name="contentId">Content ID</param>
        void DeleteContent(int contentId);
    }
}