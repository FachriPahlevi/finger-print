using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPUruNet;

namespace FingerPrint4
{
    public class UserFingerPrint
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public Fmd Fmd { get; set; }

        public UserFingerPrint() { }

        public UserFingerPrint(string Name, string Password) { 
            this.Name = Name;
            this.Password = Password;
        }
    }
}
