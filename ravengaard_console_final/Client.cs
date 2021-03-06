﻿using System;
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
        public string Password { get; set; }

        public Client() { }
        public Client(int id, string cliFN, string cliLN, string cliPhone, string cliAddress, string cliEmail, string cliPassword)
        {
            ClientId = id;
            FirstName = cliFN;
            LastName = cliLN;
            Phone = cliPhone;
            Address = cliAddress;
            Email = cliEmail;
            Password = cliPassword;
        }
    }
}
