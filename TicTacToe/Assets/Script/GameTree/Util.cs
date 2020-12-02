using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    // Retorna, se existir, uma celula em State.Cell.Empty restante para finalizar o jogo, dentro de uma possibilidades de ganho.
    private static int? TryGetLastMoveInPossibility(Dictionary<int, State.Cell> Board, State.Cell StateCellToFind, int[] Possibility)
    {
        int? EmptyCell = null;
        int Match = 0;
        foreach (int Cell in Possibility)
        {
            if (Board[Cell] == StateCellToFind)
            {
                Match++;
            }
            else if (Board[Cell] == State.Cell.Empty)
            {
                EmptyCell = Cell;
            }
        }
        return (Match > 1) ? EmptyCell : null;
    }

    // Retorna, se existir, uma celula em State.Cell.Empty restante para finalizar o jogo, dentro de todas as possibilidades de ganho.
    public static int? TryGetLastMoveInAllPossibilities(Dictionary<int, State.Cell> Board, State.Cell StateCellToFind)
    {
        int? Result = null;
        foreach (var Possibility in Win.Way)
        {
            Result = Util.TryGetLastMoveInPossibility(Board, StateCellToFind, Possibility);
            if (Result != null)
            {
                return Result;
            }
        }
        return Result;
    }

    // Utilizando o estado do tabuleiro e um grupo de possibilidades, retorna as possibilidades que possuem, ou nao, celular com um estado especifico
    public static List<int[]> GetPossibilities(Dictionary<int, State.Cell> Board, List<int[]> Possibilities, bool WithStateCell, State.Cell StateCellToFind)
    {
        List<int[]> Result = new List<int[]>();
        bool CanAddToResult = false;
        foreach (int[] Possibility in Possibilities)
        {
            foreach (int Cell in Possibility)
            {
                if (WithStateCell)
                {
                    if (Board[Cell] == StateCellToFind)
                    {
                        CanAddToResult = true;
                        break;
                    }
                    else
                    {
                        CanAddToResult = false;
                    }
                }
                else
                {
                    if (Board[Cell] == StateCellToFind)
                    {
                        CanAddToResult = false;
                        break;
                    }
                    else
                    {
                        CanAddToResult = true;
                    }
                }
            }
            if (CanAddToResult)
            {
                Result.Add(Possibility);
                CanAddToResult = false;
            }
        }
        return Result;
    }

    // Retorna uma celular qualquer em State.Cell.Empty
    public static int? GetRandomEmptyCell(Dictionary<int, State.Cell> Board)
    {
        ArrayList EmptyCells = new ArrayList();
        foreach (var Cell in Board)
        {
            if (Cell.Value == State.Cell.Empty)
            {
                EmptyCells.Add(Cell.Key);
            }
        }
        int? Result = null;
        if (EmptyCells.Count > 0)
        {
            int r = UnityEngine.Random.Range(0, EmptyCells.Count);
            Result = (int)EmptyCells[r];
        }
        return Result;
    }

    // Se possivel, retorna o maior Value em um Dictionary<int, int>
    public static int? GetHighestValueInDictionary(Dictionary<int, int> dict)
    {
        int? Key = null;
        int Value = 0;
        foreach (var Item in dict)
        {
            if (Item.Value > Value)
            {
                Key = Item.Key;
                Value = Item.Value;
            }
        }
        return Key;
    }

    // Retorna, se possivel, a celula em State.Cell.Empty que pertence ao maior numero possivel de possibilidades
    public static int? GetBestMovement(Dictionary<int, State.Cell> Board, List<int[]> Possibilities)
    {
        Dictionary<int, int> BestMovement = new Dictionary<int, int>();
        foreach (int[] Possibility in Possibilities)
        {
            foreach (int Cell in Possibility)
            {
                if (BestMovement.ContainsKey(Cell))
                {
                    BestMovement[Cell]++;
                }
                else if (Board[Cell] == State.Cell.Empty)
                {
                    BestMovement.Add(Cell, 1);
                }
            }
        }
        return Util.GetHighestValueInDictionary(BestMovement);
    }

    // Verifica se houve ganhador
    public static bool IsWinner(Dictionary<int, State.Cell> Board, State.Cell StateCellToFind)
    {
        bool IsWinner = false;
        foreach (var Possibility in Win.Way)
        {
            foreach (int Cell in Possibility)
            {
                if (Board[Cell] != StateCellToFind)
                {
                    if (IsWinner)
                    {
                        IsWinner = false;
                    }
                    break;
                }
                else
                {
                    if (!IsWinner)
                    {
                        IsWinner = true;
                    }
                }
            }
            if (IsWinner)
            {
                break;
            }
        }
        return IsWinner;
    }

    public static bool IsDraw(Dictionary<int, State.Cell> Board)
    {
        foreach (var Cell in Board)
        {
            if (Cell.Value == State.Cell.Empty)
            {
                return false;
            }
        }
        return true;
    }
}
