namespace brief.Controllers.Helpers
{
    using System.Collections.Generic;

    public interface IHeaderSettings
    {
        Dictionary<string, string[]> AcceptableValuesForHeader { get; set; }
    }
}