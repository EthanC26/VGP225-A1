using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Cells[] cells;
    private int[] board = new int[9]; // 0 = empty, 1 = X, -1 = O
    private bool isPlayerTurn = false;
    private MiniMax ai;

    void Start()
    {
        for (int i = 0; i < 9; i++) board[i] = 0;

        ai = new MiniMax(1); // AI = X
        isPlayerTurn = false; // AI starts
        Invoke(nameof(AIMove), 0.5f);
    }

    public void PlayerMove(int index)
    {
        if (!isPlayerTurn || board[index] != 0) return;

        board[index] = -1;
        cells[index].SetSprite(-1);
        cells[index].Disable();

        int winner = CheckWin(board);
        if (winner == -1)
        {
            Debug.Log("O Wins!");
            EndGame();
            return;
        }
        else if (IsBoardFull())
        {
            Debug.Log("Draw!");
            EndGame();
            return;
        }

        isPlayerTurn = false;
        Invoke(nameof(AIMove), 0.5f);
    }

    private void AIMove()
    {
        EndTurnPosition best = ai.GetBestPosition(1, board, 0);
        int move = best.position;

        if (move == -1) return;

        board[move] = 1;
        cells[move].SetSprite(1);
        cells[move].Disable();

        int winner = CheckWin(board);
        if (winner == 1)
        {
            Debug.Log("X Wins!");
            EndGame();
            return;
        }
        else if (IsBoardFull())
        {
            Debug.Log("Draw!");
            EndGame();
            return;
        }

        isPlayerTurn = true;
    }

    private bool IsBoardFull()
    {
        foreach (var cell in board)
            if (cell == 0) return false;
        return true;
    }

    private int CheckWin(int[] b)
    {
        int[,] wins = new int[,]
        {
            {0,1,2},{3,4,5},{6,7,8},
            {0,3,6},{1,4,7},{2,5,8},
            {0,4,8},{2,4,6}
        };

        for (int i = 0; i < 8; i++)
        {
            int a = wins[i,0], c = wins[i,1], d = wins[i,2];
            if (b[a] != 0 && b[a] == b[c] && b[a] == b[d])
                return b[a];
        }
        return 0;
    }

    private void EndGame()
    {
        foreach (var cell in cells)
            cell.Disable();
        isPlayerTurn = false;
    }
}
