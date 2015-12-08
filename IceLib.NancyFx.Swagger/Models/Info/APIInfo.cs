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
            this.Contact = new Contact();

            this.License = new License();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TermsOfService { get; set; }

        public Contact Contact { get; set; }

        public License License { get; set; }

        public string Version { get; set; }
    }

}
