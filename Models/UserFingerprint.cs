using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPUruNet;

namespace FingerPrint4
{
    public class UserFingerprint
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public Fmd Fmd { get; set; }

        public UserFingerprint() { }

        public UserFingerprint(string name, string password) {
            Name = name;
            Password = password;
        }
    }
}
