using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace iondie
{
    /// <summary>
    /// Credit to http://ruchkinalexandr.blogspot.com/2012/02/c_06.html for DnsCacheEntries code
    /// </summary>
    class DnsCacheSpy
    {
        public int Interval { get; set; }
        public int Jitter { get; set; }
        public List<DnsEntry> MasterList;

        public event UpdateHandler Update;
        public delegate void UpdateHandler();
        
        private System.Timers.Timer timer;

        //DLLs used
        private const string KERNEL32 = "kernel32.dll";
        private const string NETAPI32 = "netapi32.dll";
        private const string DNSAPI = "dnsapi.dll";

        [DllImport(KERNEL32, SetLastError = true)]
        private static extern IntPtr LoadLibrary(String dllName);

        [DllImport(KERNEL32, SetLastError = true)]
        private static extern IntPtr FreeLibrary(IntPtr hModule);

        [DllImport(KERNEL32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool GetDNSCacheInvoker(out IntPtr dns);

        [DllImport(NETAPI32)]
        private static extern void NetApiBufferFree(IntPtr bufptr);

        public DnsCacheSpy()
        {
            MasterList = new List<DnsEntry>();
        }

        /// <summary>
        /// Start monitoring the DNS cache for new entries
        /// </summary>
        /// <param name="Interval">Time (in ms) between checking DNS Cache</param>
        /// <param name="Jitter">Amount (25=25%) to randomize interval.</param>
        public void StartWatching(int Interval = 5000, int Jitter = 25)
        {
            this.Interval = Interval;
            this.Jitter = Jitter;
            timer = new System.Timers.Timer()
            {
                Interval = GetInterval()
            };
            timer.Elapsed += UpdateTick;
            timer.Enabled = true;

            //UpdateTick(null, null);
        }

        /// <summary>
        /// Stop watching the DNS Cache for changes
        /// </summary>
        public void StopWatching()
        {
            timer.Enabled = false;
        }

        private void UpdateTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            //set interval again:
            timer.Interval = GetInterval();
            //only update if the list changed
            if (GetDnsCacheEntries())
                Update?.Invoke();
        }

        private bool GetDnsCacheEntries()
        {
            var updated = false;
            var hModule = LoadLibrary(DNSAPI);
            var fptr = GetProcAddress(hModule, "DnsGetCacheDataTable");


            var dumpCache = (GetDNSCacheInvoker)Marshal.GetDelegateForFunctionPointer(fptr, typeof(GetDNSCacheInvoker));

            if (dumpCache(out IntPtr dnsPtr))
            {
                var dnsEntry = (DnsCacheEntry)Marshal.PtrToStructure(dnsPtr, typeof(DnsCacheEntry));
                NetApiBufferFree(dnsPtr);
                var current = dnsEntry.pNext;

                if (UpdateMasterList(new DnsEntry(dnsEntry.pszName, dnsEntry.wType)))
                    updated = true;

                while (current != IntPtr.Zero)
                {
                    dnsEntry = (DnsCacheEntry)Marshal.PtrToStructure(current, typeof(DnsCacheEntry));
                    var old = current;
                    current = dnsEntry.pNext;
                    if (UpdateMasterList(new DnsEntry(dnsEntry.pszName, dnsEntry.wType)))
                        updated = true;
                    NetApiBufferFree(old);
                }
            }
            FreeLibrary(hModule);
            return updated;
        }

        private bool UpdateMasterList(DnsEntry entry)
        {
            if (MasterList.Any(e => e.Name == entry.Name && e.Type == entry.Type))
                return false;
            MasterList.Add(entry);
            return true;
        }

        /// <summary>
        /// Get a randomized interval (based on the jitter)
        /// </summary>
        /// <returns>interval for the timer.</returns>
        private int GetInterval()
        {
            var rand = new Random();
            var jitterAsPercent = (double)this.Jitter / 100;
            var delta = rand.Next(0, (int)(jitterAsPercent * this.Interval));
            return rand.Next(2) == 1 ? this.Interval + delta : this.Interval - delta;
        }

        private struct DnsCacheEntry
        {
            //pointer to next entry
            public IntPtr pNext;
            //DNS name entry
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszName;
            //type of DNS query
            public ushort wType;
            public ushort wDataLength;
            public ulong dwFlags;

            public DnsCacheEntry(ushort zero = 0)
            {
                pNext = IntPtr.Zero;
                pszName = String.Empty;
                wType = zero;
                wDataLength = zero;
                dwFlags = zero;
            }
        }
    }
}
