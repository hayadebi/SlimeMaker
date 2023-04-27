using UnityEngine;
using System.Text;

public class SpellRestoration
{
    public string[,] convertTable = new string[65, 2] {
        { "A", "��" },
        { "B", "��" },
        { "C", "��" },
        { "D", "��" },
        { "E", "��" },
        { "F", "��" },
        { "G", "��" },
        { "H", "��" },
        { "I", "��" },
        { "J", "��" },
        { "K", "��" },
        { "L", "��" },
        { "M", "��" },
        { "N", "��" },
        { "O", "��" },
        { "P", "��" },
        { "Q", "��" },
        { "R", "��" },
        { "S", "��" },
        { "T", "��" },
        { "U", "��" },
        { "V", "��" },
        { "W", "��" },
        { "X", "��" },
        { "Y", "��" },
        { "Z", "��" },
        { "a", "��" },
        { "b", "��" },
        { "c", "��" },
        { "d", "��" },
        { "e", "��" },
        { "f", "��" },
        { "g", "��" },
        { "h", "��" },
        { "i", "��" },
        { "j", "��" },
        { "k", "��" },
        { "l", "��" },
        { "m", "��" },
        { "n", "��" },
        { "o", "��" },
        { "p", "��" },
        { "q", "��" },
        { "r", "��" },
        { "s", "��" },
        { "t", "��" },
        { "u", "��" },
        { "v", "��" },
        { "w", "��" },
        { "x", "��" },
        { "y", "��" },
        { "z", "��" },
        { "0", "��" },
        { "1", "��" },
        { "2", "��" },
        { "3", "��" },
        { "4", "��" },
        { "5", "��" },
        { "6", "��" },
        { "7", "��" },
        { "8", "��" },
        { "9", "��" },
        { "+", "��" },
        { "/", "��" },
        { "=", "��" }
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
