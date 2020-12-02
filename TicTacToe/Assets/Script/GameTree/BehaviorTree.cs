using System.Collections;
using System.Collections.Generic;

public class BehaviorTree : INode
{
    public BehaviorTree(){}
    public int Execute(Dictionary<int, State.Cell> Board)
    {
        return new CompositeCanIAWin().Execute(Board);
    }
}
