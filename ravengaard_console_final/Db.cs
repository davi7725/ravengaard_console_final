﻿using System;
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

        internal static int GetNextProductId()
        {
            return 1;
        }

        internal static bool InsertRingIntoDb(Product product, int clientId)
        {
            bool productInserted = true;
            int productId = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_NewProduct", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductType", product.ProductType));
                    command.Parameters.Add(new SqlParameter("@RingType", product.RingType));
                    command.Parameters.Add(new SqlParameter("@Rock", product.Rock));
                    command.Parameters.Add(new SqlParameter("@Chain", DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Pendant", DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Color", product.Color));

                    productId = (int)command.ExecuteScalar();
                }
                catch (SqlException e)
                {

                    Console.WriteLine(e.Message.ToString());
                    Console.ReadKey();
                    productInserted = false;
                }
                registerOrder(productId, clientId);
            }

            return productInserted;
        }

        internal static bool InsertNecklaceIntoDb(Product product, int clientId)
        {
            bool productInserted = true;
            int productId = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_NewProduct", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ProductType", product.ProductType));
                    command.Parameters.Add(new SqlParameter("@RingType", DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Rock", DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Chain", product.Chain));
                    command.Parameters.Add(new SqlParameter("@Pendant", product.Pendant));
                    command.Parameters.Add(new SqlParameter("@Color", product.Color));

                    productId = (int)command.ExecuteScalar();
                }
                catch (SqlException e)
                {

                    Console.WriteLine(e.Message.ToString());
                    Console.ReadKey();
                    productInserted = false;
                }
                registerOrder(productId, clientId);
            }

            return productInserted;
        }

        private static void registerOrder(int proId,int cliId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_NewOrder", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Pro_Id", proId));
                    command.Parameters.Add(new SqlParameter("@Cli_Id", cliId));

                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {

                    Console.WriteLine(e.Message.ToString());
                    Console.ReadKey();
                }
            }
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
                    throw e;
                }
            }
        }

        internal static void GetProduct(ProductRepository productRepo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_GetProduct", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            if(Convert.ToInt32(rdr["ProductType"]) == 1)
                            {
                                productRepo.CreateRing(Convert.ToInt32(rdr["Pro_ID"]),Convert.ToInt32(rdr["Rock"]), Convert.ToInt32(rdr["RingType"]), Convert.ToInt32(rdr["Color"]),false);
                            }
                            else if(Convert.ToInt32(rdr["ProductType"]) == 2)
                            {
                                productRepo.CreateNecklace(Convert.ToInt32(rdr["Pro_ID"]), Convert.ToInt32(rdr["Chain"]), Convert.ToInt32(rdr["Pendant"]), Convert.ToInt32(rdr["Color"]),false);
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    throw e;
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
                    throw e;
                }
            }
        }


        internal static void GetClient(ClientRepository clientRepo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("prc_GetClient", con);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            clientRepo.Create(Convert.ToInt32(rdr["Cli_ID"]),rdr["FirstName"].ToString(),rdr["LastName"].ToString(),rdr["Phone"].ToString(),rdr["AddressInfo"].ToString(), rdr["Email"].ToString(), rdr["Cli_Password"].ToString());
                        }
                    }
                }
                catch (SqlException e)
                {
                    throw e;
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
                    throw e;
                }
            }
        }

        internal static void AddProductsToTheDb(Dictionary<int, Product> dictionary, Client loggedInClient)
        {
            foreach(KeyValuePair<int,Product> prd in dictionary)
            {
                if(prd.Value.ProductType == 1)
                {
                    InsertRingIntoDb(prd.Value, loggedInClient.ClientId);
                }
                else if(prd.Value.ProductType == 2)
                {
                    InsertNecklaceIntoDb(prd.Value, loggedInClient.ClientId);
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
                    throw e;
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
                    throw e;
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
                    throw e;
                }
            }

            return isCombinationCorrect;
        }

    }
}
