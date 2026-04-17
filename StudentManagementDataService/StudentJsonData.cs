using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using StudentManagementModels;

namespace StudentManagementDataService
{
    public class StudentJsonData : IStudentDataService
    {
        private List<Student> students = new List<Student>();
        private string _filePath;

        public StudentJsonData()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StudentInfo.json");

            InitializeFile();
        }

        private void InitializeFile()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }

            Load();
        }

        private void Load()
        {
            string json = File.ReadAllText(_filePath);

            students = JsonSerializer.Deserialize<List<Student>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Student>();
        }

        private void Save()
        {
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }

        public void Add(Student student)
        {
            Load();

            student.Id = Guid.NewGuid();
            students.Add(student);

            Save();
        }

        public List<Student> GetAll()
        {
            Load();
            return students;
        }

        public Student GetById(Guid id)
        {
            Load();
            return students.FirstOrDefault(s => s.Id == id);
        }

        public Student GetByStudentId(string studentId)
        {
            Load();
            return students.FirstOrDefault(s => s.StudentID == studentId);
        }

        public void Update(Student student)
        {
            Load();

            var existing = students.FirstOrDefault(s => s.StudentID == student.StudentID);

            if (existing != null)
            {
                student.Id = existing.Id;

                int index = students.IndexOf(existing);
                students[index] = student;

                Save();
            }
        }

        public void DeleteByStudentId(string studentId)
        {
            Load();

            var student = students.FirstOrDefault(s => s.StudentID == studentId);

            if (student != null)
            {
                students.Remove(student);
                Save();
            }
        }

        public void ViewStudents()
        {
            Load();

            if (students.Count == 0)
            {
                Console.WriteLine("No records found.");
                return;
            }

            foreach (var s in students)
            {
                Console.WriteLine("Student ID: " + s.StudentID);
                Console.WriteLine("Name: " + s.Name);
                Console.WriteLine("Course: " + s.Course);
                Console.WriteLine("Year: " + s.Year);
                Console.WriteLine("Contact: " + s.ContactNo);
                Console.WriteLine("Email: " + s.Email);
                Console.WriteLine("Address: " + s.Address);
                Console.WriteLine("Date of Birth: " + s.DateOfBirth);
                Console.WriteLine("----------------------------");
            }
        }
    }
}