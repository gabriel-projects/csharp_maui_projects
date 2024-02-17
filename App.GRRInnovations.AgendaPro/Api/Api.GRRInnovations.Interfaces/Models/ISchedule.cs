
using System;

namespace Api.GRRInnovations.Interfaces.Models
{
    public interface ISchedule : IBaseModel
    {
        /// <summary>
        /// Dia/hora do inicio do evento
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Dia/hora do fim do evento
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Indica se o evento dura o dia todo
        /// </summary>
        public bool IsAllDay { get; set; }

        /// <summary>
        /// Descrição da Localização do evento
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// Assunto do evento
        /// </summary>
        string Subject { get; set; }
    }
}
