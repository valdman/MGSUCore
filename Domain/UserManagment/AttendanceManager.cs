using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using DataAccess.Application;
using Journalist;
using MongoDB.Bson;
using UserManagment.Application;

namespace UserManagment
{
    public class AttendanceManager : IAttendanceManager
    {
        private readonly IRepository<Attendance> _attendanceRepository;
        public AttendanceManager(IRepository<Attendance> attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public ObjectId AttendUserToEvent(ObjectId userId, ObjectId eventId, AttendanceType attendanceType)
        {
            Require.NotNull(userId, nameof(userId));
            Require.NotNull(eventId, nameof(eventId));

            var sameAttendances = _attendanceRepository.GetByPredicate(attendance => 
                                                                attendance.UserId == userId &&
                                                                attendance.EventId == eventId);
            if(sameAttendances.Count() > 1)
            {
                throw new PolicyException("More than one attendance of same user to same event");
            }

            if(sameAttendances.Count() == 1)
            {
                ChangeAttendanceType(userId, eventId, attendanceType);
            }

            return _attendanceRepository.Create(new Attendance
            {
                UserId = userId, 
                EventId = eventId,
                AttendanceType = attendanceType
            });

        }

        public void ChangeAttendanceType(ObjectId userId, ObjectId eventId, AttendanceType attendanceType)
        {
            Require.NotNull(userId, nameof(userId));
            Require.NotNull(eventId, nameof(eventId));

            var sameAttendances = _attendanceRepository.GetByPredicate(attendance => 
                                                                attendance.UserId == userId &&
                                                                attendance.EventId == eventId);
            if(sameAttendances.Count() != 1)
            {
                throw new PolicyException("There are no one attendance on this event for this user");
            }

            var attendanceToUpdate = sameAttendances.Single();
        }

        public IEnumerable<Attendance> GetAllIdsOfUserEvents(ObjectId userId)
        {
            Require.NotNull(userId, nameof(userId));

            return _attendanceRepository.GetByPredicate(attendance => attendance.UserId == userId);
        }

        public AttendanceType GetAttendanceType(ObjectId userId, ObjectId eventId)
        {
            Require.NotNull(userId, nameof(userId));
            Require.NotNull(eventId, nameof(eventId));

            return _attendanceRepository.GetByPredicate(attendance => 
                                                        attendance.UserId == userId && 
                                                        attendance.EventId == eventId).Single().AttendanceType;
        }
    }
}