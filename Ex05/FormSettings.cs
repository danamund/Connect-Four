using System;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05_Ilia_327576229_Dana_319126074
{
    public partial class FormSettings : Form
    {
        private const int k_WindowWidth = 270;
        private const int k_WindowHeight = 250;
        private const int k_LabelWidth = 85;
        private const int k_TextBoxWidth = 120;
        private const int k_ButtonWidth = 200;
        private const int k_ButtonHeight = 25;
        private const int k_NumericUpDownWidth = 40;
        private const int k_SmallLabelWidth = 40;

        private const int k_LeftMargin = 15;
        private const int k_TopMargin = 15;
        private const int k_VerticalSpacing = 26;
        private const int k_Indent = 5;
        private const int k_GapSmall = 5;
        private const int k_GapMedium = 15;
        private const int k_ButtonBottomMargin = 20;

        private const int k_TextBoxX = k_LeftMargin + k_Indent + k_LabelWidth + k_GapSmall;    
        private const int k_MinimumRows = 4;
        private const int k_MaximumRows = 10;
        private const int k_MinimumColumns = 4;
        private const int k_MaximumColumns = 10;
        private const int k_DefaultSize = 4;
        private const string k_ComputerName = "[Computer]";
        private const bool k_Enabled = true;

        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private TextBox m_TextBoxPlayer1;
        private CheckBox m_CheckBoxPlayer2IsHuman;
        private TextBox m_TextBoxPlayer2;

        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private NumericUpDown m_NumericUpDownRows;
        private Label m_LabelColumns;
        private NumericUpDown m_NumericUpDownColumns;
        private Button m_ButtonStart;

        public FormSettings()
        {
            initializeComponent();
        }

        public string Player1Name
        {
            get
            {
                return m_TextBoxPlayer1.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_TextBoxPlayer2.Text;
            }
        }

        public bool IsVsComputer
        {
            get
            {
                return !m_CheckBoxPlayer2IsHuman.Checked;
            }
        }

        public int RowsCount
        {
            get
            {
                return (int)m_NumericUpDownRows.Value;
            }
        }

        public int ColumnsCount
        {
            get
            {
                return (int)m_NumericUpDownColumns.Value;
            }
        }

        private void initializeComponent()
        {
            initializeFormProperties();
            initializePlayerOneControls();
            initializePlayerTwoControls();
            initializeBoardSizeControls();
            initializeStartButton();
        }

        private void initializeFormProperties()
        {
            this.Text = "Game Settings";
            this.Size = new Size(k_WindowWidth, k_WindowHeight);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void initializePlayerOneControls()
        {
            m_LabelPlayers = createAndAddLabel("Players:", new Point(k_LeftMargin, k_TopMargin));
            int player1YPosition = k_TopMargin + k_VerticalSpacing;

            m_LabelPlayer1 = createAndAddLabel("Player 1:", new Point(k_LeftMargin + k_Indent, player1YPosition + 3));
            m_LabelPlayer1.AutoSize = false;
            m_LabelPlayer1.Width = k_LabelWidth;
            m_TextBoxPlayer1 = createAndAddTextBox(new Point(k_TextBoxX, player1YPosition), string.Empty, k_Enabled);
        }

        private void initializePlayerTwoControls()
        {
            int player2YPosition = k_TopMargin + k_VerticalSpacing * 2;

            m_CheckBoxPlayer2IsHuman = createAndAddCheckBox("Player 2:", new Point(k_LeftMargin + k_Indent, player2YPosition), k_LabelWidth + k_GapSmall);
            m_CheckBoxPlayer2IsHuman.CheckedChanged += new EventHandler(m_CheckBoxPlayer2IsHuman_CheckedChanged);
            m_TextBoxPlayer2 = createAndAddTextBox(new Point(k_TextBoxX, player2YPosition), k_ComputerName, !k_Enabled);
        }

        private Label createAndAddLabel(string i_TextToPrint, Point i_Location)
        {
            Label newLabel = new Label();

            newLabel.Text = i_TextToPrint;
            newLabel.Location = i_Location;
            newLabel.AutoSize = true;
            this.Controls.Add(newLabel);

            return newLabel;
        }

        private CheckBox createAndAddCheckBox(string i_TextToPrint, Point i_Location, int i_WidthBox)
        {
            CheckBox newCheckBox = new CheckBox();

            newCheckBox.Text = i_TextToPrint;
            newCheckBox.Location = i_Location;
            newCheckBox.Width = i_WidthBox;
            this.Controls.Add(newCheckBox);

            return newCheckBox;
        }

        private TextBox createAndAddTextBox(Point i_Location, string i_TextToPrint, bool i_IsEnabled)
        {
            TextBox newTextBox = new TextBox();

            newTextBox.Location = i_Location;
            newTextBox.Width = k_TextBoxWidth;
            newTextBox.Text = i_TextToPrint;
            newTextBox.Enabled = i_IsEnabled;
            this.Controls.Add(newTextBox);

            return newTextBox;
        }

        private void initializeBoardSizeControls()
        {
            int boardSizeTop = k_TopMargin + k_VerticalSpacing * 3 + 5;

            m_LabelBoardSize = createAndAddLabel("Board Size:", new Point(k_LeftMargin, boardSizeTop));

            int dimensionsTop = boardSizeTop + k_VerticalSpacing;
            int currentControlX = k_LeftMargin + k_Indent;

            m_LabelRows = createAndAddLabel("Rows:", new Point(currentControlX, dimensionsTop + 2));
            m_LabelRows.Width = k_SmallLabelWidth;
            currentControlX += k_SmallLabelWidth;
            m_NumericUpDownRows = createAndAddNumericUpDown(new Point(currentControlX, dimensionsTop), k_MinimumRows, k_MaximumRows);

            currentControlX += k_NumericUpDownWidth + k_GapMedium;
            m_LabelColumns = createAndAddLabel("Cols:", new Point(currentControlX, dimensionsTop + 2));
            m_LabelColumns.Width = k_SmallLabelWidth;
            currentControlX += k_SmallLabelWidth;
            m_NumericUpDownColumns = createAndAddNumericUpDown(new Point(currentControlX, dimensionsTop), k_MinimumColumns, k_MaximumColumns);
        }
        private NumericUpDown createAndAddNumericUpDown(Point i_Location, int i_Minimum, int i_Maximum)
        {
            NumericUpDown newControl = new NumericUpDown();

            newControl.Location = i_Location;
            newControl.Minimum = i_Minimum;
            newControl.Maximum = i_Maximum;
            newControl.Value = k_DefaultSize;
            newControl.Width = k_NumericUpDownWidth; 

            this.Controls.Add(newControl);

            return newControl;
        }
        private void initializeStartButton()
        {
            m_ButtonStart = new Button();
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.Width = k_ButtonWidth;
            m_ButtonStart.Height = k_ButtonHeight;

            int buttonLeft = (this.ClientSize.Width - k_ButtonWidth) / 2;
            int buttonTop = this.ClientSize.Height - k_ButtonHeight - k_ButtonBottomMargin;

            m_ButtonStart.Location = new Point(buttonLeft, buttonTop);
            m_ButtonStart.Click += new EventHandler(m_ButtonStart_Click);
            this.Controls.Add(m_ButtonStart);
        }

        private void m_CheckBoxPlayer2IsHuman_CheckedChanged(object i_Sender, EventArgs i_EventArgs)
        {
            if (m_CheckBoxPlayer2IsHuman.Checked)
            {
                m_TextBoxPlayer2.Enabled = true;
                m_TextBoxPlayer2.Text = string.Empty;
            }
            else
            {
                m_TextBoxPlayer2.Enabled = false;
                m_TextBoxPlayer2.Text = k_ComputerName;
            }
        }

        private void m_ButtonStart_Click(object i_Sender, EventArgs i_EventArgs)
        {
            if (!areGameSettingsValid())
            {
                MessageBox.Show("Please enter names for all players.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool areGameSettingsValid()
        {
            bool isValidInput = true;

            if (string.IsNullOrEmpty(m_TextBoxPlayer1.Text))
            {
                isValidInput = false;
            }
            else if (m_CheckBoxPlayer2IsHuman.Checked && string.IsNullOrEmpty(m_TextBoxPlayer2.Text))
            {
                isValidInput = false;
            }

            return isValidInput;
        }
    }
}