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
            IStudentDataService dataService = new StudentDBData();
            var appService = new StudentAppService(dataService);
            int choice;

            do
            {
                Console.WriteLine("--- Student Information Management ---");
                Console.WriteLine("1. View All Students");
                Console.WriteLine("2. Add a Student");
                Console.WriteLine("3. Update a Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("\nInvalid input. Please enter a number between 1 and 5.\n");
                    continue;
                }

                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        var students = appService.GetAllStudents();

                        if (students.Count == 0)
                        {
                            Console.WriteLine("No records found.\n");
                            break;
                        }

                        for (int i = 0; i < students.Count; i++)
                        {
                            var s = students[i];
                            Console.WriteLine($"Student {i + 1}:");
                            Console.WriteLine($"Student ID: {s.StudentID}");
                            Console.WriteLine($"Name: {s.Name}");
                            Console.WriteLine($"Course: {s.Course}");
                            Console.WriteLine($"Year: {s.Year}");
                            Console.WriteLine($"Contact No.: {s.ContactNo}");
                            Console.WriteLine($"Email: {s.Email}");
                            Console.WriteLine($"Address: {s.Address}");
                            Console.WriteLine($"Date of Birth: {s.DateOfBirth}");
                            Console.WriteLine();
                        }
                        break;

                    case 2:
                        Student newStudent = ReadStudentInput(appService);

                        if (!appService.AddStudent(newStudent))
                        {
                            Console.WriteLine("A student with this Student ID already exists.\n");
                        }
                        else
                        {
                            Console.WriteLine("Student Added Successfully!\n");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Student ID: ");
                        string updateId = Console.ReadLine();

                        var existing = appService.GetStudentById(updateId);

                        if (existing == null)
                        {
                            Console.WriteLine("Student not found.\n");
                            Console.WriteLine();
                            break;
                        }

                        Student updatedStudent = new Student
                        {
                            StudentID = updateId,
                            Name = ReadRequiredString("Name: "),
                            Course = ReadRequiredString("Course: "),
                            Year = ReadInt("Year: ", 1, 4),
                            ContactNo = ReadPhone(),
                            Email = ReadEmail(),
                            Address = ReadRequiredString("Address: "),
                            DateOfBirth = ReadRequiredString("Date of Birth: ")
                        };

                        appService.UpdateStudent(updatedStudent);
                        
                        Console.WriteLine("Updated Successfully!\n");
                        break;

                    case 4:
                        Console.Write("Enter Student ID: ");
                        string deleteId = Console.ReadLine();

                        if (!appService.DeleteStudent(deleteId))
                        {
                            Console.WriteLine("Student not found.\n");
                        }
                        else
                        {
                            Console.WriteLine("Deleted Successfully!\n");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exiting the program...\n");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.\n");
                        break;
                }
            } while (choice != 5);
        }

        private static Student ReadStudentInput(StudentAppService appService)
        {
            Student student = new Student();

            Console.Write("Student Number: ");
            student.StudentID = Console.ReadLine();

            student.Name = ReadRequiredString("Name: ");

            student.Course = ReadRequiredString("Course: ");

            student.Year = ReadInt("Year: ", 1, 4);

            student.ContactNo = ReadPhone();

            student.Email = ReadEmail();

            student.Address = ReadRequiredString("Address: ");

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

                Console.WriteLine("Input cannot be empty.\n");
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

                Console.WriteLine($"Invalid input. Enter a number between {min} and {max}.\n");
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

                Console.WriteLine("Invalid email format.\n");
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

                Console.WriteLine("Contact number must contain digits only.\n");
            }
        }
    }
}