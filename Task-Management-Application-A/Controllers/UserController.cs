using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Management_Application_A.Models;

namespace Task_Management_Application_A.Controllers
{
    public class UserController : Controller
    {
        public static string ConnStr = ConfigurationManager.ConnectionStrings["TaskManagementConnection"].ConnectionString;

        // GET: Task
        public ActionResult Index()
        {
            List<User> userList = new List<User>();
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT * FROM [User]";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();
                        user.UserUID = (int)reader["UserUID"];
                        user.Name = reader["Name"].ToString();
                        user.Address = reader["Address"].ToString();
                        user.EmailID = reader["EmailID"].ToString();
                        user.ContactNo = reader["ContactNo"].ToString();
                        user.IsDeleted = (Boolean)reader["IsDeleted"];

                        // Set other properties accordingly

                        userList.Add(user);
                    }
                    reader.Close();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
           
                return View(userList);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            User user = new User();
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT * FROM [User] where UserUID ="+ id;
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.UserUID = (int)reader["UserUID"];
                        user.Name = reader["Name"].ToString();
                        user.Address = reader["Address"].ToString();
                        user.EmailID = reader["EmailID"].ToString();
                        user.ContactNo = reader["ContactNo"].ToString();
                        user.IsDeleted = (Boolean)reader["IsDeleted"];

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(user);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "INSERT INTO [User] (Name, EmailID, ContactNo,Address,IsDeleted) VALUES (@Name, @Email, @ContactNo, @Address,@IsDeleted)";

                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.EmailID);
                    command.Parameters.AddWithValue("@ContactNo", user.ContactNo);
                    command.Parameters.AddWithValue("@Address", user.Address);
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // User created successfully
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Failed to create user
                        ViewBag.ErrorMessage = "Failed to create user.";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            User user = new User();
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT * FROM [User] where UserUID = @UserId";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@UserId", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.UserUID = (int)reader["UserUID"];
                        user.Name = reader["Name"].ToString();
                        user.Address = reader["Address"].ToString();
                        user.EmailID = reader["EmailID"].ToString();
                        user.ContactNo = reader["ContactNo"].ToString();
                        user.IsDeleted = (Boolean)reader["IsDeleted"];

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(user);
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "UPDATE [User] SET Name = @Name, EmailID = @Email, Address = @Address, ContactNo=@ContactNo WHERE UserUID = @UserId";

                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.EmailID);
                    command.Parameters.AddWithValue("@ContactNo", user.ContactNo);
                    command.Parameters.AddWithValue("@Address", user.Address);
                    command.Parameters.AddWithValue("@UserId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // User created successfully
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Failed to create user
                        ViewBag.ErrorMessage = "Failed to create user.";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            User user = new User();
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT * FROM [User] where UserUID = @UserId";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@UserId", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.UserUID = (int)reader["UserUID"];
                        user.Name = reader["Name"].ToString();
                        user.Address = reader["Address"].ToString();
                        user.EmailID = reader["EmailID"].ToString();
                        user.ContactNo = reader["ContactNo"].ToString();
                        user.IsDeleted = (Boolean)reader["IsDeleted"];

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(user);
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "DELETE FROM [User] WHERE UserUID = @UserId";
                    SqlCommand command = new SqlCommand(query, Dbcon);

                    command.Parameters.AddWithValue("@UserId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // User created successfully
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Failed to create user
                        ViewBag.ErrorMessage = "Failed to create user.";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
    }
}
