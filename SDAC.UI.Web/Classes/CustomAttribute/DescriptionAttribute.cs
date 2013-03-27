using System;

namespace SDAC.UI.Web.CustomAttributes
{
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; private set; }
        public DescriptionAttribute(string text)
        {
            Description = text;
        }
    }
}
