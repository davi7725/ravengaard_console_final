using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Client() { }
        public Client(string cliFN, string cliLN, string cliPhone, string cliAddress, string cliEmail)
        {
            FirstName = cliFN;
            LastName = cliLN;
            Phone = cliPhone;
            Address = cliAddress;
            Email = cliEmail;
        }
    }
}
