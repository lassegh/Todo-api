using System;
using IIS.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IIS.Service
{
	public class ToDoService
	{
        private readonly string _connectionString;

        public ToDoService(DatabaseOptions options)
        {
	        _connectionString = options.ConnectionString;
        }

        public List<ToDoModel> GetAllTodos()
        {
            string queryString = "Select * from dbo.ToDoTable";
            List<ToDoModel> todoList = new List<ToDoModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ToDoModel todo = new ToDoModel
                    {
                        Id = reader.GetInt32(0),
                        Content = reader.GetString(1),
                        IsDone = reader.GetBoolean(2)
                    };
                    todoList.Add(todo);
                }
            
                command.Connection.Close();
            }

            return todoList;
        }

        public ToDoModel AddTodo(ToDoModel todo)
        {
            string postString = "Insert dbo.ToDoTable (Content, IsDone) Values (@Content, @IsDone); Select SCOPE_IDENTITY()";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(postString, conn);

                cmd.Parameters.AddWithValue("@Content", todo.Content);
                cmd.Parameters.AddWithValue("@IsDone", todo.IsDone);

                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
	                todo.Id = Convert.ToInt32(reader.GetDecimal(0));
                }
                cmd.Connection.Close();

                return todo;
            }
        }

        public bool UpdateTodo(ToDoModel todo, int id)
        {
            string queryString = "Update dbo.ToDoTable set Content=@Content, IsDone=@IsDone where ToDoId=@Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Content", todo.Content);
                command.Parameters.AddWithValue("@IsDone", todo.IsDone);
                command.Parameters.AddWithValue("@Id", id);
                command.Connection.Open();

                int response = command.ExecuteNonQuery();

                command.Connection.Close();

                if (response == 0) return false;
                return true;
            }
        }
    }
}
