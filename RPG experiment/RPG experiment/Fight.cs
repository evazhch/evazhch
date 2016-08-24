using System.Drawing;
using System.Windows.Forms;
using RPG_experiment;

public class Fight
{
    private struct Fenemy
    {
        public int id;
        public int hp;
        public int order;
        public int status;
    };
    private static Fenemy[] enemy = new Fenemy[3];

    private struct Fplayer
    {
        public int id;
        public int hp;
        public int mp;
        public int order;
        public int status;
    };
    private static Fplayer[] player = new Fplayer[3];
    private static Bitmap bg;
    private static int isgameover = 1;
    private static int winitem1 = -1;
    private static int winitem2 = -1;
    private static int winitem3 = -1;
    private static int lostmoney = 0;

    public static int iswin = 0;
    public static Player.Status player_last_status = Player.Status.WALK;
    public static int fighting = 0;
    //战斗位置设置
    public static Point[] player_pos = new Point[3];
    public static Point[] enemy_pos = new Point[3];
    //ctrl面板
    private static Panel ctrl = new Panel();
    private static Bitmap fm_s1;
    private static Bitmap fm_s2;
    private static Panel select = new Panel();
    //显示血量
    private static long blood_time = 0;
    private static int blood_value = 0;
    private static int draw_blood_x = 0;
    private static int draw_blood_y = 0;
    //胜负面板
    private static Panel pan_win = new Panel();
    private static Panel pan_lose = new Panel();
    private static Panel pan_gameover = new Panel();
    //物品和技能面板
    private static Panel pan_item = new Panel();
    private static int menu = 0;//0-物品1-技能
    public static int page = 1;
    public static int selnow = 1;
    public static Bitmap bitmap_sel;
    //init
    public static void init()
    {
        //ctrl
        fm_s1 = new Bitmap(@"fight/fm_s1.png");
        fm_s1.SetResolution(96, 96);
        fm_s2 = new Bitmap(@"fight/fm_s2.png");
        fm_s2.SetResolution(96, 96);

        Button btn_att = new Button();
        btn_att.set(142, 0, 0, 0, "fight/fm_b1_1.png", "fight/fm_b1_2.png", "fight/fm_b1_2.png", -1, -1, -1, -1);
        btn_att.click_event += new Button.Click_event(btn_att_event);

        Button btn_item = new Button();
        btn_item.set(192, 0, 0, 0, "fight/fm_b2_1.png", "fight/fm_b2_2.png", "fight/fm_b2_2.png", -1, -1, -1, -1);
        btn_item.click_event += new Button.Click_event(btn_item_event);

        Button btn_skill = new Button();
        btn_skill.set(242, 0, 0, 0, "fight/fm_b1_1.png", "fight/fm_b3_2.png", "fight/fm_b3_2.png", -1, -1, -1, -1);
        btn_skill.click_event += new Button.Click_event(btn_skill_event);

        ctrl.button = new Button[3];
        ctrl.button[0] = btn_att;
        ctrl.button[1] = btn_item;
        ctrl.button[2] = btn_skill;
        ctrl.set(200, 285, "", 0, -1);
        ctrl.draw_event += new Panel.Draw_event(ctrl_draw);
        ctrl.init();

        //select面板
        set_pos();
        Button btn_p_1 = new Button();
        btn_p_1.set(player_pos[0].X - 95, player_pos[0].Y - 120, 0, 0, "fight/fm_b4_1.png", "fight/fm_b4_2.png", "fight/fm_b4_2.png", -1, 1, -1, -1);
        btn_p_1.click_event += new Button.Click_event(btn_p_1_event);

        Button btn_p_2 = new Button();
        btn_p_2.set(player_pos[1].X - 95, player_pos[1].Y - 120, 0, 0, "fight/fm_b4_1.png", "fight/fm_b4_2.png", "fight/fm_b4_2.png", -1, 2, -1, -1);
        btn_p_2.click_event += new Button.Click_event(btn_p_2_event);

        Button btn_p_3 = new Button();
        btn_p_3.set(player_pos[2].X - 95, player_pos[2].Y - 120, 0, 0, "fight/fm_b4_1.png", "fight/fm_b4_2.png", "fight/fm_b4_2.png", -1, 3, -1, -1);
        btn_p_3.click_event += new Button.Click_event(btn_p_3_event);

        Button btn_e_1 = new Button();
        btn_e_1.set(enemy_pos[0].X - 95, enemy_pos[0].Y - 120, 0, 0, "fight/fm_b4_1.png", "fight/fm_b4_2.png", "fight/fm_b4_2.png", -1, 4, -1, -1);
        btn_e_1.click_event += new Button.Click_event(btn_e_1_event);

        Button btn_e_2 = new Button();
        btn_e_2.set(enemy_pos[1].X - 95, enemy_pos[1].Y - 120, 0, 0, "fight/fm_b4_1.png", "fight/fm_b4_2.png", "fight/fm_b4_2.png", -1, 5, -1, -1);
        btn_e_2.click_event += new Button.Click_event(btn_e_2_event);

        Button btn_e_3 = new Button();
        btn_e_3.set(enemy_pos[2].X - 95, enemy_pos[2].Y - 120, 0, 0, "fight/fm_b4_1.png", "fight/fm_b4_2.png", "fight/fm_b4_2.png", -1, 0, -1, -1);
        btn_e_3.click_event += new Button.Click_event(btn_e_3_event);

        select.button = new Button[6];
        select.button[0] = btn_p_1;
        select.button[1] = btn_p_2;
        select.button[2] = btn_p_3;
        select.button[3] = btn_e_1;
        select.button[4] = btn_e_2;
        select.button[5] = btn_e_3;
        select.set(0, 0, "", 0, -1);
        select.draw_event += new Panel.Draw_event(select_draw);
        select.init();

        //胜负面板
        Button wlbtn_close = new Button();
        wlbtn_close.set(-1000,-1000,2000,2000,"","","",-1,-1,-1,-1);
        wlbtn_close.click_event+=new Button.Click_event(wlbtn_close_event);

        Button wlbtn_gameover = new Button();
        wlbtn_gameover.set(-1000, -1000, 2000, 2000, "", "", "", -1, -1, -1, -1);
        wlbtn_gameover.click_event += new Button.Click_event(wlbtn_gameover_event);

        pan_win.button = new Button[1];
        pan_win.button[0] = wlbtn_close;
        pan_win.set(226, 162, "fight/win.png", 0, -1);
        pan_win.draw_event += new Panel.Draw_event(pan_win_draw);
        pan_win.init();

        pan_lose.button = new Button[1];
        pan_lose.button[0] = wlbtn_close;
        pan_lose.set(226, 162, "fight/lose1.png", 0, -1);
        pan_lose.draw_event += new Panel.Draw_event(pan_lose_draw);
        pan_lose.init();

        pan_gameover.button = new Button[1];
        pan_gameover.button[0] = wlbtn_gameover;
        pan_gameover.set(226, 162, "fight/lose1.png", 0, -1);
        //pan_gameover.draw_event += new Panel.Draw_event(pan_win_draw);
        pan_gameover.init();

        //物品和技能面板
        bitmap_sel = new Bitmap(@"item/sbt7_2.png");
        bitmap_sel.SetResolution(96, 96);

        Button previous_page = new Button();
        previous_page.set(59, 341, 0, 0, "item/sbt3_1.png", "item/sbt3_2.png", "item/sbt3_2.png", -1, -1, -1, -1);
        previous_page.click_event += new Button.Click_event(click_previous_page);

        Button next_page = new Button();
        next_page.set(210, 341, 0, 0, "item/sbt5_1.png", "item/sbt5_2.png", "item/sbt5_2.png", -1, -1, -1, -1);
        next_page.click_event += new Button.Click_event(click_next_page);

        Button use = new Button();
        use.set(134, 341, 0, 0, "item/sbt4_1.png", "item/sbt4_2.png", "item/sbt4_2.png", -1, -1, -1, -1);
        use.click_event += new Button.Click_event(click_use);

        Button close = new Button();
        close.set(307, 29, 0, 0, "item/sbt6_1.png", "item/sbt6_2.png", "item/sbt6_2.png", -1, -1, -1, -1);
        close.click_event += new Button.Click_event(click_close);

        Button sel1 = new Button();
        sel1.set(37, 55, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel1.click_event += new Button.Click_event(click_sel1);

        Button sel2 = new Button();
        sel2.set(37, 150, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel2.click_event += new Button.Click_event(click_sel2);

        Button sel3 = new Button();
        sel3.set(37, 245, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel3.click_event += new Button.Click_event(click_sel3);

        Button under = new Button();
        under.set(-1000, -1000, 1000, 1000, "","", "", -1, -1, -1, -1);

        pan_item.button = new Button[8];
        pan_item.button[0] = previous_page;
        pan_item.button[1] = next_page;
        pan_item.button[2] = use;
        pan_item.button[3] = close;
        pan_item.button[4] = sel1;               
        pan_item.button[5] = sel2;
        pan_item.button[6] = sel3;
        pan_item.button[7] = under;
        pan_item.draw_event += new Panel.Draw_event(pan_item_draw);
        pan_item.init();
    }
    public static void set_pos()//位置设置
    {
        player_pos[0].X = 597;
        player_pos[0].Y = 158;
        player_pos[1].X = 595;
        player_pos[1].Y = 261;
        player_pos[2].X = 588;
        player_pos[2].Y = 417;

        enemy_pos[0].X = 153;
        enemy_pos[0].Y = 158;
        enemy_pos[1].X = 179;
        enemy_pos[1].Y = 261;
        enemy_pos[2].X = 120;
        enemy_pos[2].Y = 417;
    }
    //动画控制
    private static Animation anm;
    private static int anm_frame = -1;
    private static long last_anm_time = 0;
    //开始战斗
    public static void start(int[] enemy, string bg_path, int isgameover, int winitem1, int winitem2, int winitem3, int losemoney)
    {
        Form1.music_player.URL = "fight/fight.mp3";
        set_pos();
        //敌人
        if (enemy.Length < 3)
        {
            MessageBox.Show("enemy数组长度小与3！");
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            if (enemy[i] >= Enemy.enemy.Length)
            {
                MessageBox.Show("enemy值大于id");
                return;
            }
            if (enemy[i] != -1 && Enemy.enemy[enemy[i]] == null)
            {
                MessageBox.Show("敌人id" + enemy[i].ToString() + "未定义");
                return;
            }
            Fight.enemy[i].id = enemy[i];
            if (Fight.enemy[i].id != -1)
            {
                Fight.enemy[i].hp = Enemy.enemy[enemy[i]].maxhp;
                Fight.enemy[i].order = -1 * Enemy.enemy[enemy[i]].fspeed;
                Fight.enemy[i].status = 0;
            }
            else
            {
                Fight.enemy[i].hp = 0;
                Fight.enemy[i].order = -1;
                Fight.enemy[i].status = 0;
            }
        }
        //背景图
        if (bg_path != null && bg_path != "")
        {
            Fight.bg = new Bitmap(bg_path);
            Fight.bg.SetResolution(96, 96);
        }
        //胜负
        Fight.isgameover = isgameover;
        Fight.winitem1 = winitem1;
        Fight.winitem2 = winitem2;
        Fight.winitem3 = winitem3;
        Fight.lostmoney = losemoney;
        //玩家
        int[] player = get_fplayer();
        for (int i = 0; i < 3; i++)
        {
            Fight.player[i].id = player[i];
            if (Fight.player[i].id != -1)
            {
                Fight.player[i].hp = Form1.player[player[i]].hp;
                Fight.player[i].mp = Form1.player[player[i]].hp;
                Fight.player[i].order = -1 * get_property(PLAYER_PTY.SPD, player[i]);
                Fight.player[i].status = 0;
            }
            else
            {
                Fight.player[i].hp = 0;
                Fight.player[i].mp = 0;
                Fight.player[i].order = -1;
                Fight.player[i].status = 0;
            }
        }
        //开始战斗
        if (Player.status != Player.Status.FIGHT)
            player_last_status = Player.Status.FIGHT;
        fighting = 1;

        fight_logic();
    }
    //获取战斗玩家列表
    public static int[] get_fplayer()
    {
        int[] ret = new int[] { -1, -1, -1 };
        int start = Player.current_player;
        int start2 = 0;
        int end = Player.current_player;
        for (int i = 0; i < 3; i++)
        {
            //前遍历
            int j = 0;
            for (j = start; j < Form1.player.Length; j++)
                if (Form1.player[j].is_action == 1)
                {
                    ret[i] = j;
                    start = j + 1;
                    break;
                }
            if (j < Form1.player.Length)
                continue;
            //后遍历
            for (j = start2; j < end; j++)
                if (Form1.player[j].is_action == 1)
                {
                    ret[i] = j;
                    start2 = j + 1;
                    break;
                }
        }
        return ret;
    }
    //获取属性
    public enum PLAYER_PTY
    {
        ATT = 1,
        DEF = 2,
        SPD = 3,
        FTE = 4,
    }
    //获取属性
    public static int get_property(PLAYER_PTY pty, int index)
    {
        if (player[index].id < 0)
            return 0;
        if (player[index].id >= Form1.player.Length)
            return 0;
        if (Form1.player[player[index].id] == null)
            return 0;

        Player p = Form1.player[player[index].id];

        int value = 0;
        if (pty == PLAYER_PTY.ATT)
            value = p.attack;
        else if (pty == PLAYER_PTY.DEF)
            value = p.defense;
        else if (pty == PLAYER_PTY.SPD)
            value = p.speed;
        else if (pty == PLAYER_PTY.FTE)
            value = p.fortune;

        if (p.equip_att >= 0)
        {
            if (pty == PLAYER_PTY.ATT)
                value += Item.item[p.equip_att].value2;
            else if (pty == PLAYER_PTY.DEF)
                value += Item.item[p.equip_att].value3;
            else if (pty == PLAYER_PTY.SPD)
                value += Item.item[p.equip_att].value4;
            else if (pty == PLAYER_PTY.FTE)
                value += Item.item[p.equip_att].value5;
        }
        if (p.equip_def >= 0)
        {
            if (pty == PLAYER_PTY.ATT)
                value += Item.item[p.equip_def].value2;
            else if (pty == PLAYER_PTY.DEF)
                value += Item.item[p.equip_def].value3;
            else if (pty == PLAYER_PTY.SPD)
                value += Item.item[p.equip_def].value4;
            else if (pty == PLAYER_PTY.FTE)
                value += Item.item[p.equip_def].value5;
        }
        return value;
    }
    //绘图
    public static void draw(Graphics g)
    {
        ////背景
        if (bg != null)
            g.DrawImage(bg, 0, 0);
        ////绘制角色
        for (int i = 0; i < 3; i++)
        {
            int index = player[i].id;
            if (index < 0)
                continue;
            if (Form1.player[index].fbitmap != null)
            {
                Bitmap bitmap = get_fbitmap(Form1.player[index].fbitmap, player[i].status);
                g.DrawImage(bitmap, player_pos[i].X + Form1.player[index].fx_offset, player_pos[i].Y + Form1.player[index].fy_offset);
            }
        }
        //绘制敌人
        for (int i = 0; i < 3; i++)
        {
            int index = enemy[i].id;
            if (index < 0)
                continue;
            if (Enemy.enemy[index].fbitmap != null)
            {
                Bitmap bitmap = get_fbitmap(Enemy.enemy[index].fbitmap, enemy[i].status);
                g.DrawImage(bitmap, enemy_pos[i].X + Enemy.enemy[index].fx_offset, enemy_pos[i].Y + Enemy.enemy[index].fy_offset);
            }
        }
        ////绘制动画
        draw_anm(g);
        ////绘制血量
        draw_blood(g);
    }
    //根据状态获取图片
    private static Bitmap get_fbitmap(Bitmap bitmap, int status)
    {
        if (bitmap == null)
            return null;
        Rectangle rect = new Rectangle(bitmap.Width / 4 * status, 0, bitmap.Width / 4, bitmap.Height);//矩形区域
        return bitmap.Clone(rect, bitmap.PixelFormat);//复制小图
    }
    //战斗流程
    struct Turn
    {
        public int type;//player-1,,enemy=2;
        public int index;
        public int speed;
        public int att_type;//1-攻击2-物品3-技能
        public int att_index;//物品或技能index

        public int target_type;
        public int target_index;

        //0-无 1-发动者动画 2-接受者动画
        public int anm_status;
    }
    private static Turn turn = new Turn();
    private static Turn get_next()
    {
        turn.speed = int.MinValue;
        //查找
        for (int i = 0; i < 3; i++)
        {
            if (player[i].id >= 0 && player[i].order > turn.speed)
            {
                turn.type = 1;
                turn.index = i;
                turn.speed = player[i].order;
            }
            if (enemy[i].id >= 0 && enemy[i].order > turn.speed)
            {
                turn.type = 2;
                turn.index = i;
                turn.speed = enemy[i].order;
            }
        }
        //下一轮
        if (turn.type == 1)
            player[turn.index].order -= get_property(PLAYER_PTY.SPD, turn.index);
        else
            enemy[turn.index].order -= Enemy.enemy[enemy[turn.index].id].fspeed;
        return turn;
    }
    public static void fight_logic()
    {
        Fight.turn = get_next();
        //恢复状态
        for (int i = 0; i < 3; i++)
        {
            if (player[i].status == 1 || player[i].status == 2)
                player[i].status = 0;
            if (enemy[i].status == 1 || enemy[i].status == 2)
                enemy[i].status = 0;
        }
        if (Fight.turn.type == 1)
        {
            ctrl.show();
        }
        else if (Fight.turn.type == 2)
        {
            //选择攻击对象

            int index = 0;
            System.Random random = new System.Random();
            do
            {
                index = random.Next(0, 3);
            } while (player[index].hp <= 0 || player[index].id < 0);
            turn.target_type = 1;
            turn.target_index = index;
            //选择攻击方法
            index = random.Next(0, Enemy.enemy[enemy[turn.index].id].fightlist.Length);
            if (Enemy.enemy[enemy[turn.index].id].fightlist[index] < 0)
            {
                turn.att_type = 1;
                turn.att_index = 0;
            }
            else
            {
                turn.att_type = 3;
                int id = enemy[turn.index].id;
                turn.att_index = Enemy.enemy[id].fightlist[index];
            }
            turn.anm_status = 0;
            fight_do();
        }
       
    }
     public static void btn_att_event()
    {
        ctrl.hide();
        turn.att_type=1;
        select.show();
        //fight_logic();
    }
    public static void btn_item_event()
    {
        menu_show(0);
    }
    public static void btn_skill_event()
    {
        menu_show(1);
    }
    public static void ctrl_draw(Graphics g, int x_offset, int y_offset)
    {
        if (Fight.turn.type != 1)
            return;
        Player p = Form1.player[player[turn.index].id];
        //画脸
        g.DrawImage(p.fface, x_offset, y_offset);
        //画名字
        Font name_font = new Font("黑体", 16);
        Brush name_brush = Brushes.Black;
        g.DrawString(p.name, name_font, name_brush, x_offset + 170, y_offset + 115, new StringFormat());
        //生命条 （可改变生命值查看效果 player[turn.index].hp=20;）
        Rectangle rect = new Rectangle(0, 0, (int)((100 + 0.0) / p.max_hp * (player[turn.index].hp)), fm_s1.Height);
        if (rect.Width > 0)
            g.DrawImage(fm_s1.Clone(rect, fm_s1.PixelFormat), x_offset + 141, y_offset + 145);
        Font s1_font = new Font("黑体", 9);
        Brush s1_brush = Brushes.BurlyWood;
        string str = player[turn.index].hp.ToString() + "/" + p.max_hp.ToString();
        g.DrawString(str, s1_font, s1_brush, x_offset + 180, y_offset + 146, new StringFormat());
        //体力条
        rect = new Rectangle(0, 0, (int)((100 + 0.0) / p.max_mp * (player[turn.index].mp)), fm_s2.Height);
        if (rect.Width > 0)
            g.DrawImage(fm_s2.Clone(rect, fm_s2.PixelFormat), x_offset + 141, y_offset + 171);
        str = player[turn.index].mp.ToString() + "/" + p.max_mp.ToString();
        g.DrawString(str, s1_font, s1_brush, x_offset + 180, y_offset + 173, new StringFormat());
    }
    public static void btn_p_1_event()
     {
        if (player[0].id < 0)
            return;
        if (player[0].hp <= 0)
            return;
        turn.target_type = 1;
        turn.target_index = 0;
        turn.anm_status = 0;
        select.hide();
        fight_do();
    }
    public static void btn_p_2_event()
    {
        if (player[1].id < 0)
            return;
        if (player[1].hp <= 0)
            return;
        turn.target_type = 1;
        turn.target_index = 1;
        turn.anm_status = 0;
        select.hide();
        fight_do();
    }
    public static void btn_p_3_event()
    {
        if (player[2].id < 0)
            return;
        if (player[2].hp <= 0)
            return;
        turn.target_type = 1;
        turn.target_index = 2;
        turn.anm_status = 0;
        select.hide();
        fight_do();
    }
    public static void btn_e_1_event()
    {
        if (enemy[0].id < 0)
            return;
        if (enemy[0].hp <= 0)
            return;
        turn.target_type = 2;
        turn.target_index = 0;
        turn.anm_status = 0;
        select.hide();
        fight_do();
    }
    public static void btn_e_2_event()
    {
        if (enemy[1].id < 0)
            return;
        if (enemy[1].hp <= 0)
            return;
        turn.target_type = 2;
        turn.target_index = 1;
        turn.anm_status = 0;
        select.hide();
        fight_do();
    }
    public static void btn_e_3_event()
    {
        if (enemy[2].id < 0)
            return;
        if (enemy[2].hp <= 0)
            return;
        turn.target_type = 2;
        turn.target_index = 2;
        turn.anm_status = 0;
        select.hide();
        fight_do();
    }
    //动作
    private static void fight_do()
    {
        if (turn.anm_status == 0)
        {
            //status
            if (turn.type == 1)
            {
                player[turn.index].status = 1;
                if (turn.target_type == 2)
                    enemy[turn.target_index].status = 2;

            }
            else
            {
                enemy[turn.index].status = 1;
                player[turn.target_index].status = 2;
            }

            //anm
            if (turn.type == 1 && turn.att_type == 1)
            {
                turn.anm_status = 1;
                fight_do();
                return;
            }
            else if (turn.type == 2 && turn.att_type == 1)
            {
                turn.anm_status = 1;
                fight_do();
                return;
            }
            else if (turn.type == 1 && turn.att_type == 2)
            {
                anm = Form1.player[player[turn.index].id].anm_item;
            }
            else if (turn.type == 1 && turn.att_type == 3)
            {
                anm = Form1.player[player[turn.index].id].anm_skill;
            }
            else if (turn.type == 2 && turn.att_type == 3)
                anm = Enemy.enemy[enemy[turn.index].id].anm_skill;
            anm_frame = 0;
            turn.anm_status = 1;
            //fight_do();
            //fight_logic();
            return;
        }
        else if (turn.anm_status == 1)
        {
            if (turn.type == 1 && turn.att_type == 1)
                anm = Form1.player[player[turn.index].id].anm_att;
            else if (turn.type == 2 && turn.att_type == 1)
                anm = Enemy.enemy[enemy[turn.index].id].anm_att;
            else if (turn.att_type == 2)
                anm = Item.item[turn.att_index].fanm;
            else if(turn.att_type==3)
            {
                anm = Skill.skill[turn.att_index].fanm;
            }
            anm_frame = 0;
            turn.anm_status = 2;
            //fight_do();
            return;
        }
        else if (turn.anm_status == 2)
        {
            //扣血处理
            reduce_blood();
            //胜负判断
            tell_win();
          // fight_logic();
        }
        
    }
    public static void select_draw(Graphics g, int x_offset, int y_offset)
    {
        int index = select.current_button;
        x_offset = ctrl.x;
        y_offset = ctrl.y;
        if (select.current_button < 0)
            return;
        if (select.current_button <= 2)
        {
            if (player[index].id < 0)
                return;
            Player p = Form1.player[player[index].id];
            //画底图
            if (ctrl.bitmap != null)
                g.DrawImage(ctrl.bitmap, x_offset, y_offset);
            //话脸
            g.DrawImage(p.fface, x_offset, y_offset);
            //画名字
            Font name_font = new Font("黑体", 16);
            Brush name_brush = Brushes.Black;
            g.DrawString(p.name, name_font, name_brush, x_offset + 170, y_offset + 115, new StringFormat());
            //生命条 （可改变生命值查看效果 player[turn.index].hp=20;）
            Rectangle rect = new Rectangle(0, 0, (int)((100 + 0.0) / p.max_hp * (player[turn.index].hp)), fm_s1.Height);
            if (rect.Width > 0)
                g.DrawImage(fm_s1.Clone(rect, fm_s1.PixelFormat), x_offset + 141, y_offset + 145);
            Font s1_font = new Font("黑体", 9);
            Brush s1_brush = Brushes.BurlyWood;
            string str = player[turn.index].hp.ToString() + "/" + p.max_hp.ToString();
            g.DrawString(str, s1_font, s1_brush, x_offset + 180, y_offset + 146, new StringFormat());
            //体力条
            rect = new Rectangle(0, 0, (int)((100 + 0.0) / p.max_mp * (player[turn.index].mp)), fm_s2.Height);
            if (rect.Width > 0)
                g.DrawImage(fm_s2.Clone(rect, fm_s2.PixelFormat), x_offset + 141, y_offset + 171);
            str = player[turn.index].mp.ToString() + "/" + p.max_mp.ToString();
            g.DrawString(str, s1_font, s1_brush, x_offset + 180, y_offset + 173, new StringFormat());
        }
        if (select.current_button >= 3)
        {
            if (enemy[index - 3].id < 0)
                return;
            //画底图
            if (ctrl.bitmap != null)
                g.DrawImage(ctrl.bitmap, x_offset, y_offset);
            ////话脸
            //g.DrawImage(p.fface, x_offset, y_offset);
            //画名字
            Font name_font = new Font("黑体", 16);
            Brush name_brush = Brushes.Black;
            string str = Enemy.enemy[enemy[index - 3].id].name;
            g.DrawString(str, name_font, name_brush, x_offset + 170, y_offset + 115, new StringFormat());
            //生命条 （可改变生命值查看效果 player[turn.index].hp=20;）
            Rectangle rect = new Rectangle(0, 0, (int)((100 + 0.0) / Enemy.enemy[enemy[index - 3].id].maxhp * (enemy[index - 3].hp)), fm_s1.Height);
            if (rect.Width > 0)
                g.DrawImage(fm_s1.Clone(rect, fm_s1.PixelFormat), x_offset + 141, y_offset + 145);
            Font s1_font = new Font("黑体", 9);
            Brush s1_brush = Brushes.BurlyWood;
            str = enemy[index - 3].hp.ToString() + "/" + Enemy.enemy[enemy[index - 3].id].maxhp.ToString();
            g.DrawString(str, s1_font, s1_brush, x_offset + 180, y_offset + 146, new StringFormat());
            //体力条
            //rect = new Rectangle(0, 0, (int)((100 + 0.0) / p.max_mp * (player[turn.index].mp)), fm_s2.Height);
            if (rect.Width > 0)
                g.DrawImage(fm_s2, x_offset + 141, y_offset + 171);
            str = "   ?";
            g.DrawString(str, s1_font, s1_brush, x_offset + 180, y_offset + 173, new StringFormat());
        }
    }
    //画动画
    private static void draw_anm(Graphics g)
    {
        if (anm_frame < 0)
            return;
        if (anm == null || anm.bitmap == null)
        {
            anm_frame = -1;
            last_anm_time = 0;
            return;
        }
        //位置
        int x = 0;
        int y = 0;
        if (turn.anm_status == 1)
        {
            if (turn.type == 1)
            {
                x = player_pos[turn.index].X - 120;
                y = player_pos[turn.index].Y - 120;
            }
            else
            {
                x = enemy_pos[turn.index].X - 120;
                y = enemy_pos[turn.index].Y - 120;
            }
        }
        else
        {
            if (turn.target_type == 1)
            {
                x = player_pos[turn.target_index].X - 120;
                y = player_pos[turn.target_index].Y - 120;
            }
            else
            {
                x = enemy_pos[turn.target_index].X - 120;
                y = enemy_pos[turn.target_index].Y - 120;
            }
        }
        anm.draw(g, anm_frame, x, y);
        if (Comm.Time() - last_anm_time >= Animation.RATE)
        {
            anm_frame = anm_frame + 1;
            last_anm_time = Comm.Time();
            if (anm_frame / anm.anm_rate >= anm.max_fram)
            {
                anm_frame = -1;
                last_anm_time = 0;
                //回调回fight_do
                fight_do();
            }
        }
    }
    //扣血
    private static void reduce_blood()
    {
        //获取攻击力
        int att = 0;
        if (turn.att_type == 1)
        {
            if (turn.att_type == 1)
            {
                int att1 = get_property(PLAYER_PTY.ATT, turn.index);
                int att2 = get_property(PLAYER_PTY.FTE, turn.index);
                System.Random random = new System.Random();
                att = att1 + random.Next(0, att2);
            }
            else if (turn.type == 2)
            {
                int id = enemy[turn.index].id;
                int att1 = Enemy.enemy[id].attack;
                int att2 = Enemy.enemy[id].fortune;
                System.Random random = new System.Random();
                att = att1 + random.Next(0, att2);
            }
        }
        else if(turn.att_type==2)
        {
            int att1 = Item.item[turn.att_index].fvalue1;
            int att2 = Item.item[turn.att_index].fvalue2;
            System.Random random = new System.Random();
            att = att1 + random.Next(0, att2);
            //
        }
        else if (turn.att_type == 3)
        {
            int att1 =Skill.skill[turn.att_index].fvalue1;
            int att2 = Skill.skill[turn.att_index].fvalue2;
            System.Random random = new System.Random();
            att = att1 + random.Next(0, att2);
        }
        //获取防御力
        int def = 0;
        if (turn.target_type == 1)
        {
            def = get_property(PLAYER_PTY.DEF, turn.target_index);
        }
        else if (turn.target_type == 2)
        {
            int id = enemy[turn.target_index].id;
            def = Enemy.enemy[id].defense;
        }
        //扣除血量
        if (turn.target_type == 1)
        {
            player[turn.target_index].hp -= att - def / 2;
            if (player[turn.target_index].hp <= 0)
            {
                player[turn.target_index].hp = 0;
                player[turn.target_index].status = 3;
                player[turn.target_index].order = int.MinValue;
            }
            else if (player[turn.target_index].hp > Form1.player[player[turn.target_index].id].max_hp)
            {
                player[turn.target_index].hp = Form1.player[player[turn.target_index].id].max_hp;
            }
        }
        else if (turn.target_type == 2)
        {
            enemy[turn.target_index].hp -= att - def / 2;
            if (enemy[turn.target_index].hp <= 0)
            {
                enemy[turn.target_index].hp = 0;
                enemy[turn.target_index].status = 3;
                enemy[turn.target_index].order = int.MinValue;
            }
            else if (enemy[turn.target_index].hp > Enemy.enemy[enemy[turn.target_index].id].maxhp)
            {
                enemy[turn.target_index].hp = Enemy.enemy[enemy[turn.target_index].id].maxhp;
            }
        }
        blood_time = Comm.Time();
        blood_value = att - def / 2;
        set_draw_blood_pos();
    }
    //血量位置
    private static void set_draw_blood_pos()
    {
        //位置
        draw_blood_x = 0;
        draw_blood_y = 0;
        if (turn.target_type == 1)
        {
            draw_blood_x = player_pos[turn.target_index].X - 5;
            draw_blood_y = player_pos[turn.target_index].Y - 120;
        }
        else
        {
            draw_blood_x = enemy_pos[turn.target_index].X - 5;
            draw_blood_y = enemy_pos[turn.target_index].Y - 120;
        }
    }
    //绘制血量
    private static void draw_blood(Graphics g)
    {
        long time = Comm.Time();
        if (time < blood_time || time > blood_time + 2000)
            return;

        Font font = new Font("黑体", 16);
        Brush brush;
        string str = "";
        if (blood_value > 0)
        {
            brush = Brushes.Maroon;
            str = "-" + blood_value.ToString();
        }
        else
        {
            brush = Brushes.ForestGreen;
            str = "+" + (-blood_value).ToString();
        }
        g.DrawString(str, font, brush, draw_blood_x, draw_blood_y, new StringFormat());
    }
    //胜负判定
    private static bool is_enemy_win()
    {
        for(int i=0;i<3;i++)
        {
            if (player[i].id >= 0 && player[i].hp > 0)
                return false;
        }
        return true;
    }
    private static bool is_player_win()
    {
        for (int i = 0; i < 3; i++)
        {
            if (enemy[i].id >= 0 && enemy[i].hp > 0)
                return false;
        }
        return true;
    }
    //胜负判断
    private static void tell_win()
    {
        if(is_enemy_win())
        {
            iswin = 0;
            //MessageBox.Show("敌人胜");
            end();
        }
        else if(is_player_win())
        {
            iswin = 1;
            //MessageBox.Show("玩家胜");
            end();
        }
        else
        {
            fight_logic();
        }
    }
    //结束战斗
    private static void end()
    {
        select.hide();
        ctrl.hide();
        Player.status = player_last_status;
        fighting = 0;
        for(int i=0;i<3;i++)
        {
            if (player[i].id < 0)
                continue;
            Form1.player[player[i].id].hp = player[i].hp;
            Form1.player[player[i].id].mp = player[i].mp;
        }
        if (iswin == 1)
            pan_win.show();
        else
            if (isgameover == 1)
                pan_gameover.show();
            else
                pan_lose.show();
        Form1.music_player.URL = Form1.map[Map.current_map].music;
    }

    public static void wlbtn_close_event()
    {
        pan_win.hide();
        pan_lose.hide();

        if(iswin==1)
        {
            if (winitem1 >= 0)
                Item.add_item(winitem1, 1);
            if (winitem2 >= 0)
                Item.add_item(winitem2, 1);
            if (winitem3 > 0)
                Item.add_item(winitem3, 1);
        }
        else
        {
            Player.money = Player.money - lostmoney;
            if (Player.money < 0) Player.money = 0;
        }
        //任务回调
        Task.story(0, 1);
    }
    public static void wlbtn_gameover_event()
    {
        Title.show();
    }

    public static void pan_win_draw(Graphics g,int x_offset,int y_offset)
    {
        for (int i = 0; i < 3; i++)
        {
            //获取item和pos
            int index = -1;
            int pos_x = 0;
            if (i == 0)
            {
                index = winitem1;
                pos_x = 50;
            }
            else if(i==1)
            {
                index = winitem2;
                pos_x = 133;
            }
            else if(i==2)
            {
                index = winitem3;
                pos_x = 226;
            }
            if (index < 0)
                continue;
            if (Item.item[index].bitmap != null)
                g.DrawImage(Item.item[index].bitmap, x_offset + pos_x, y_offset + 128);
        }
    }
    public static void pan_lose_draw(Graphics g,int x_offset,int y_offset)
    {
        Font font = new Font("黑体", 12);
        Brush brush = Brushes.Black;
        string str = lostmoney.ToString();
        g.DrawString(str, font, brush, x_offset + 194, y_offset + 146, new StringFormat());
    }

    //物品和技能
    public static void menu_show(int menu)
    //menu 0-物品，1-技能
    {
        page = 1;
        Fight.menu = menu;
        pan_item.show();
    }
    public static void click_use()
    {
        if(menu==0)
        {
            int index = -1;
            for(int i=0,count=0;i<Item.item.Length;i++)
            {
                if (Item.item[i].num <= 0)
                    continue;
                count++;
                if (count <= (page - 1) * 3 + selnow - 1)
                    continue;
                index = i;
                break;
            }
            if(index>=0)
            {
                if(Item.item[index].check_fuse())
                {
                    ctrl.hide();
                    turn.att_type = 2;
                    turn.att_index = index;
                    select.show();
                    int tindex = Fight.turn.index;
                    player[tindex].status = 1;
                }
            }
        }
        else
        {
            int index = -1;
            int[] pskill = Form1.player[player[turn.index].id].skill;
            for(int i=0,count=0;i<pskill.Length;i++)
            {
                if(pskill[i]<0)
                    continue;
                count++;
                if(count<=(page-1)*3+selnow-1)
                    continue;
                index=i;
                break;
            }
            if(Skill.skill[pskill[index]].check_fuse(player[turn.index].mp))
            {
                player[turn.index].mp -= Skill.skill[pskill[index]].mp;
                ctrl.hide();
                turn.att_type = 3;
                turn.att_index = index;
                select.show();
                int tindex = Fight.turn.index;
                player[tindex].status = 1;
            }
        }
    }
    public static void click_close()
    {
        ctrl.show();
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
    public static void click_previous_page()
    {
        page--;
        if (page < 1) page = 1;
    }
    public static void click_next_page()
    {
        page++;
    }
    public static void pan_item_draw(Graphics g, int x_offset, int y_offset)
    {
        if (menu == 0)
        {
            //标签
            Font font = new Font("黑体", 22);
            Brush brush = Brushes.Gray;
            string str = lostmoney.ToString();
            g.DrawString("物品", font, brush, x_offset + 138, y_offset + 5, new StringFormat());
            //物品
            for (int i = 0, count = 0, showcount = 0; i < Item.item.Length && showcount < 3; i++)
            {
                if (Item.item[i].num <= 0)
                    continue;
                count++;
                if (count <= (page - 1) * 3)
                    continue;
                if (Item.item[i].bitmap != null)
                    g.DrawImage(Item.item[i].bitmap, x_offset + 46, y_offset + 66 + showcount * 96);

                Font font_n = new Font("黑体", 12);
                Brush brush_n = Brushes.GreenYellow;
                g.DrawString(Item.item[i].name + "X" + Item.item[i].num.ToString(), font_n, brush_n, x_offset + 126, y_offset + 64 + showcount * 96, new StringFormat());

                Font font_d = new Font("黑体", 10);
                Brush brush_d = Brushes.LawnGreen;
                g.DrawString(Item.item[i].description, font_d, brush_d, x_offset + 128, y_offset + 64 + showcount * 96, new StringFormat());
                showcount++;
            }
        }
        else
        {
            //标签
            Font font = new Font("黑体", 22);
            Brush brush = Brushes.Gray;
            string str = lostmoney.ToString();
            g.DrawString("技能", font, brush, x_offset + 138, y_offset + 5, new StringFormat());
            int[] pskill = Form1.player[player[turn.index].id].skill;

            //物品
            for (int i = 0, count = 0, showcount = 0; i < pskill.Length && showcount < 3; i++)
            {
                if (pskill[i] <= 0)
                    continue;
                count++;
                if (count <= (page - 1) * 3)
                    continue;
                if (Skill.skill[pskill[i]].bitmap != null)
                    g.DrawImage(Skill.skill[pskill[i]].bitmap, x_offset + 46, y_offset + 66 + showcount * 96);

                Font font_n = new Font("黑体", 12);
                Brush brush_n = Brushes.GreenYellow;
                g.DrawString(Skill.skill[pskill[i]].name, font_n, brush_n, x_offset + 128, y_offset + 64 + showcount * 96, new StringFormat());

                Font font_d = new Font("黑体", 10);
                Brush brush_d = Brushes.LawnGreen;
                g.DrawString(Skill.skill[pskill[i]].description, font_d, brush_d, x_offset + 128, y_offset + 64 + showcount * 96, new StringFormat());
                showcount++;
            }
        }
        //显示选择框
        g.DrawImage(Statusmenu.bitmap_sel, x_offset + 37, y_offset + 55 + (selnow - 1) * 95);
    }
  
}