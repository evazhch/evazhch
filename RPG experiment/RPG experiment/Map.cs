using System.Drawing;
public class Map
{
    public class layer_sort_comparer : System.Collections.IComparer
    {
        public int Compare(object s1, object s2)
        {
            return ((layer_sort)s1).y - ((layer_sort)s2).y;
        }
    }
    public struct layer_sort
    {
        public int y;
        public int index;
        //0-主角 1-npc
        public int type;
    }
    public static int current_map = 0;

    public string bitmap_path;
    public Bitmap bitmap;
    
    public string shade_path;
    public Bitmap shade;
    
    public string block_path;
    public Bitmap block;
    
    public string music;
    public Map()
    {
    bitmap_path ="map.bmp";
    }
    public static void draw(Map[] map,Player[] player,Npc[] npc,Graphics g,Rectangle stage)
    {
        Map m=map[current_map];
        //绘图位置x
        int map_sx = get_map_sx(map,player,stage);//地图屏幕坐标
        //绘图位置y
        int map_sy = get_map_sy(map,player,stage);//地图屏幕坐标

        int p_x = Player.get_pos_x(player);//角色坐标
        int p_y = Player.get_pos_y(player);//角色坐标
        int map_h = m.bitmap.Height;
        int map_w = m.bitmap.Width;

        if(p_x <= stage.Width/2)
        {
            map_sx = 0;
        }
        else if(p_x>=map_w-stage.Width/2)
        {
            map_sx = stage.Width - map_w;
        }
        else
        {
            map_sx = stage.Width / 2 - p_x;
        }

        if (p_y <= stage.Height / 2)
        {
            map_sy = 0;
        }
        else if (p_y >= map_h - stage.Height / 2)
        {
            map_sy = stage.Height - map_h;
        }
        else
        {
            map_sy = stage.Height / 2 - p_y;
        }
        //绘图
        g.DrawImage(m.bitmap,map_sx,map_sy);
        Player.draw(player, g, map_sx, map_sy);
        draw_player_npc(map, player, npc, g, map_sx, map_sy);
        g.DrawImage(m.shade, map_sx, map_sy);
        Player.draw_flag(g, map_sx, map_sy);
        //for(int i=0;i< npc.Length;i++)
        //{
        //    if (npc[i] == null)
        //        continue;
        //    if (npc[i].map != current_map)
        //        continue;
        //    npc[i].draw(g, map_sx, map_sy);
        //}
    }
    public static void change_map(Map[] map,Player[] player,Npc[] npc,int newindex,int x,int y,int face,WMPLib.WindowsMediaPlayer music_player)
    {
        //卸载旧地图
        if(map[current_map].bitmap!=null)
        {
            map[current_map].bitmap = null;
        }
        if(map[current_map].shade!=null)
        {
            map[current_map].shade = null;
        }
        if(map[current_map].block!=null)
        {
            map[current_map].block = null;
        }
        //加载新地图资源
        map[newindex].bitmap = new Bitmap(map[newindex].bitmap_path);
        map[newindex].bitmap.SetResolution(96, 96);
        map[newindex].shade = new Bitmap(map[newindex].shade_path);
        map[newindex].shade.SetResolution(96, 96);
        map[newindex].block = new Bitmap(map[newindex].block_path);
        map[newindex].block.SetResolution(96, 96);
        //npc资源
        for (int i = 0; i < npc.Length;i++)
        {
            if (npc[i] == null)
                continue;
            if (npc[i].map == current_map)
                npc[i].unload();
            if (npc[i].map == newindex)
                npc[i].load();
        }
            //current_map
            current_map = newindex;
        //位置设置
        Player.set_pos(player,x,y,face);
        //音乐
        music_player.URL = map[current_map].music;
    }
    public static bool can_through(Map[] map,int x,int y)
    {
        Map m = map[current_map];
        if (x <= 0) return false;
        else if (x >= m.block.Width) return false;
        else if (y <= 0) return false;
        else if (y >= m.block.Height) return false;

        if (m.block.GetPixel(x, y).B == 0)
              return false;
        else
            return true;

    }
    public static int get_map_sx(Map[] map,Player[] player,Rectangle stage)
    {
        Map m = map[current_map];
        if (m.bitmap == null)
            return 0;
        int map_sx = 0;
        int p_x = Player.get_pos_x(player);//脚色坐标
        int map_w = m.bitmap.Width;
        if(p_x<=stage.Width/2)
        {
            map_sx = 0;
        }
        else if(p_x>=map_w-stage.Width/2)
        {
            map_sx = stage.Width - map_w;
        }
        else
        {
            map_sx = stage.Width / 2 - p_x;
        }
        return map_sx;
    }
    public static int get_map_sy(Map[] map, Player[] player, Rectangle stage)
    {
        Map m = map[current_map];
        if (m.bitmap == null)
            return 0;
        int map_sy = 0;
        int p_y = Player.get_pos_x(player);//脚色坐标
        int map_h = m.bitmap.Height;
        if (p_y <= stage.Height / 2)
        {
            map_sy = 0;
        }
        else if (p_y >= map_h - stage.Height / 2)
        {
            map_sy = stage.Height - map_h;
        }
        else
        {
            map_sy = stage.Height / 2 - p_y;
        }
        return map_sy;
    }
    public static void draw_player_npc(Map[]map,Player[]player,Npc[]npc,Graphics g,int map_sx,int map_sy)
    {
         

        //绘制主角和npc
        layer_sort[] layer_sort = new layer_sort[npc.Length + 1];
        for(int i=0;i<npc.Length;i++)
        {
            if(npc[i]!=null)
            {
                layer_sort[i].y = npc[i].y;
                layer_sort[i].index = i;
                layer_sort[i].type = 1;
            }
            else
            {
                layer_sort[i].y = int.MaxValue;
                layer_sort[i].index = i;
                layer_sort[i].type = 1;
            }
        }
            layer_sort[npc.Length].y = Player.get_pos_y(player);
            layer_sort[npc.Length].index = 0;
            layer_sort[npc.Length].type = 0;

            System.Array.Sort(layer_sort, new layer_sort_comparer());

            for(int i=0;i<layer_sort.Length;i++)
            {
                //画主角
                if(layer_sort[i].type==0)
                {
                    Player.draw(player,g,map_sx,map_sy);

                }
                //画npc
                else if(layer_sort[i].type==1)
                {
                    int index=layer_sort[i].index;
                    if(npc[index]==null)
                        continue;
                    if(npc[index].map!=current_map)
                        continue;
                    npc[index].draw(g,map_sx,map_sy);
                }
            }

        }
    }
