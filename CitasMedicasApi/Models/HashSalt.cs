using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicasApi.Models
{
    public class HashSalt
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}