namespace API.Helpers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using API.Controllers.Models;
    using API.DataLogic.Models;

    public static class BeaconScheduleGenerator
    {
        public static IList<BeaconSchedule> Generate(IList<BeaconAvailability> beaconAvailability)
        {
            foreach (var beacon in beaconAvailability)
            {
                BeaconSchedule beaconSchedule = new BeaconSchedule()
                {
                    BeaconId = beacon.BeaconId,
                    BeaconLocation = beacon.Location,
                    BeaconFriendlyName = beacon.FriendlyName,
                    Timeslots = new List<Timeslot>()
                };

                // Now populate the timeslots for this beacon based on week, day, hour
                // Weeks always start on monday (yay added complexity)
                var currentDay = DateTime.Now.DayOfWeek;
                int firstWeekDays = 7;
                switch (currentDay)
                {
                    case DayOfWeek.Monday:
                        {
                            firstWeekDays = 6;
                            break;
                        }
                    case DayOfWeek.Tuesday:
                        {
                            firstWeekDays = 5;
                            break;
                        }
                    case DayOfWeek.Wednesday:
                        {
                            firstWeekDays = 4;
                            break;
                        }
                    case DayOfWeek.Thursday:
                        {
                            firstWeekDays = 3;
                            break;
                        }
                    case DayOfWeek.Friday:
                        {
                            firstWeekDays = 2;
                            break;
                        }
                    case DayOfWeek.Saturday:
                        {
                            firstWeekDays = 1;
                            break;
                        }
                    case DayOfWeek.Sunday:
                        {
                            firstWeekDays = 0;
                            break;
                        }
                }

                Timeslot firstWeek = new Timeslot()
                {
                    // Always use "date" for dates so they start at midnight
                    Start = DateTime.Now.Date,
                    End = DateTime.Now.AddDays(firstWeekDays).Date,
                    Unit = TimeslotUnit.Weeks,
                    Timeslots = new List<Timeslot>()
                };

                // Check if there are any bookings for the duration of one week
                var weeklyBookings = beacon.Bookings.Where(booking => booking.Start < firstWeek.Start && booking.End > firstWeek.End);
                firstWeek.Bookings = weeklyBookings.Select(booking => new TimeslotBooking()
                {
                    ContentId = booking.ContentId,
                    ContentTitle = booking.Description
                }).ToList();

                // Add all the required days to the week
                int day = 0;
                while (day <= firstWeekDays)
                {
                    Timeslot slot = new Timeslot();
                    slot.Unit = TimeslotUnit.Days;

                    // Create start day and set to the date so it defaults to midnight
                    slot.Start = DateTime.Now.AddDays(day).Date;

                    // Increment day
                    day += 1;

                    // End date
                    slot.End = DateTime.Now.AddDays(day).Date;

                    var bookings = beacon.Bookings.Where(booking => booking.Start < slot.Start && booking.End > slot.End);
                    slot.Bookings = bookings.Select(booking => new TimeslotBooking()
                    {
                        ContentId = booking.ContentId,
                        ContentTitle = booking.Description
                    }).ToList();

                    firstWeek.Timeslots.Add(slot);
                }
            }

            return null;
        }
    }
}