using Microsoft.Data.SqlClient;
using System.Data;

namespace Car.SqlFunction
{
    public class sqlFunction
    {

        private string _connectionString { get; }

        public sqlFunction()
        {
            _connectionString = "Server=.\\sqlexpress;Database=Car;Integrated Security=True;TrustServerCertificate=true";
        }

        public DataTable startquery(string sqlquery, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = sqlquery;
                command.Parameters.Clear();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                command.Dispose();
                connection.Close();
                throw new Exception(ex.Message);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
            return dataTable;

        }

        public int startNonquery(string sqlquery, SqlParameter[] parameters)
        {
            int result = 0;
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = sqlquery;
                command.Parameters.Clear();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
            return result;
        }
        public int numberadd(int a,int b)
        {
            int toltal = a + b;
            return toltal;
        }
    }
}
