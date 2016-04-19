using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Xna.Framework;


using AvoidTheAnimals;
using System.Threading;

namespace Launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Avoid the Animals!";

            ResolutionDrop.Items.Add(new ResItem(1080, 720));
            ResolutionDrop.Items.Add(new ResItem(1080 * 16/9, 1080));
            ResolutionDrop.Items.Add(new ResItem(400, 300));
            ResolutionDrop.SelectedItem = ResolutionDrop.Items[0];
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ResItem resSel = (ResItem)ResolutionDrop.SelectedItem;
            Thread t = StartTheThread(resSel.height, resSel.width);

            this.Close();
        }

        public Thread StartTheThread(int param1, int param2)
        {
            var t = new Thread(() => StartGame(param1, param2));
            t.Start();
            return t;
        }

        private void StartGame(int height, int width) {
            AvoidTheAnimals.Game1 game = new Game1(height, width);
            game.Run();
        }

        private void ResolutionDrop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    class ResItem
    {
        public int height, width;
        public ResItem(int width, int height)
        {
            this.height = height;
            this.width = width;
        }

        public override string ToString()
        {
            return width + " x " + height;
        }
    }
}
