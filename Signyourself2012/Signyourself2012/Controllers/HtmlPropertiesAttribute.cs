   using System;
using System.Collections.Generic;
   using System.ComponentModel;

namespace Signyourself2012
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,AllowMultiple=false,Inherited=true)]
    public class HtmlPropertiesAttribute : Attribute
    {
        public string CssClass
        {
            get;
            set;
        }
        public int MaxLength
        {
            get;
            set;
        }
        public int Size
        {
            get;
            set;
        }
        public int Min
        {
            get;
            set;
        }
        public IDictionary<string,object> HtmlAttributes()
        {
            //Todo: we could use TypeDescriptor to get the dictionary of properties and their values
            IDictionary<string, object> htmlatts = new Dictionary<string, object>();
            if (MaxLength != 0)
            {
                htmlatts.Add("MaxLength", MaxLength);
            }
            if (Size != 0)
            {
                htmlatts.Add("Size", Size);
            }
            if (Min != 0)
            {
                htmlatts.Add("Min", Min);
            }
            return htmlatts;
        }
    }
}
