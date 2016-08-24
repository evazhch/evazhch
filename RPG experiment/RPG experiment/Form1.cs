using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RPG_experiment
{
   
    public partial class Form1 : Form
    {
       
        //int x = 50; int y = 50; 
        //int face = 1;
        int animation_ctr1 = 0;
         public static Player[] player = new Player[3];
            public static Map[] map = new Map[4];
            public static WMPLib.WindowsMediaPlayer music_player = new WMPLib.WindowsMediaPlayer();
            public static Npc[] npc = new Npc[8];
            public Bitmap mc_nomal;
            public Bitmap mc_event;
            public int mc_mod = 0;//0-nomal,1-event
        

           
           
        public Form1()
        {
            InitializeComponent();
           
        }
        private void Draw()
        {
            Graphics g1 = stage.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            Bitmap bitmap = new Bitmap(@"RPG1.png");
            //自定义绘图
            if (Fight.fighting == 0)
                Map.draw(map, player, npc, g, new Rectangle(0, 0, stage.Width, stage.Height));
            else
                Fight.draw(g);
            animation_ctr1 = animation_ctr1 + 1;
            
            //Map.draw(map,player,npc,g,new Rectangle(0,0,stage.Width,stage.Height));
            
            if (Panel.panel != null)
                Panel.draw(g);
            draw_mouse(g);
            //显示图像并释放资源
            myBuffer.Render();
            myBuffer.Dispose();
            //自定义绘图
            

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Player.key_ctrl(player,map,npc,e);
            //Draw();
            if (Panel.panel != null)
                Panel.key_ctrl(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Player.key_ctr1_up(player, e);
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //光标
            mc_nomal = new Bitmap(@"mc_1.png");
            mc_nomal.SetResolution(96, 96);
            mc_event = new Bitmap(@"mc_2.png");
            mc_event.SetResolution(96, 96);
            
            
            Title.init();
            Message.init();
            Statusmenu.init();
            Shop.init();
            Save.init();
            Fight.init();
            Define.define(player, npc, map);
           
            Map.change_map(map, player,npc,0, 50, 280, 1,music_player);
            
            //Button b = new Button();
            //b.click_event += new Button.Click_event(tryevent);
            //b.click();
            Title.show();
            //this.Show();
            //Fight.start(new int[] { 0, 0, -1 }, "fight/f_scene.png", 1, 0, 1, 1, 100); 
            Cursor.Hide();
           
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Player.timer_logic(player, map);
            for (int i = 0; i < npc.Length;i++)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != Map.current_map)
                    continue;
                npc[i].timer_logic(map);
            }
                Draw();
        }

        //public void tryevent()
        //{
        //    MessageBox.Show("成功");
        //}
        
        private  void stage_MouseMove(object sender,MouseEventArgs e)
        {
            if (Panel.panel != null)
                Panel.mouse_move(e);
            mc_mod = Npc.check_mouse_collision(map, player, npc, new Rectangle(0, 0, stage.Width, stage.Height), e);
        }
        private  void stage_MouseClick(object sender,MouseEventArgs e)
        {
            Player.mouse_click(map, player, new Rectangle(0, 0, stage.Width, stage.Height), e);
            
            Npc.mouse_click(map, player, npc, new Rectangle(0, 0, stage.Width, stage.Height), e);
            if (Panel.panel != null)
                Panel.mouse_click(e);
        }
        public static void key_ctrl_up(Player[]player,KeyEventArgs e)
        {
            Player.stop_walk(player);
        }
       
        private void draw_mouse(Graphics g)
        {
            Point showpoint = stage.PointToClient(Cursor.Position);
            if (mc_mod == 0)
                g.DrawImage(mc_nomal, showpoint.X, showpoint.Y);
            else
                g.DrawImage(mc_event, showpoint.X, showpoint.Y);
        }
        private void stage_MouseEnter(object sender,EventArgs e)
        {
            Cursor.Hide();
        }
        private void stage_MouseLeave(object sender,EventArgs e)
        {
            Cursor.Show();
        }
    }
    
}
