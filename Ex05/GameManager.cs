using Ex05_Dana_319126074_Ilia_327576229;
using System;

namespace Ex05_Ilia_327576229_Dana_319126074
{
    public delegate void CellUpdatedDelegate(int i_Row, int i_Column, eCoinType i_Value);
    public delegate void GameEndedDelegate(eCoinType i_Winner);

    public class GameManager
    {
        private readonly GameBoard r_Board;
        private readonly bool r_IsVsComputer;
        private readonly Random r_RandomGenerator;

        private eCoinType m_CurrentPlayer;
        private eCoinType m_Winner;

        private int m_Player1Score;
        private int m_Player2Score;

        public event CellUpdatedDelegate CellUpdated;
        public event GameEndedDelegate GameEnded;

        public GameManager(int i_BoardRows, int i_BoardColumns, bool i_IsVsComputer)
        {
            r_Board = new GameBoard(i_BoardRows, i_BoardColumns);
            r_IsVsComputer = i_IsVsComputer;

            m_CurrentPlayer = eCoinType.PlayerOne;
            m_Winner = eCoinType.Empty;

            m_Player1Score = 0;
            m_Player2Score = 0;
            r_RandomGenerator = new Random();
        }

        public GameBoard GetBoard
        {
            get
            {
                return r_Board;
            }
        }
 
        public int Player1Score
        {
            get
            {
                return m_Player1Score;
            }
        }

        public int Player2Score
        {
            get
            {
                return m_Player2Score;
            }

        }

        public bool IsVsComputer
        {
            get
            {
                return r_IsVsComputer;
            }
        }

        private void switchTurn()
        {
            if (m_CurrentPlayer == eCoinType.PlayerOne)
            {
                m_CurrentPlayer = eCoinType.PlayerTwo;
            }
            else
            {
                m_CurrentPlayer = eCoinType.PlayerOne;
            }
        }

        private void updateScore()
        {
            if (m_Winner == eCoinType.PlayerOne)
            {
                m_Player1Score++;
            }
            else if (m_Winner == eCoinType.PlayerTwo)
            {
                m_Player2Score++;
            }
        }

        private void onCellUpdated(int i_Row, int i_Column, eCoinType i_Value)
        {
            if (CellUpdated != null)
            {
                CellUpdated.Invoke(i_Row, i_Column, i_Value);
            }
        }

        private void onGameEnded()
        {
            if (GameEnded != null)
            {
                GameEnded.Invoke(m_Winner);
            }
        }

        public bool PlayTurn(int i_Column)
        {
            bool turnIsSuccessful = false;
            int rowToPlaceTokenAt = r_Board.PlaceToken(i_Column, m_CurrentPlayer);

            if (rowToPlaceTokenAt != -1)
            {
                turnIsSuccessful = true;
                onCellUpdated(rowToPlaceTokenAt, i_Column, m_CurrentPlayer);

                if (r_Board.CheckForWin(rowToPlaceTokenAt, i_Column, m_CurrentPlayer))
                {
                    m_Winner = m_CurrentPlayer;
                    updateScore();
                    onGameEnded(); 
                }
                else if (r_Board.CheckIfBoardIsFull())
                {
                    m_Winner = eCoinType.Empty; 
                    onGameEnded(); 
                }
                else
                {
                    switchTurn();

                    if (r_IsVsComputer && m_CurrentPlayer == eCoinType.PlayerTwo)
                    {
                        playComputerTurn();
                    }
                }
            }

            return turnIsSuccessful;
        }

        private void playComputerTurn()
        {
            bool computerMoved = false;

            if (!r_Board.CheckIfBoardIsFull())
            {
                while (!computerMoved)
                {
                    int randomColumn = r_RandomGenerator.Next(0, r_Board.Width);
                    int rowPlaced = r_Board.PlaceToken(randomColumn, m_CurrentPlayer);

                    if (rowPlaced != -1)
                    {
                        computerMoved = true;
                        onCellUpdated(rowPlaced, randomColumn, m_CurrentPlayer);

                        if (r_Board.CheckForWin(rowPlaced, randomColumn, m_CurrentPlayer))
                        {
                            m_Winner = m_CurrentPlayer;
                            updateScore();
                            onGameEnded();
                        }
                        else if (r_Board.CheckIfBoardIsFull())
                        {
                            m_Winner = eCoinType.Empty;
                            onGameEnded();
                        }
                        else
                        {
                            switchTurn();
                        }
                    }
                }
            }
        }

        public void ResetGame()
        {
            r_Board.ClearBoard();
            m_Winner = eCoinType.Empty;
            m_CurrentPlayer = eCoinType.PlayerOne;
        }

    }
}