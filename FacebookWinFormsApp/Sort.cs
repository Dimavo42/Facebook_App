using System.Collections.Generic;

namespace MyFBApp
{
    public class Sort : IStrategy
    {
        public int elementMoves { get; private set; }

        public object Operation(object i_Data)
        {
            List<ImageListFormDataBinding> list = i_Data as List<ImageListFormDataBinding>;
            elementMoves = 0;
            list.Sort((elementA, elementB) =>
            {
                int result = elementA.CompareTo(elementB);
                if (result != 0)
                    elementMoves++;
                return result;

            });
            return list;
        }

        public int OperationCount()
        {
            return elementMoves;
        }
    }
}
