using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iondie
{
    public class DnsEntry
    {
        public string Name { get; set; }
        public DNS_RECORD_TYPE Type { get; set; }

        public DnsEntry()
        {
            this.Name = String.Empty;
            this.Type = DNS_RECORD_TYPE.DNS_TYPE_ZERO;
        }

        public DnsEntry(string name, uint recordType)
        {
            this.Name = name;
            this.Type = (DNS_RECORD_TYPE)recordType;
        }

        public DnsEntry(string name, DNS_RECORD_TYPE recordType)
        {
            this.Name = name;
            this.Type = recordType;
        }
    }
}
