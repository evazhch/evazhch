using System.Drawing;
using RPG_experiment;

public class Animation
{
    public static long RATE = 400;
    public string bitmap_path;
    public Bitmap bitmap;
    public int row = 1;
    public int col = 1;
    public int max_fram = 1;
    public int anm_rate;
   
    public void load()
    {
        if (bitmap_path != null && bitmap_path != "")
        {
            bitmap = new Bitmap(bitmap_path);
            bitmap.SetResolution(96, 96);
        }
    }
    public void unload()
    {
        if (bitmap != null)
        {
            bitmap = null;
        }
    }

    public Bitmap get_bitmap(int fram)
    {
        if(bitmap==null)
            return null;
        if(fram>max_fram)
            return null;
        //定义区域
        Rectangle rect=new Rectangle(
            bitmap.Width/row*(fram%row),
            bitmap.Height/col*(fram/row),
            bitmap.Width/row,
            bitmap.Height/col);
            //return
            return bitmap.Clone(rect,bitmap.PixelFormat);
            
    }
    public void draw(Graphics g,int fram,int x,int y)
    {
        Bitmap bitmap = get_bitmap(fram / anm_rate);
        if (bitmap == null)
            return ;
        g.DrawImage(bitmap, x, y);
    }
  
   


   
}
