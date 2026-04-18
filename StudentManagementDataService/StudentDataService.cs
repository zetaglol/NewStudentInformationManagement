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
            students.Add(student);
        }

        public List<Student> GetAll()
        {
            return students;
        }

        public Student GetByStudentId(string studentId)
        {
            return students.FirstOrDefault(s => s.StudentID == studentId);
        }

        public bool ExistsByStudentId(string studentId)
        {
            return students.Any(s => s.StudentID == studentId);
        }

        public void Update(Student student)
        {
            var existing = GetByStudentId(student.StudentID);

            if (existing != null)
            {
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
    }
}