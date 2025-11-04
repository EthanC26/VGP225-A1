using UnityEngine;

public class GameManger : MonoBehaviour
{
    public Cells[] cells;

    private int[] board = new int[9]; // 0 = empty, 1 = X, -1 = O
    public bool isPlayerTurn; // true = O's turn, false = X's turn


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 9; i++) board[i] = 0;
        isPlayerTurn = true; //  (O) starts first
        //Invoke(nameof(AIMove), 0.5f);

    }

    public void PlayerMove(int Index)
    {
        if (!isPlayerTurn || board[Index] != 0)
            return;

        board[Index] = -1; // O is -1
        cells[Index].SetSprite(-1);
        cells[Index].Disable();
        isPlayerTurn = false;

        Debug.Log("Player chose move: " + Index);

        int winner = CheckWin(board);
        if (winner == -1)
        {
            Debug.Log("O Wins!");
            return;
        }

        if (IsBoardFull())
        {
            Debug.Log("It's a Draw!");
            return;
        }

        Invoke(nameof(AIMove), 0.5f);
    }

    private void AIMove()
    {
        int bestMove = GetBestMove();
        if (bestMove == -1) return;

        board[bestMove] = 1; // X is 1
        cells[bestMove].SetSprite(1);
        cells[bestMove].Disable();

        int winner = CheckWin(board);
        if (winner == 1)
        {
            Debug.Log("X Wins!");
            return;
        }
        if (IsBoardFull())
        {
            Debug.Log("It's a Draw!");
            return;
        }
        isPlayerTurn = true;
    }

    private bool IsBoardFull()
    {
        foreach (var cell in board)
        {
            if (cell == 0) return false;
        }
        return true;
    }

    private int CheckWin(int[] b)
    {
        int[,] wins = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8}, // rows
            {0,3,6}, {1,4,7}, {2,5,8}, // columns
            {0,4,8}, {2,4,6}           // diagonals
        };

        for (int i = 0; i < 8; i++)
        {
            int a = wins[i, 0], c = wins[i, 1], d = wins[i, 2];
            if (b[a] != 0 && b[a] == b[c] && b[a] == b[d])
                return b[a];
        }

        return 0; // No winner
    }

    private int MiniMax(int[] b, int player)
    {
        int winner = CheckWin(b);
        if (winner != 0)
            return winner * player;

        bool full = true;
        for (int i = 0; i < 9; i++)
        {
            if (b[i] == 0)
                full = false;
        }
        if (full)
            return 0;

        int bestScore = -2; // worse than the worst case
        for (int i = 0; i < 9; i++)
        {
            if (b[i] == 0)
            {
                b[i] = player;
                int score = -MiniMax(b, -player);
                b[i] = 0;
                if (score > bestScore)
                    bestScore = score;
            }
        }
        return bestScore;
    }

    private int GetBestMove()
    {
        int bestScore = -2;
        int move = -1;

        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                board[i] = 1; // AI is 1
                int score = -MiniMax(board, -1);
                board[i] = 0;
                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }
        Debug.Log("AI chooses move: " + move);
        return move;
    }
}