using UnityEngine;
using System.Text;

public class SpellRestoration
{
    public string[,] convertTable = new string[65, 2] {
        { "A", "A" }, 
        { "B", "B" }, 
        { "C", "C" }, 
        { "D", "D" }, 
        { "E", "E" }, 
        { "F", "F" }, 
        { "G", "G" }, 
        { "H", "H" }, 
        { "I", "I" }, 
        { "J", "J" }, 
        { "K", "K" }, 
        { "L", "L" }, 
        { "M", "M" }, 
        { "N", "N" }, 
        { "O", "O" }, 
        { "P", "P" }, 
        { "Q", "Q" }, 
        { "R", "R" }, 
        { "S", "S" }, 
        { "T", "T" }, 
        { "U", "U" }, 
        { "V", "V" }, 
        { "W", "W" }, 
        { "X", "X" }, 
        { "Y", "Y" }, 
        { "Z", "Z" }, 
        { "a", "a" }, 
        { "b", "b" }, 
        { "c", "c" }, 
        { "d", "d" }, 
        { "e", "e" }, 
        { "f", "f" }, 
        { "g", "g" }, 
        { "h", "h" }, 
        { "i", "i" }, 
        { "j", "j" }, 
        { "k", "k" }, 
        { "l", "l" }, 
        { "m", "m" }, 
        { "n", "n" }, 
        { "o", "o" }, 
        { "p", "p" }, 
        { "q", "q" }, 
        { "r", "r" }, 
        { "s", "s" }, 
        { "t", "t" }, 
        { "u", "u" }, 
        { "v", "v" }, 
        { "w", "w" }, 
        { "x", "x" }, 
        { "y", "y" }, 
        { "z", "z" }, 
        { "A", "A" }, 
        { "B", "B" }, 
        { "C", "C" }, 
        { "D", "D" }, 
        { "E", "E" }, 
        { "F", "F" }, 
        { "G", "G" }, 
        { "H", "F" }, 
        { "I", "I" }, 
        { "J", "J" }, 
        { "K", "K" }, 
        { "L", "L" }, 
        { "M", "M" }, 
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
