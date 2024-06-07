namespace RichillCapital.Texas.Modules;

public interface ITexasService
{
    void StartSession();
    void AddPlayer(string id, string name);
    void BuyIn(int groups = 1);
    void CashOut(int finalChipValue);
    void CloseSession();
}
