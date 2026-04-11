using System;
using System.Collections.Generic;
using StudentManagementModels;

namespace StudentManagementDataService
{
    public interface IStudentDataService
    {
        void Add(Student student);
        List<Student> GetAll();
        Student GetById(Guid id);

        Student GetByStudentId(string studentId);
        void Update(Student student);
        void DeleteByStudentId(string studentId);
    }
}