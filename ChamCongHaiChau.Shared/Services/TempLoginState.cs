using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCongHaiChau.Shared.Services
{
    public class TempLoginState
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public void Clear()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }

}
