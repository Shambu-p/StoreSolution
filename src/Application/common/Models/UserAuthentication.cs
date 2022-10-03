using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSolution.Application.common.Models {
    public class UserAuthentication {
        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte Role { get; set; }
        public string Token {get; set; }
    }
}