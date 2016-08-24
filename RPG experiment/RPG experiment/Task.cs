using RPG_experiment;
using System.Windows.Forms;
public class Task
{
    //控制变量
    public static int[] p = new int[100];
    public static Task[] task;
    public static int id = 0;
    public static int step = 0;
    public static Player.Status player_last_status = Player.Status.WALK;
    public int npc_id = -1;
    public enum VARTYPE
    {
        ANY = 0,
        EQUAL = 1,
        GREATER = 2,
        LESS = 3,
    }

    public int cvar1_index = 0;
    public int cvar1 = 0;
    public VARTYPE cvar1_type = VARTYPE.ANY;
    public int cvar2_index = 0;
    public int cvar2 = 0;
    public VARTYPE cvar2_type = VARTYPE.ANY;
    public int money = 0;
    public VARTYPE money_type = VARTYPE.ANY;
    public int check_conditions(int index)
    {
        //预设条件
        //id
        if (index != npc_id)
            return -1;
        //var1
        if (cvar1_type == VARTYPE.EQUAL)
        {
            if (p[cvar1_index] != cvar1)
                return -1;
        }
        else if (cvar1_type == VARTYPE.GREATER)
        {
            if (p[cvar1_index] <= cvar1)
                return -1;
        }
        else if (cvar1_type == VARTYPE.LESS)
        {
            if (p[cvar1_index] >= cvar1)
                return -1;
        }
        //var2
        if (cvar2_type == VARTYPE.EQUAL)
        {
            if (p[cvar2_index] != cvar2)
                return -1;
        }
        else if (cvar2_type == VARTYPE.GREATER)
        {
            if (p[cvar2_index] <= cvar2)
                return -1;
        }
        else if (cvar2_type == VARTYPE.LESS)
        {
            if (p[cvar2_index] >= cvar2)
                return -1;
        }
        //money
        if (money_type == VARTYPE.EQUAL)
        {
            if (Player.money != cvar2)
                return -1;
        }
        else if (money_type == VARTYPE.GREATER)
        {
            if (Player.money <= cvar2)
                return -1;
        }
        else if (money_type == VARTYPE.LESS)
        {
            if (Player.money >= cvar2)
                return -1;
        }
        return 0;
    }
    //预设结果处理
    public enum VARRESULT
    {
        NOTHING = 0,
        ASSIGN = 1,
        ADD = 2,
        SUB = 3,
    }
    public int rcvar1_index = 0;
    public int rcvar1 = 0;
    public VARRESULT rcvar1_type = VARRESULT.NOTHING;
    public int rcvar2_index = 0;
    public int rcvar2 = 0;
    public VARRESULT rcvar2_type = VARRESULT.NOTHING;
    public void deal_result()
    {
        //预设结果处理
        //var1
        if (rcvar1_type == VARRESULT.ASSIGN)
        {
            p[rcvar1_index] = rcvar1;
        }
        else if (rcvar1_type == VARRESULT.ADD)
        {
            p[rcvar1_index] += rcvar1;
        }
        else if (rcvar1_type == VARRESULT.SUB)
        {
            p[rcvar1_index] -= rcvar1;
        }
        //var2
        if (rcvar2_type == VARRESULT.ASSIGN)
        {
            p[rcvar2_index] = rcvar2;
        }
        else if (rcvar2_type == VARRESULT.ADD)
        {
            p[rcvar2_index] += rcvar2;
        }
        else if (rcvar1_type == VARRESULT.SUB)
        {
            p[rcvar2_index] -= rcvar2;
        }
    }
    public delegate int Task_event(int index, int step);
    public event Task_event evt;
    //task_event 返回值
    //0 处理成功且完成
    //其他 走到第n步
    public int task_event(int task_id, int step)
    {
        int ret = 0;
        if (evt != null)
        {
            ret = evt(task_id, step);
            Task.step = ret;
        }
        return ret;
    }
    public int storyname(int index, int step)
    {
        if (true)//自定义条件
            return -1;
        if (step == 0)
        {
            //第一段剧情
            return 1;
        }
        if (step == 1)
        {
            //第二段剧情
            return 0;
        }
    }
    public int var1 = 0;
    public int var2 = 0;
    public int var3 = 0;
    public int var4 = 0;
    //--------------------------------------------
    //set
    //-------------------------------------------
    public void set(int npc_id, Task_event evt,
        int cvar1_index, int cvar1, VARTYPE cvar1_type,
        int cvar2_index, int cvar2, VARTYPE cvar2_type,
        int money, VARTYPE money_type,
        int rcvar1_index, int rcvar1, VARRESULT rcvar1_type,
        int rcvar2_index, int rcvar2, VARRESULT rcvar2_type,
        int var1, int var2, int var3, int var4)
    {
        this.npc_id = npc_id;
        this.evt += evt;
        this.cvar1_index = cvar1_index;
        this.cvar1 = cvar1;
        this.cvar1_type = cvar1_type;
        this.cvar2_index = cvar2_index;
        this.cvar2 = cvar2;
        this.cvar2_type = cvar2_type;
        this.rcvar1_index = rcvar1_index;
        this.rcvar1 = rcvar1;
        this.rcvar1_type = rcvar1_type;
        this.rcvar2_index = rcvar2_index;
        this.rcvar2 = rcvar2;
        this.rcvar2_type = rcvar2_type;
        this.money = money;
        this.money_type = money_type;
        this.var1 = var1;
        this.var2 = var2;
        this.var3 = var3;
        this.var4 = var4;
    }
    public void set(int npc_id, Task_event evt,
       int cvar1_index, int cvar1, VARTYPE cvar1_type,
       int rcvar1_index, int rcvar1, VARRESULT rcvar1_type,
       int var1, int var2, int var3, int var4)
    {
        set(npc_id, evt,
      cvar1_index, cvar1, cvar1_type,
      0, 0, VARTYPE.ANY,
      0, VARTYPE.ANY,
      rcvar1_index, rcvar1, rcvar1_type,
      0, 0, VARRESULT.NOTHING,
      var1, var2, var3, var4);
    }
    public void set(int npc_id, Task_event evt,
      int cvar1_index, int cvar1, VARTYPE cvar1_type,
      int rcvar1_index, int rcvar1, VARRESULT rcvar1_type
      )
    {
        set(npc_id, evt,
             cvar1_index, cvar1, cvar1_type,
             rcvar1_index, rcvar1, rcvar1_type,
                0, 0, 0, 0);
    }
    public void set(int npc_id, Task_event evt,
        int var1, int var2, int var3, int var4
     )
    {
        set(npc_id, evt,
            0, 0, VARTYPE.ANY,
            0, 0, VARRESULT.NOTHING,
            var1, var2, var3, var4);
    }
    public void set(int npc_id, Task_event evt)
    {
        set(npc_id, evt, 0, 0, 0, 0);
    }
    //任务流程
    public static void story(int npc_id, int type)//type-0-正常 1-回调
    {
        //保存状态
        if (Player.status != Player.Status.TASK && Player.status!=Player.Status.FIGHT)
            player_last_status = Player.status;
        Player.status = Player.Status.TASK;
        //事件
        if (task == null) return;

        if(type==1&&id>=0)
        {
            int ret = task[id].task_event(id, step);
            if (ret == 0)
                task[id].deal_result();
        }
        else if(type==0)
        
            for(int i=task.Length-1;i>=0;i--)
            {
                if (task[i] == null) continue;
                if (task[i].check_conditions(npc_id) != 0)
                    continue;
                id = i;
                step = 0;
                int ret = task[i].task_event(i, step);
                if (ret == 0)
                    task[i].deal_result();
                break;
            }
            
        
        //恢复状态
            Player.status = player_last_status;
    }
    public static void story(int i)
    {
        story(i, 0);
    }
    //切换地图
    //var1 map-id
    //var2 var3 坐标
    //var4 面向
    public static int change_map(int task_id,int step)
    {
        if (task == null) return -1;
        if (task[task_id] == null) return -1;

        int map_id = task[task_id].var1;
        int x = task[task_id].var2;
        int y = task[task_id].var3;
        int f = task[task_id].var4;
        Map.change_map(Form1.map, Form1.player, Form1.npc, map_id, x, y, f, Form1.music_player);
        return 0;
    }
    //切换地图
    public static void change_map(int map_id,int x,int y,int face)
    {
        Map.change_map(Form1.map, Form1.player, Form1.npc, map_id, x, y, face, Form1.music_player);
    }
    //对话
    public static void talk(string name,string str,string face,Message.Face fpos)
    {
        Message.show(name, str, face, fpos);
        block();
    }
    public static void talk(string name, string str, string face)
    {
        Message.show(name, str, face,Message.Face.LEFT);
        block();
    }
    //提示
    public static void tip(string str)
    {
        Message.showtip(str);
        block();
    }
    //设置npc位置
    public static void set_npc_pos(int npc_id,int x,int y)
    {
        if (Form1.npc == null) return;
        if (Form1.npc[npc_id] == null) return;

        Form1.npc[npc_id].x = x;
        Form1.npc[npc_id].y = y;
    }
    //播放npc动画
    public static void play_npc_anm(int npc_id,int anm_id)
    {
        if (Form1.npc == null) return;
        if (Form1.npc[npc_id] == null) return;
        Form1.npc[4].play_anm(anm_id);
    }
    //战斗
    public static void fight(int[] enemy,string bg_path,int isgameover,int winitem1,int winitem2,int winitem3,int losemoney)
    {
        Fight.start(enemy, bg_path, isgameover, winitem1, winitem2, winitem3, losemoney);
    }
    //增减物品
    public static void add_item(int item_id,int num)
    {
        Item.add_item(item_id, num);
    }
    //学习技能
    public static void learn_skill(int p_id,int skill_id,int type)
    {
        Skill.learn_skill(p_id, skill_id, type);
    }
    //回复
    public static void recover()
    {
        if (Form1.player == null) return;
        for(int i=0;i<Form1.player.Length;i++)
        {
            if (Form1.player[i] == null) continue;
            Form1.player[i].hp = Form1.player[i].max_hp;
            Form1.player[i].mp = Form1.player[i].max_mp;
        }
        tip("完全恢复！");
    }
    //保存
    public static void block()
    {
        while (Player.status == Player.Status.PANNEL)
            Application.DoEvents();
    }
}
