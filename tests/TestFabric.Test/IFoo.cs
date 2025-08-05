namespace TestFabric.Test;

public interface IFoo
{
    int Id { get; }
    double Value { get; set; }
    void Do();
    void Do(int arg1);
    void Do(double arg1, bool arg2);
    void Do(bool arg1, double arg2, string arg3);
    void Do(string arg1, bool arg2, double arg3, Guid arg4);
    void Do(Guid arg1, bool arg2, double arg3, string arg4, int arg5);
    int Calculate();
    bool Calculate(double arg1);
    string Calculate(double arg1, bool arg2);
    double Calculate(bool arg1, double arg2, string arg3);
    Guid Calculate(string arg1, bool arg2, double arg3, Guid arg4);
    byte Calculate(Guid arg1, bool arg2, double arg3, string arg4, int arg5);
}
