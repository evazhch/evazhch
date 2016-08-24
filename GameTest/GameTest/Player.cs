using System.Windows.Forms;
using System.Drawing;

namespace GameTest
{
    public class Player
    {
        public int x = 0;
        public int y = 0;
        public int face = 0;

        public int anm_frame = 0;
        public long last_walk_time = 0;
        public long walk_interval = 100;
        public int speed = 20;
        
        public int x_offset = -20;
        public int y_offset = -30;

        //当前角色
        public static int current_player = 0;
        //是否激活
        public int is_active = 0;

        public Bitmap bitmap;

        public Player()
        {
            bitmap = new Bitmap(@"player1.png");
            bitmap.SetResolution(96, 96);
        }
        public static void key_ctrl( Player[] player ,Map[] map,KeyEventArgs e)
        {
            Player p = player[current_player];
            //加速键
            if (e.KeyCode == Keys.E)
                p.speed = 40;
            if(e.KeyCode==Keys.Q)
                p.speed = 20;
            //切换角色
            if (e.KeyCode == Keys.Tab) { key_change_player(player); }
            //是否专向
            if (e.KeyCode == Keys.Up && p.face != 4)
                p.face = 4;
            else
                if (e.KeyCode == Keys.Down && p.face != 1)
                    p.face = 1;
            else
                    if (e.KeyCode == Keys.Left && p.face != 2)
                        p.face = 2;
            else
                        if (e.KeyCode == Keys.Right && p.face != 3)
                            p.face = 3;
            //间隔判断
            if (Comm.Time() - p.last_walk_time <= p.walk_interval)
                return;
            //移动处理
            if (e.KeyCode == Keys.Up && Map.can_through(map,p.x,p.y-p.speed))  
                p.y = p.y - p.speed;
            else if (e.KeyCode == Keys.Down && Map.can_through(map, p.x, p.y + p.speed)) 
                p.y = p.y + p.speed;
            else if (e.KeyCode == Keys.Left && Map.can_through(map, p.x-p.speed, p.y)) 
                p.x = p.x - p.speed;
            else if (e.KeyCode == Keys.Right && Map.can_through(map, p.x+p.speed, p.y)) 
                p.x = p.x + p.speed;
            //动画帧
            p.anm_frame = p.anm_frame + 1;
            if (p.anm_frame >= int.MaxValue) p.anm_frame = 0;
            //时间
            p.last_walk_time = Comm.Time();
        }
        public static void key_ctrl_up(Player[] play,KeyEventArgs e)
        {
            Player p = play[current_player];
            //动画帧
            p.anm_frame = 0;
            p.last_walk_time = 0;

        }
        public static void key_change_player(Player[] player)
        {
            for(int i=current_player+1;i<player.Length;i++)
               if(player[i].is_active==1)
               {
                   set_player(player, current_player, i);
                   return;
               }
           for(int i=0;i<current_player;i++)
               if(player[i].is_active==1)
               {
                   set_player(player, current_player, i);
                   return;
               }
        }
        public static void set_player(Player[] player,int oldindex,int newindex)
        {
            current_player = newindex;
            player[newindex].x = player[oldindex].x;
            player[newindex].y = player[oldindex].y;
            player[newindex].face = player[oldindex].face;
        }
        public static void draw(Player[] player,Graphics g,int map_sx,int map_sy)
        {
            Player p = player[current_player];
            //自定义绘图
            Rectangle crazycodeRg1 = new Rectangle(p.bitmap.Width / 4 * (p.anm_frame % 4), p.bitmap.Height / 4 * (p.face - 1), p.bitmap.Width / 4, p.bitmap.Height / 4);//定义区域
            Bitmap bitmap0 = p.bitmap.Clone(crazycodeRg1,p.bitmap.PixelFormat);//复制小图
            g.DrawImage(bitmap0,map_sx+p.x+p.x_offset,map_sy+p.y+p.y_offset);
        }
        public static void set_pos(Player[] player,int x,int y,int face)
        {
            player[current_player].x = x;
            player[current_player].y = y;
            player[current_player].face = face;
        }
        public static int get_pos_x(Player[] player)
        {
            return player[current_player].x;
        }
        public static int get_pos_y(Player[] player)
        {
            return player[current_player].y;
        }
        public static int get_pos_face(Player[] player)
        {
            return player[current_player].face;
        }
    
    }
}
