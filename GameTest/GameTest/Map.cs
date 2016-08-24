
using System.Drawing;

namespace GameTest
{
    public class Map
    {
        public static int current_map = 0;

        public string bitmap_path;
        public Bitmap bitmap;

        //遮挡层变量
        public string shade_path;
        public Bitmap shade;
        //障碍区域变量
        public string block_path;
        public Bitmap block;
        //背景层
        public string back_path;
        public Bitmap back;

        //音乐
        public string music;

        /// <summary>
        /// MAP构造
        /// </summary>
        public Map()
        {
            bitmap_path = "map1_b.png";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="g"></param>
        public static void draw(Map[] map,Player[]player,Npc[] npc, Graphics g,Rectangle stage)
        {
            Map m = map[current_map];
            //绘图位置
            int map_sx = 0;
            int p_x = Player.get_pos_x(player);
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

            int map_sy = 0;
            int p_y = Player.get_pos_y(player);
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


            if (m.back != null)
                g.DrawImage(m.back, 0, 0);
            g.DrawImage(m.bitmap, map_sx, map_sy);
            Player.draw(player, g, map_sx, map_sy);
            for (int i = 0; i < npc.Length;i++)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != current_map)
                    continue;
                npc[i].draw(g, map_sx, map_sy);
            }
                g.DrawImage(m.shade, map_sx, map_sy);
        }

        public static void chang_map(Map[] map,Player[] player,Npc[] npc,int newindex,int x,int y,int face,WMPLib.WindowsMediaPlayer music_player)
        {
            //音乐加载
            music_player.URL = map[current_map].music;

            //卸载旧地图
            if (map[current_map].back != null)
            {
                map[current_map].back = null;
            }

            if(map[current_map].bitmap!=null)
            {
                map[current_map].bitmap = null;
            }
            if (map[current_map].shade != null)
            {
                map[current_map].shade = null;
            }
            if (map[current_map].block != null)
            {
                map[current_map].block = null;
            }
            //加载新地图
            if (map[newindex].block_path != null && map[newindex].block_path != "")
            {
                map[newindex].block = new Bitmap(map[newindex].block_path);
                map[newindex].block.SetResolution(96, 96);
            }
            if (map[newindex].bitmap_path != null && map[newindex].bitmap_path != "")
            {
                map[newindex].bitmap = new Bitmap(map[newindex].bitmap_path);
                map[newindex].bitmap.SetResolution(96, 96);
            }
            if (map[newindex].shade_path != null && map[newindex].shade_path != "")
            {
                map[newindex].shade = new Bitmap(map[newindex].shade_path);
                map[newindex].shade.SetResolution(96, 96);
            }
            if (map[newindex].back_path != null && map[newindex].back_path != "")
            {
                map[newindex].back = new Bitmap(map[newindex].back_path);
                map[newindex].back.SetResolution(96, 96);
            }
            //NPC
            for (int i = 0; i < npc.Length; i++)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != current_map)
                    npc[i].unload();
                if (npc[i].map == newindex)
                    npc[i].load();
            }
            //curruntmap
            current_map = newindex;
            //位置设置
            Player.set_pos(player, x, y, face);

        }

        /// <summary>
        ///   can_through
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool can_through(Map[] map,int x,int y)
        {
            Map m = map[current_map];
            if (x < 0) return false;
            else if (x >= m.block.Width) return false;
            else if (y < 0) return false;
            else if (y >= m.block.Height) return false;

            if (m.block.GetPixel(x, y).B == 0)
                return false;
            else
                return true;
        }

    }
}
