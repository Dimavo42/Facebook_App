using System.Collections.Generic;

namespace MyFBApp
{
    public class SortReverse : IStrategy
    {
        public int elementMoves { get; private set; }

        public object Operation(object i_Data)
        {
            List<ImageListFormDataBinding> list = i_Data as List<ImageListFormDataBinding>;
            elementMoves = 0;
            list.Sort((a, b) =>
            {
                int result = a.CompareTo(b);
                if (result != 0)
                    elementMoves++;
                return result;
            });
            list.Reverse();
            elementMoves += list.Count - 1;
            return list;
        }

        public int OperationCount()
        {
            return elementMoves;
        }
    }
}
