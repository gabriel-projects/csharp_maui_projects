using Api.GRRInnovations.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.Domain.Wrappers.Out
{
    public class WrapperOutAppointment : WrapperBase<IAppointment>
    {
        public WrapperOutAppointment() : base(null) { }

        public WrapperOutAppointment(IAppointment data) : base(data) { }

        [JsonPropertyName("uid")]
        public Guid Uid
        {
            get => Data.Uid;
            set => Data.Uid = value;
        }

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

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt
        {
            get => Data.CreatedAt.ToUniversalTime();
            set => Data.CreatedAt = value;
        }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt
        {
            get => Data.UpdatedAt.ToUniversalTime();
            set => Data.UpdatedAt = value;
        }

        public static async Task<WrapperOutAppointment> From(IAppointment appointment)
        {
            var wrapper = new WrapperOutAppointment();
            await wrapper.Populate(appointment).ConfigureAwait(false);

            return wrapper;
        }

        public static async Task<List<WrapperOutAppointment>> From(List<IAppointment> appointments)
        {
            var result = new List<WrapperOutAppointment>();

            foreach (var term in appointments)
            {
                var wrapper = new WrapperOutAppointment(null);
                await wrapper.Populate(term).ConfigureAwait(false);

                result.Add(wrapper);
            }

            return result;
        }
    }
}
