using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EloBuddy;
using EloBuddy.SDK;

namespace DualScreenBuddy
{
    public partial class MainForm : Form
    {
        public Size ImageSize { get; set; }
        public MainForm()
        {
            InitializeComponent();
            ImageSize = Properties.Resources.Whole.Size;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var aspectRatio = ImageSize.Width / ImageSize.Height;
            if (this.Height < ImageSize.Height)
            {
                panel1.Size = new Size(this.Height, aspectRatio * this.Height);
            }
            Game.OnUpdate += Game_OnUpdate;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            lbl_Pos.Text = string.Format("X:      {0} Y:      {1}", ObjectManager.Player.Position.X, ObjectManager.Player.Position.Y);
            var aspectRatioOfScreen = Screen.FromControl(label1).Bounds.Width/Screen.FromControl(label1).Bounds.Height;


            var aspectRatioOfMap = panel1.Width/panel1.Height;
            var diff = Screen.FromControl(label1).Bounds.Width ;
            var pg = panel1.CreateGraphics();
            panel1.Invalidate();
            panel1.Refresh();

            listBox1.Items.Clear();
            foreach (var hero in EntityManager.Heroes.AllHeroes)
            {
                listBox1.Items.Add(hero.ChampionName + "=" + hero.Name +
                                   string.Format("X:      {0} Y:      {1}", hero.Position.X, hero.Position.Y));

                var pos = hero.Position.To2D();
                pos.X = pos.X / (panel1.Width / 10);
                pos.Y = pos.Y / (panel1.Height / 10);
                listBox1.Items.Add(pos.X + " : " + pos.Y);
                pg.DrawString(hero.Name, DefaultFont, new SolidBrush(Color.White), pos.X, pos.Y);
                //pg.DrawEllipse(Pens.White, new Rectangle(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y), 16, 16 ));
                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
