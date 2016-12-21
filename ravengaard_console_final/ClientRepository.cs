using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class ClientRepository
    {
        Dictionary<int, Client> clientDictionary = new Dictionary<int, Client>();

        public void Clear()
        {
            clientDictionary.Clear();
        }

        public Client CreateClient(string cliFN, string cliLN, string cliPhone, string cliAddress, string cliEmail)
        {
            Client client = new Client(cliFN, cliLN, cliPhone, cliAddress, cliEmail);

            return client;
        }
    }
}
