namespace FermierExpert.Services.Contracts
{
    public interface IComparer<T>
    {
        bool Compare(T a, T b);
    }
}
