using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using StudentManagementModels;

namespace StudentManagementDataService
{
    public class StudentDBData : IStudentDataService
    {
        private string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True;TrustServerCertificate=True;";
        private SqlConnection sqlConnection;

        public StudentDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public void Add(Student student)
        {
            string query = @"
                INSERT INTO Students
                (Id, StudentID, Name, Course, Year, ContactNo, Email, Address, DateOfBirth)
                VALUES
                (@id, @studentId, @name, @course, @year, @contactNo, @email, @address, @dob)";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@studentId", student.StudentID);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@course", student.Course);
            cmd.Parameters.AddWithValue("@year", student.Year);
            cmd.Parameters.AddWithValue("@contactNo", student.ContactNo);
            cmd.Parameters.AddWithValue("@email", student.Email);
            cmd.Parameters.AddWithValue("@address", student.Address);
            cmd.Parameters.AddWithValue("@dob", student.DateOfBirth);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            string query = "SELECT * FROM Students";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student();

                student.Id = Guid.Parse(reader["Id"].ToString());
                student.StudentID = reader["StudentID"].ToString();
                student.Name = reader["Name"].ToString();
                student.Course = reader["Course"].ToString();
                student.Year = Convert.ToInt32(reader["Year"]);
                student.ContactNo = reader["ContactNo"].ToString();
                student.Email = reader["Email"].ToString();
                student.Address = reader["Address"].ToString();
                student.DateOfBirth = reader["DateOfBirth"].ToString();

                students.Add(student);
            }

            sqlConnection.Close();
            return students;
        }

        public Student GetById(Guid id)
        {
            Student student = null;

            string query = "SELECT * FROM Students WHERE Id = @id";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                student = new Student
                {
                    Id = Guid.Parse(reader["Id"].ToString()),
                    StudentID = reader["StudentID"].ToString(),
                    Name = reader["Name"].ToString(),
                    Course = reader["Course"].ToString(),
                    Year = Convert.ToInt32(reader["Year"]),
                    ContactNo = reader["ContactNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    DateOfBirth = reader["DateOfBirth"].ToString()
                };
            }

            sqlConnection.Close();
            return student;
        }

        public Student GetByStudentId(string studentId)
        {
            Student student = null;

            string query = "SELECT * FROM Students WHERE StudentID = @studentId";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@studentId", studentId);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                student = new Student
                {
                    Id = Guid.Parse(reader["Id"].ToString()),
                    StudentID = reader["StudentID"].ToString(),
                    Name = reader["Name"].ToString(),
                    Course = reader["Course"].ToString(),
                    Year = Convert.ToInt32(reader["Year"]),
                    ContactNo = reader["ContactNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    DateOfBirth = reader["DateOfBirth"].ToString()
                };
            }

            sqlConnection.Close();
            return student;
        }

        public void Update(Student student)
        {
            string query = @"
                UPDATE Students
                SET StudentID = @studentId,
                    Name = @name,
                    Course = @course,
                    Year = @year,
                    ContactNo = @contactNo,
                    Email = @email,
                    Address = @address,
                    DateOfBirth = @dob
                WHERE Id = @id";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@studentId", student.StudentID);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@course", student.Course);
            cmd.Parameters.AddWithValue("@year", student.Year);
            cmd.Parameters.AddWithValue("@contactNo", student.ContactNo);
            cmd.Parameters.AddWithValue("@email", student.Email);
            cmd.Parameters.AddWithValue("@address", student.Address);
            cmd.Parameters.AddWithValue("@dob", student.DateOfBirth);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void DeleteByStudentId(string studentId)
        {
            string query = "DELETE FROM Students WHERE StudentID = @studentId";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@studentId", studentId);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public bool TestConnection()
        {
            try
            {
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}