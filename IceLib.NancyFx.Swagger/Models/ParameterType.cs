using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Swagger.Models
{
    public enum ParameterType
    {
        [Description("Header")]
        Header,

        [Description("Path")]
        Path,

        [Description("FormData")]
        FormData,

        [Description("Body")]
        Body
    }
}
