using System;
using System.Linq;
using StudentManagementDataService;
using StudentManagementModels;

namespace StudentManagementAppService
{
    public class StudentAppService
    {
        private readonly IStudentDataService _dataService;

        public StudentAppService(IStudentDataService dataService)
        {
            _dataService = dataService;
        }

        public List<Student> GetAllStudents()
        {
            return _dataService.GetAll();
        }

        public bool AddStudent(Student student)
        {
            bool exists = _dataService
                .GetAll()
                .Any(s => s.StudentID.Trim().ToLower() == student.StudentID.Trim().ToLower());

            if (exists)
                return false;

            _dataService.Add(student);
            return true;
        }

        public bool UpdateStudent(Student updatedStudent)
        {
            var existing = _dataService.GetByStudentId(updatedStudent.StudentID);

            if (existing == null)
                return false;

            existing.Name = updatedStudent.Name;
            existing.Course = updatedStudent.Course;
            existing.Year = updatedStudent.Year;
            existing.ContactNo = updatedStudent.ContactNo;
            existing.Email = updatedStudent.Email;
            existing.Address = updatedStudent.Address;
            existing.DateOfBirth = updatedStudent.DateOfBirth;

            _dataService.Update(existing);
            return true;
        }

        public bool DeleteStudent(string studentId)
        {
            var existing = _dataService.GetByStudentId(studentId);

            if (existing == null)
                return false;

            _dataService.DeleteByStudentId(studentId);
            return true;
        }
    }
}