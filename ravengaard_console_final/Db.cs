using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ravengaard_console_final
{
    public class Db
    {

        static private string connectionString = "Server=ealdb1.eal.local; Database=ejl72_db; User Id=ejl72_usr; Password=Baz1nga72";

        static public bool InsertClientIntoDb(Client client)
        {
            bool clientInserted = true;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_NewUser", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@FirstName", client.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", client.LastName));
                    command.Parameters.Add(new SqlParameter("@Phone", client.Phone));
                    command.Parameters.Add(new SqlParameter("@AddressInfo", client.Address));
                    command.Parameters.Add(new SqlParameter("@Cli_Password", client.Password));
                    command.Parameters.Add(new SqlParameter("@Email", client.Email));

                    command.ExecuteNonQuery();
                }
                catch
                {
                    clientInserted = false;
                }
            }

            return clientInserted;
        }

        static public bool isUsernamePasswordCorrect(string username, string password)
        {
            bool isCombinationCorrect = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_CheckLogin", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Cli_Password", password));
                    command.Parameters.Add(new SqlParameter("@Email", username));

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        isCombinationCorrect = true;
                    }
                }
                catch (SqlException e)
                {
                    Ui.WriteL(e.ToString());
                    Ui.WriteL("There was an error, try again later!");
                }
            }

            return isCombinationCorrect;
        }

    }
}
