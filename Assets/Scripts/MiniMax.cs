using System;

public class EndTurnPosition
{
    public int position;
    public int score;

    public EndTurnPosition() { }

    public EndTurnPosition(int score)
    {
        this.score = score;
    }
}

public class MiniMax
{
    public int aiPick; // 1 = AI, -1 = Player

    public MiniMax(int aiPick)
    {
        this.aiPick = aiPick;
    }

    public EndTurnPosition GetBestPosition(int currentPlayer, int[] board, int depth)
    {
        int gameOverScore = IsGameOver(board);
        if (IsBoardFull(board) && gameOverScore == 0) return new EndTurnPosition(0); // Draw
        if (gameOverScore == 10) return new EndTurnPosition(10 - depth); // AI win
        if (gameOverScore == -10) return new EndTurnPosition(depth - 10); // AI loss

        depth++;
        EndTurnPosition bestPos = CreateEndPositionForLevel(currentPlayer);

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
            {
                board[i] = currentPlayer;
                EndTurnPosition result = GetBestPosition(-currentPlayer, (int[])board.Clone(), depth);

                if (currentPlayer == aiPick)
                {
                    if (result.score > bestPos.score)
                    {
                        bestPos.score = result.score;
                        bestPos.position = i;
                    }
                }
                else
                {
                    if (result.score < bestPos.score)
                    {
                        bestPos.score = result.score;
                        bestPos.position = i;
                    }
                }

                board[i] = 0; // Undo move
            }
        }

        return bestPos;
    }

    private EndTurnPosition CreateEndPositionForLevel(int currentPlayer)
    {
        EndTurnPosition pos = new EndTurnPosition();
        pos.score = (currentPlayer == aiPick) ? int.MinValue : int.MaxValue;
        return pos;
    }

    private bool IsBoardFull(int[] board)
    {
        foreach (int cell in board)
            if (cell == 0) return false;
        return true;
    }

    private int IsGameOver(int[] board)
    {
        int[,] wins = new int[,]
        {
            {0,1,2},{3,4,5},{6,7,8},
            {0,3,6},{1,4,7},{2,5,8},
            {0,4,8},{2,4,6}
        };

        for (int i = 0; i < 8; i++)
        {
            int a = wins[i, 0], b = wins[i, 1], c = wins[i, 2];
            if (board[a] != 0 && board[a] == board[b] && board[a] == board[c])
            {
                return board[a] == aiPick ? 10 : -10;
            }
        }
        return 0;
    }
}
