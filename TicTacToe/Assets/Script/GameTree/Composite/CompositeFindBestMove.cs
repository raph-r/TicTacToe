using System.Collections;
using System.Collections.Generic;

public class CompositeFindBestMoveForIA : INode
{
    public CompositeFindBestMoveForIA(){}
    public int Execute(Dictionary<int, State.Cell> Board)
    {
        int? Result = null;

        // Tira as possibilidades que dependem de uma posicao onde o jogador marcou
        List<int[]> PossibilitiesWithoutPlayerMovements = Util.GetPossibilities(Board, Win.Way, false, State.Cell.Player);
        // Segue com o empate ou efetua a primeira jogada
        if (PossibilitiesWithoutPlayerMovements.Count == 0 || PossibilitiesWithoutPlayerMovements.Count == Win.Way.Count)
        {
            Result = Util.GetRandomEmptyCell(Board);
        }
        else
        {
            // Das possibilidades que não dependem das posicoes onde o jogador marcou, procura as possibilidades possuiem posicoes marcadas com a IA
            List<int[]> PossibilitiesWithIAMovements = Util.GetPossibilities(Board, PossibilitiesWithoutPlayerMovements, true, State.Cell.AI);
            if (PossibilitiesWithIAMovements.Count > 0)
            {
                // Caso exista, segue com o desenvolvimento desta possibilidade
                Result = Util.GetBestMovement(Board, PossibilitiesWithIAMovements);
            }
            else
            {
                // Caso nao exista, inicializa uma nova possibilidade
                Result = Util.GetBestMovement(Board, PossibilitiesWithoutPlayerMovements);
            }

            // Caso todas as avaliacoes de errado, efetua uma jogada qualquer
            if (Result == null)
            {
                Result = Util.GetRandomEmptyCell(Board);
                if (Result == null)
                {
                    // UnityEngine.Debug.Log("Error -> Nao entrou em um dos criterios de avaliacao ou o método  GetRandomEmptyCell foi chamado sem ter uma celula State.Cell.Empty");
                    UnityEngine.Debug.Log("Error -> GetRandomEmptyCell returned State.Cell.Empty");
                    return 0;
                }
            }
        }
        return (int)Result;
    }
}