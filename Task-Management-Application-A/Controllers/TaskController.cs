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
    public class TaskController : Controller
    {
        public static string ConnStr = ConfigurationManager.ConnectionStrings["TaskManagementConnection"].ConnectionString;

        // GET: Task
        public ActionResult Index()
        {
            List<Task> taskList = new List<Task>();
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT TD.*, U.Name FROM TaskDetails AS TD JOIN [User] AS U ON TD.AssigneeTo = U.UserUID";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Task task = new Task();
                        task.TaskUID = (int)reader["TaskId"];
                        task.Title = reader["Title"].ToString();
                        task.Description = reader["Description"].ToString();
                        task.Priority = (int)reader["Priority"];
                        task.AssigneeTo = (int)reader["AssigneeTo"];
                        task.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                        task.DueDate = Convert.ToDateTime(reader["DueDate"]);
                        task.AssignetoVal = reader["Name"].ToString();
                        if (task.Priority == 1)
                        {
                            task.PriorityVal = "Low";
                        }
                        else if (task.Priority == 2)
                        {
                            task.PriorityVal = "Medium";
                        }
                        else
                        {
                            task.PriorityVal = "High";
                        }


                        // Set other properties accordingly

                        taskList.Add(task);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View(taskList);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            Task task = new Task();
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT TD.*, U.Name FROM TaskDetails AS TD JOIN [User] AS U ON TD.AssigneeTo = U.UserUID where TaskId =" + id;
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        task.TaskUID = (int)reader["TaskId"];
                        task.Title = reader["Title"].ToString();
                        task.Description = reader["Description"].ToString();
                        task.Priority = (int)reader["Priority"];
                        
                        task.AssigneeTo = (int)reader["AssigneeTo"];
                        task.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                        task.DueDate = Convert.ToDateTime(reader["DueDate"]);
                        task.AssignetoVal = reader["Name"].ToString();
                        if (task.Priority == 1)
                        {
                            task.PriorityVal = "Low";
                        }
                        else if (task.Priority == 2)
                        {
                            task.PriorityVal = "Medium";
                        }
                        else
                        {
                            task.PriorityVal = "High";
                        }


                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            Task task = new Task();
            try
            {
                List<SelectListItem> userList = new List<SelectListItem>();
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT UserUID,Name FROM [User]";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userList.Add(new SelectListItem()
                        {
                            Text = reader["UserUID"].ToString(),
                            Value = reader["Name"].ToString()
                        });
                    }
                    task.UserList = userList;
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            

            return View(task);
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(Task task)
        {

            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "INSERT INTO TaskDetails (Title, Description, DueDate,Priority,AssigneeTo,IsCompleted) VALUES (@Title, @Description, @DueDate, @Priority,@AssigneeTo,@IsCompleted)";

                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@DueDate", task.DueDate);
                    command.Parameters.AddWithValue("@Priority", task.Priority);
                    command.Parameters.AddWithValue("@AssigneeTo", task.AssigneeTo);
                    command.Parameters.AddWithValue("@IsCompleted", false);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // User created successfully
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Failed to create user
                        ViewBag.ErrorMessage = "Failed to create task.";
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
            Task task = new Task();
            try
            {
                List<SelectListItem> userList = new List<SelectListItem>();
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT UserUID,Name FROM [User]";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userList.Add(new SelectListItem()
                        {
                            Text = reader["UserUID"].ToString(),
                            Value = reader["Name"].ToString()
                        });
                    }
                    task.UserList = userList;
                    reader.Close();
                }

                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT * FROM TaskDetails where TaskId = @TaskId";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@TaskId", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        task.TaskUID = (int)reader["TaskId"];
                        task.Title = reader["Title"].ToString();
                        task.Description = reader["Description"].ToString();
                        task.Priority = (int)reader["Priority"];
                        task.AssigneeTo = (int)reader["AssigneeTo"];
                        task.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                        task.DueDate = Convert.ToDateTime(reader["DueDate"]);

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "UPDATE TaskDetails SET Title = @Title, Description = @Description, DueDate = @DueDate, Priority=@Priority, AssigneeTo = @AssigneeTo, IsCompleted = @IsCompleted WHERE TaskId = @TaskId";

                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@DueDate", task.DueDate);
                    command.Parameters.AddWithValue("@Priority", task.Priority);
                    command.Parameters.AddWithValue("@AssigneeTo", task.AssigneeTo);
                    command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                    command.Parameters.AddWithValue("@TaskId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // User created successfully
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Failed to create user
                        ViewBag.ErrorMessage = "Failed to task update.";
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
            Task task = new Task();
            try
            {
                using (SqlConnection Dbcon = new SqlConnection(ConnStr))
                {
                    Dbcon.Open();
                    string query = "SELECT TD.*, U.Name FROM TaskDetails AS TD JOIN [User] AS U ON TD.AssigneeTo = U.UserUID where TaskId = @TaskId";
                    SqlCommand command = new SqlCommand(query, Dbcon);
                    command.Parameters.AddWithValue("@TaskId", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        task.TaskUID = (int)reader["TaskId"];
                        task.Title = reader["Title"].ToString();
                        task.Description = reader["Description"].ToString();
                        task.Priority = (int)reader["Priority"];
                        task.AssigneeTo = (int)reader["AssigneeTo"];
                        task.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                        task.DueDate = Convert.ToDateTime(reader["DueDate"]);
                        task.AssignetoVal = reader["Name"].ToString();
                        if (task.Priority == 1)
                        {
                            task.PriorityVal = "Low";
                        }
                        else if (task.Priority == 2)
                        {
                            task.PriorityVal = "Medium";
                        }
                        else
                        {
                            task.PriorityVal = "High";
                        }



                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(task);
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
                    string query = "DELETE FROM TaskDetails WHERE TaskId = @TaskId";
                    SqlCommand command = new SqlCommand(query, Dbcon);

                    command.Parameters.AddWithValue("@TaskId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // User created successfully
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Failed to create user
                        ViewBag.ErrorMessage = "Failed to delete task.";
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