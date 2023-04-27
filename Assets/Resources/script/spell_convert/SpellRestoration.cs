using UnityEngine;
using System.Text;

public class SpellRestoration
{
    public string[,] convertTable = new string[65, 2] {
        { "A", "‚ " },
        { "B", "‚¢" },
        { "C", "‚¤" },
        { "D", "‚¦" },
        { "E", "‚¨" },
        { "F", "‚©" },
        { "G", "‚«" },
        { "H", "‚­" },
        { "I", "‚¯" },
        { "J", "‚±" },
        { "K", "‚³" },
        { "L", "‚µ" },
        { "M", "‚·" },
        { "N", "‚¹" },
        { "O", "‚»" },
        { "P", "‚½" },
        { "Q", "‚¿" },
        { "R", "‚Â" },
        { "S", "‚Ä" },
        { "T", "‚Æ" },
        { "U", "‚È" },
        { "V", "‚É" },
        { "W", "‚Ê" },
        { "X", "‚Ë" },
        { "Y", "‚Ì" },
        { "Z", "‚Í" },
        { "a", "‚Ð" },
        { "b", "‚Ó" },
        { "c", "‚Ö" },
        { "d", "‚Ù" },
        { "e", "‚Ü" },
        { "f", "‚Ý" },
        { "g", "‚Þ" },
        { "h", "‚ß" },
        { "i", "‚à" },
        { "j", "‚â" },
        { "k", "‚ä" },
        { "l", "‚æ" },
        { "m", "‚ç" },
        { "n", "‚è" },
        { "o", "‚é" },
        { "p", "‚ê" },
        { "q", "‚ë" },
        { "r", "‚í" },
        { "s", "‚ª" },
        { "t", "‚¬" },
        { "u", "‚®" },
        { "v", "‚°" },
        { "w", "‚²" },
        { "x", "‚´" },
        { "y", "‚¶" },
        { "z", "‚¸" },
        { "0", "‚º" },
        { "1", "‚¼" },
        { "2", "‚¾" },
        { "3", "‚À" },
        { "4", "‚Ã" },
        { "5", "‚Å" },
        { "6", "‚Ç" },
        { "7", "‚Î" },
        { "8", "‚Ñ" },
        { "9", "‚Ô" },
        { "+", "‚×" },
        { "/", "‚Ú" },
        { "=", "‚ñ" }
    };

    public string ToSpellRestoration(object data)
    {
        string json = JsonUtility.ToJson(data);
        string base64string = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        string spellRestoration = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

        for(int index=0; index < convertTable.GetLength(0); index++){
            spellRestoration = spellRestoration.Replace(convertTable[index, 0], convertTable[index, 1]);
        }

        return spellRestoration;
    }

    public T FromSpellRestoration<T>(string spellRestoration)
    {
        for(int index=0; index < convertTable.GetLength(0); index++){
            spellRestoration = spellRestoration.Replace(convertTable[index, 1], convertTable[index, 0]);
        }

        try {
            byte[] byteArray = System.Convert.FromBase64String(spellRestoration);
            string jsonBack = Encoding.UTF8.GetString(byteArray);
            var restoreData = JsonUtility.FromJson<T>(jsonBack);

            return restoreData;
        }
        catch (System.FormatException) {
            return default(T);
        }
    }
}
