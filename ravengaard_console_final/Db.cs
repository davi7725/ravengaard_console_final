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
                    command.Parameters.Add(new SqlParameter("@Email", client.Email.ToLower()));

                    command.ExecuteNonQuery();
                }
                catch
                {
                    clientInserted = false;
                }
            }

            return clientInserted;
        }

        internal static void GetColor(ColorRepository colorRepo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_GetColor", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            colorRepo.Create(Convert.ToInt32(rdr["Color_ID"]), rdr["Color_Name"].ToString());
                        }
                    }
                }
                catch (SqlException e)
                {
                    Ui.WriteL(e.ToString());
                    Ui.WriteL("There was an error, try again later!");
                }
            }
        }

        internal static void GetPendant(PendantRepository pendantRepo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_GetPendant", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            pendantRepo.Create(Convert.ToInt32(rdr["Pendant_ID"]), rdr["Pendant_Name"].ToString(), Convert.ToSingle(rdr["Pendant_Height"]), Convert.ToSingle(rdr["Pendant_Width"]));
                        }
                    }
                }
                catch (SqlException e)
                {
                    Ui.WriteL(e.ToString());
                    Ui.WriteL("There was an error, try again later!");
                }
            }
        }

        internal static void GetChain(ChainRepository chainRepo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_GetChain", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            chainRepo.Create(Convert.ToInt32(rdr["Chain_ID"]), rdr["Chain_Name"].ToString(), Convert.ToSingle(rdr["Chain_Length"]), Convert.ToSingle(rdr["Chain_Thickness"]));
                        }
                    }
                }
                catch (SqlException e)
                {
                    Ui.WriteL(e.ToString());
                    Ui.WriteL("There was an error, try again later!");
                    Ui.Wait();
                }
            }
        }

        internal static void GetRock(RockRepository rockRepo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_GetRock", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            rockRepo.Create(Convert.ToInt32(rdr["Rock_ID"]), rdr["Rock_Name"].ToString());
                        }
                    }
                }
                catch (SqlException e)
                {
                    Ui.WriteL(e.ToString());
                    Ui.WriteL("There was an error, try again later!");
                }
            }
        }

        internal static void GetRingType(RingTypeRepository ringTypeRepo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_GetRingType", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            ringTypeRepo.Create(Convert.ToInt32(rdr["RingType_ID"]), rdr["RingType_Name"].ToString());
                        }
                    }
                }
                catch (SqlException e)
                {
                    Ui.WriteL(e.ToString());
                    Ui.WriteL("There was an error, try again later!");
                }
            }
        }

        static public bool isUsernamePasswordCorrect(string username, string password)
        {
            bool isCombinationCorrect = false;
            username = username.ToLower();

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
