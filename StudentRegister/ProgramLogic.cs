using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegister
{
    internal class ProgramLogic
    {
        public MenuUI menuUI;
        bool isRunning = true;
        private StudentDbContext dbCtx;
        public StudentDbContext dbContexUiMenu => dbCtx;

        public ProgramLogic(StudentDbContext dbCtx)
        {
            this.dbCtx = dbCtx;
            menuUI = new MenuUI(this);
        }

        public void Run()
        {
            do
            {
                menuUI.PrintMenu();
                menuUI.MenuChoices(menuUI.UserInput());

            } while (isRunning);
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void AddStudent(string firstName, string lastName, string city)
        {
            dbCtx.Students.Add(new Student() { FirstName = firstName, LastName = lastName, City = city });
            dbCtx.SaveChanges();
        }

        public void EditStudent(string studentToEdit, string changedDataStudent, string studentWhatToEdit)
        {
            var std = dbCtx.Students.Where(a => a.StudentId == int.Parse(studentToEdit)).FirstOrDefault<Student>();
            if (std == null)
            {
                menuUI.ErrorNotFound();
                return;
            }
            switch (studentWhatToEdit)
            {
                case "1":
                    std.FirstName = changedDataStudent;
                    break;
                case "2":
                    std.LastName = changedDataStudent;
                    break;
                case "3":
                    std.City = changedDataStudent;
                    break;
            }
            dbCtx.SaveChanges();

        }

    }
}
