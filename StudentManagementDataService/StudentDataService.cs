using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagementModels;

namespace StudentManagementDataService
{
    public class StudentDataService : IStudentDataService
    {
        private List<Student> students = new List<Student>();

        public void Add(Student student)
        {
            student.Id = Guid.NewGuid();
            students.Add(student);
        }

        public List<Student> GetAll()
        {
            return students;
        }

        public Student GetById(Guid id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        public Student GetByStudentId(string studentId)
        {
            return students.FirstOrDefault(s => s.StudentID == studentId);
        }

        public void Update(Student student)
        {
            var existing = GetByStudentId(student.StudentID);

            if (existing != null)
            {
                student.Id = existing.Id; // preserve Guid
                int index = students.IndexOf(existing);
                students[index] = student;
            }
        }

        public void DeleteByStudentId(string studentId)
        {
            var student = GetByStudentId(studentId);

            if (student != null)
            {
                students.Remove(student);
            }
        }

        public void ViewStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No records found. Please make a new record.");
                Console.WriteLine();
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine("Student {0}:", i + 1);
                Console.WriteLine("Student ID: " + students[i].StudentID);
                Console.WriteLine("Full Name: " + students[i].Name);
                Console.WriteLine("Course: " + students[i].Course);
                Console.WriteLine("Year: " + students[i].Year);
                Console.WriteLine("Contact No.: " + students[i].ContactNo);
                Console.WriteLine("Email: " + students[i].Email);
                Console.WriteLine("Full Address: " + students[i].Address);
                Console.WriteLine("Date of Birth: " + students[i].DateOfBirth);
            }
        }
    }
}