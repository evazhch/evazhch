using System.Drawing;
using RPG_experiment;
public class Item
{
    //Item数组
    public static Item[] item;
    public int num = 0;
    public string name = "";
    public string description = "";
    public Bitmap bitmap;
    public int isdepletion = 1;
    public int value1 = 0;
    public int value2 = 0;
    public int value3 = 0;
    public int value4 = 0;
    public int value5 = 0;
    public int cost = 100;
    //战斗中使用
    public int canfuse = 0;
    public int fvalue1 = 0;
    public int fvalue2 = 0;
    public Animation fanm;

    public void set(string name,string description,string bitmap_path,
        int isdepletion,int value1,int value2,int value3,int value4,int value5)
    {
        this.name = name;
        this.description = description;
        if(bitmap_path!=null&&bitmap_path!="")
        {
            bitmap = new Bitmap(bitmap_path);
            bitmap.SetResolution(96, 96);
        }
        this.isdepletion = isdepletion;
        this.value1 = value1;
        this.value2 = value2;
        this.value3 = value3;
        this.value4 = value4;
        this.value5 = value5;

    }
    public delegate void Use_event(Item item);
    public event Use_event use_event;
    public void use()
    {
        if (num <= 0)
            return;
        if (isdepletion != 0)
            num--;
        if (use_event != null)
            use_event(this);
    }
    public static void add_item(int index,int num)
    {
        if (item == null) return;
        if (index < 0) return;
        if (index >= item.Length) return;
        if (item[index] == null) return;
        item[index].num += num;
        if (item[index].num < 0)
            item[index].num = 0;
    }
    //添加hp,使用value1
    public static void add_up(Item item)
    {
        Player player = Form1.player[Player.select_player];
        player.hp += item.value1;
        if (player.hp > player.max_hp)
            player.hp = player.max_hp;
        if (player.hp < 0)
            player.hp = 0;
    }
    //添加mp,使用value1
    public static void add_mp(Item item)
    {
        Player player = Form1.player[Player.select_player];
        player.mp += item.value1;
        if (player.mp > player.max_mp)
            player.mp = player.max_mp;
    }
    public static void unequip(int type)
    {
        //获取index
        int index;
        if (type == 1)
        {
            index = Form1.player[Player.select_player].equip_att;
            Form1.player[Player.select_player].equip_att = -1;
        }
        else if (type == 2)
            {
                index = Form1.player[Player.select_player].equip_def;
                Form1.player[Player.select_player].equip_def = -1;
            }
            else return;

        if (item == null) return;
        if (index < 0) return;
        if (index >= item.Length) return;
        if (item[index] == null) return;

        add_item(index, 1);
    }
    //value1 类型 1-att,2-def
    //value2-5 att def fspeed fortune 增简值
    public static void equip(Item item)
    {
        if (Item.item == null) return;
        if (item == null) return;
        int index = -1;
        for(int i=0;i<Item.item.Length;i++)
        {
            if (Item.item[i] == null)
                continue;
            if(item.name==Item.item[i].name
                && item.description==Item.item[i].description)
            {
                index = i;
                break;
            }
        }
        if (index < 0) return;
        if (index >= Item.item.Length) return;
        if (Item.item[index] == null) return;
        unequip(item.value1);

        if (item.value1 == 1)
            Form1.player[Player.select_player].equip_att = index;
        else if (item.value1 == 2)
            Form1.player[Player.select_player].equip_def = index;
        else
            return;
    }
    public static void lpybook(Item item)
    {
        Message.show("", "heihei", "", Message.Face.LEFT);
        Task.block();
        Message.show("", "hehe", "", Message.Face.LEFT);
        Task.block();
    }

    //战斗中使用
    public void fset(Animation fanm,int fvalue1,int fvalue2)
    {
        this.fanm = fanm;
        this.fvalue1 = fvalue1;
        this.fvalue2 = fvalue2;
        this.canfuse = 1;
        if (fanm != null)
            fanm.load();
    }
    public bool check_fuse()
    {
        if (num <= 0)
            return false;
        if (canfuse != 1)
            return false;
        if (isdepletion != 0)
            num--;
        return true;
    }

}



