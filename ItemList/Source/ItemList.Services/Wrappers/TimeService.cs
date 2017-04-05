using System;
using ItemList.Contracts.Services;

namespace ItemList.Services.Wrappers
{
    public class TimeService : ITimeService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}