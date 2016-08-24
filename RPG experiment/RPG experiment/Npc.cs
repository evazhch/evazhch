using System.Drawing;
using System.Windows.Forms;
public class Npc
{ 
    public enum Npc_type
    {
        NOMAL=0,
        CHARACTER=1,
    }
    public Npc_type npc_type = Npc_type.NOMAL;
//碰撞区域
    public int region_x=20;
    public int region_y =40;
//位置
    public int map = -1;
    public int x = 0;
    public int y = 0;
    public int x_offset = -20;
    public int y_offset = -40;
//显示
    public string bitmap_path;
    public Bitmap bitmap;
    public bool visible = true;
    public enum Collosion_type
    {
        KEY=1,
        ENTER=2,
    }
    public Collosion_type collision_type = Collosion_type.KEY;

 //动画
    public Animation[] anm;
    public int anm_fram = 0;
    public int current_anm = -1;
    public long last_anm_time = 0;
//人物类
    public Comm.Direction face = Comm.Direction.DOWN;
    public int walke_fram = 0;
    public long last_walk_time = 0;
    public long walk_interval = 80;
    public int speed = 5;
    public Comm.Direction idle_walk_direction = Comm.Direction.DOWN;
    public int idle_walk_time = 0;
    public int idle_walk_time_now = 0;
 //鼠标碰撞区域
    public int mc_xoffset = 0;
    public int mc_yoffset = -30;
    public int mc_w = 10;
    public int mc_h = 10;
    public static int mc_distance_x = 30;
    public static int mc_distance_y = 20;
//加载
    public void load()
    {
        if(bitmap_path!=null)
        {
            bitmap = new Bitmap(bitmap_path);
            bitmap.SetResolution(96, 96);
        }
        if(anm!=null)
        {
            for (int i = 0; i < anm.Length;i++)
            {
                anm[i].load();
            }
        }
        //鼠标碰撞区域
        if(bitmap!=null)
        {
            if(npc_type==Npc_type.NOMAL)
            {
                if (mc_w == 0)
                    mc_w = bitmap.Width;
                if (mc_h == 0)
                    mc_h = bitmap.Height;

            }
            else if(npc_type==Npc_type.CHARACTER)
            {
                if (mc_w == 0)
                    mc_w = bitmap.Width/4;
                if (mc_h == 0)
                    mc_h = bitmap.Height/4;
            }
        }
        else 
        {
            if (mc_w == 0)
                mc_w = region_x;
            if (mc_h == 0)
                mc_h = region_y;
         }
    }
    public void unload()
    {
        if(bitmap_path!=null)
        {
            bitmap = null;
        }
        if(anm!=null)
        {
            for (int i = 0; i < anm.Length; i++)
                anm[i].unload();
        }
    }
    public void draw(Graphics g,int map_sx,int map_sy)
    {
        if (visible != true)
            return;
        //绘制角色
        if (current_anm < 0)
        {
            if (npc_type == Npc_type.NOMAL)
            {
                if (bitmap != null)
                {
                    x_offset = -bitmap.Width / 2; y_offset = -bitmap.Height / 2;
                    region_x = bitmap.Width; region_y = bitmap.Height;
                    g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
                    
                }
            }   
            else if(npc_type==Npc_type.CHARACTER)
            {
               
                draw_character(g, map_sx, map_sy);
            }
        }
        //绘制动画
        else
        {
           
            draw_anm(g, map_sx, map_sy);
        }
        
    }
    public bool is_collision(int collision_x,int collision_y)
    {
        Rectangle rect = new Rectangle(x - region_x / 2, y- region_y / 2, region_x, region_y);
        return rect.Contains(new Point(collision_x, collision_y));
    }
    public bool is_line_collision(Point p1,Point p2)
    {
        if (is_collision(p2.X, p2.Y)) return true;
        int px, py;
        px = p1.X + (p2.X - p1.X) / 2;
        py = p1.Y + (p2.Y - p1.Y) / 2;
        if (is_collision(px, py)) return true;

        px = p2.X - (p2.X - p1.X) / 4;
        py = p2.Y - (p2.Y - p1.Y) / 4;
        if (is_collision(px, py)) return true;

        return false;
    }
     public void draw_anm(Graphics g,int map_sx,int map_sy)
    {
        if(anm==null
            ||current_anm>=anm.Length
            ||anm[current_anm]==null
            ||anm[current_anm].bitmap_path==null)
        {
            current_anm = -1;
            anm_fram = 0;
            last_anm_time = 0;
            return;
        }
        anm[current_anm].draw(g, anm_fram, map_sx + x + x_offset, y + map_sy + y_offset);
         if(Comm.Time()-last_anm_time>=Animation.RATE)
         {
             anm_fram = anm_fram + 1; 
             last_anm_time = Comm.Time();
             if(anm_fram/anm[current_anm].anm_rate>=anm[current_anm].max_fram)
             {
                 current_anm = -1;
                 anm_fram = 0;
                 last_anm_time = 0;
             }
         }
    }
    public void play_anm(int index)
     {
         current_anm = index;
         anm_fram = 0;
     }
    //npc画角色
    public void draw_character(Graphics g,int map_sx,int map_sy)
    {
        Rectangle rent = new Rectangle(
            bitmap.Width/8*(walke_fram%4),
            bitmap.Height/4*((int)face-1),
            bitmap.Width/8,
            bitmap.Height/4);
        Bitmap bitmap0 = bitmap.Clone(rent, bitmap.PixelFormat);
        g.DrawImage(bitmap0, map_sx + x + x_offset, map_sy + y + y_offset);
    }
    //走一步
    public void walk(Map[]map,Comm.Direction direction,bool isblock)
    {
        //转向
        face = direction;
        //间隔判定
        if (Comm.Time() - last_walk_time <= walk_interval)
            return;
        //行走
        //up
        else if(direction==Comm.Direction.UP && (!isblock||Map.can_through(map,x,y-speed)))
        {
            y=y-speed;
        }
        //down
        else if(direction==Comm.Direction.DOWN && (!isblock||Map.can_through(map,x,y+speed)))
        {
            y=y+speed;
        }
        //right
        else if(direction==Comm.Direction.RIGHT && (!isblock||Map.can_through(map,x+speed,y)))
        {
            x=x+speed;
        }
        //left
        else if(direction==Comm.Direction.LEFT && (!isblock||Map.can_through(map,x-speed,y)))
        {
            x=x-speed;
        }
        //动画帧
        walke_fram = walke_fram + 1;
        if (walke_fram >= int.MaxValue) walke_fram = 0;
        //时间
        last_walk_time = Comm.Time();
        
    }
     public void stop_walk()
    {
        walke_fram = 0;
        last_walk_time = 0;
    }
    public void timer_logic(Map[] map)
     {
        if(npc_type==Npc_type.CHARACTER && idle_walk_time!=0)
        {
            Comm.Direction direction;
            if (idle_walk_time_now >= 0)
                direction = idle_walk_direction;
            else
                direction = Comm.opposite_direction(idle_walk_direction);

            walk(map, direction, true);

            if(idle_walk_time_now>=0)
            {
                idle_walk_time_now = idle_walk_time_now + 1;
                if (idle_walk_time_now > idle_walk_time)
                    idle_walk_time_now =-1;
            }
            else if(idle_walk_time_now<0)
            {
                idle_walk_time_now = idle_walk_time_now - 1;
                if (idle_walk_time_now < -idle_walk_time)
                    idle_walk_time_now = 1;
            }
        }

     
     }

