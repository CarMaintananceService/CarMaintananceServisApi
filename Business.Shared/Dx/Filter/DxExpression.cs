using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Shared.Dx.Filter
{
    public partial class DxExpression
    {
        public DxExpression()
        {
            Rules = new List<object>();
            Condition = "and";
        }

        [JsonProperty("condition", NullValueHandling = NullValueHandling.Ignore)]
        public string Condition { get; set; }

        [JsonProperty("rules", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Rules { get; set; }
    }


}
