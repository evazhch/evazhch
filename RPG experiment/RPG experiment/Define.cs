using System.Drawing;

public static class Define
{
    public static void define(Player[]player,Npc[]npc,Map[]map)
    {
        player[0] = new Player();
        player[0].bitmap = new Bitmap(@"rpg1.png");
        player[0].bitmap.SetResolution(96, 96);
        player[0].is_action = 1;

        player[1] = new Player();
        player[1].bitmap = new Bitmap(@"rpg2.png");
        player[1].bitmap.SetResolution(96, 96);
        player[1].is_action = 1;

        player[2] = new Player();
        player[2].bitmap = new Bitmap(@"rpg3.png");
        player[2].bitmap.SetResolution(96, 96);
        player[2].is_action = 1;

        Player.current_player = 0;
        //map define
        map[0] = new Map();
        map[0].bitmap_path = "map.bmp";
        map[0].shade_path = "shade.png";
        map[0].block_path = "block.png";
        map[0].bitmap = new Bitmap(@"map.bmp");
        map[0].bitmap.SetResolution(96, 96);
        map[0].shade = new Bitmap(@"shade.png");
        map[0].block = new Bitmap(@"block.png");
        map[0].music = "0.mp3";

        map[1] = new Map();
        map[1].bitmap_path = "map1.bmp";
        map[1].shade_path = "shade1.png";
        map[1].block_path = "block1.bmp";
        map[1].music = "0.mp3";

        map[2] = new Map();
        map[2].bitmap_path = "map2.png";
        map[2].shade_path = "shade2.png";
        map[2].block_path = "block2.png";
        //npc define
        npc[0] = new Npc();
        npc[0].map = 0;
        npc[0].x = 400;
        npc[0].y = 410;
        npc[0].bitmap_path = "npc.png";

        npc[1] = new Npc();
        npc[1].map = 0;
        npc[1].x = 600;
        npc[1].y = 425;
        npc[1].bitmap_path = "npc1.png";

        npc[2] = new Npc();
        npc[2].map = 0;
        npc[2].x = 800;
        npc[2].y = 503;
        npc[2].region_x = 20;
        npc[2].region_y = 200;
        npc[2].bitmap_path = "npc1.png";
        npc[2].collision_type = Npc.Collosion_type.ENTER;

        npc[3] = new Npc();
        npc[3].map = 1;
        npc[3].x = 210;
        npc[3].y = 125;
        npc[3].region_x = 100;
        npc[3].region_y = 20;
        npc[3].bitmap_path = "npc1.png";
        npc[3].collision_type = Npc.Collosion_type.ENTER;

        npc[4] = new Npc();
        npc[4].map = 1;
        npc[4].x = 200;
        npc[4].y = 200;
        npc[4].bitmap_path = "npc3.png";
        npc[4].collision_type = Npc.Collosion_type.KEY;
        Animation npc4anm1 = new Animation();
        npc4anm1.bitmap_path = "anm1.png";
        npc4anm1.row = 4;
        npc4anm1.col = 4;
        npc4anm1.max_fram = 16;
        npc4anm1.anm_rate = 2;
        npc[4].anm = new Animation[1];
        npc[4].anm[0] = npc4anm1;

        npc[5] = new Npc();
        npc[5].map = 1;
        npc[5].x = 200;
        npc[5].y = 300;
        npc[5].bitmap_path = "npc4.png";
        npc[5].collision_type = Npc.Collosion_type.KEY;
        npc[5].npc_type = Npc.Npc_type.CHARACTER;
        npc[5].idle_walk_direction = Comm.Direction.LEFT;
        npc[5].idle_walk_time = 40;

        npc[7] = new Npc();
        npc[7].map = 0;
        npc[7].x = 300;
        npc[7].y = 400;
        npc[7].x_offset = -50;
        npc[7].mc_xoffset = 0;
        npc[7].mc_yoffset = 0;
        npc[7].mc_w = 90;
        npc[7].mc_h = 50;
        npc[7].bitmap_path = "npc_shoe.png";
        npc[7].collision_type = Npc.Collosion_type.KEY;

        npc[6] = new Npc();
        npc[6].map = 0;
        npc[6].x = 700;
        npc[6].y = 380;
        npc[6].bitmap_path = "npc2.png";
        npc[6].collision_type = Npc.Collosion_type.KEY;


        player[0].status_bimap = new Bitmap(@"item/face1.png");
        player[0].status_bimap.SetResolution(96, 96);

        player[1].status_bimap = new Bitmap(@"item/face2.png");
        player[1].status_bimap.SetResolution(96, 96);

        player[2].status_bimap = new Bitmap(@"item/face3.png");
        player[2].status_bimap.SetResolution(96, 96);

        //item
        Item.item = new Item[6];
        Item.item[0] = new Item();
        Item.item[0].set("红药水", "回复少量hp", "item/item1.png", 1, 30, 0, 0, 0, 0);
        Item.item[0].cost = 30;
        Item.item[0].use_event += new Item.Use_event(Item.add_up);

        Item.item[2] = new Item();
        Item.item[2].set("短剑", "一把钢制短剑", "item/item3.png", 1, 1, 10, 0, 0, 5);
        //Item.item[1].cost = 30;
        Item.item[2].use_event += new Item.Use_event(Item.equip);

        Item.item[1] = new Item();
        Item.item[1].set("蓝药水", "回复少量hp", "item/item2.png", 1, 30, 0, 0, 0, 0);
        //Item.item[2].cost = 30;
        Item.item[1].use_event += new Item.Use_event(Item.add_mp);

        Item.item[3] = new Item();
        Item.item[3].set("柴刀", "传说中的装备\n上沾有诚哥之血\n", "item/item4.png", 1, 1, 3, 0, 0, 50);
        Item.item[3].cost = 30;
        Item.item[3].use_event += new Item.Use_event(Item.equip);

        Item.item[4] = new Item();
        Item.item[4].set("木盾", "有些防御力的木盾", "item/item5.png", 1, 2, 0, 20, 5, 0);
        //Item.item[4].cost = 30;
        Item.item[4].use_event += new Item.Use_event(Item.equip);



        Item.item[5] = new Item();
        Item.item[5].set("攻略本", "一本游记\n可打开观阅", "item/item6.png", 0, 30, 0, 0, 0, 0);
        //Item.item[5].cost = 30;
        Item.item[5].use_event += new Item.Use_event(Item.lpybook);

        Item.add_item(0, 3);
        Item.add_item(1, 3);
        Item.add_item(2, 2);
        Item.add_item(3, 1);
        Item.add_item(4, 1);
        Item.add_item(5, 1);

        //skill
        Skill.skill = new Skill[2];
        Skill.skill[0] = new Skill();
        Skill.skill[0].set("治疗术", "回复少量hp\nmp:20", "item/skill2.png", 20, 20, 0, 0, 0, 0);
        Skill.skill[0].use_event += new Skill.Use_event(Skill.add_hp);

        Skill.skill[1] = new Skill();
        Skill.skill[1].set("黑洞漩涡", "攻击技能将敌人\n吸入漩涡\nmp:20", "item/skill1.png", 20, 0, 0, 0, 0, 0);
        Skill.skill[1].use_event += new Skill.Use_event(Skill.add_hp);

        Skill.learn_skill(0, 0, 1);
        Skill.learn_skill(0, 1, 1);
        Skill.learn_skill(1, 0, 1);

        //战斗定义
        Animation anm_att = new Animation();
        anm_att.bitmap_path = "fight/anm_att.png";
        anm_att.row = 4;
        anm_att.col = 2;
        anm_att.max_fram = 5;
        anm_att.anm_rate = 1;

        Animation anm_item = new Animation();
        anm_item.bitmap_path = "fight/anm_item.png";
        anm_item.row = 4;
        anm_item.col = 2;
        anm_item.max_fram = 4;
        anm_item.anm_rate = 1;

        Animation anm_skill = new Animation();
        anm_skill.bitmap_path = "fight/anm_skill.png";
        anm_skill.row = 4;
        anm_skill.col = 1;
        anm_skill.max_fram = 4;
        anm_skill.anm_rate = 1;

        player[0].fset("主角1", "fight/p1.png", -120, -120, "fight/fm_face1.png", anm_att, anm_item, anm_skill);
        player[1].fset("主角2", "fight/p2.png", -120, -120, "fight/fm_face2.png", anm_att, anm_item, anm_skill);
        player[2].fset("主角3", "fight/p3.png", -120, -120, "fight/fm_face3.png", anm_att, anm_item, anm_skill);

        //fight_item
        Animation anm_item0 = new Animation();
        anm_item0.bitmap_path = "fight/anm_heal.png";
        anm_item0.row = 5;
        anm_item0.col = 1;
        anm_item0.max_fram = 5;
        anm_item0.anm_rate = 1;
        Item.item[0].fset(anm_item0, -50, 20);

        Animation anm_item2 = new Animation();
        anm_item2.bitmap_path = "fight/anm_sword.png";
        anm_item2.row = 5;
        anm_item2.col = 2;
        anm_item2.max_fram = 6;
        anm_item2.anm_rate = 1;
        Item.item[2].fset(anm_item2,50, 20);
        //fight_skill
        Animation anm_skill0 = new Animation();
        anm_skill0.bitmap_path = "fight/anm_heal.png";
        anm_skill0.row = 5;
        anm_skill0.col = 1;
        anm_skill0.max_fram = 5;
        anm_skill0.anm_rate = 1;
        Skill.skill[0].fset(anm_skill0, -50, 20);

        Animation anm_skill1 = new Animation();
        anm_skill1.bitmap_path = "fight/anm_sword.png";
        anm_skill1.row = 5;
        anm_skill1.col = 2;
        anm_skill1.max_fram = 6;
        anm_skill1.anm_rate = 1;
        Skill.skill[1].fset(anm_skill1, 50, 20);


        //敌人设置
        Enemy.enemy = new Enemy[2];
        Enemy.enemy[0] = new Enemy();
        Enemy.enemy[0].set("老虎", "fight/enemy.png", -160, -120, 30, 20, 10, 15, 10, anm_att, anm_skill,new int[]{-1,-1,1,1,1,-1,-1,-1});
        Enemy.enemy[1] = new Enemy();
        Enemy.enemy[1].set("鞋精", "fight/enemy2.png", -160, -120, 3, 40, 10, 15, 10, anm_att, anm_skill, new int[] { -1, -1, 1, 1, 1, 1 });

        //定义任务
        //Task
        Task.task = new Task[100];
        //地图1 切换点
        Task.task[0] = new Task();
        Task.task[0].set(2, new Task.Task_event(Task.change_map), 2, 350, 500, 4);
        //地图二切换点
        Task.task[1] = new Task();
        Task.task[1].set(3, new Task.Task_event(Task.change_map), 0, 45, 500, 2);
        //陌生人见面
        Task.task[2] = new Task();
        Task.task[2].set(1, new Task.Task_event(Map1stroy.meet), 0, 0, Task.VARTYPE.ANY, 0, 1, Task.VARRESULT.ASSIGN);
        //陌生人催促
        Task.task[3] = new Task();
        Task.task[3].set(1, new Task.Task_event(Map1stroy.afermeet), 0, 1, Task.VARTYPE.EQUAL, 0, 0, Task.VARRESULT.NOTHING);
        //陌生人 给予奖励
        Task.task[4] = new Task();
        Task.task[4].set(1, new Task.Task_event(Map1stroy.reward), 0, 2, Task.VARTYPE.EQUAL, 0, 0, Task.VARRESULT.ASSIGN);
        //陌生人 给予奖励之后
        Task.task[5] = new Task();
        Task.task[5].set(1, new Task.Task_event(Map1stroy.afterreward), 0, 3, Task.VARTYPE.EQUAL, 0, 0, Task.VARRESULT.NOTHING);
        //鞋子默认
        Task.task[6] = new Task();
        Task.task[6].set(0, new Task.Task_event(Map1stroy.shoe), 0, 0, Task.VARTYPE.ANY, 0, 0, Task.VARRESULT.NOTHING);
        //鞋子战斗
        Task.task[7] = new Task();
        Task.task[7].set(0, new Task.Task_event(Map1stroy.shoefight), 0, 1, Task.VARTYPE.EQUAL, 0, 0, Task.VARRESULT.NOTHING);


        //保存
        Task.task[8] = new Task();
        Task.task[8].set(6, new Task.Task_event(Map1stroy.save));
    }
}