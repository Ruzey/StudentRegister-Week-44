using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegister
{
    internal class MenuUI
    {
        private bool studentChangesActive;
        private ProgramLogic programLogic;
        public MenuUI(ProgramLogic programLogic)
        {
            this.programLogic = programLogic;
        }
        public void PrintMenu()
        {
            Console.WriteLine("\n1. Registera en ny Student\n"+
                                "2. Ändra en Student\n"+
                                "3. Lista alla Studenter\n"+
                                "9. EXIT");
        }

        public string UserInput()
        {
            Console.Write("\nAnge ditt val: ");
            return Console.ReadLine();
        }

        public void MenuChoices(string inputUser)
        {
            switch (inputUser)
            {
                case "1":
                    NewStudent();
                    break;
                case "2":
                    ChangesToStudent();
                    break;
                case "3":
                    PrintAllStudents();
                    break;
                case "9":
                    programLogic.Stop();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }

        void PrintAllStudents()
        {
            Console.Clear();
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-15}", "StudentID", "Förnamn", "Efternamn", "Stad");
            foreach (var item in programLogic.dbContexUiMenu.Students)
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-15}",item.StudentId,item.FirstName,item.LastName,item.City);
            }
        }

        void NewStudent()
        {
            Console.WriteLine("Lägg till ny student.");
            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();

            Console.Write("Stad: ");
            string city = Console.ReadLine();

            programLogic.AddStudent(firstName, lastName, city);
        }

        void ChangesToStudent()
        {
            studentChangesActive = true;
            Console.Write("Ange studentID på studenten du vill redigera: ");
            string studentToEdit = Console.ReadLine();
            do
            {
                Console.WriteLine("\nAnge vad du vill ändra hos studenten.\n" +
                        "1. Förnamn\n" +
                        "2. Efternamn\n" +
                        "3. Stad\n" +
                        "9. Avsluta ändringar");
                Console.Write("Ange ditt val: ");
                string studentWhatToEdit = Console.ReadLine();

                programLogic.EditStudent(studentToEdit, StudentChangesChoices(studentWhatToEdit), studentWhatToEdit);
            } while (studentChangesActive);

        }

        string StudentChangesChoices(string studentWhatToEdit)
        {
            string changedDataStudent ="";
            switch(studentWhatToEdit)
            {
                case "1":
                    Console.Write("\nAnge nytt förnamn för studenten: ");
                    changedDataStudent = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("\nAnge nytt efternamn för studenten: ");
                    changedDataStudent = Console.ReadLine();
                    break;
                case "3":
                    Console.Write("\nAnge ny stad för studenten: ");
                    changedDataStudent = Console.ReadLine();
                    break;
                case "9":
                    studentChangesActive = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
                    
            }
            return changedDataStudent;
        }


        public void ErrorNotFound()
        {
            Console.WriteLine("ERROR! Det finns ingen student med detta studentID.");
            studentChangesActive = false;
        }
    }
}
