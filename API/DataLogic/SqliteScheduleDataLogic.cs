namespace API.DataLogic
{
    using API.DataLogic.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using API.DataLogic.ViewModels;
    using API.Controllers.Models;

    public class SqliteScheduleDataLogic : IScheduleDataLogic
    {
        /// <summary>
        /// Gets all the content for a given beacon at a givent time 
        /// </summary>
        /// <param name="beaconId">ID of the beacon</param>
        /// <param name="currentTime">Current time</param>
        /// <returns></returns>
        public ViewModels.ScheduledContent GetScheduledContent(int beaconId, DateTime currentTime)
        {
            using (var db = new ApplicationDbContext())
            {
                var beacon = db.Beacons.FirstOrDefault(b => b.Id == beaconId);
                var contentIds = db.ScheduledItems.Where(s => s.BeaconId == beaconId && s.StartDateTime <= currentTime && s.EndDateTime >= currentTime)?.Select(s => s.ContentId);

                if (contentIds != null && beacon != null)
                {
                    var content = db.Content.Where(c => contentIds.Contains(c.Id))?.ToList();
                    if (content != null)
                    {
                        return new ViewModels.ScheduledContent()
                        {
                            Location = beacon.Location,
                            Content = content
                        };
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all the scheduled content where the content is scheduled after datetime.now
        /// </summary>
        /// <returns>Future content, sorted by beacon</returns>
        public IEnumerable<BeaconAvailability> GetFutureScheduledContent()
        {
            // This is a complex query, so need to draw on beacon and content data
            List<BeaconAvailability> beaconAvailability = new List<BeaconAvailability>();

            using (var db = new ApplicationDbContext())
            {
                foreach(var beacon in db.Beacons)
                {
                    var availability = new BeaconAvailability();
                    availability.BeaconId = beacon.Id;
                    availability.FriendlyName = beacon.FriendlyName;
                    availability.Location = beacon.Location;
                    availability.Bookings = db.ScheduledItems.Where(item => item.BeaconId == beacon.Id && item.EndDateTime >= DateTime.Now)
                    .Select(b => new BeaconBooking()
                    {
                        Start = b.StartDateTime,
                        End = b.EndDateTime,
                        Description = db.Content.FirstOrDefault(c => c.Id == b.ContentId).Title,
                        ContentId = b.ContentId
                    }).ToList();

                    beaconAvailability.Add(availability);
                }
            }

            return beaconAvailability;
        }

        /// <summary>
        /// Schedules the content as described by the bookings
        /// </summary>
        /// <param name="bookings">Bookings for content</param>
        public SubmissionStatus ScheduleContent(IEnumerable<BeaconBookingModel> bookings)
        {
            // This is a complex query, so need to draw on beacon and content data
            IBeaconDataLogic beaconDataLogic = new SqliteBeaconDataLogic();
            IContentDataLogic contentDataLogic = new SqliteContentDataLogic();
            SubmissionStatus status = new SubmissionStatus()
            {
                StatusCode = SubmissionStatusCode.Success
            };

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    List<ScheduledItem> newScheduledItems = new List<ScheduledItem>();
                    var bookingsList = bookings.ToList();
                    foreach (var booking in bookingsList)
                    {
                        var beacon = beaconDataLogic.GetBeacon(booking.BeaconId);
                        if (beacon == null)
                        {
                            status.StatusCode = SubmissionStatusCode.Warning;
                            status.Messages.Add($"Beacon with ID {booking.BeaconId} was not found and so this booking will be skipped.");
                            continue;
                        }

                        var content = contentDataLogic.GetContent(booking.ContentId);
                        if (content == null)
                        {
                            status.StatusCode = SubmissionStatusCode.Warning;
                            status.Messages.Add($"Content with ID {booking.ContentId} was not found and so this booking will be skipped.");
                            continue;
                        }

                        // Remove all already scheduled items for this beacon
                        var items = db.ScheduledItems.Where(scheduledItem => scheduledItem.ContentId == booking.ContentId)?.ToList();
                        if (items != null || items.Count > 0)
                        {
                            db.ScheduledItems.RemoveRange(items);
                            db.SaveChanges();
                        }

                        // So, since this is an update, add new scheduled items
                        newScheduledItems.Add(new ScheduledItem()
                        {
                            BeaconId = booking.BeaconId,
                            ContentId = booking.ContentId,
                            StartDateTime = booking.Start,
                            EndDateTime = booking.End
                        });
                    }

                    // Add the new items
                    db.ScheduledItems.AddRange(newScheduledItems);

                    // Save the deletions and new items
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                status.StatusCode = SubmissionStatusCode.Failure;
                status.Messages.Add($"Critial error creating new scheduled items: {ex.Message}");
            }

            return status;
        }
    }
}