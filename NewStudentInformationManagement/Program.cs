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
            var dataService = new StudentDataService();
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
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                }

                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        dataService.ViewStudents();
                        break;

                    case 2:
                        Student newStudent = ReadStudentInput(appService);

                        if (!appService.AddStudent(newStudent))
                        {
                            Console.WriteLine("A student with this Student ID already exists.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Student Added Successfully!");
                            Console.WriteLine();
                        }
                        break;

                    case 3:
                        Console.Write("Enter Student ID: ");
                        string updateId = Console.ReadLine();

                        var existing = appService
                            .GetAllStudents()
                            .FirstOrDefault(s => s.StudentID.Trim().ToLower() == updateId.Trim().ToLower());

                        if (existing == null)
                        {
                            Console.WriteLine("Student not found.\n");
                            break;
                        }

                        Student updatedStudent = new Student
                        {
                            StudentID = updateId
                        };

                        Console.Write("Name: ");
                        updatedStudent.Name = Console.ReadLine();

                        Console.Write("Course: ");
                        updatedStudent.Course = Console.ReadLine();

                        Console.Write("Year: ");
                        updatedStudent.Year = Console.ReadLine();

                        Console.Write("Contact No.: ");
                        updatedStudent.ContactNo = Console.ReadLine();

                        Console.Write("Email: ");
                        updatedStudent.Email = Console.ReadLine();

                        Console.Write("Address: ");
                        updatedStudent.Address = Console.ReadLine();

                        Console.Write("Date of Birth: ");
                        updatedStudent.DateOfBirth = Console.ReadLine();

                        appService.UpdateStudent(updatedStudent);
                        
                        Console.WriteLine("Updated Successfully!\n");
                        break;

                    case 4:
                        Console.Write("Enter Student ID: ");
                        string deleteId = Console.ReadLine();

                        if (!appService.DeleteStudent(deleteId))
                            Console.WriteLine("Student not found.");
                        else
                            Console.WriteLine("Deleted Successfully!");
                        break;

                    case 5:
                        Console.WriteLine("Exiting the program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
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
                string inputId = Console.ReadLine();

                bool exists = appService
                    .GetAllStudents()
                    .Any(s => s.StudentID.Trim().ToLower() == inputId.Trim().ToLower());

                if (exists)
                {
                    Console.WriteLine("A student with this Student ID already exists. Please try again.");
                }
                else
                {
                    student.StudentID = inputId;
                    break;
                }
            }

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