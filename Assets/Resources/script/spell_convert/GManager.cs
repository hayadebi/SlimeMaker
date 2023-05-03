using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.IO.Compression;
public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    [Header("次のコメントまで汎用変数")]
    public int isEnglish = 0;
    public bool walktrg = true;
    public float seMax = 0.008f;
    public float audioMax = 0.01f;
    public bool over = false;
    public int setmenu = 0;
    public bool ESCtrg = false;
    public int setrg = -1;
    public int[] manager_trg;
    public GameObject[] all_ui;
    [Header("【クイックライクの変数】")]
    public GameObject[] magic_effectobj;
    [System.Serializable]
    public struct MonsterID
    {
        public string[] monster_name;
        [Multiline]
        public string[] monster_script;
        public string[] unique_namejp;
        public string[] unique_nameen;
        public int hp;
        public int at;
        public int df;
        public float speed;
        public float at_area;
        public float at_speed;
        public float lvup_speed;
        public int[] normal_atid;
        [Header("percentはID値を記す、同じIDが多いものは確率が高い")]
        public int[] lvup_stateid_percent;
        public int[] drop_itemid_percent;
        public int[] get_specalatid;
        public GameObject mon_obj;
    }
    [Header("種族情報※ステータスは変動前の初期値")]
    public MonsterID[] all_monster;

    [System.Serializable]
    public struct ItemID
    {
        public string[] item_name;
        [Multiline]
        public string[] script;
        public int storage_num;
        public int get_trg;
        public int event_id;
        public Sprite item_sprite;
    }
    [Header("アイテム情報")]
    public ItemID[] all_item;

    [System.Serializable]
    public struct AttackID
    {
        public string[] at_name;
        [Multiline]
        public string[] script;
        public int event_id;
        public GameObject atlv_obj;//-1
        public float at_area;
        public int at;
        public int lv;//0は使えない
    }
    [Header("攻撃技情報")]
    public AttackID[] all_attack;

    [System.Serializable]
    public struct StateID
    {
        public string[] state_name;
        [Multiline]
        public string[] script;
        public int event_id;
        public Sprite state_sprite;
    }
    [Header("LvUP時の上昇ステータス情報")]
    public StateID[] all_state;

    [System.Serializable]
    public struct PlayerID
    {
        public int monster_id;
        public string qr_pass;
        public string[] unique_name;
        [Header("ステータス変動後")]
        public int max_hp;
        public int hp;
        public int at;
        public int df;
        public float speed;
        public float at_area;
        public float at_speed;
        public float lvup_speed;
        public int lv;
        public int nextlvup_num;
        public int get_lvnum;
        public float cooltime;
        public int normal_spell;
        public int special_spell;
    }
    [Header("操作キャラ情報")]
    public PlayerID dungeon_player;
    [Header("術士の待機状態、trueだとついて来る")]
    public bool mainplayer_mode = true;
    [Header("入れ替え控えのモンスター")]
    public string[] stock_monster;
    [Header("ロストモンスター")]
    public string[] lost_monster;
    public int lost_num;
    [Header("ダンジョン外では最大10個までアイテムを持てる、IDを記す")]
    public int[] player_hand;

    public string temptext = "";
    [Header("【2匹はスライム兄弟メーカーの変数】")]
    public bool storymode = false;
    public int select_stage = 0;//storymodeがtrueの時のみ有効
    public GameObject[] stageobj_id;
    public Sprite[] stageobj_createimg;
    [System.Serializable]
    public struct StageobjName
    {
        public string[] name;
        public string[] script;
    }
    public StageobjName[] stageobj_data;
    public bool debug_trg = false;
    public int goal_num = 0;
    public bool cleartrg = false;
    public float cleartime = 0;
    public bool slime_titleanim = false;
    public bool slime_titleui = false;
    [Header("ストーリー用18×26ステージ。8種類")]
    public TextAsset[] story_stagefile;
    [System.Serializable]
    public struct TestStage
    {
        public int[] test_x;
    }
    [Header("テストプレイ用18×26ステージ盤面")]
    public TestStage[] test_y;
    public bool notsay = false;
    public int gimmick_length = 13;
    public float reset_time = 0;
    public int set_createid;
    public bool fixed_createid = false;
    public bool dx_mode = false;
    public int dx_stageid = -1;
    public TextAsset[] dx_filename;

    public string[] not_word;
    public bool sorttrg = true;
    public string check_onword = "";
    public string check_notword = "";
    public string tmp_stagename="";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(instance.reset_time >= 0)
        {
            instance.reset_time -= Time.deltaTime;
        }
    }
    public byte[] ComporessGZIP(string _str)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var inflateStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                using (var writer = new StreamWriter(inflateStream))
                {
                    writer.Write(_str);
                }
            }
            return memoryStream.ToArray();
        }
    }
    /// <summary>
    /// GZIP圧縮されたバッファを解凍します。
    /// </summary>
    public string DeComporessGZIP(byte[] _bytes)
    {
        using (var memoryStream = new MemoryStream(_bytes))
        {
            using (var deflateStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(deflateStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}