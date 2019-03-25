using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SEMaker.Models
{
    public class EventDataAccessLayer
    {
        string connectionString = "Data Source=DESKTOP-AC90BCL\\SQLEXPRESS;Initial Catalog=semaker;Integrated Security=True";

        //To View all employees details      
        public IEnumerable<Event> GetAllEvents()
        {
            List<Event> lstevent = new List<Event>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEvents", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Event evnt = new Event();

                    evnt.ID = Convert.ToInt32(rdr["EventID"]);
                    evnt.Name = rdr["Name"].ToString();
                    evnt.Sport = rdr["Sport"].ToString();
                    evnt.City = rdr["City"].ToString();
                    evnt.Author = rdr["Author"].ToString();

                    lstevent.Add(evnt);
                }
                con.Close();
            }
            return lstevent;
        }

        public IEnumerable<Event> GetMyEvents(String author)
        {
            List<Event> lstevent = new List<Event>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetMyEvents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Author", author);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Event evnt = new Event();

                    evnt.ID = Convert.ToInt32(rdr["EventID"]);
                    evnt.Name = rdr["Name"].ToString();
                    evnt.Sport = rdr["Sport"].ToString();
                    evnt.City = rdr["City"].ToString();
                    evnt.Author = rdr["Author"].ToString();

                   
                    lstevent.Add(evnt);
                }
                con.Close();
            }
            return lstevent;
        }


        //To Add new employee record      
        public void AddEvent(Event evnt, String author)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", evnt.Name);
                cmd.Parameters.AddWithValue("@Sport", evnt.Sport);
                cmd.Parameters.AddWithValue("@City", evnt.City);
                cmd.Parameters.AddWithValue("@Author", author);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar employee    
        public void UpdateEvent(Event evnt)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", evnt.ID);
                cmd.Parameters.AddWithValue("@Name", evnt.Name);
                cmd.Parameters.AddWithValue("@Sport", evnt.Sport);
                cmd.Parameters.AddWithValue("@City", evnt.City);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular employee    
        public Event GetEventData(int? id)
        {
            Event evnt = new Event();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblEvents WHERE EventID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    evnt.ID = Convert.ToInt32(rdr["EventID"]);
                    evnt.Name = rdr["Name"].ToString();
                    evnt.Sport = rdr["Sport"].ToString();
                    evnt.City = rdr["City"].ToString();
                }
            }
            return evnt;
        }

        //To Delete the record on a particular employee    
        public void DeleteEvent(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEvent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EvntId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
