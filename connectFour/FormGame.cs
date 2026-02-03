using Ex05_Dana_319126074_Ilia_327576229;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Ilia_327576229_Dana_319126074
{
    public partial class FormGame : Form
    {
        private readonly GameManager r_GameManager;
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;

        private Button[,] m_BoardButtons;
        private Label m_LabelPlayer1Score;
        private Label m_LabelPlayer2Score;

        private const string k_PlayerOneSign = "X";
        private const string k_PlayerTwoSign = "O";
        private const string k_ComputerName = "[Computer]";

        private const int k_ButtonSize = 45;     
        private const int k_NumberButtonHeight = 25;  
        private const int k_Margin = 18;
        private const int k_ButtonSpacing = 6;
        private const int k_BottomTextHeight = 45;

        public FormGame(int i_Rows, int i_Columns, bool i_IsVsComputer, string i_Player1Name, string i_Player2Name)
        {
            r_GameManager = new GameManager(i_Rows, i_Columns, i_IsVsComputer);

            r_Player1Name = i_Player1Name;
            r_Player2Name = i_Player2Name;
            r_GameManager.CellUpdated += boardCellUpdated;
            r_GameManager.GameEnded += gameEnded;

            initializeGameComponent(i_Rows, i_Columns, i_Player1Name, i_Player2Name);
        }

        private void initializeGameComponent(int i_Rows, int i_Columns, string i_Player1Name, string i_Player2Name)
        {
            setFormProperties();
            updateClientSize(i_Rows, i_Columns);
            createScoreLabels(i_Player1Name, i_Player2Name);
            createBoardControls(i_Rows, i_Columns);
        }

        private void setFormProperties()
        {
            this.Text = "4 in a Row !!";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void updateClientSize(int i_Rows, int i_Columns)
        {
            int totalWidth = (2 * k_Margin) + (i_Columns * k_ButtonSize) + ((i_Columns - 1) * k_ButtonSpacing);
            int headerHeight = k_NumberButtonHeight + k_ButtonSpacing; 
            int boardHeight = (i_Rows * k_ButtonSize) + ((i_Rows - 1) * k_ButtonSpacing); 
            int totalHeight = k_Margin + headerHeight + boardHeight + k_BottomTextHeight;

            this.ClientSize = new Size(totalWidth, totalHeight);
        }
        
        private void createScoreLabels(string i_Player1Name, string i_Player2Name)
        {
            int labelsYPosition = this.ClientSize.Height - k_BottomTextHeight + 10;
            int halfScreenWidth = this.ClientSize.Width / 2;
            Point player1Location = new Point(0, labelsYPosition);
            Point player2Location = new Point(halfScreenWidth, labelsYPosition);

            m_LabelPlayer1Score = createSingleLabel(i_Player1Name, player1Location, halfScreenWidth);
            m_LabelPlayer2Score = createSingleLabel(i_Player2Name, player2Location, halfScreenWidth);
        }

        private Label createSingleLabel(string i_UserName, Point i_Location, int i_Width)
        {
            Label newLabel = new Label();

            newLabel.Text = string.Format("{0}: 0", i_UserName);
            newLabel.Location = i_Location;
            newLabel.AutoSize = false;
            newLabel.Width = i_Width;
            newLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.Controls.Add(newLabel);

            return newLabel;
        }
        private void createBoardControls(int i_Rows, int i_Columns)
        {
            createSelectionButtons(i_Columns);
            createBoardMatrix(i_Rows, i_Columns);
        }

        private void createSelectionButtons(int i_Columns)
        {
            for (int column = 0; column < i_Columns; column++)
            {
                Button currentButton = new Button();

                currentButton.Text = (column + 1).ToString();
                currentButton.Size = new Size(k_ButtonSize, k_NumberButtonHeight);

                int buttonLeft = k_Margin + column * (k_ButtonSize + k_ButtonSpacing);
                int buttonTop = k_Margin;

                currentButton.Location = new Point(buttonLeft, buttonTop);
                currentButton.Tag = column;
                currentButton.Click += new EventHandler(columnButton_Click);
                this.Controls.Add(currentButton);
            }
        }

        private void createBoardMatrix(int i_Rows, int i_Columns)
        {
            m_BoardButtons = new Button[i_Rows, i_Columns];
            int matrixTop = k_Margin + k_NumberButtonHeight + k_ButtonSpacing;

            for (int row = 0; row < i_Rows; row++)
            {
                for (int column = 0; column < i_Columns; column++)
                {
                    Button cellCurrentButton = new Button();

                    cellCurrentButton.Size = new Size(k_ButtonSize, k_ButtonSize);
                    int buttonLeft = k_Margin + column * (k_ButtonSize + k_ButtonSpacing);
                    int buttonTop = matrixTop + row * (k_ButtonSize + k_ButtonSpacing);

                    cellCurrentButton.Location = new Point(buttonLeft, buttonTop);
                    cellCurrentButton.Enabled = false;
                    cellCurrentButton.BackColor = Color.LightGray;

                    m_BoardButtons[row, column] = cellCurrentButton;
                    this.Controls.Add(cellCurrentButton);
                }
            }
        }

        private void columnButton_Click(object i_Sender, EventArgs i_EventArgs)
        {
            Button clickedButton = i_Sender as Button;
            int columnIndex = (int)clickedButton.Tag;

            r_GameManager.PlayTurn(columnIndex);
        }

        private void boardCellUpdated(int i_Row, int i_Column, eCoinType i_Value)
        {
            Button buttonToUpdate = m_BoardButtons[i_Row, i_Column];

            switch (i_Value)
            {
                case eCoinType.PlayerOne:
                    buttonToUpdate.Text = k_PlayerOneSign;
                    break;
                case eCoinType.PlayerTwo:
                    buttonToUpdate.Text = k_PlayerTwoSign;
                    break;
                default:
                    buttonToUpdate.Text = string.Empty;
                    break;
            }
        }

        private void gameEnded(eCoinType i_Winner)
        {
            updateScoreLabels();
            showEndGameMessage(i_Winner);
        }

        private void updateScoreLabels()
        {
            m_LabelPlayer1Score.Text = string.Format("{0}: {1}", r_Player1Name, r_GameManager.Player1Score);
            m_LabelPlayer2Score.Text = string.Format("{0}: {1}", r_Player2Name, r_GameManager.Player2Score);
        }
        private string getWinnerName(eCoinType i_Winner)
        {
            string nameToReturn = string.Empty;

            if (i_Winner == eCoinType.PlayerOne)
            {
                nameToReturn = r_Player1Name;
            }
            else if (i_Winner == eCoinType.PlayerTwo)
            {
                nameToReturn = r_Player2Name;

                if (nameToReturn == k_ComputerName)
                {
                    nameToReturn = "Computer";
                }
            }

            return nameToReturn;
        }

        private void showEndGameMessage(eCoinType i_Winner)
        {
            string messageBody;
            string messageTitle;

            if (i_Winner == eCoinType.Empty) 
            {
                messageTitle = "A Tie!";
                messageBody = string.Format("Tie!!{0}Another Round?", Environment.NewLine);
            }
            else
            {
                string winnerName = getWinnerName(i_Winner);
                messageTitle = "A Win!";
                messageBody = string.Format("{0} Won!!{1}Another Round?", winnerName, Environment.NewLine);
            }

            DialogResult gameResult = MessageBox.Show(messageBody, messageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (gameResult == DialogResult.Yes)
            {
                resetGraphicBoard();
                r_GameManager.ResetGame();
            }
            else
            {
                this.Close();
            }
        }

        private void resetGraphicBoard()
        {
            foreach (Button currentButton in m_BoardButtons)
            {
                currentButton.Text = string.Empty;
            }
        }
    }
}