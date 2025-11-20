using System.Collections.Generic;
using System.Data;
using System.IO;
using System;
using ExcelDataReader;
using UnityEditor;
using UnityEngine;
using LitJson;
using Unity.VisualScripting;
using System.Runtime.InteropServices;   // ← 你已经导入的这个

public static class ExcelToJson
{
    private const string EXCEL_FOLDER = "Assets/Excels";
    private const string JSON_FOLDER = "Assets/Config";
    private const string JsonToCs_FOLDER = "Assets/Script/Config";
    private const string ConfigCollection_FlODER = "Assets/Script/Root";

    [MenuItem("Tools/Build/生成Json配置表")]
    public static void ConvertAllExcelToJson()
    {
        if (!Directory.Exists(JSON_FOLDER))
        {
            Directory.CreateDirectory(JSON_FOLDER);
        }
        if (!Directory.Exists(JsonToCs_FOLDER))
        {
            Directory.CreateDirectory(JsonToCs_FOLDER);
        }
        foreach (var file in Directory.GetFiles(JSON_FOLDER, "*", SearchOption.AllDirectories))
        {
            if (file.EndsWith(".json") || file.EndsWith(".cs") || file.EndsWith(".meta"))
                File.Delete(file);
        }
        foreach (var file in Directory.GetFiles(JsonToCs_FOLDER, "*", SearchOption.AllDirectories))
        {
            if (file.EndsWith(".cs") || file.EndsWith(".meta"))
                File.Delete(file);
        }

        var files = Directory.GetFiles(EXCEL_FOLDER, "*.xlsx", SearchOption.AllDirectories);
        int count = 0;
        var variableContent = new System.Text.StringBuilder();
        var initContent = new System.Text.StringBuilder();
        foreach (var file in files)
        {
            if (file.Contains("~") || file.Contains("#")) continue;

            using var stream = File.OpenRead(file);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
            });
            var fileName = Path.GetFileNameWithoutExtension(file);
            foreach (DataTable table in dataSet.Tables)
            {
                var list = new List<Dictionary<string, object>>();
                int rowCount = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (rowCount < 1)
                    {
                        WriteJsonToCsFile(fileName, table.Columns, row);
                    }
                    else
                    {
                        var dict = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            var value = row[col];
                            dict[col.ColumnName] = value == DBNull.Value ? null : value;
                            Debug.Log($"列名：{col.ColumnName}，值：{value}");
                        }
                        list.Add(dict);
                    }
                    rowCount++;
                }

                // string sheetName = string.IsNullOrEmpty(table.TableName) ? "Sheet1" : table.TableName;
                // string fileName = Path.GetFileNameWithoutExtension(file) +
                //                  (dataSet.Tables.Count > 1 ? "_" + sheetName : "");

                string jsonPath = Path.Combine(JSON_FOLDER, fileName + "Config.json");

                // LitJson 一行搞定，美化输出，完美支持 null、int、long、double、bool、string
                string json = LitJsonChinesePretty(list);
                // 想更漂亮？再格式化一下（LitJson 自带）

                File.WriteAllText(jsonPath, json, System.Text.Encoding.UTF8);
                Debug.Log($"成功 → {fileName}Config.json");
                count++;
                break;
            }
            variableContent.AppendLine($"    private {fileName}ConfigCollection {fileName}ConfigCollection = new {fileName}ConfigCollection();");
            variableContent.AppendLine($"    public {fileName}ConfigCollection {fileName} => {fileName}ConfigCollection;");
            initContent.AppendLine($"        await {fileName}ConfigCollection.Init();");
        }
        WriteConfigCollectionFile(variableContent.ToString(), initContent.ToString());
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("全部完成！", $"成功转换 {count} 张配置表！", "OK");
    }
    private static void WriteConfigCollectionFile(string variableContent, string initContent)
    {
        var path = Path.Combine(ConfigCollection_FlODER, "Config.cs");
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("using Cysharp.Threading.Tasks;");
        sb.AppendLine();
        sb.AppendLine("public class Config");
        sb.AppendLine("{");
        sb.AppendLine(variableContent);
        sb.AppendLine("    public async UniTask Init()");
        sb.AppendLine("    {");
        sb.AppendLine(initContent);
        sb.AppendLine("    }");
        sb.AppendLine("}");
        File.WriteAllText(path, sb.ToString(), System.Text.Encoding.UTF8);
        Debug.Log($"成功 → Config.cs");
    }
    private static void WriteJsonToCsFile(string fileName, DataColumnCollection columns, DataRow row)
    {
        var csPath = Path.Combine(JsonToCs_FOLDER, fileName + "Config.cs");
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using LitJson;");
        sb.AppendLine("using Cysharp.Threading.Tasks;");
        sb.AppendLine();
        sb.AppendLine($"public class {fileName}Config");
        sb.AppendLine("{");
        sb.AppendLine();
        foreach (DataColumn col in columns)
        {
            sb.AppendLine($"    public  {row[col]} {col.ColumnName};");
        }
        sb.AppendLine("}");
        sb.AppendLine();
        sb.AppendLine($"public class {fileName}ConfigCollection");
        sb.AppendLine("{");
        sb.AppendLine($"    private Dictionary<int, {fileName}Config> m_Dict ;");
        sb.AppendLine($"    private List<{fileName}Config> m_List ;");
        sb.AppendLine();
        sb.AppendLine($"    public async UniTask Init()");
        sb.AppendLine($"    {{");
        sb.AppendLine($"        var json = await App.Instance.Res.LoadAssetAsync<TextAsset>(\"{fileName}Config\");");
        sb.AppendLine($"        m_List = JsonMapper.ToObject<List<{fileName}Config>>(json.text);");
        sb.AppendLine($"        m_Dict = new Dictionary<int, {fileName}Config>();");
        sb.AppendLine($"        foreach (var item in m_List)");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            m_Dict.Add((int)item.ID, item);");
        sb.AppendLine($"        }}");
        sb.AppendLine($"    }}");
        sb.AppendLine("}");

        File.WriteAllText(csPath, sb.ToString(), System.Text.Encoding.UTF8);
        Debug.Log($"成功 → {fileName}Config.cs");
    }

    private static string LitJsonChinesePretty(object obj)
    {
        var writer = new JsonWriter
        {
            PrettyPrint = true,
            IndentValue = 4      // 可改成 2，如果你喜欢紧凑一点
        };

        JsonMapper.ToJson(obj, writer);

        // 一行魔法：把所有 \uXXXX 还原成原始汉字
        return System.Text.RegularExpressions.Regex.Unescape(writer.ToString());
    }
}