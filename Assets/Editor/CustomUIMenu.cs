using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// 脚本必须放在名为 "Editor" 的文件夹内
public static class OverrideUIMenu
{
    // ********************************************************************
    // 覆盖默认的 Button 创建逻辑
    // 路径与默认的 Button 菜单项完全相同
    // 优先级设置为 10，确保它在默认 UI 菜单项附近或之前执行
    // ********************************************************************
    [MenuItem("GameObject/UI/Button", false, 10)]
    public static void CreateCustomButton(MenuCommand menuCommand)
    {
        // =========================================================
        // 步骤 1: 确保存在 Canvas
        // 这是自定义创建 UI 组件的关键，你需要检查并创建 Canvas
        // =========================================================
        GameObject parentGo = menuCommand.context as GameObject;
        Canvas canvas = parentGo != null ? parentGo.GetComponentInParent<Canvas>() : null;

        // 如果没有选中 Canvas 或其子对象，则创建一个新的 Canvas
        if (canvas == null)
        {
            // 查找场景中是否有 Canvas
            canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                // 创建一个标准的 Canvas GameObject
                GameObject canvasGo = new GameObject("Canvas");
                canvas = canvasGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGo.AddComponent<CanvasScaler>();
                canvasGo.AddComponent<GraphicRaycaster>();
                
                // 注册创建 Canvas 的 Undo
                Undo.RegisterCreatedObjectUndo(canvasGo, "Create Canvas");
            }
            parentGo = canvas.gameObject;
        }

        // =========================================================
        // 步骤 2: 创建自定义的 Button 预制件或结构
        // ---------------------------------------------------------
        // 在这里，你可以替换为实例化一个项目中的 Prefab，
        // 或者像下面这样手动构建一个自定义结构的 Button。
        // =========================================================
        
        // ** 示例: 创建一个带有 TextMeshProUGUI 的 Button **
        
        // 检查项目是否安装了 TextMeshPro
        bool hasTMP = System.Type.GetType("TMPro.TextMeshProUGUI, Unity.TextMeshPro") != null;

        // 创建 Button 根对象
        GameObject buttonGo = new GameObject("MyCustomButton");
        buttonGo.AddComponent<RectTransform>();
        Button button = buttonGo.AddComponent<Button>();
        buttonGo.AddComponent<Image>(); // 默认 UI Button 带有 Image

        // 添加文本组件
        GameObject textGo = new GameObject("Text (TMP)");
        textGo.transform.SetParent(buttonGo.transform, false); 
        
        if (hasTMP)
        {
            // 如果使用 TextMeshPro，需要添加 TMPro 命名空间
            // UnityEditor.UI.MenuOptions.AddTextMeshProComponent(buttonGo.transform); 
            // 这是一个内部方法，为了通用性，我们手动添加并设置。
            var tmpText = textGo.AddComponent(System.Type.GetType("TMPro.TextMeshProUGUI, Unity.TextMeshPro")) as Component;
            if (tmpText != null)
            {
                // 设置 TextMeshProUGUI 属性（需要反射或导入 TMPro 命名空间）
                // 简单起见，这里假设你能直接设置
                // ((TMPro.TextMeshProUGUI)tmpText).text = "Custom Button";
            }
        }
        else
        {
            // 否则，使用默认的 Text 组件
            Text textComponent = textGo.AddComponent<Text>();
            textComponent.text = "Custom Button";
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.color = Color.black; 
        }

        // 设置文本 RectTransform 覆盖整个 Button
        RectTransform textRt = textGo.GetComponent<RectTransform>();
        textRt.anchorMin = Vector2.zero;
        textRt.anchorMax = Vector2.one;
        textRt.sizeDelta = Vector2.zero;
        
        // =========================================================
        // 步骤 3: 设置父级、对齐并注册 Undo
        // =========================================================
        
        // 确保它被设置为正确的父级，并且在 Hierarchy 中正确对齐
        GameObjectUtility.SetParentAndAlign(buttonGo, parentGo);
        
        // 注册创建 Button 的 Undo
        Undo.RegisterCreatedObjectUndo(buttonGo, "Create Custom Button");
        
        // 选中新创建的对象
        Selection.activeObject = buttonGo;
        
        // 设置初始大小（可选）
        RectTransform rt = buttonGo.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(160, 30);
        }
        
        // 额外提示：如果你想完全防止默认创建逻辑运行，只需提供一个同路径、同优先级的方法即可。
        // 你不需要手动调用默认的逻辑。
    }

    // ********************************************************************
    // 覆盖默认 Button 的验证逻辑 (可选)
    // ********************************************************************
    // [MenuItem("GameObject/UI/Button", true, 10)]
    // public static bool ValidateCreateCustomButton()
    // {
    //     // 返回 true 使菜单项启用，返回 false 禁用它
    //     // 这里我们让它始终启用
    //     return true; 
    // }
}