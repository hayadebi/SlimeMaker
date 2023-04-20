using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class state_start : MonoBehaviour
{
    public Text[] all_state;
    // Start is called before the first frame update
    void Start()
    {
        if (GManager.instance.isEnglish == 0)
        {
            all_state[0].text = "固有名："+GManager.instance.dungeon_player.unique_name[0];
            all_state[1].text = "種族："+GManager.instance.all_monster[GManager.instance.dungeon_player.monster_id].monster_name[0];
            all_state[2].text = "体力：" + GManager.instance.dungeon_player.max_hp.ToString();
            all_state[3].text = "攻撃力：" + GManager.instance.dungeon_player.at.ToString();
            all_state[4].text = "防御力：" + GManager.instance.dungeon_player.df.ToString();
            all_state[5].text = "素早さ：" + GManager.instance.dungeon_player.speed.ToString();
            all_state[6].text = "攻撃範囲：" + GManager.instance.dungeon_player.at_area.ToString();
            all_state[7].text = "攻撃速度：" + GManager.instance.dungeon_player.at_speed.ToString();
            all_state[8].text = "成長速度：" + GManager.instance.dungeon_player.lvup_speed.ToString();
            all_state[9].text = "種族技：" + GManager.instance.all_attack[GManager.instance.dungeon_player.normal_spell].at_name[0];
            if(GManager.instance.dungeon_player.special_spell != -1)
                all_state[10].text = "才能技：" + GManager.instance.all_attack[GManager.instance.dungeon_player.special_spell].at_name[0];
            else if(GManager.instance.dungeon_player.special_spell == -1)
                all_state[10].text = "才能技：無し";
        }
        else
        {
            all_state[0].text = "Proper name：" + GManager.instance.dungeon_player.unique_name[0];
            all_state[1].text = "Tribe：" + GManager.instance.all_monster[GManager.instance.dungeon_player.monster_id].monster_name[0];
            all_state[2].text = "HP：" + GManager.instance.dungeon_player.max_hp.ToString();
            all_state[3].text = "AT：" + GManager.instance.dungeon_player.at.ToString();
            all_state[4].text = "DF：" + GManager.instance.dungeon_player.df.ToString();
            all_state[5].text = "Speed：" + GManager.instance.dungeon_player.speed.ToString();
            all_state[6].text = "AT range：" + GManager.instance.dungeon_player.at_area.ToString();
            all_state[7].text = "AT speed：" + GManager.instance.dungeon_player.at_speed.ToString();
            all_state[8].text = "Growth rate：" + GManager.instance.dungeon_player.lvup_speed.ToString();
            all_state[9].text = "Tribal skill：" + GManager.instance.all_attack[GManager.instance.dungeon_player.normal_spell].at_name[0];
            if (GManager.instance.dungeon_player.special_spell != -1)
                all_state[10].text = "Special skill：" + GManager.instance.all_attack[GManager.instance.dungeon_player.special_spell].at_name[0];
            else if (GManager.instance.dungeon_player.special_spell == -1)
                all_state[10].text = "Special skill：None";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
