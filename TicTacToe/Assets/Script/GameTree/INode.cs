using System.Collections;
using System.Collections.Generic;

interface INode
{
    int Execute(Dictionary<int, State.Cell> Board);
}
