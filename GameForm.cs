﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO: Add a victory label to the end screen, to congratulate the winning player

namespace GridGame
{
    public partial class GameForm : Form
    {
        public Panel ActivePanel;
        public readonly GamePanel GamePanel;
        public readonly PreGamePanel PreGamePanel;        
        public readonly MainMenuPanel MainMenuPanel;
        public readonly VictoryPanel VictoryPanel;

        public GameForm()
        {
            InitializeComponent();
            Text = "Connect 4!";

            MainMenuPanel = new MainMenuPanel(this)
            {
                Visible = false
            };
            Controls.Add(MainMenuPanel);

            GamePanel = new GamePanel(this)
            {
                Visible = false
            };
            Controls.Add(GamePanel);

            PreGamePanel = new PreGamePanel(this)
            {
                Visible = false
            };
            Controls.Add(PreGamePanel);

            VictoryPanel = new VictoryPanel(this)
            {
                Visible = false
            };
            Controls.Add(VictoryPanel);

            ActivePanel = MainMenuPanel;
            ActivePanel.Visible = true;

        }
    }
}
