using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iondie
{
    public class DnsEntry
    {
        public string Name { get; set; }
        public DNS_RECORD_TYPE Type { get; set; }

        public string TypeName
        {
            get
            {
                return Enum.GetName(typeof(DNS_RECORD_TYPE), Type);
            }
        }

        public string TypeDescription
        {
            get
            {
                FieldInfo fi = typeof(DNS_RECORD_TYPE).GetField(Type.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes( typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }

                return Type.ToString();
            }
        }

        public DnsEntry(string name, uint recordType)
        {
            Name = name;
            Type = (DNS_RECORD_TYPE)recordType;
        }

        public DnsEntry(string name, DNS_RECORD_TYPE recordType)
        {
            Name = name;
            Type = recordType;
        }
    }
}
