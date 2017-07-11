using System.Collections.Generic;

namespace brief.Controllers.Helpers
{
    using Base;

    public class HeaderSettings : IHeaderSettings
    {
        public Dictionary<string, string[]> AcceptableValuesForHeader { get; set; }
    }
}
