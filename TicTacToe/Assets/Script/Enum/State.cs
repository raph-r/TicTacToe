using System.Collections;
using System.Collections.Generic;

public class State
{
    // Estado do resultado
    public enum Match {InProgress, PlayerWin, IAWin, Draw};

    // Estado das celulas
    public enum Cell {Empty, Player, AI};

    // Estado do jogo
    public enum Game {Configure, Start, TurnOfPlayer, TurnOfAI, Finished};
}
