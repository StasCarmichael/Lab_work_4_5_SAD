using System;

namespace BLL.Interface
{
    public interface ITimeInterval
    {
        int SinceWhen { get; }
        int ToWhen { get; }
        DateTime Date { get; }
    }
}
