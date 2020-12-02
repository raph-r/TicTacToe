using System.Collections;
using System.Collections.Generic;

public class CompositeCanPlayerWin : INode
{
    public CompositeCanPlayerWin(){}

    public int Execute(Dictionary<int, State.Cell> Board)
    {
        // Tenta vencer
        int? Result = Util.TryGetLastMoveInAllPossibilities(Board, State.Cell.Player);
        return (Result != null) ? (int)Result : new CompositeFindBestMoveForIA().Execute(Board);
    }
}