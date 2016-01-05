using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Models
{
    public class APIInfo
    {
        public APIInfo()
        {
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.TermsOfService = string.Empty;
            this.Version = string.Empty;

            this.Contact = new Contact();

            this.License = new License();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("termsOfService")]
        public string TermsOfService { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("license")]
        public License License { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

}
