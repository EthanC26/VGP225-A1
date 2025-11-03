//using UnityEngine;

//public class AIPlayer
//{
//    private int aiPlayer; // 1 = X, -1 = O

//    public AIPlayer(int aiPlayer)
//    {
//        this.aiPlayer = aiPlayer;
//    }

//    // Get the best move for the current board
//    public int GetBestMove(int[] board)
//    {
//        int move = -1;
//        int score = -2;

//        for (int i = 0; i < 9; i++)
//        {
//            if (board[i] == 0)
//            {
//                board[i] = aiPlayer;
//                int tempScore = -MiniMax(board, -aiPlayer);
//                board[i] = 0;

//                if (tempScore > score)
//                {
//                    score = tempScore;
//                    move = i;
//                }
//            }
//        }

//        return move;
//    }

//    private int MiniMax(int[] board, int player)
//    {
//        int winner = CheckWin(board);
//        if (winner != 0)
//            return winner * player; // negamax style

//        int move = -1;
//        int score = -2;

//        for (int i = 0; i < 9; i++)
//        {
//            if (board[i] == 0)
//            {
//                board[i] = player;
//                int thisScore = -MiniMax(board, -player);
//                board[i] = 0;

//                if (thisScore > score)
//                {
//                    score = thisScore;
//                    move = i;
//                }
//            }
//        }

//        if (move == -1) // no moves left, draw
//            return 0;

//        return score;
//    }

//    private int CheckWin(int[] b)
//    {
//        int[,] wins = new int[,]
//        {
//            {0,1,2},{3,4,5},{6,7,8},
//            {0,3,6},{1,4,7},{2,5,8},
//            {0,4,8},{2,4,6}
//        };

//        for (int i = 0; i < 8; i++)
//        {
//            int a = wins[i, 0], c = wins[i, 1], d = wins[i, 2];
//            if (b[a] != 0 && b[a] == b[c] && b[a] == b[d])
//                return b[a];
//        }

//        return 0;
//    }
//}
