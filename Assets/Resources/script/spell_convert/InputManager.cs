using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;
using UnityEngine.UI;
using System;

[System.Serializable]
public class SaveData
{
    public string a;
    public string b;
    public string c;
    public string d;
    public string e;
    public string f;
}

public class InputManager : MonoBehaviour
{
    private SaveData saveData = null;
    private SpellRestoration sr;
    public string temp_set;
    private string pass;
    void Start()
    {
        this.saveData = new SaveData();
        this.sr = new SpellRestoration();
        //pass = OnClickButton01(temp_set);
        //print(pass);
        //print(OnClickButton02(pass));
    }
    
    public string OnClickButton01(string temptext)
    {
        this.saveData.a = temptext;
        var spellRestoration = this.sr.ToSpellRestoration(this.saveData);
        return spellRestoration;
    }
    
    public string OnClickButton02(string temptext)
    {
        var spellRestoration = temptext;

        var tempSaveData = this.sr.FromSpellRestoration<SaveData>(spellRestoration);

        if (tempSaveData != null) {
            return tempSaveData.a;
        } else {
            Debug.Log("FromSpellRestoration() Error");
        }
        return "";
    }

    public int StateConverter(int maxnum = 1)
    {
        
        char[] tmp_char = GManager.instance.temptext.ToCharArray();
        int tmp_n = (int)tmp_char[0];
        print(tmp_n);
        if (tmp_n < maxnum)
        {
            return tmp_n;
        }
        else if (tmp_n % maxnum < maxnum)
        {
            return (tmp_n % maxnum);
        }
        return 0;
    }
}
