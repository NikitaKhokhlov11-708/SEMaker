﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SEMaker.Models
{
    public class DataAccessLayer
    {
        string connectionString = "Data Source=DESKTOP-0P8OOV9;Initial Catalog=semaker;Integrated Security=True";

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

        //To Add new user record      
        public void AddUser(User usr)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Name", usr.Name);
                cmd.Parameters.AddWithValue("@Surname", usr.Surname);
                cmd.Parameters.AddWithValue("@SecondName", usr.SecondName);
                cmd.Parameters.AddWithValue("@BirthDate", usr.BirthDate);
                cmd.Parameters.AddWithValue("@Login", usr.Login);
                cmd.Parameters.AddWithValue("@Password", usr.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update user record      
        public void UpdateUser(User usr)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", usr.Id);
                cmd.Parameters.AddWithValue("@Name", usr.Name);
                cmd.Parameters.AddWithValue("@Surname", usr.Surname);
                cmd.Parameters.AddWithValue("@SecondName", usr.SecondName);
                cmd.Parameters.AddWithValue("@BirthDate", usr.BirthDate);
                cmd.Parameters.AddWithValue("@Login", usr.Login);
                cmd.Parameters.AddWithValue("@Password", usr.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Delete particular user   
        public void DeleteUser(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular employee    
        public User GetUserData(string login)
        {
            User usr = new User();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblUsers WHERE Login= '" + login + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    usr.Id = Convert.ToInt32(rdr["UserId"]);
                    usr.Name = rdr["Name"].ToString();
                    usr.Surname = rdr["Surname"].ToString();
                    usr.SecondName = rdr["SecondName"].ToString();
                    usr.BirthDate = DateTime.Parse(rdr["BirthDate"].ToString());
                    usr.Login = rdr["Login"].ToString();
                    usr.Password = rdr["Password"].ToString();
                }
            }
            if (usr.Name == null)
                return null;
            return usr;
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

        public IEnumerable<int> GetApplications(int? id)
        {
            List<int> lstApplications = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetApplications", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EventID", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    lstApplications.Add(Convert.ToInt32(rdr["EventID"]));
                }
                con.Close();
            }
            return lstApplications;
        }

        public void Apply(int? id, string name)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spApply", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@EvntId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void RemoveApplication(int? id, string name)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRemoveApplication", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@EvntId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public IEnumerable<Event> GetMyApplications(String author)
        {
            List<Event> lstevent = new List<Event>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetMyApplications", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", author);
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

        public bool CheckApplication(String author, int id)
        {
            bool res;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckApplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", author);
                cmd.Parameters.AddWithValue("@EvntId", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                res = rdr.HasRows;
                con.Close();
            }
            return res;
        }
    }
}
