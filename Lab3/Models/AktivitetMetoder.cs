using System.Data.SqlClient;

namespace Lab3.Models
{
    public class AktivitetMetoder
    {
        public AktivitetMetoder() { }

        public List<AktivitetModel> GetAktivitetLista(out string errormsg) {  
            
        SqlConnection dbConnection = new SqlConnection();
        dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";
        String sqlstring = "Select * From Hobby";
        SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
        SqlDataReader reader = null;

        List<AktivitetModel> AktivitetModelLista = new List<AktivitetModel>();

        errormsg = "";

            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    AktivitetModel am = new AktivitetModel();
                    am.Aktivitet = reader["Aktivitet"].ToString();
                    am.Id = Convert.ToInt16(reader["Id"]);

                    AktivitetModelLista.Add(am);
                }
                reader.Close();
                return AktivitetModelLista;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
                }
            finally
            {
            dbConnection.Close();
}
        }
    }
}
