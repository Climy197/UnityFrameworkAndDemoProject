using UnityEditor;
using UnityEngine;
using System.IO;

public static class ScriptGenerator
{
    public static void GenerateScript(string fileName, string content)
    {
        var settings = MyCustomSettings.GetOrCreateSettings();
        string folder = settings.BindPath;

        if (string.IsNullOrEmpty(folder))
        {
            Debug.LogError("BindPath 未设置，请在 Project Settings → MyCustomSettings 中设置一个有效路径。");
            return;
        }

        // 1. 若目录不存在则创建
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
            Debug.Log("自动创建目录：" + folder);
        }

        // 2. 拼接文件完整路径
        string filePath = Path.Combine(folder, fileName + ".cs");

        // 3. 写入内容
        File.WriteAllText(filePath, content);

        // 4. 通知 Unity 刷新
        AssetDatabase.Refresh();

        Debug.Log("脚本生成成功：" + filePath);
    }
}