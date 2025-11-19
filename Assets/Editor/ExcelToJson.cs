using System.Collections.Generic;
using System.Data;
using System.IO;
using System;
using ExcelDataReader;
using UnityEditor;
using UnityEngine;
using LitJson;   // ← 你已经导入的这个

public static class ExcelToJson
{
    private const string EXCEL_FOLDER = "Assets/Excels";
    private const string JSON_FOLDER = "Assets/Config";
    private const string JsonToCs_FOLDER = "Assets/Scripts/Config";

    [MenuItem("Tools/Build/生成Json配置表")]
    public static void ConvertAllExcelToJson()
    {
        if (!Directory.Exists(JSON_FOLDER))
        {
            Directory.CreateDirectory(JSON_FOLDER);
        }
        foreach (var file in Directory.GetFiles(JSON_FOLDER, "*", SearchOption.AllDirectories))
        {
            if (file.EndsWith(".json") || file.EndsWith(".cs") || file.EndsWith(".meta"))
                File.Delete(file);
        }

        var files = Directory.GetFiles(EXCEL_FOLDER, "*.xlsx", SearchOption.AllDirectories);
        int count = 0;
        foreach (var file in files)
        {
            if (file.Contains("~") || file.Contains("#")) continue;

            using var stream = File.OpenRead(file);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
            });

            foreach (DataTable table in dataSet.Tables)
            {
                var list = new List<Dictionary<string, object>>();
                int rowCount = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (rowCount <= 1)
                    {

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

                string sheetName = string.IsNullOrEmpty(table.TableName) ? "Sheet1" : table.TableName;
                string fileName = Path.GetFileNameWithoutExtension(file) +
                                 (dataSet.Tables.Count > 1 ? "_" + sheetName : "");
                string jsonPath = Path.Combine(JSON_FOLDER, fileName + ".json");

                // LitJson 一行搞定，美化输出，完美支持 null、int、long、double、bool、string
                string json = LitJsonChinesePretty(list);
                // 想更漂亮？再格式化一下（LitJson 自带）

                File.WriteAllText(jsonPath, json, System.Text.Encoding.UTF8);
                Debug.Log($"成功 → {fileName}.json");
                count++;
            }
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("全部完成！", $"成功转换 {count} 张配置表！", "OK");
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