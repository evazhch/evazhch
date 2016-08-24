using System.Windows.Forms;
using System.Drawing;

public class Player
{
    public  enum Status
    {
        WALK=1,
        PANNEL=2,
        TASK=3,
        FIGHT=4
    }
    public static Status status = Status.WALK;
    public Bitmap bitmap;
    //当前角色
    public static int current_player = 0;
    public int is_action = 0;
    //行走
    public int x = 0;
    public int y = 0;
    public int face = 1;
    public int anm_frame = 0;
    public long last_walk_time = 0;
    public long walk_interval = 100;
    public int speed = 20;
    public int x_offest = -20;
    public int y_offest = -80;
    public int collision_ray = 30;
    //鼠标操作
    public static int target_x = -1;
    public static int target_y = -1;
    //目的地标记
    public static Bitmap move_flag;
    public static long FLAG_SHOW_TIME = 3000;
    public static long flag_start_time = 0;
    //状态
    public int max_hp = 100;
    public int hp = 100;
    public int max_mp = 100;
    public int mp = 100;
    public int attack = 10;
    public int defense = 10;
    public int fspeed = 100;
    public int fortune = 10;
    public int equip_att = -1;
    public int equip_def = -1;
    public static int select_player=0;
    public Bitmap status_bimap;
    public int[] skill = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    public static int money = 200;
    //战斗
    public Bitmap fbitmap;
    public int fx_offset = -120;
    public int fy_offset = -120;//图像偏移
    public Bitmap fface;//角色脸谱
    public Animation anm_att;//攻击动画
    public Animation anm_item;//物品动画
    public Animation anm_skill;//技能动画
    public string name = "";




