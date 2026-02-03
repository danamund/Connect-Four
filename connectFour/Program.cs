using Ex05_Dana_319126074_Ilia_327576229;
using System;
using System.Windows.Forms;

namespace Ex05_Ilia_327576229_Dana_319126074
{
    static class Program
    {
        private static void runGameApp()
        {
            FormSettings settingsForm = new FormSettings();

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                runGame(settingsForm);
            }
        }

        private static void runGame(FormSettings i_Settings)
        {
            FormGame gameForm = new FormGame(i_Settings.RowsCount, i_Settings.ColumnsCount, i_Settings.IsVsComputer, i_Settings.Player1Name, i_Settings.Player2Name);

            gameForm.ShowDialog();
        }
        public static void Main()
        {
            Application.EnableVisualStyles();
            runGameApp();
        }
    }
}