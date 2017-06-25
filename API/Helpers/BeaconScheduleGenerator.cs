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
            IList<BeaconSchedule> schedule = new List<BeaconSchedule>();
            DateTime startTime = DateTime.Now.GetStartOfWeek();

            foreach (var beacon in beaconAvailability)
            {
                schedule.Add(new BeaconSchedule()
                {
                    BeaconId = beacon.BeaconId,
                    BeaconFriendlyName = beacon.FriendlyName,
                    BeaconLocation = beacon.Location,
                    Timeslots = PopulateWeeksForBeacon(beacon, startTime)
                });
            }

            return schedule;
        }

        private static IList<Timeslot> PopulateWeeksForBeacon(BeaconAvailability beaconAvailability, DateTime startTime)
        {
            var slots = new List<Timeslot>();
            DateTime currentDate = startTime;
            while (currentDate < DateTime.Now.AddMonths(3))
            {
                Timeslot newTimeslot = new Timeslot();
                newTimeslot.Start = currentDate;
                newTimeslot.End = currentDate.AddDays(7);
                newTimeslot.Unit = TimeslotUnit.Weeks;
                newTimeslot.Bookings = beaconAvailability.Bookings.Where(b => b.Start >= newTimeslot.Start && b.End <= newTimeslot.End).Select(booking => new TimeslotBooking()
                {
                    ContentId = booking.ContentId,
                    ContentTitle = booking.Description
                }).ToList();
                newTimeslot.Timeslots = PopulateDaysOfWeekForBeacon(beaconAvailability, currentDate.Date, currentDate.AddDays(7).Date);

                currentDate = currentDate.AddDays(7);
                slots.Add(newTimeslot);                
            }

            return slots;
        }

        private static IList<Timeslot> PopulateDaysOfWeekForBeacon(BeaconAvailability beaconAvailability, DateTime startDate, DateTime endTime)
        {
            var slots = new List<Timeslot>();
            var currentDate = startDate;
            while (currentDate < endTime)
            {
                Timeslot newTimeslot = new Timeslot();
                newTimeslot.Start = currentDate;
                newTimeslot.End = currentDate.AddDays(1);
                newTimeslot.Unit = TimeslotUnit.Days;
                newTimeslot.Bookings = beaconAvailability.Bookings.Where(b => b.Start >= newTimeslot.Start && b.End <= newTimeslot.End).Select(booking => new TimeslotBooking()
                {
                    ContentId = booking.ContentId,
                    ContentTitle = booking.Description
                }).ToList();
                newTimeslot.Timeslots = PopulateHoursOfDayForBeacon(beaconAvailability, currentDate.Date, currentDate.AddDays(1).Date);

                currentDate = currentDate.AddDays(1);
                slots.Add(newTimeslot);                
            }

            return slots;
        }

        private static IList<Timeslot> PopulateHoursOfDayForBeacon(BeaconAvailability beaconAvailability, DateTime startDate, DateTime endTime)
        {
            var slots = new List<Timeslot>();
            var currentDate = startDate;
            while (currentDate < endTime)
            {
                Timeslot newTimeslot = new Timeslot();
                newTimeslot.Start = currentDate;
                newTimeslot.End = currentDate.AddHours(1);
                newTimeslot.Unit = TimeslotUnit.Hours;
                newTimeslot.Bookings = beaconAvailability.Bookings.Where(b => b.Start >= newTimeslot.Start && b.End <= newTimeslot.End).Select(booking => new TimeslotBooking()
                {
                    ContentId = booking.ContentId,
                    ContentTitle = booking.Description
                }).ToList();

                currentDate = currentDate.AddHours(1);
                slots.Add(newTimeslot);                
            }

            return slots;
        }
    }
}