    public Player()
    {
        bitmap = new Bitmap(@"rpg1.png");
        bitmap.SetResolution(96, 96);
        move_flag = new Bitmap(@"move_flag.png");
        move_flag.SetResolution(96, 96);
    }
    //--------------------------------------------------------------
    //操控
    //
    //...............................................................
    public static void key_ctrl(Player[] player,Map[] map,Npc[] npc,KeyEventArgs e)
    {
        if (Player.status != Status.WALK)
            return;
        Player p = player[current_player];
        //切换角色
        if (e.KeyCode == Keys.Tab) { key_change_player(player); }
        //是否转向
        if (e.KeyCode == Keys.Up )
            walk(player,map,Comm.Direction.UP);
        else if (e.KeyCode == Keys.Down )
            walk(player, map, Comm.Direction.DOWN);
        else if (e.KeyCode == Keys.Left )
            walk(player, map, Comm.Direction.LEFT);
        else if (e.KeyCode == Keys.Right)
            walk(player, map, Comm.Direction.RIGHT);
        else if(e.KeyCode==Keys.Escape)
        {
            Statusmenu.show();
            Task.block();
        }
        //npc碰撞
        npc_collision(player, map, npc, e);
        /*
        //间隔判定
        if (Comm.Time() - p.last_walk_time <= p.walk_interval)
            return;
        //移动处理
        if (e.KeyCode == Keys.Down && Map.can_through(map,p.x,p.y+p.speed)) { p.y = p.y + p.speed; } // p.face = 1; }
        else if (e.KeyCode == Keys.Up && Map.can_through(map, p.x, p.y - p.speed)) { p.y = p.y - p.speed; }  //p.face = 4; }
        else if (e.KeyCode == Keys.Left && Map.can_through(map, p.x-p.speed, p.y)) { p.x = p.x - p.speed; } //p.face = 2; }
        else if (e.KeyCode == Keys.Right && Map.can_through(map, p.x+p.speed, p.y)) { p.x = p.x + p.speed; } //p.face = 3; }
        else return;
        //动画帧
        p.anm_frame = p.anm_frame + 1;
        if (p.anm_frame >= int.MaxValue) p.anm_frame = 0;
        //时间
        p.last_walk_time = Comm.Time();
         */
    }
    public static void key_ctr1_up(Player[] player,KeyEventArgs e)
    {
        Player p = player[current_player];
        //动画帧
        p.anm_frame = 0;
        p.last_walk_time = 0;
    }
    public static void draw(Player[] player,Graphics g,int map_sx,int map_sy)//int animation_ctr1)
    {
        //Bitmap bitmap = new Bitmap(@"RPG1.png");
        //自定义绘图
        //animation_ctr1 = animation_ctr1 + 1;
        Player p = player[current_player];
        Rectangle crazycoderRg1 = new Rectangle(
                                                p.bitmap.Width / 8 * (p.anm_frame% 8), 
                                                p.bitmap.Height / 4 * (p.face - 1), 
                                                p.bitmap.Width / 8, 
                                                p.bitmap.Height / 4
                                                );//定义区域
        Bitmap bitmap0 = p.bitmap.Clone(crazycoderRg1, p.bitmap.PixelFormat);//复制小图
        g.DrawImage(bitmap0, map_sx + p.x+p.x_offest, map_sy + p.y+p.y_offest);
        //Graphics g = pictureBox1.CreateGraphics();
        //g.DrawImage(bitmap0, x, y);
        //Bitmap bitmap = new Bitmap(@"experiment1.png");
        //bitmap.SetResolution(96, 96);
        //Graphics g1 = pictureBox1.CreateGraphics();
        //BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
        //BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);

        //Graphics g = myBuffer.Graphics;
        //g.DrawImage(bitmap0, p.x, p.y);
        //myBuffer.Render();
        //myBuffer.Dispose();

        //g.DrawRectangle(new Pen(Color.Black), 10, 10, 50, 50);*/
        //Graphics g = pictureBox1.CreateGraphics();
        //Bitmap r = new Bitmap("experiment1.png");
        //r.SetResolution(96, 96);
        //g.RotateTransform(30);
        //g.DrawImage(r, 0, 0);
    }
    //---------------------------------------
    //切换角色
    //---------------------------------------
    public static void key_change_player(Player[] player)
    {
        for(int i=current_player+1;i<player.Length;i++)
            if (player[i].is_action==1)
            {
                set_player(player, current_player, i);
                return;
            }
        for(int i=0;i<current_player;i++)
            if(player[i].is_action==1)
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

    public static void walk(Player[]player,Map[]map,Comm.Direction direction)
    {
        Player p = player[current_player];
        //转向
        p.face = (int)direction;
        //间隔判定
        if (Comm.Time() - p.last_walk_time <= p.walk_interval)
            return;
        //行走
        //up
        if(direction==Comm.Direction.UP&&Map.can_through(map,p.x,p.y-p.speed))
        {
            p.y = p.y - p.speed;
        }
        //down
        else if (direction==Comm.Direction.DOWN && Map.can_through(map,p.x,p.y+p.speed))
        {
            p.y = p.y + p.speed;
        }
        //right
        else if (direction==Comm.Direction.RIGHT && Map.can_through(map,p.x+p.speed,p.y))
        {
            p.x = p.x + p.speed;
        }
        //left
        else if (direction == Comm.Direction.LEFT && Map.can_through(map, p.x - p.speed, p.y))
        {
            p.x = p.x - p.speed;
        }
        //动画帧
        p.anm_frame = p.anm_frame + 1;
        if (p.anm_frame >= int.MaxValue) p.anm_frame = 0;
        //时间
        p.last_walk_time = Comm.Time();
    }
    public static Point get_collision_point(Player[]player)
    {
        Player p = player[current_player];
        int collision_x = 0;
        int collision_y = 0;

        if(p.face==(int)Comm.Direction.UP)
        {
            collision_x = p.x;
            collision_y = p.y - p.collision_ray;
        }
        if (p.face == (int)Comm.Direction.DOWN)
        {
            collision_x = p.x;
            collision_y = p.y + p.collision_ray;
        }
        if (p.face == (int)Comm.Direction.LEFT)
        {
            collision_x = p.x-p.collision_ray;
            collision_y = p.y;
        }
        if (p.face == (int)Comm.Direction.RIGHT)
        {
            collision_x = p.x+p.collision_ray;
            collision_y = p.y;
        }
        return new Point(collision_x, collision_y);
    }
    public static void npc_collision(Player[]player,Map[]map,Npc[]npc,KeyEventArgs e)
    {
        Player p = player[current_player];
        Point p1 = new Point(p.x, p.y);
        Point p2 = get_collision_point(player);

        for(int i=0;i<npc.Length;i++)
        {
            if (npc[i] == null)
                continue;
            if (npc[i].map != Map.current_map)
                continue;

            if(npc[i].is_line_collision(p1,p2))
            {
                if(npc[i].collision_type==Npc.Collosion_type.ENTER)
                {
                    Task.story(i);
                    break;
                }
                else if (npc[i].collision_type==Npc.Collosion_type.KEY)
                {
                    if(e.KeyCode==Keys.Space||e.KeyCode==Keys.Enter)
                    {
                        Task.story(i);
                        break;

                    }
                }
            }
        }
    }
    public static int is_reach_x(Player[]player,int target_x)
    {
        Player p = player[current_player];
        if (p.x - target_x > p.speed / 2) return 1;
        if (p.x - target_x < -p.speed / 2) return -1;
        return 0;
    }
    public static int is_reach_y(Player[]player,int target_y)
    {
        Player p = player[current_player];
        if (p.y - target_y > p.speed / 2) return 1;
        if (p.y - target_y < -p.speed / 2) return -1;
        return 0;
    } 
    public static void mouse_click(Map[] map,Player[]player,Rectangle stage,MouseEventArgs e)
    {
        if (Player.status != Status.WALK)
            return;
        if(e.Button==MouseButtons.Left)
        {
            target_x = e.X - Map.get_map_sx(map, player, stage);
            target_y = e.Y - Map.get_map_sy(map, player, stage);
            flag_start_time = Comm.Time();
        }
        else if(e.Button==MouseButtons.Right)
        {
            Statusmenu.show();
            Task.block();
        }

    }
    public static void timer_logic(Player[]player,Map[]map)
    {
        move_logic(player, map);
    }
    public static void move_logic(Player[]player,Map[]map)
    {
        if (target_x < 0 || target_y < 0)
            return;
        step_to(player, map, target_x, target_y);
    }
    public static void stop_walk(Player[]player)
    {
        Player p = player[current_player];
        //动画帧
        p.anm_frame = 0;
        p.last_walk_time = 0;
        //目标位置
        target_x = -1;
        target_y = -1;
    }
    public static void step_to(Player[]player,Map[]map,int target_x,int target_y)
    {
        if(is_reach_x(player,target_x)==0 && is_reach_y(player,target_y)==0)
        {
            stop_walk(player);
            return;
        }
        Player p = player[current_player];
        if(is_reach_x(player,target_x)>0 && Map.can_through(map,p.x-p.speed,p.y))
        {
            walk(player, map, Comm.Direction.LEFT);
            return;
        }
        else if(is_reach_x(player,target_x)<0 && Map.can_through(map,p.x+p.speed,p.y))
        {
            walk(player, map, Comm.Direction.RIGHT);
            return;
        }

        if (is_reach_y(player, target_y) > 0 && Map.can_through(map, p.x , p.y-p.speed))
        {
            walk(player, map, Comm.Direction.UP);
            return;
        }
        else if (is_reach_y(player, target_y) < 0 && Map.can_through(map, p.x , p.y+p.speed))
        {
            walk(player, map, Comm.Direction.DOWN);
            return;
        }
        stop_walk(player);
    }
    public static void draw_flag(Graphics g,int map_sx,int map_sy)
    {
        if (target_x < 0 || target_y < 0)
            return;
        if (move_flag == null)
            return;
        if (Comm.Time() - flag_start_time > FLAG_SHOW_TIME)
            return;
        g.DrawImage(move_flag, map_sx + target_x , map_sy + target_y );
    }

    //设置战斗
    public void fset(string name, string fbitmap_path, int fx_offset, int fy_offset, string fface_path
       , Animation anm_att, Animation anm_item, Animation anm_skill)
    {
        this.name = name;
        if (fbitmap_path != null && fbitmap_path != "")
        {
            this.fbitmap = new Bitmap(fbitmap_path);
            this.fbitmap.SetResolution(96, 96);
        }
        this.fx_offset = fx_offset;
        this.fy_offset = fy_offset;
        if (fface_path != null && fface_path != "")
        {
            this.fface = new Bitmap(fface_path);
            this.fface.SetResolution(96, 96);
        }

        this.anm_att = anm_att;
        this.anm_item = anm_item;
        this.anm_skill = anm_skill;

        anm_att.load();
        anm_item.load();
        anm_skill.load();
    }

}  
