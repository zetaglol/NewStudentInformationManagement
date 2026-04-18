using System;
using System.Collections.Generic;
using StudentManagementAppService;
using StudentManagementDataService;
using StudentManagementModels;

namespace Student_Info_Management
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStudentDataService dataService = new StudentJsonData();
            var appService = new StudentAppService(dataService);
            int choice;

            do
            {
                Console.WriteLine("\n====== PUP STUDENT INFORMATION MANAGEMENT ======");
                Console.WriteLine("1. View All Students");
                Console.WriteLine("2. Add a Student");
                Console.WriteLine("3. Update a Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("Enter Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("\n================================================");
                    Console.WriteLine("Invalid input!\nPlease enter a number between 1 and 5.");
                    continue;
                }

                Console.WriteLine("\n================================================");
                switch (choice)
                {
                    case 1:
                        var students = appService.GetAllStudents();

                        if (students.Count == 0)
                        {
                            Console.WriteLine("No records found.");
                            break;
                        }

                        for (int i = 0; i < students.Count; i++)
                        {
                            var s = students[i];
                            Console.WriteLine($"Student {i + 1}:");
                            Console.WriteLine($"Student ID: {s.StudentID}");
                            Console.WriteLine($"Full Name (Surname, First Name M.I.): {s.Name}");
                            Console.WriteLine($"Course (e.g. BSIT): {s.Course}");
                            Console.WriteLine($"Year (e.g. 1): {s.Year}");
                            Console.WriteLine($"Contact No.: {s.ContactNo}");
                            Console.WriteLine($"Email: {s.Email}");
                            Console.WriteLine($"Full Address: {s.Address}");
                            Console.WriteLine($"Date of Birth: {s.DateOfBirth}");
                            Console.WriteLine("\n================================================");
                        }
                        break;

                    case 2:
                        Student newStudent = ReadStudentInput(appService);

                        appService.AddStudent(newStudent);

                        Console.WriteLine("Student Added Successfully!\n");
                        break;

                    case 3:
                        Console.Write("Enter Student ID: ");
                        string updateId = Console.ReadLine();

                        var existing = appService.GetStudentById(updateId);

                        if (existing == null)
                        {
                            Console.WriteLine("\n================================================");
                            Console.WriteLine("Student not found.");
                            break;
                        }

                        Student updatedStudent = new Student
                        {
                            StudentID = updateId,
                            Name = ReadRequiredString("Full Name: "),
                            Course = ReadRequiredString("Course: "),
                            Year = ReadInt("Year: ", 1, 4),
                            ContactNo = ReadPhone(),
                            Email = ReadEmail(),
                            Address = ReadRequiredString("Full Address: "),
                            DateOfBirth = ReadRequiredString("Date of Birth: ")
                        };

                        appService.UpdateStudent(updatedStudent);

                        Console.WriteLine("\n================================================");
                        Console.WriteLine("Student information updated successfully!");
                        break;

                    case 4:
                        Console.Write("Enter Student ID: ");
                        string deleteId = Console.ReadLine();

                        if (!appService.DeleteStudent(deleteId))
                        {
                            Console.WriteLine("\n================================================");
                            Console.WriteLine("Student not found.");
                        }
                        else
                        {
                            Console.WriteLine("\n================================================");
                            Console.WriteLine("Student information deleted successfully!");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exiting the program...\n");
                        break;

                    default:
                        Console.WriteLine("\n================================================");
                        Console.WriteLine("Invalid choice!\nPlease enter a number between 1 and 5.");
                        break;
                }
            } while (choice != 5);
        }

        private static Student ReadStudentInput(StudentAppService appService)
        {
            Student student = new Student();

            while (true)
            {
                Console.Write("Student Number: ");
                student.StudentID = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(student.StudentID))
                {
                    Console.WriteLine("Student ID cannot be empty.\n");
                    continue;
                }

                if (appService.StudentExists(student.StudentID))
                {
                    Console.WriteLine("\nA student with this Student ID already exists.");
                    continue;
                }
                break;
            }

            student.Name = ReadRequiredString("Full Name (Surname, First Name M.I.): ");

            student.Course = ReadRequiredString("Course (e.g. BSIT): ");

            student.Year = ReadInt("Year (e.g. 1): ", 1, 4);

            student.ContactNo = ReadPhone();

            student.Email = ReadEmail();

            student.Address = ReadRequiredString("Full Address: ");

            student.DateOfBirth = ReadRequiredString("Date of Birth: ");

            return student;
        }

        private static string ReadRequiredString(string prompt)
        {
            string input;

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("\nInput cannot be empty!");
            }
        }

        private static int ReadInt(string prompt, int min = 0, int max = int.MaxValue)
        {
            int value;

            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out value) &&
                    value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"\nInvalid input!\nEnter a number between {min} and {max}.");
            }
        }

        private static string ReadEmail()
        {
            string email;

            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                    return email;

                Console.WriteLine("\nInvalid email format!");
            }
        }

        private static string ReadPhone()
        {
            string phone;

            while (true)
            {
                Console.Write("Contact No.: ");
                phone = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(phone) && phone.All(char.IsDigit))
                    return phone;

                Console.WriteLine("\nContact number must contain digits only!");
            }
        }
    }
}