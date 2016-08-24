using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTest
{
    public partial class Form1 : Form
    {
 
        int animation_ctrl = 0;
        Player[] player = new Player[3];
        Map[] map = new Map[2];
        Npc[] npc = new Npc[2];

        WMPLib.WindowsMediaPlayer music_player = new WMPLib.WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender,EventArgs e)
        {
            player[0] = new Player();
            player[0].bitmap = new Bitmap(@"player1.png");
            player[0].bitmap.SetResolution(96, 96);
            player[0].is_active = 1;

            player[1] = new Player();
            player[1].bitmap = new Bitmap(@"player2.png");
            player[1].bitmap.SetResolution(96, 96);
            player[1].is_active = 1;

            player[2] = new Player();
            player[2].bitmap = new Bitmap(@"player3.png");
            player[2].bitmap.SetResolution(96, 96);
            player[2].is_active = 1;

            map[0] = new Map();
            map[0].bitmap_path = "map1.png";
            map[0].shade_path = "map1_shade.png";
            map[0].block_path = "map1_block.png";
            map[0].back_path = "map1_back.png";
            map[0].music = "1.mp3";

            map[1] = new Map();
            map[1].bitmap_path = "map2.png";
            map[1].shade_path = "map2_shade.png";
            map[1].block_path = "map2_block.png";
            map[1].back_path = "map2_back.png";
            map[1].music = "2.mp3";

            npc[0] = new Npc();
            npc[0].map = 0;
            npc[0].x = 100;
            npc[0].y = 100;
            npc[0].bitmap_path = "npc1.png";

            Map.chang_map(map, player,npc,0, 0, 0,1,music_player);
            Draw();

        }

        private void Draw()
        {
            //pictureBox1.Refresh();
            //Bitmap bitmap = new Bitmap(@"player1.png");
            //bitmap.SetResolution(96, 96);
            //创建在picturebox1上的图像g1
            Graphics g1 = pictureBox1.CreateGraphics();
            //将图像画在内存上
            

            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;

            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);

            Graphics g = myBuffer.Graphics;

            //自定义绘图

            //animation_ctrl = animation_ctrl + 1;

            Map.draw(map,player,npc,g,new Rectangle(0,0,pictureBox1.Width,pictureBox1.Height));
            
            myBuffer.Render();
            myBuffer.Dispose();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Player.key_ctrl(player,map,e);
            Draw();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Player.key_ctrl_up(player, e);
            Draw();
        }
    }
}
