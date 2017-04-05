using System;
using ItemList.Contracts.Services;

namespace ItemList.Services.TimeServices
{
    public class TimeService : ITimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}