﻿using System;
using System.Data.SqlClient;
using System.Data;

namespace Lab3.Models
{
    public class PersonMetoder
    {
        public PersonMetoder() { }

        public int InsertPerson(PersonDetalj pd, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";

            String sqlstring = "INSERT INTO Person ( Fornamn, Efternamn, Fodelsear, Epost, Bor) VALUES (@fornamn,@efternamn,@fodelsear,@epost,@bor)";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("fornamn", SqlDbType.NVarChar, 30).Value = pd.Fornamn;
            dbCommand.Parameters.Add("efternamn", SqlDbType.NVarChar, 30).Value = pd.Efternamn;
            dbCommand.Parameters.Add("epost", SqlDbType.NVarChar, 50).Value = pd.Epost;
            dbCommand.Parameters.Add("fodelsear", SqlDbType.Int).Value = pd.Fodelsear;
            dbCommand.Parameters.Add("bor", SqlDbType.Int).Value = pd.Bor;
            
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else
                {errormsg = "Det skapas inte en person i databasen.";}
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally 
            { 
                dbConnection.Close(); 
            }
        }

        public int DeleteKarlsson(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";

            String sqlstring = "DELETE FROM Person WHERE Efternamn = 'Karlsson'";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else
                { errormsg = "Det skapas inte en person i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public List<PersonDetalj> GetPersonWithDataSet(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";

            String sqlstring = "Select * From Person";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            //Adapter & dataset
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<PersonDetalj> PersonList = new List<PersonDetalj>();

            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myPerson");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myPerson"].Rows.Count;
                if (count > 0)
                {
                    while (i < count)
                    {
                        PersonDetalj pd = new PersonDetalj();
                        pd.Fornamn = myDS.Tables["myPerson"].Rows[i]["Fornamn"].ToString();
                        pd.Efternamn = myDS.Tables["myPerson"].Rows[i]["Efternamn"].ToString();
                        pd.Epost = myDS.Tables["myPerson"].Rows[i]["Epost"].ToString();
                        pd.Bor = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Bor"]);
                        pd.Fodelsear = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Fodelsear"]);
                        pd.Id = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Id"]);

                        i++;
                        PersonList.Add(pd);
                    }
                    errormsg = "";
                    return PersonList;
                }
                else
                {
                    errormsg = "Det hämtas ingen Person";
                    return null;
                }
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
        public List<PersonDetalj> GetPersonWithReader(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";

            String sqlstring = "Select * From Person";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            SqlDataReader reader = null;

            List<PersonDetalj> PersonList = new List<PersonDetalj>();

            errormsg = "";

            try
            {
                dbConnection.Open();

                reader = dbCommand.ExecuteReader();

                while (reader.Read()) 
                {
                    PersonDetalj Person = new PersonDetalj();
                    Person.Fornamn = reader["Fornamn"].ToString();
                    Person.Efternamn = reader["Efternamn"].ToString();
                    Person.Epost = reader["Epost"].ToString();
                    Person.Bor = Convert.ToInt16(reader["Bor"]);
                    Person.Fodelsear = Convert.ToInt16(reader["Fodelsear"]);
                    Person.Id = Convert.ToInt16(reader["Id"]);

                    PersonList.Add(Person);
                }
                reader.Close();
                return PersonList;
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
        public List<PersonAktivitetModel> GetPersonAktivitet(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Hobbys; Integrated Security = True";

            String sqlstring = "SELECT Person.Fornamn, Person.Efternamn, Hobby.Aktivitet FROM ((Person INNER JOIN HarHobby ON Person.Id = HarHobby.Person) INNER JOIN Hobby ON HarHobby.Hobby = Hobby.Id);";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            SqlDataReader reader = null;

            List<PersonAktivitetModel> AktivitetLista = new List<PersonAktivitetModel>();

            errormsg = "";

            try
            {
                dbConnection.Open();

                reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    PersonAktivitetModel Aktivitet = new PersonAktivitetModel();
                    Aktivitet.Fornamn = reader["Fornamn"].ToString();
                    Aktivitet.Efternamn = reader["Efternamn"].ToString();
                    Aktivitet.Aktivitet = reader["Aktivitet"].ToString();

                    AktivitetLista.Add(Aktivitet);
                }
                reader.Close();
                return AktivitetLista;
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
