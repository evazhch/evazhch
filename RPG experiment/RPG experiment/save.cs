using System.Drawing;
using System.Windows.Forms;
using RPG_experiment;
using System.IO;
using System.Text;
public class Save
{
    //面板
    private static Panel pan_save = new Panel();
    public static Panel pan_confirm = new Panel();
    private static int menu = 0;//0-保存1-读取
    public static int page = 1;
    public static int selnow = 1;
    public static Bitmap bitmap_sel;
    //--------------------------------------------------
    //  保存
    //--------------------------------------------------
    public static void save(int index)
    {
        FileStream fs = new FileStream(
                 "save" + index.ToString() + ".dat",FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        try
        {
            
            //时间
            bw.Write(System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString());
            //剧情变量
            for(int i=0;i<Task.p.Length;i++)
            {
                bw.Write(Task.p[i]);
            }
            //地图数据
            bw.Write(Map.current_map);
            //角色数据
            bw.Write(Player.current_player);
            bw.Write(Player.select_player);
            bw.Write(Player.money);
            for(int i=0;i<Form1.player.Length;i++)
            {
                if (Form1.player[i] == null)
                    continue;
                Player p = Form1.player[i];
                bw.Write(p.x);
                bw.Write(p.y);
               bw.Write(p.face);

                bw.Write(p.walk_interval);
                bw.Write(p.speed);
                bw.Write(p.x_offest);
                bw.Write(p.y_offest);
                bw.Write(p.max_hp);
                bw.Write(p.hp);
                bw.Write(p.max_mp);
                bw.Write(p.mp);
                bw.Write(p.attack);
                bw.Write(p.defense);
                bw.Write(p.fspeed);
                bw.Write(p.fortune);
                bw.Write(p.equip_att);
                bw.Write(p.equip_def);
                bw.Write(p.fx_offset);
                bw.Write(p.fy_offset);
                //bw.Write(p.name);
                bw.Write(p.is_action);
                bw.Write(p.collision_ray);
                
                for(int i2=0;i2<10;i2++)
                {
                    bw.Write(p.skill[i2]);
                }
            }
            //npc数据
            for(int i=0;i<Form1.npc.Length;i++)
            {
                if (Form1.npc[i] == null)
                    continue;
                Npc n = Form1.npc[i];
                bw.Write(n.map);
                bw.Write(n.x);
                bw.Write(n.y);
                bw.Write(n.x_offset);
                bw.Write(n.y_offset);
                
                bw.Write(n.visible);
                bw.Write(n.region_x);
                bw.Write(n.region_y);
                //bw.Write((int)n.face);
                bw.Write(n.walke_fram);
                bw.Write(n.walk_interval);
                bw.Write(n.idle_walk_time);
                bw.Write(n.idle_walk_time_now);
                bw.Write(n.mc_xoffset);
                bw.Write(n.mc_yoffset);
                bw.Write(n.mc_w);
                bw.Write(n.mc_h);
                bw.Write(n.bitmap_path);
            }
            //物品数据
            for(int i=0;i<Item.item.Length;i++)
            {
                if (Item.item[i] == null)
                    continue;
                Item item = Item.item[i];
                bw.Write(item.num);
            }
        bw.Flush();
        bw.Close();
        fs.Close();
        }
        catch
        {
            MessageBox.Show("保存文件失败");
        }
       
    }
    //--------------------------------------------------
    //  读取
    //--------------------------------------------------
    public static void load(int index)
    {
        int current_map;
        try
        {
            Define.define(Form1.player, Form1.npc, Form1.map);
            FileStream fs = new FileStream(
                "save" + index.ToString() + ".dat",FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            //时间
            string time = br.ReadString();
            //剧情变量
            for(int i=0;i<Task.p.Length;i++)
            {
                Task.p[i] =br.ReadInt32();
            }
            //地图数据
            current_map = br.ReadInt32();
            //角色数据
            Player.current_player = br.ReadInt32();
            Player.select_player = br.ReadInt32();
            Player.money = br.ReadInt32();

            for(int i=0;i<Form1.player.Length;i++)
            {
                if (Form1.player[i] == null)
                    continue;

                Form1.player[i].x = br.ReadInt32();
                Form1.player[i].y = br.ReadInt32();
                Form1.player[i].face = br.ReadInt32();
                Form1.player[i].walk_interval = br.ReadInt64();
                Form1.player[i].speed = br.ReadInt32();
                Form1.player[i].x_offest = br.ReadInt32();
                Form1.player[i].y_offest = br.ReadInt32();
                Form1.player[i].max_hp = br.ReadInt32();
                Form1.player[i].hp = br.ReadInt32();
                Form1.player[i].max_mp = br.ReadInt32();
                Form1.player[i].mp = br.ReadInt32();
                Form1.player[i].attack = br.ReadInt32();
                Form1.player[i].defense = br.ReadInt32();
                Form1.player[i].fspeed = br.ReadInt32();
                Form1.player[i].fortune = br.ReadInt32();
                Form1.player[i].equip_att = br.ReadInt32();
                Form1.player[i].equip_def = br.ReadInt32();
                Form1.player[i].fx_offset = br.ReadInt32();
                Form1.player[i].fy_offset = br.ReadInt32();
               //Form1.player[i].name = br.ReadString();
                Form1.player[i].is_action = br.ReadInt32();
                Form1.player[i].collision_ray = br.ReadInt32();

                for(int i2=0;i2<10;i2++)
                {
                    Form1.player[i].skill[i2] = br.ReadInt32();
                }
            }
            //npc数据
            for(int i=0;i<Form1.npc.Length;i++)
            {
                if (Form1.npc[i] == null)
                    continue;
                Form1.npc[i].map = br.ReadInt32();
                Form1.npc[i].x = br.ReadInt32();
                Form1.npc[i].y = br.ReadInt32();
                Form1.npc[i].x_offset = br.ReadInt32();
                Form1.npc[i].y_offset = br.ReadInt32();
                Form1.npc[i].visible = br.ReadBoolean();
                Form1.npc[i].region_x = br.ReadInt32();
                Form1.npc[i].region_y = br.ReadInt32();
                //Form1.npc[i].face = (Comm.Direction)br.ReadInt32();
                Form1.npc[i].walke_fram = br.ReadInt32();
                Form1.npc[i].walk_interval = br.ReadInt64();
                Form1.npc[i].idle_walk_time = br.ReadInt32();
                Form1.npc[i].idle_walk_time_now = br.ReadInt32();
                Form1.npc[i].mc_xoffset = br.ReadInt32();
                Form1.npc[i].mc_yoffset = br.ReadInt32();
                Form1.npc[i].mc_w = br.ReadInt32();
                Form1.npc[i].mc_h = br.ReadInt32();
                Form1.npc[i].bitmap_path = br.ReadString();
            }
            for (int i = 0; i < Form1.npc.Length; i++)
            {
               
                if (Form1.npc[i].bitmap_path != "")
                {
                    Form1.npc[i].bitmap =
                        new Bitmap(Form1.npc[i].bitmap_path);
                    Form1.npc[i].bitmap.SetResolution(96, 96);
                }
            }
            //物品数据
            for(int i=0;i<Item.item.Length;i++)
            {
                if (Item.item[i] == null)
                    continue;
                Item.item[i].num = br.ReadInt32();
            }
            br.Close();
            fs.Close();
        }
        catch
        {
            MessageBox.Show("读取文件失败");
            return;
        }
        int x = Form1.player[Player.current_player].x;
        int y = Form1.player[Player.current_player].y;
        int f = Form1.player[Player.current_player].face;
        Task.change_map(current_map, x, y, f);
        Save.pan_save.hide();
    }
    public static void init()
    {
        //bitmap_sel = new Bitmap(@"item/sbt7_2.png");
        //bitmap_sel.SetResolution(96, 96);

        Button yes = new Button();
        yes.set(372, 326, 0, 0, "item/sbt3_1.png", "item/sbt3_2.png", "item/sbt3_2.png", -1, -1, -1, -1);
        yes.click_event += new Button.Click_event(click_yes);

        Button cancel = new Button();
        cancel.set(523, 326, 0, 0, "item/sbt5_1.png", "item/sbt5_2.png", "item/sbt5_2.png", -1, -1, -1, -1);
        cancel.click_event += new Button.Click_event(click_cancel);

        Button previous_page = new Button();
        previous_page.set(37, 326, 0, 0, "item/sbt3_1.png", "item/sbt3_2.png", "item/sbt3_2.png", -1, -1, -1, -1);
        previous_page.click_event += new Button.Click_event(click_privious_page);

        Button next_page = new Button();
        next_page.set(52, 326, 0, 0, "item/sbt5_1.png", "item/sbt5_2.png", "item/sbt5_2.png", -1, -1, -1, -1);
        next_page.click_event += new Button.Click_event(click_next_page);


        Button save = new Button();
        save.set(44, 326, 0, 0, "item/sbt4_1.png", "item/sbt4_2.png", "item/sbt4_2.png", -1, -1, -1, -1);
        save.click_event += new Button.Click_event(click_save);
        
        Button read = new Button();
        read.set(44, 326, 0, 0, "item/sbt4_1.png", "item/sbt4_2.png", "item/sbt4_2.png", -1, -1, -1, -1);
        read.click_event += new Button.Click_event(click_read);
        
        Button close = new Button();
        close.set(627, 16, 0, 0, "item/sbt6_1.png", "item/sbt6_2.png", "item/sbt6_2.png", -1, -1, -1, -1);
        close.click_event += new Button.Click_event(click_close);

        Button sel1 = new Button();
        sel1.set(35, 38, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel1.click_event += new Button.Click_event(click_sel1);

        Button sel2 = new Button();
        sel2.set(35, 133, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel2.click_event += new Button.Click_event(click_sel2);

        Button sel3 = new Button();
        sel3.set(35, 228, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel3.click_event += new Button.Click_event(click_sel3);

        Button under = new Button();
        under.set(-100, -100, 2000, 2000, "", "", "", -1, -1, -1, -1);
        //under.click_event += new Button.Click_event(click_equip_att);

        pan_save.button = new Button[9];
        pan_confirm.button = new Button[2];
        pan_confirm.button[0] = yes;
        pan_confirm.button[1] = cancel;
        pan_confirm.set(58, 71, "item/status_bg2.png", 0, 1);
        pan_confirm.draw_event += new Panel.Draw_event(pan_confirm_drawbg);
        pan_confirm.init();
        pan_save.button[0] = previous_page;
        pan_save.button[1] = next_page;
        pan_save.button[2] = save;
        pan_save.button[3] = read;
        pan_save.button[4] = close;
        pan_save.button[5] = sel1;
        pan_save.button[6] = sel2;
        pan_save.button[7] = sel3;
        pan_save.button[8] = under;
        pan_save.set(58, 71, "item/status_bg2.png", 5, 8);
        pan_save.draw_event += new Panel.Draw_event(pan_save_draw);
        pan_save.init();
    }
    private static string[] info;
    public static void show(int menu)
    //menu 0-保存 1-读取
    {
        page=1;
        Save.menu=menu;
        pan_save.show();

        if(menu==0)
        {
            pan_save.button[2].x=134;
            pan_save.button[2].y=341;
            pan_save.button[3].x=-9000;
            pan_save.button[3].y=-9000;
        }
        else
        {
            pan_save.button[3].x = 134;
            pan_save.button[3].y = 341;
            pan_save.button[2].x = -9000;
            pan_save.button[2].y = -9000;
        }
        info = get_save_info((page - 1) * 3);
    }
    public static void click_privious_page()
    {
        page--;
        if (page < 1) page = 1;
        info = get_save_info((page - 1) * 3);
    }
    public static void click_next_page()
    {
        page++;
        info = get_save_info((page - 1) * 3);
    }
    public static void click_close()
    {
        if (menu == 0)
            pan_save.hide();
        else
            Title.show();
    }
    public static void click_sel1()
    {
        selnow = 1;
    }
    public static void click_sel2()
    {
        selnow = 2;
    }

    public static void click_sel3()
    {
        selnow = 3;
    }
    public static void click_save()
    {
        int index = (page - 1) * 3 + selnow - 1;
        if (!File.Exists("save" + index.ToString() + ".dat"))
            save(index);
        else
            pan_confirm.show();

        info = get_save_info((page - 1) * 3);
    }
    public static void click_read()
    {
        int index = (page - 1) * 3 + selnow - 1;
        if (!File.Exists("save" + index.ToString() + ".dat"))
            return;
        load(index);
    }
    public static void click_yes()
    {
        int index = (page - 1) * 3 + selnow - 1;
        save(index);
        pan_save.hide();
    }
    public static void click_cancel()
    {
        Save.show(0);
    }
    //获取保存文件信息
    public static string[] get_save_info(int start)
    {
        string[] ret = new string[] { "", "", "" };
        for (int i=0;i<3;i++)
        {
            if (!File.Exists("save" + (start + i).ToString() + ".dat"))
                continue;
            try
            {
                FileStream fs = new FileStream(
                    "save" + (start + i).ToString() + ".dat", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                //时间
                ret[i] = br.ReadString();
                br.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("读取文件错误");
            }  
        } return ret;
    }
    public static void pan_save_drawbg(Graphics g,int x_offset,int y_offset)
    {
        if (menu == 1)
            Title.title.draw_me(g);
    }
    public static void pan_save_draw(Graphics g,int x_offset,int y_offset)
    {
        //标签
        Font font = new Font("黑体", 22);
        Brush brush = Brushes.Gray;
        if (menu == 0)
            g.DrawString("存储", font, brush, x_offset + 138, y_offset + 5, new StringFormat());
        else
            g.DrawString("读取", font, brush, x_offset + 138, y_offset + 5, new StringFormat());
        //显示信息
        drawinfo(g, x_offset, y_offset);
        //显示选择框
        g.DrawImage(Statusmenu.bitmap_sel, x_offset + 37, y_offset + 55 + (selnow - 1) * 95);
    }
    public static void drawinfo(Graphics g,int x_offset,int y_offset)
    {
        Font font_n = new Font("黑体", 12);
        Brush brush_n = Brushes.GreenYellow;
        Font font_d = new Font("黑体", 10);
        Brush brush_d = Brushes.LawnGreen;
        for(int i=0;i<3;i++)
        {
            string str = "存档" + ((page - 1) * 3 + i).ToString();
            g.DrawString(str, font_n, brush_n, x_offset + 80, y_offset + 74 + i * 96, new StringFormat());
            g.DrawString(info[i], font_d, brush_d, x_offset + 80, y_offset + 101 + i * 96, new StringFormat());
        }
    }
    public static void pan_confirm_drawbg(Graphics g,int x_offset,int y_offset)
    {
        Save.pan_save.draw_me(g);
    }
}