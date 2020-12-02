using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    
public class Controller : MonoBehaviour
{
    public Canvas set_btn;

    public Text TxtWinner;

    private Dictionary<int, State.Cell> Board;

    private State.Game StateGame;
    private State.Match StateMatch;

    private BehaviorTree Tree;

    // Start is called before the first frame update
    void Start()
    {
        this.Board = new Dictionary<int, State.Cell>();
        this.Tree = new BehaviorTree(); 
        this.StateGame = State.Game.Configure;
        this.TryExecuteGameState();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.StateMatch == State.Match.InProgress)
        {
            this.TryExecuteGameState();
        }
    }

    private void ResetAllTextCell()
    {
        foreach (Button btn in this.set_btn.GetComponentsInChildren<Button>())
        {
            btn.GetComponentInChildren<Text>().text = "-";
        }
    }

    private void SetBoardToEmptyState()
    {
        if (this.Board.Count > 0)
        {
            this.Board.Clear();
        }
        for (int i = 0; i < 9; i++)
        {
            this.Board.Add(i, State.Cell.Empty);
        }
    }

    public void ConfigureGame()
    {
        this.ResetAllTextCell();
        this.SetBoardToEmptyState();
        this.TxtWinner.text = "";
        this.StateMatch = State.Match.InProgress;
        this.StateGame = State.Game.Start;
    }

    private void TryExecuteGameState()
    {
        switch (this.StateGame)
        {
            case State.Game.Configure:
                this.ConfigureGame();
                break;
            case State.Game.Start:
                this.DrawPlayerOne();
                break;
            case State.Game.TurnOfAI:
                this.MakeMove(this.Tree.Execute(this.Board), State.Cell.AI, "O");
                break;
            default:
                break;
        }
    }

    private void DrawPlayerOne()
    {
        this.StateGame = (UnityEngine.Random.Range(0, 2) == 1) ? State.Game.TurnOfPlayer : State.Game.TurnOfAI;
    }

    public void MakeMove(int Cell, State.Cell StateCell, string symbol)
    {
        if (this.Board[Cell] == State.Cell.Empty)
        {
            this.Board[Cell] = StateCell;
            this.set_btn.GetComponentsInChildren<Button>()[Cell].GetComponentInChildren<Text>().text = symbol;
            this.UpdateMatchState(StateCell);
            this.UpdateGameState();
        }
    }

    public void MakePlayerMove(int Cell)
    {
        if (this.StateMatch == State.Match.InProgress)
        {
            this.MakeMove(Cell, State.Cell.Player, "X");
        }
    }

    public void UpdateMatchState(State.Cell StateCellToFind)
    {
        if (Util.IsWinner(this.Board, StateCellToFind))
        {
            this.StateMatch = (StateCellToFind == State.Cell.AI) ? State.Match.IAWin : State.Match.PlayerWin;
            this.StateGame = State.Game.Finished;
            this.TxtWinner.text = this.WinnerMessage();
        }
        else if (Util.IsDraw(this.Board))
        {
            this.StateMatch = State.Match.Draw;
            this.StateGame = State.Game.Finished;
            this.TxtWinner.text = this.WinnerMessage();
        }
    }

    public void UpdateGameState()
    {
        if (this.StateMatch == State.Match.InProgress)
        {
            this.StateGame = this.StateGame == State.Game.TurnOfAI ? State.Game.TurnOfPlayer : State.Game.TurnOfAI;
        }
    }

    private string WinnerMessage()
    {
        switch (this.StateMatch)
        {
            case State.Match.IAWin:
                return "You Lose =(";
            case State.Match.PlayerWin:
                return "You Win =)";
            case State.Match.Draw:
                return "Draw =/";
            default:
                return "Error =(";
        }
    }
}
