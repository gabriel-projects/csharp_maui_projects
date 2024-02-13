using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.Domain.Wrappers
{
    public class WrapperError
    {
        [JsonPropertyName("error")]
        public string Title { get; set; } = "Erro";


        [JsonPropertyName("detail")]
        public string Message { get; set; }
    }
}
