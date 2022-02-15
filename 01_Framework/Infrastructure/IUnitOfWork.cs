namespace _01_Framework.Infrastructure {
    public interface IUnitOfWork {
        void BeginTran ();
        void Commit ();
        void RollBack ();
    }
}
