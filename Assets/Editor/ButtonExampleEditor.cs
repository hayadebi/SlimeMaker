using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonExample))]
public class ButtonExampleEditor : Editor
{
    [Multiline]
    public string target_stagedata;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ButtonExample buttonExample1 = (ButtonExample)target;

        // ボタンを押すと関数が実行されるボタンを表示
        if (GUILayout.Button("ステージデータをデコード"))
        {
            buttonExample1.StageDecoding();
        }

        ButtonExample buttonExample2 = (ButtonExample)target;

        // ボタンを押すと関数が実行されるボタンを表示
        if (GUILayout.Button("ステージデータをエンコード"))
        {
            buttonExample2.StageEncoding();
        }
    }
}
