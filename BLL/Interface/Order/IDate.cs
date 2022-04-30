
namespace BLL.Interface
{
    public interface IDate
    {
        int SinceWhen { get; }
        int ToWhen { get; }

        int Year { get; }
        int Month { get; }
        int Day { get; }
    }
}
