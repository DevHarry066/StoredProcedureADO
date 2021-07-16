using System;
using System.Data;
using System.Data.SqlClient;

namespace StoredProcedureADO
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                string ConnectionString = "data source=.; database=StudentDB; integrated security=SSPI";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetStudents", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlCommand cmd1 = new SqlCommand()
                    {
                        CommandText = "spGetStudentById",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter param1 = new SqlParameter
                    {
                        ParameterName = "@Id", //Parameter name defined in stored procedure
                        SqlDbType = SqlDbType.Int, //Data Type of Parameter
                        Value = 101, //Value passes to the paramtere
                        Direction = ParameterDirection.Input //Specify the parameter as input
                    };


                    cmd1.Parameters.Add(param1);




                    //Create Student
                    SqlCommand cmd2 = new SqlCommand()
                    {
                        CommandText = "spCreateStudent",
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };


                    SqlParameter param2 = new SqlParameter
                    {
                        ParameterName = "@Name", //Parameter name defined in stored procedure
                        SqlDbType = SqlDbType.NVarChar, //Data Type of Parameter
                        Value = "Test",
                        Direction = ParameterDirection.Input //Specify the parameter as input
                    };


                    cmd2.Parameters.Add(param2);
                    //Another approach to add input parameter
                    cmd2.Parameters.AddWithValue("@Email", "Test@dotnettutorial.net");
                    cmd2.Parameters.AddWithValue("@Mobile", "1234567890");

                    //


                    SqlParameter outParameter = new SqlParameter
                    {
                        ParameterName = "@Id", //Parameter name defined in stored procedure
                        SqlDbType = SqlDbType.Int, //Data Type of Parameter
                        Direction = ParameterDirection.Output //Specify the parameter as ouput
                    };


                    cmd2.Parameters.Add(outParameter);

                    connection.Open();

                    /*
                    //Execute List of Students
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Console.WriteLine(sdr["Id"] + ",  " + sdr["Name"] + ",  " + sdr["Email"] + ",  " + sdr["Mobile"]);
                    }
                    */

                    /*
                    //Execute Student with id
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    while (sdr1.Read())
                    {
                        Console.WriteLine(sdr1["Id"] + ",  " + sdr1["Name"] + ",  " + sdr1["Email"] + ",  " + sdr1["Mobile"]);
                    }
                    */

                    //Create new student
                    cmd2.ExecuteNonQuery();

                    Console.WriteLine("Newely Generated Student ID : " + outParameter.Value.ToString());
                }
            }


            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }
            Console.ReadKey();
        }
    }
}