    //鼠标碰撞
    public bool is_mouse_collision(int collision_x,int collision_y)
    {
        //有图
        if(bitmap!=null)
        {
            if(npc_type==Npc_type.NOMAL)
            {
                int center_x = x + x_offset + bitmap.Width / 2;
                int center_y = y + y_offset + bitmap.Height / 2;
                Rectangle rect = new Rectangle(center_x - mc_w / 2, center_y - mc_h / 2, mc_w, mc_h);
                return rect.Contains(new Point(collision_x, collision_y));
            }
            else
            {
                int center_x = x + x_offset + bitmap.Width / 2/4;
                int center_y = y + y_offset + bitmap.Height / 2/4;
                Rectangle rect = new Rectangle(center_x - mc_w / 2, center_y - mc_h / 2, mc_w, mc_h);
                return rect.Contains(new Point(collision_x, collision_y));            
            }
        }
        //无图
        else
        {
            Rectangle rect = new Rectangle(x - mc_w / 2, y - mc_h / 2, mc_w, mc_h);
            return rect.Contains(new Point(collision_x, collision_y));
        }
    }
    //检测距离
    public bool check_me_distance(Npc npc,int player_x,int player_y)
    {
        Rectangle rect = new Rectangle(npc.x - mc_distance_x / 2, npc.y - mc_distance_y / 2, mc_distance_x, mc_distance_y);
        return rect.Contains(new Point(player_x, player_y));
    }
    //鼠标操作
    public static void mouse_click(Map[]map,Player[]player,Npc[]npc,Rectangle stage,MouseEventArgs e)
    {
        if (Player.status != Player.Status.WALK)
            return;
        if (npc == null)
            return;
        for(int i=0;i<npc.Length;i++)
        {
            if (npc[i] == null)
                continue;
            if (npc[i].map != Map.current_map)
                continue;
            int collision_x = e.X - Map.get_map_sx(map, player, stage);
            int collision_y = e.Y - Map.get_map_sy(map, player, stage);
            if(!npc[i].check_me_distance(npc[i],Player.get_pos_x(player),Player.get_pos_y(player)))
            {
                //Player.stop_walk(player);
                //Message.showtip("请走近点");

                //Task.block();
                Player.mouse_click(map, player, stage, e);
                continue;
            }
            Player.stop_walk(player);
            Task.story(i);
        }
    }
    //检测碰撞
    //0-没有 1-有
    public static int check_mouse_collision(Map[]map,Player[]player,Npc[]npc,Rectangle stage,MouseEventArgs e)
    {
        if (Player.status != Player.Status.WALK)
            return 0;
        if (npc == null)
            return 0;
        for (int i=0;i<npc.Length;i++)
        {
            if (npc[i] == null)
                continue;
            if (npc[i].map != Map.current_map)
                continue;
            int collision_x = e.X - Map.get_map_sx(map, player, stage);
            int collision_y = e.Y - Map.get_map_sy(map, player, stage);
            if(npc[i].is_mouse_collision(collision_x,collision_y))
            {
                return 1;
            }
        }
        return 0;
    }
}
