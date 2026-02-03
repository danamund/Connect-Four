using Ex05_Dana_319126074_Ilia_327576229;
using System.Reflection;

namespace Ex05_Ilia_327576229_Dana_319126074
{
    public class GameBoard
    {
        public const int k_WinSequence = 4;
        public const int k_MinimumBoardSize = 4;
        public const int k_MaximumBoardSize = 10;

        private readonly int r_Height;
        private readonly int r_Width;
        private readonly eCoinType[,] r_Board;

        public GameBoard(int i_BoardHeight, int i_BoardWidth)
        {
            r_Height = i_BoardHeight;
            r_Width = i_BoardWidth;
            r_Board = new eCoinType[i_BoardHeight, i_BoardWidth];

            initBoard();
        }

        public int Height
        {
            get
            {
                return r_Height;
            }
        }

        public int Width
        {
            get
            {
                return r_Width;
            }
        }

        private void initBoard()
        {
            for (int heightIndex = 0; heightIndex < r_Height; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < r_Width; widthIndex++)
                {
                    r_Board[heightIndex, widthIndex] = eCoinType.Empty;
                }
            }
        }

        public eCoinType ReturnCellValue(int i_Rows, int i_Columns)
        {
            return r_Board[i_Rows, i_Columns];
        }

        public int PlaceToken(int i_Column, eCoinType i_Player)
        {
            int rowIndex = -1;

            if (i_Column >= 0 && i_Column < r_Width)
            {
                for (int index = Height - 1; index >= 0; index--)
                {
                    if (r_Board[index, i_Column] == eCoinType.Empty)
                    {
                        r_Board[index, i_Column] = i_Player;
                        rowIndex = index;
                        break;
                    }
                }
            }

            return rowIndex;
        }

        public bool CheckIfBoardIsFull()
        {
            bool boardIsFull = true;

            for (int index = 0; index < r_Width; index++)
            {
                if (r_Board[0, index] == eCoinType.Empty)
                {
                    boardIsFull = false;
                    break;
                }
            }

            return boardIsFull;
        }

        public bool CheckForWin(int i_Row, int i_Column, eCoinType i_Player)
        {
            return checkForWinInARow(i_Row, i_Column, i_Player) ||
                   checkForWinInAColumn(i_Row, i_Column, i_Player) ||
                   checkDiagonalDescendingWin(i_Row, i_Column, i_Player) ||
                   checkDiagonalAscendingWin(i_Row, i_Column, i_Player);
        }

        private bool checkForWinInARow(int i_Row, int i_Column, eCoinType i_Player)
        {
            int sameSymbolInARowCount = 1;

            for (int index = 1; index < k_WinSequence; index++)
            {
                if (i_Column + index < r_Width && r_Board[i_Row, i_Column + index] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            for (int index = 1; index < k_WinSequence; index++)
            {
                if (i_Column - index >= 0 && r_Board[i_Row, i_Column - index] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            return sameSymbolInARowCount >= k_WinSequence;
        }

        private bool checkForWinInAColumn(int i_Row, int i_Column, eCoinType i_Player)
        {
            int sameSymbolInARowCount = 1;

            for (int index = 1; index < k_WinSequence; index++)
            {
                if (i_Row + index < r_Height && r_Board[i_Row + index, i_Column] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            for (int index = 1; index < k_WinSequence; index++)
            {
                if (i_Row - index >= 0 && r_Board[i_Row - index, i_Column] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            return sameSymbolInARowCount >= k_WinSequence;
        }

        private bool checkDiagonalDescendingWin(int i_Row, int i_Column, eCoinType i_Player)
        {
            int sameSymbolInARowCount = 1;

            for (int index = 1; index < k_WinSequence; index++)
            {
                int rowToCheck = i_Row + index;
                int columnToCheck = i_Column + index;

                if (rowToCheck < Height && columnToCheck < Width && r_Board[rowToCheck, columnToCheck] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            for (int index = 1; index < k_WinSequence; index++)
            {
                int rowToCheck = i_Row - index;
                int columnToCheck = i_Column - index;

                if (rowToCheck >= 0 && columnToCheck >= 0 && r_Board[rowToCheck, columnToCheck] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            return sameSymbolInARowCount >= k_WinSequence;
        }

        private bool checkDiagonalAscendingWin(int i_Row, int i_Column, eCoinType i_Player)
        {
            int sameSymbolInARowCount = 1;

            for (int index = 1; index < k_WinSequence; index++)
            {
                int rowToCheck = i_Row - index;
                int columnToCheck = i_Column + index;

                if (rowToCheck >= 0 && columnToCheck < Width && r_Board[rowToCheck, columnToCheck] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            for (int index = 1; index < k_WinSequence; index++)
            {
                int rowToCheck = i_Row + index;
                int columnToCheck = i_Column - index;

                if (rowToCheck < Height && columnToCheck >= 0 && r_Board[rowToCheck, columnToCheck] == i_Player)
                {
                    sameSymbolInARowCount++;
                }
                else
                {
                    break;
                }
            }

            return sameSymbolInARowCount >= k_WinSequence;
        }

        public void ClearBoard()
        {
            initBoard();
        }
    }
}