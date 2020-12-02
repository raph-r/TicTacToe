using System.Collections;
using System.Collections.Generic;

public class CompositeCanIAWin : INode
{
    public CompositeCanIAWin(){}

    public int Execute(Dictionary<int, State.Cell> Board)
    {
        // Tenta bloquear o inimigo
        int? Result = Util.TryGetLastMoveInAllPossibilities(Board, State.Cell.AI);
        return (Result != null) ? (int)Result : new CompositeCanPlayerWin().Execute(Board);
    }
}
