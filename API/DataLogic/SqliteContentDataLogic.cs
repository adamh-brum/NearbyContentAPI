namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Contains the SQLite implementation of the IContentDataLogic
    /// </summary>
    public class SqliteContentDataLogic : IContentDataLogic
    {
        /// <summary>
        /// Saves content to the DB
        /// </summary>
        /// <param name="title">The title for the content</param>
        /// <param name="content">Content</param>
        /// <returns>Content ID</returns>
        public int AddContent(string title, string content)
        {
            Content newObject = new Content()
            {
                Title = title,
                Value = content
            };

            using (var db = new ApplicationDbContext())
            {
                db.Content.Add(newObject);
                db.SaveChanges();
            }

            return newObject.Id;
        }

        /// <summary>
        /// Deletes a content record from the DB
        /// </summary>
        /// <param name="contentId">Deletes content where the ID matches contentId</param>
        public void DeleteContent(int contentId)
        {
            using (var db = new ApplicationDbContext())
            {
                var contentRecord = db.Content.FirstOrDefault(content => content.Id == contentId);
                db.Content.Remove(contentRecord);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns the content that matches the given ID
        /// </summary>
        /// <param name="contentId">Content ID</param>
        /// <returns>Content data model</returns>
        public Content GetContent(int contentId)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Content.FirstOrDefault(c => c.Id == contentId);
            }
        }

        /// <summary>
        /// Returns all content
        /// </summary>
        /// <returns>All content</returns>
        public IEnumerable<Content> GetContent()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Content.ToList();
            }
        }
    }
}