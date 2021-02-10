using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridGame
{
    public class PreGamePanel : Panel
    {
        private readonly GameForm parentForm;
        private readonly ColorDialog colorDialog = new ColorDialog();
        private readonly DataGridView paramTable;
        private static readonly int CONFIRM_BUTTON_WIDTH = 150;
        private static readonly int CONFIRM_BUTTON_HEIGHT = 40;

        public PreGamePanel(GameForm parentForm)
        {
            this.parentForm = parentForm;
            
            Location = new Point(0, 0);
            Size = new Size(parentForm.Width, parentForm.Height);
            Name = "PreGameMenu";

            var playerColumn = new DataGridViewTextBoxColumn
            {
                Name = "playerColumn",
                HeaderText = "Player",
                ReadOnly = true
            };

            var nameColumn = new DataGridViewTextBoxColumn
            {
                Name = "nameColumn",
                HeaderText = "Name",
            };

            var colorColumn = new DataGridViewButtonColumn
            {
                Name = "colorColumn",
                Text = "",
                HeaderText = "Color",
                ReadOnly = true,
                FlatStyle = FlatStyle.Popup,
                UseColumnTextForButtonValue = true,
            };

            paramTable = new DataGridView
            {
                Location = new Point(0, 0),
                BackgroundColor = Color.Aqua,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
            };
            paramTable.DefaultCellStyle.SelectionBackColor = paramTable.DefaultCellStyle.BackColor;
            paramTable.DefaultCellStyle.SelectionForeColor = paramTable.DefaultCellStyle.ForeColor;
            paramTable.Columns.AddRange(new DataGridViewColumn[] {playerColumn, nameColumn, colorColumn});
            paramTable.CellContentClick += new DataGridViewCellEventHandler(ParamTableEventHandler);
            paramTable.ScrollBars = ScrollBars.None;

            //Rows:
            //Player 1:
            var row1Id = paramTable.Rows.Add();
            var row1 = paramTable.Rows[row1Id];
            row1.Cells["playerColumn"].Value = "1";
            row1.Cells["nameColumn"].Value = "Player 1";
            row1.Cells["colorColumn"].Style.ForeColor = Color.Red;
            row1.Cells["colorColumn"].Style.BackColor = Color.Red;


            //Player 2:
            var row2Id = paramTable.Rows.Add();
            var row2 = paramTable.Rows[row2Id];
            row2.Cells["playerColumn"].Value = "2";
            row2.Cells["nameColumn"].Value = "Player 2";
            row2.Cells["colorColumn"].Style.ForeColor = Color.Yellow;
            row2.Cells["colorColumn"].Style.BackColor = Color.Yellow;

            SizeParamTable(paramTable);
            Controls.Add(paramTable);


            //Buttons
            var confirmButton = new Button()
            {
                Text = "Confirm",
                Location = new Point(Width - (2 * CONFIRM_BUTTON_WIDTH), Height - (3 * CONFIRM_BUTTON_HEIGHT)),
                Size = new Size(CONFIRM_BUTTON_WIDTH, CONFIRM_BUTTON_HEIGHT),
            };
            confirmButton.Click += new EventHandler(ConfirmButtonEventHandler);
            Controls.Add(confirmButton);
        }

        private void SizeParamTable(DataGridView paramTable)
        {
            var states = DataGridViewElementStates.None;
            var width = paramTable.Columns.GetColumnsWidth(states) + paramTable.RowHeadersWidth;
            var height = paramTable.Rows.GetRowsHeight(states) + paramTable.ColumnHeadersHeight;
            paramTable.ClientSize = new Size(width, height);
        }

        private void ParamTableEventHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0) //third column, any row except for the header
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    var table = (DataGridView)sender;
                    var button = (DataGridViewButtonCell)table[e.ColumnIndex, e.RowIndex];
                    button.Style.BackColor = colorDialog.Color;
                    button.Style.ForeColor = colorDialog.Color;
                    table.ClearSelection();
                }
            }
        }

        private void ConfirmButtonEventHandler(object sender, EventArgs e)
        {
            var players = new Player[2];

            for (int i = 0; i < players.Length; i++)
            {
                var color = ((DataGridViewButtonCell)paramTable["colorColumn", i]).Style.BackColor;
                var nameTextBox = (DataGridViewTextBoxCell)paramTable["nameColumn", i];

                if(nameTextBox.Value == null)
                {
                    MessageBox.Show("Player " + i + " has not selected a name", "Select a name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var name = (string)nameTextBox.Value;
                players[i] = new Player(color, name);
            }

            parentForm.GamePanel.SwitchTo(players);
        }

        public void SwitchTo()
        {
            parentForm.ActivePanel.Visible = false;
            parentForm.ActivePanel = this;
            parentForm.ActivePanel.Visible = true;
        }
    }
}
