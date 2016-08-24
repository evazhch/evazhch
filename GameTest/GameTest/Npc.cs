using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameTest
{
    public class Npc
    {
        public int map = -1;
        //位置
        public int x = 0;
        public int y = 0;
        public int face = 0;
        public int x_offset = -20;
        public int y_offset = -30;
        //显示
        public string bitmap_path="";
        public Bitmap bitmap;
        bool visible = true;

        //加载
        public void load()
        {
            if(bitmap_path!="")
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }
        }
        //卸载
        public void unload()
        {
            if(bitmap!=null)
            {
                bitmap = null;
            }
        }
        public void draw(Graphics g,int map_sx,int map_sy)
        {
            if (visible != true)
                return;
            if (bitmap != null)
                g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
        }
    }
}
