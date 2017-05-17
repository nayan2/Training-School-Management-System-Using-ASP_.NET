using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class TsmsDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Batche> Batches { get; set; }
        public DbSet<Studentassignbatche> Studentassignbatches { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Financeofinstructor> Financeofinstructors { get; set; }
        public DbSet<Financeofstudent> Financeofstudents { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamAssign> ExamAssigns { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}