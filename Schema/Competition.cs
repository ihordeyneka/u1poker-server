using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U1Poker.Schema
{
    public class Competition
    {
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public bool RegistrationActive { get; set; }
    }
}
