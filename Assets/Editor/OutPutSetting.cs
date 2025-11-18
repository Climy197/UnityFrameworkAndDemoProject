using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "BindPathConfig", menuName = "Config/Bind Path Config")]
public class MyCustomSettings : ScriptableObject
{
    public string BindPath;

    private static MyCustomSettings instance;

    public static MyCustomSettings GetOrCreateSettings()
    {
        const string path = "Assets/ProjectSettings/BindPathConfig.asset";
        instance = UnityEditor.AssetDatabase.LoadAssetAtPath<MyCustomSettings>(path);
        if (instance == null)
        {
            instance = CreateInstance<MyCustomSettings>();
            UnityEditor.AssetDatabase.CreateAsset(instance, path);
            UnityEditor.AssetDatabase.SaveAssets();
        }
        return instance;
    }
    public static string GetBindPath()
    {
        const string defaultPath = "Assets/ProjectSettings/BindPathConfig.asset";
        var setting = UnityEditor.AssetDatabase.LoadAssetAtPath<MyCustomSettings>(defaultPath);
        if (setting == null)
        {
            return "Assets/Script/Binder";
        }
        return setting.BindPath;
    }
}

// 这个类负责在 ProjectSettings 里显示
static class MyCustomSettingsProvider
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        var provider = new SettingsProvider("Project/My Custom Settings", SettingsScope.Project)
        {
            label = "My Custom Settings",

            guiHandler = (searchContext) =>
            {
                var settings = MyCustomSettings.GetOrCreateSettings();

                EditorGUILayout.LabelField("自定义路径设置", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();

                settings.BindPath = EditorGUILayout.TextField("路径", settings.BindPath);

                if (GUILayout.Button("Browse...", GUILayout.MaxWidth(100)))
                {
                    string selected = EditorUtility.OpenFolderPanel("选择自定义路径", settings.BindPath, "");
                    if (!string.IsNullOrEmpty(selected))
                    {
                        settings.BindPath = selected;
                        EditorUtility.SetDirty(settings);
                    }
                }

                EditorGUILayout.EndHorizontal();
            },

            keywords = new System.Collections.Generic.HashSet<string>(new[] { "path", "custom", "folder" })
        };

        return provider;
    }
}