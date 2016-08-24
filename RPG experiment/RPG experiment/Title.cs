using System.Windows.Forms;
using System.Drawing;
using RPG_experiment;

public static class Title
{
    public static Panel title = new Panel();
    public static Panel confirm = new Panel();
    public static string title_music = "2.mp3";
    public static Bitmap bg_1 = new Bitmap("T_bg1.png");
    public static Bitmap bg_2 = new Bitmap("T_bg2.png");
    public static Bitmap bg_3 = new Bitmap("T_bg3.png");
    public static Bitmap bg_font = new Bitmap("T_logo.png");
    public static long last_change_bg_time = 0;
    public static int bg_now=0;
    

    public static void init()
    {
        //主界面
        Button btn_new = new Button();
        btn_new.set(325, 350, 120, 50, "T_start_2.png", "T_start_1.png","T_start_1.png", 2, 1, -1, -1);
        btn_new.click_event += new Button.Click_event(newgame);

        Button btn_load = new Button();
        btn_load.set(325, 400, 120, 50, "T_load_1.png", "T_load_2.png", "T_load_2.png", 0, 2, -1, -1);
        btn_load.click_event += new Button.Click_event(loadgame);

        Button btn_exit = new Button();
        btn_exit.set(325, 450, 120, 50, "T_exit_1.png", "T_exit_2.png", "T_exit_2.png", 1, 0, -1, -1);
        btn_exit.click_event += new Button.Click_event(exitgame);

        Button btn_yes = new Button();
        btn_yes.set(140, 60, 120, 50, "confirm_yes_1.png", "confirm_yes_2.png", "confirm_yes_2.png", 1, 1, -1, -1);
        btn_yes.click_event += new Button.Click_event(comfirm_yes);

        Button btn_no = new Button();
        btn_no.set(140, 120, 120, 50, "confirm_no_1.png", "confirm_no_2.png", "confirm_no_2.png", 0, 0, -1, -1);
        btn_no.click_event += new Button.Click_event(comfirm_no);


        bg_1.SetResolution(96, 96);
        bg_2.SetResolution(96, 96);
        bg_3.SetResolution(96, 96);
        bg_font.SetResolution(96, 96);
        title.draw_event += new Panel.Draw_event(drawtitle);
        

        title.button = new Button[3];
        title.button[0] = btn_new;
        title.button[1] = btn_load;
        title.button[2] = btn_exit;
        title.set(0, 0, "T_bg1.png", 0, -1);
        title.init();

        
        confirm.button = new Button[2];
        confirm.button[0] = btn_yes;
        confirm.button[1] = btn_no;
        confirm.set(200, 300, "confirm_bg.png", 0, 1);
        confirm.drawbg_event += new Panel.Drawbg_event(drawconfirm);
        confirm.init();
    }
    public static void newgame()
    {
        Define.define(Form1.player, Form1.npc, Form1.map);
        Map.change_map(Form1.map, Form1.player, Form1.npc, 0, 80, 400,1, Form1.music_player);
        title.hide();
    }
    public static void loadgame()
    {
       // MessageBox.Show("loadgame");
       //Shop.show(new int[]{0,1,2,3,-1,-1,-1});
        Save.show(1);
    }
    public static void exitgame()
    {
        
        confirm.show();
        
    }
    public static void comfirm_yes()
    {
        Application.Exit();
    }
    public static void comfirm_no()
    {
        title.show();
    }
    public static void show()
    {
        Form1.music_player.URL = title_music;
        title.show();
    }
    public static void drawtitle(Graphics g,int x_offset,int y_offset)
    {
        //绘制背景
        if (bg_now == 0)
            g.DrawImage(bg_1, 0, 0);
        else    if (bg_now == 1)
            g.DrawImage(bg_2, 0, 0);
        else    if (bg_now == 2)
            g.DrawImage(bg_3, 0, 0);
        //绘制logo
        g.DrawImage(bg_font, 260, 80);
        //背景处理
        if(Comm.Time()-last_change_bg_time>5000)
        {
            bg_now = bg_now + 1;
            if (bg_now > 2) bg_now = 0;
            last_change_bg_time = Comm.Time();
        }
    }
    public static void drawconfirm(Graphics g,int x_offset,int y_offset)
    {
        title.draw_me(g);
        
    }
}