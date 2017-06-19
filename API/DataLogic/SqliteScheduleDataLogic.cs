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
        public ViewModels.ScheduledContent GetScheduledContent(Guid beaconId, DateTime currentTime)
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
            IBeaconDataLogic beaconDataLogic = new SqliteBeaconDataLogic();
            IContentDataLogic contentDataLogic = new SqliteContentDataLogic();

            List<BeaconAvailability> beaconAvailability = new List<BeaconAvailability>();

            using (var db = new ApplicationDbContext())
            {
                beaconAvailability = db.Beacons.Select(b => new BeaconAvailability()
                {
                    BeaconId = b.Id,
                    Location = b.Location,
                    FriendlyName = b.FriendlyName,
                    Bookings = new List<BeaconBooking>()
                }).ToList();

                var schedule = db.ScheduledItems.Where(item => item.StartDateTime >= DateTime.Now);
                foreach (var item in schedule)
                {
                    var beaconAvailabilityRecord = beaconAvailability.FirstOrDefault(c => c.BeaconId == item.BeaconId);

                    if (beaconAvailabilityRecord == null)
                    {
                        var beacon = beaconDataLogic.GetBeacon(item.BeaconId);

                        if (beacon == null)
                        {
                            // This should never happen
                            // So if it's happening, it indicates a bug
                            // Therefore fail LOUDLY! 
                            throw new ArgumentNullException("A beacon referenced in scheduled content no longer exists...");
                        }
                    }

                    var content = contentDataLogic.GetContent(item.ContentId);
                    beaconAvailabilityRecord.Bookings.Add(new BeaconBooking()
                    {
                        Start = item.StartDateTime,
                        End = item.EndDateTime,
                        Description = content.Title,
                        ContentId = item.ContentId
                    });
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
                    foreach (var booking in bookings)
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

                        bool datesValid = booking.Start > DateTime.Now && booking.End > booking.Start;
                        if (!datesValid)
                        {
                            status.StatusCode = SubmissionStatusCode.Warning;
                            status.Messages.Add($"Booking for content ID {booking.ContentId} failed. Bookings must be in the future and the end date must be after the start date.");
                            continue;
                        }

                        // All valid!
                        newScheduledItems.Add(new ScheduledItem()
                        {
                            BeaconId = booking.BeaconId,
                            ContentId = booking.ContentId,
                            StartDateTime = booking.Start,
                            EndDateTime = booking.End
                        });
                    }

                    db.ScheduledItems.AddRange(newScheduledItems);
                    db.SaveChanges();
                }
            }
            catch
            {
                status.StatusCode = SubmissionStatusCode.Failure;
                status.Messages.Add("Critial error creating new scheduled items.");
            }

            return status;
        }
    }
}