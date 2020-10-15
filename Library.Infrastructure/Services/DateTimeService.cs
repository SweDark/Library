using Library.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now { get => DateTime.Now; }
    }
}
