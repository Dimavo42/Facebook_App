namespace MyFBApp
{
    public interface IStrategy
    {
        int elementMoves { get; }

        object Operation(object i_Data);

        int OperationCount();
    }
}
