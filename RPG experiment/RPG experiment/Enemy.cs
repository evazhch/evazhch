using System.Drawing;
using RPG_experiment;

public class Enemy
{
    public static Enemy[] enemy;
    //基本属性
    public string name = "";
    public Bitmap fbitmap;
    public int fx_offset=-120;
    public int fy_offset=-120;

    public int maxhp=100;
    public int attack = 10;
    public int defense = 10;
    public int fspeed = 10;
    public int fortune = 10;
    public Animation anm_att;
    //skill
    public Animation anm_skill;
    //战斗列表//》=0为技能-1为攻击
    public int[] fightlist = new int[] { -1, 0, -1, -1 };

    public void set(string name,string fbitmap_path,int fx_offset,int fy_offset,
        int maxhp,int attack,int defense,int fspeed,int fortune,
        Animation anm_att,Animation anm_skill,int[] fightlist)
    {
        this.name = name;
        if (fbitmap_path != null && fbitmap_path != "")
        {
            this.fbitmap = new Bitmap(fbitmap_path);
            this.fbitmap.SetResolution(96, 96);
        }
        this.fx_offset = fx_offset;
        this.fy_offset = fy_offset;
        
        this.maxhp = maxhp;
        this.attack=attack;
        this.defense = defense;
        this.fspeed = fspeed;
        this.fortune = fortune;

        this.anm_att = anm_att;
        this.anm_skill = anm_skill;

        this.fightlist = fightlist;

    }
}