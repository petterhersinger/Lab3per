using System.Data;
using System.Data.SqlClient;

namespace Lab3.Models
{
    public class PersonAktivitetMetoder
    {
        public PersonAktivitetMetoder() { }

        public List<PersonAktivitetModel> GetPersonAktivitetModel(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";
            String sqlstring = "SELECT Person.Fornamn, Person.Efternamn, Hobby.Aktivitet FROM Person INNER JOIN HarHobby ON Person.Id = HarHobby.Person INNER JOIN Hobby ON HarHobby.Hobby = Hobby.Id;";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            SqlDataReader reader = null;

            List<PersonAktivitetModel> PersonAktivitetModelLista = new List<PersonAktivitetModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    PersonAktivitetModel pa = new PersonAktivitetModel();
                    pa.Fornamn = reader["Fornamn"].ToString();
                    pa.Efternamn = reader["Efternamn"].ToString();
                    pa.Aktivitet = reader["Aktivitet"].ToString();

                    PersonAktivitetModelLista.Add(pa);
                }
                reader.Close();
                return PersonAktivitetModelLista;
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
        public List<PersonAktivitetModel> GetPersonAktivitetModel(out string errormsg, int filterId)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";
            String sqlstring = "SELECT Person.Fornamn, Person.Efternamn, Hobby.Aktivitet FROM Person INNER JOIN HarHobby ON Person.Id = HarHobby.Person INNER JOIN Hobby ON HarHobby.Hobby = Hobby.Id WHERE Hobby.Id = @filterId;";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("filterId", SqlDbType.Int). Value = filterId;

            SqlDataReader reader = null;

            List<PersonAktivitetModel> PersonAktivitetModelLista = new List<PersonAktivitetModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    PersonAktivitetModel pa = new PersonAktivitetModel();
                    pa.Fornamn = reader["Fornamn"].ToString();
                    pa.Efternamn = reader["Efternamn"].ToString();
                    pa.Aktivitet = reader["Aktivitet"].ToString();

                    PersonAktivitetModelLista.Add(pa);
                }
                reader.Close();
                return PersonAktivitetModelLista;
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

