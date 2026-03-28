using System;
using System.Collections.Generic;
using StudentManagementDataService;
using StudentManagementModels;

namespace Student_Info_Management
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new StudentDataService();
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
                        service.ViewStudents();
                        break;

                    case 2:
                        service.Add();
                        break;

                    case 3:
                        service.Update();
                        break;

                    case 4:
                        service.DeleteStudent();
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
    }
}