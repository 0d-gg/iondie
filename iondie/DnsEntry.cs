﻿using System;
using System.ComponentModel;
using System.Reflection;

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

    /// <summary>
    /// The DNS_RECORD_TYPE is a 16-bit integer value that specifies DNS record types that can be enumerated by the DNS server.
    /// https://msdn.microsoft.com/en-us/library/cc448878.aspx
    /// </summary>
    public enum DNS_RECORD_TYPE
    {
        [Description("An empty record type ([RFC1034] section 3.6 and [RFC1035] section 3.2.2).")]
        DNS_TYPE_ZERO = 0x0000,
        [Description("An A record type, used for storing an IP address ([RFC1035] section 3.2.2).")]
        DNS_TYPE_A = 0x0001,
        [Description("An authoritative name-server record type ([RFC1034] section 3.6 and [RFC1035] section 3.2.2).")]
        DNS_TYPE_NS = 0x0002,
        [Description("A mail-destination record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_MD = 0x0003,
        [Description("A mail forwarder record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_MF = 0x0004,
        [Description("A record type that contains the canonical name of a DNS alias ([RFC1035] section 3.2.2).")]
        DNS_TYPE_CNAME = 0x0005,
        [Description("A Start of Authority (SOA) record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_SOA = 0x0006,
        [Description("A mailbox record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_MB = 0x0007,
        [Description("A mail group member record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_MG = 0x0008,
        [Description("A mail-rename record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_MR = 0x0009,
        [Description("A record type for completion queries ([RFC1035] section 3.2.2).")]
        DNS_TYPE_NULL = 0x000A,
        [Description("A record type for a well-known service ([RFC1035] section 3.2.2).")]
        DNS_TYPE_WKS = 0x000B,
        [Description("A record type containing FQDN pointer ([RFC1035] section 3.2.2).")]
        DNS_TYPE_PTR = 0x000C,
        [Description("A host information record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_HINFO = 0x000D,
        [Description("A mailbox or mailing list information record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_MINFO = 0x000E,
        [Description("A mail-exchanger record type ([RFC1035] section 3.2.2).")]
        DNS_TYPE_MX = 0x000F,
        [Description("A record type containing a text string ([RFC1035] section 3.2.2).")]
        DNS_TYPE_TXT = 0x0010,
        [Description("A responsible-person record type [RFC1183].")]
        DNS_TYPE_RP = 0x0011,
        [Description("A record type containing AFS database location [RFC1183].")]
        DNS_TYPE_AFSDB = 0x0012,
        [Description("An X25 PSDN address record type [RFC1183].")]
        DNS_TYPE_X25 = 0x0013,
        [Description("An ISDN address record type [RFC1183].")]
        DNS_TYPE_ISDN = 0x0014,
        [Description("A route through record type [RFC1183].")]
        DNS_TYPE_RT = 0x0015,
        [Description("A cryptographic public key signature record type [RFC2931].")]
        DNS_TYPE_SIG = 0x0018,
        [Description("A record type containing public key used in DNSSEC [RFC2535].")]
        DNS_TYPE_KEY = 0x0019,
        [Description("An IPv6 address record type [RFC3596].")]
        DNS_TYPE_AAAA = 0x001C,
        [Description("A location information record type [RFC1876].")]
        DNS_TYPE_LOC = 0x001D,
        [Description("A next-domain record type [RFC2065].")]
        DNS_TYPE_NXT = 0x001E,
        [Description("A server selection record type [RFC2782].")]
        DNS_TYPE_SRV = 0x0021,
        [Description("An Asynchronous Transfer Mode (ATM) address record type [ATMA].")]
        DNS_TYPE_ATMA = 0x0022,
        [Description("An NAPTR record type [RFC2915].")]
        DNS_TYPE_NAPTR = 0x0023,
        [Description("A DNAME record type [RFC2672].")]
        DNS_TYPE_DNAME = 0x0027,
        [Description("A DS record type [RFC4034].")]
        DNS_TYPE_DS = 0x002B,
        [Description("An RRSIG record type [RFC4034].")]
        DNS_TYPE_RRSIG = 0x002E,
        [Description("An NSEC record type [RFC4034].")]
        DNS_TYPE_NSEC = 0x002F,
        [Description("A DNSKEY record type [RFC4034].")]
        DNS_TYPE_DNSKEY = 0x0030,
        [Description("A DHCID record type [RFC4701].")]
        DNS_TYPE_DHCID = 0x0031,
        [Description("An NSEC3 record type [RFC5155].")]
        DNS_TYPE_NSEC3 = 0x0032,
        [Description("An NSEC3PARAM record type [RFC5155].")]
        DNS_TYPE_NSEC3PARAM = 0x0033,
        [Description("A TLSA record type [RFC6698].")]
        DNS_TYPE_TLSA = 0x0034,
        [Description("A query-only type requesting all records [RFC1035].")]
        DNS_TYPE_ALL = 0x00FF,
        [Description("A record type containing Windows Internet Name Service (WINS) forward lookup data [MS-WINSRA].")]
        DNS_TYPE_WINS = 0xFF01,
        [Description("A record type containing WINS reverse lookup data [MS-WINSRA].")]
        DNS_TYPE_WINSR = 0xFF02
    }
}
