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

        public Client Create(int id, string cliFN, string cliLN, string cliPhone, string cliAddress, string cliEmail, string password)
        {
            Client client = new Client(id, cliFN, cliLN, cliPhone, cliAddress, cliEmail, password);
            clientDictionary.Add(id, client);

            return client;
        }

        internal Client Load(string username)
        {
            Client loadedClient = null;
            foreach(Client cli in clientDictionary.Values)
            {
                if(cli.Email == username)
                {
                    loadedClient = cli;
                }
            }

            return loadedClient;
        }

        public int NextId()
        {
            int greatestId = 0;
            foreach (int id in clientDictionary.Keys)
            {
                if (id > greatestId)
                {
                    greatestId = id;
                }
            }
            return greatestId + 1;
        }
    }
}
