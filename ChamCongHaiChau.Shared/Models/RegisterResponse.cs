using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCongHaiChau.Shared.Models
{
    public class RegisterResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
    }
}
