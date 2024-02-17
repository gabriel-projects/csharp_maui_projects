using Api.GRRInnovations.Interfaces.Models;
using System;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.Domain.Wrappers.In
{
    public class WrapperInSchedule<TSchedule> : WrapperBase<TSchedule, WrapperInSchedule<TSchedule>> where TSchedule : ISchedule
    {
        public WrapperInSchedule() : base() { }

        public WrapperInSchedule(TSchedule data) : base(data) { }

        [JsonPropertyName("start_time")]
        public DateTime StartTime
        {
            get => Data.StartTime;
            set => Data.StartTime = value;
        }

        [JsonPropertyName("end_time")]
        public DateTime EndTime
        {
            get => Data.EndTime;
            set => Data.EndTime = value;
        }

        [JsonPropertyName("is_all_day")]
        public bool IsAllDay
        {
            get => Data.IsAllDay;
            set => Data.IsAllDay = value;
        }

        [JsonPropertyName("location")]
        public string Location
        {
            get => Data.Location;
            set => Data.Location = value;
        }

        [JsonPropertyName("subject")]
        public string Subject
        {
            get => Data.Subject;
            set => Data.Subject = value;
        }
    }
}
