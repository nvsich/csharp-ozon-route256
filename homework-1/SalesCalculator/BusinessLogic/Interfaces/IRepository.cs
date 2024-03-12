namespace ApplicationCore.Interfaces;

public interface IRepository<T> where T : class
{
    List<T> Find(Func<T, bool> predicate);
}