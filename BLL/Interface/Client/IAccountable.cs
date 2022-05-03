namespace BLL.Interface
{
    public interface IAccountable
    {
        double AmountOfMoney { get; }
        void PutMoney(double sum);
        bool WithdrawMoney(double sum);
    }
}