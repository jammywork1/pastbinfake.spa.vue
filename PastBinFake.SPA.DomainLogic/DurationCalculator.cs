using System;
using System.Collections.Generic;
using PastBinFake.SPA.Enums;

namespace PastBinFake.SPA.DomainLogic
{
    public class DurationCalculator {
        private static Dictionary<DurationEnum, Func<DateTime, DateTime?>> _map = new Dictionary<DurationEnum, Func<DateTime, DateTime?>> () {

            { DurationEnum.Nothing, dt => null},
            { DurationEnum.TenMin, dt => dt.AddMinutes(10)},        
            { DurationEnum.OneHour, dt => dt.AddHours(1)},        
            { DurationEnum.OneDay, dt => dt.AddDays(1) },        
            { DurationEnum.OneWeek, dt => dt.AddDays(7)},
            { DurationEnum.OneMonth, dt => dt.AddMonths(1)}
        };

        /// <summary>
        /// Calculate expired date time at <c>moment</c> at <c>duration</c>
        /// </summary>
        /// <param name="moment">From this moment the countdown</param>
        /// <param name="duration"><c>DurationEnum</c> value, used for calculating</param>
        /// <returns>Return calculated expire date time. Can be null</returns>
        public static DateTime? Calculate(DateTime moment, DurationEnum duration) {
            var calcFunc = _map[duration];
            return calcFunc(moment);
        }
    }

}