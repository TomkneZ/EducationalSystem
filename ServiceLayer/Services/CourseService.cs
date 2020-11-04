﻿using DatabaseStructure;
using DatabaseStructure.Models;
using ServiceLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly DBContext context;

        public CourseService(DBContext context)
        {
            this.context = context;
        }
        public void AddProfessor(int professorId, int courseId)
        {
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            var professor = context.Professors.FirstOrDefault(p => p.PersonId == professorId);
            course.CourseProfessor = professor;
            context.SaveChanges();
        }

        public void AddStudent(int studentId, int courseId)
        {

        }

        public Professor ShowProfessorInfo(int courseId)
        {
            var course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            return course.CourseProfessor;
        }

    }
}
