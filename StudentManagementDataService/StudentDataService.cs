using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagementModels;

namespace StudentManagementDataService
{
    public class StudentDataService
    {
        private List<Student> students = new List<Student>();

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

        public void Add()
        {
            Student s = ReadStudentInput();
            students.Add(s);
            Console.WriteLine("Student Added Successfully!");
            Console.WriteLine();
        }

        public void Update()
        {
            Console.Write("Enter Student Number to Update: ");

            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine();
                return;
            }
            index -= 1;

            if (index < 0 || index >= students.Count)
            {
                Console.WriteLine("Student not found.");
                Console.WriteLine();
                return;
            }

            Student updatedStudent = ReadStudentInput();
            students[index] = updatedStudent;
            Console.WriteLine("Student Updated Successfully!");
            Console.WriteLine();
        }

        public void DeleteStudent()
        {
            Console.Write("Enter Student Number to Delete: ");

            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine();
                return;
            }
            index -= 1;

            if (index < 0 || index >= students.Count)
            {
                Console.WriteLine("Student not found.");
                Console.WriteLine();
                return;
            }

            students.RemoveAt(index);
            Console.WriteLine("Student Deleted Successfully!");
            Console.WriteLine();
        }

        private Student ReadStudentInput()
        {
            Student student = new Student();

            Console.Write("Student Number: ");
            student.StudentID = Console.ReadLine();

            Console.Write("Name (Surname, First Name M.I.): ");
            student.Name = Console.ReadLine();

            Console.Write("Course (e.g. BSIT): ");
            student.Course = Console.ReadLine();

            Console.Write("Year (e.g. 1): ");
            student.Year = Console.ReadLine();

            Console.Write("Contact No.: ");
            student.ContactNo = Console.ReadLine();

            Console.Write("Email: ");
            student.Email = Console.ReadLine();

            Console.Write("Full Address: ");
            student.Address = Console.ReadLine();

            Console.Write("Date of Birth: ");
            student.DateOfBirth = Console.ReadLine();

            return student;
        }
    }
}