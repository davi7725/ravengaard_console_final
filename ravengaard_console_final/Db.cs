using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ravengaard_console_final
{
    public class Db
    {

        SqlConnection con = new SqlConnection("");

        static public bool InsertClientIntoDb(Client client)
        {
            bool clientInserted = false;

            return clientInserted;
        }
    }
}
