public class Map1stroy
{
    //陌生人
    public static int meet(int task_id,int step)
    {
        Task.talk("算命先生", "少年，鄙人见你根骨清奇，保卫天下安危的重任就交给你了", "", Message.Face.REGHT);
        Task.talk("diang哥", "老头，说人话", "");
        Task.talk("算命先生", "其实附近有猛兽出没", "", Message.Face.REGHT);
        Task.talk("diang哥", "好吧在哪", "");
        Task.talk("算命先生", "鄙人手里有宝剑一柄", "", Message.Face.REGHT);
        Task.tip("获得短剑*3");
        Task.add_item(2, 3);
        return 0;
    }
    public static int afermeet(int task_id,int step)
    {
        Task.talk("算命先生", "坎卦算命大洋一块", "", Message.Face.REGHT);
        return 0;
    }
    public static int reward(int task_id,int step)
    {
        Task.talk("算命先生", "ball啦啦啦", "", Message.Face.REGHT);
        Task.tip("获得《逗逼志》");
        Task.add_item(5,1);
        return 0;
    }
    public static int afterreward(int task_id,int step)
    {
        Task.talk("", "", "", Message.Face.REGHT);
        return 0;
    }
    //鞋子
    public static int shoe(int task_id,int step)
    {
        Task.tip("一个脑袋");
        return 0;
    }
    public static int shoefight(int task_id,int step)
    {
        if(step==0)//战斗前
        {
            Task.talk("鞋子精", "奶奶的，岁打扰我碎觉", "");
            Task.fight(new int[] { -1, 1, -1 }, "fight/f_scene.png",0, 0, 1, -1, 0);
            Task.block();
            return 1;

        }
        else//战斗后
        {
            if(Fight.iswin==1)
            {
                Task.tip("");
                Task.set_npc_pos(0, -1000, -1000);
                Task.p[0] = 2;
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
    public static int save(int task_id,int step)
    {
        Task.talk("xx", "我可以保存游戏", "");
        Save.show(0);
        Task.block();
        return 0;
    }
}