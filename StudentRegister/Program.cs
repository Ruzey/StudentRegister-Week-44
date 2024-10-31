namespace StudentRegister
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbCtx = new StudentDbContext();
            var ProgramLogic = new ProgramLogic(dbCtx);


            ProgramLogic.Run();


            dbCtx.SaveChanges();
        }
    }
}
