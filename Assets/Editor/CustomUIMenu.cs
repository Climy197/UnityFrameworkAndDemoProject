using TMPro;
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



        // 创建 Button 根对象
        GameObject buttonGo = new GameObject("UIButton");
        buttonGo.AddComponent<RectTransform>();
        Button button = buttonGo.AddComponent<Button>();
        buttonGo.AddComponent<Image>(); // 默认 UI Button 带有 Image
        buttonGo.AddComponent<UIButton>();
        // 添加文本组件
        GameObject textGo = new GameObject("Text (TMP)");
        textGo.transform.SetParent(buttonGo.transform, false);
        textGo.AddComponent<TextMeshProUGUI>();

        RectTransform textRt = textGo.GetComponent<RectTransform>();
        textRt.anchorMin = Vector2.zero;
        textRt.anchorMax = Vector2.one;
        textRt.sizeDelta = Vector2.zero;

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
    [MenuItem("GameObject/UI/Text", false, 10)]
    public static void CreateCustomText(MenuCommand menuCommand)
    {
        GameObject parentGo = menuCommand.context as GameObject;
        GameObject go = new GameObject("UILabel");
        go.transform.SetParent(parentGo.transform, false);
        go.AddComponent<RectTransform>();
        var text = go.AddComponent<TextMeshProUGUI>();
        go.AddComponent<UILabel>();
    }

    [MenuItem("GameObject/UI/UIImage", false, 10)]
    public static void CreateCustomImage(MenuCommand menuCommand)
    {
        GameObject parentGo = menuCommand.context as GameObject;

        GameObject go = new GameObject("UIImage");
        go.transform.SetParent(parentGo.transform, false);
        go.AddComponent<RectTransform>();
        var image = go.AddComponent<Image>();
        go.AddComponent<UIImage>();
    }
    [MenuItem("GameObject/UI/UIScroll View", false, 11)]
    public static void CreateCustomScrollView(MenuCommand menuCommand)
    {
        // 确保存在 Canvas
        GameObject parentGo = menuCommand.context as GameObject;
        Canvas canvas = parentGo != null ? parentGo.GetComponentInParent<Canvas>() : null;

        if (canvas == null)
        {
            canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasGo = new GameObject("Canvas");
                canvas = canvasGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGo.AddComponent<CanvasScaler>();
                canvasGo.AddComponent<GraphicRaycaster>();
                parentGo = canvasGo;
            }
        }

        // 创建 ScrollView 主对象
        GameObject go = new GameObject("UIScrollView");
        go.transform.SetParent(parentGo.transform, false);

        // 添加 RectTransform 组件
        RectTransform scrollViewRect = go.AddComponent<RectTransform>();
        scrollViewRect.anchorMin = Vector2.zero;
        scrollViewRect.anchorMax = Vector2.one;
        scrollViewRect.offsetMin = Vector2.zero;
        scrollViewRect.offsetMax = Vector2.zero;

        // 添加 ScrollRect 组件
        ScrollRect scrollView = go.AddComponent<ScrollRect>();
        scrollView.horizontal = false; // 默认只启用垂直滚动
        scrollView.vertical = true;
        scrollView.movementType = ScrollRect.MovementType.Clamped;

        // 添加自定义 UIScrollView 组件（如果项目中存在）
        go.AddComponent<UIScrollView>();

        // 创建 Viewport
        GameObject viewport = new GameObject("Viewport");
        viewport.transform.SetParent(go.transform, false);
        RectTransform viewportRect = viewport.AddComponent<RectTransform>();
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.offsetMin = Vector2.zero;
        viewportRect.offsetMax = Vector2.zero;
        viewport.AddComponent<Image>(); // 添加背景图片
        Mask mask = viewport.AddComponent<Mask>();
        mask.showMaskGraphic = false;

        // 创建 Content
        GameObject content = new GameObject("Content");
        content.transform.SetParent(viewport.transform, false);
        RectTransform contentRect = content.AddComponent<RectTransform>();
        contentRect.anchorMin = Vector2.zero;
        contentRect.anchorMax = Vector2.one;
        contentRect.offsetMin = Vector2.zero;
        contentRect.offsetMax = Vector2.zero;
        contentRect.pivot = new Vector2(0.5f, 1f); // 顶部居中

        // 设置 ScrollRect 的 Viewport 和 Content
        scrollView.viewport = viewportRect;
        scrollView.content = contentRect;

        // 创建垂直滚动条
        CreateScrollbar(go.transform, scrollView, true);

        // 选中新创建的 ScrollView
        Selection.activeGameObject = go;
    }

    // 创建滚动条的辅助方法
    private static void CreateScrollbar(Transform parent, ScrollRect scrollRect, bool isVertical)
    {
        GameObject scrollbarGo = new GameObject(isVertical ? "Scrollbar Vertical" : "Scrollbar Horizontal");
        scrollbarGo.transform.SetParent(parent, false);

        RectTransform scrollbarRect = scrollbarGo.AddComponent<RectTransform>();
        if (isVertical)
        {
            scrollbarRect.anchorMin = new Vector2(1, 0);
            scrollbarRect.anchorMax = new Vector2(1, 1);
            scrollbarRect.offsetMin = new Vector2(-20, 0);
            scrollbarRect.offsetMax = new Vector2(0, 0);
        }
        else
        {
            scrollbarRect.anchorMin = new Vector2(0, 0);
            scrollbarRect.anchorMax = new Vector2(1, 0);
            scrollbarRect.offsetMin = new Vector2(0, 0);
            scrollbarRect.offsetMax = new Vector2(0, 20);
        }

        Scrollbar scrollbar = scrollbarGo.AddComponent<Scrollbar>();
        scrollbar.direction = isVertical ? Scrollbar.Direction.BottomToTop : Scrollbar.Direction.LeftToRight;

        // 设置滚动条的背景
        GameObject background = new GameObject("Background");
        background.transform.SetParent(scrollbarGo.transform, false);
        RectTransform backgroundRect = background.AddComponent<RectTransform>();
        backgroundRect.anchorMin = Vector2.zero;
        backgroundRect.anchorMax = Vector2.one;
        backgroundRect.offsetMin = Vector2.zero;
        backgroundRect.offsetMax = Vector2.zero;
        background.AddComponent<Image>();

        // 设置滚动条的滑动区域
        GameObject handle = new GameObject("Handle");
        handle.transform.SetParent(scrollbarGo.transform, false);
        RectTransform handleRect = handle.AddComponent<RectTransform>();
        if (isVertical)
        {
            handleRect.anchorMin = new Vector2(0, 0);
            handleRect.anchorMax = new Vector2(1, 0.3f);
            handleRect.offsetMin = Vector2.zero;
            handleRect.offsetMax = Vector2.zero;
        }
        else
        {
            handleRect.anchorMin = new Vector2(0, 0);
            handleRect.anchorMax = new Vector2(0.3f, 1);
            handleRect.offsetMin = Vector2.zero;
            handleRect.offsetMax = Vector2.zero;
        }
        handle.AddComponent<Image>();

        // 关联滚动条和 ScrollRect
        scrollbar.handleRect = handleRect;
        if (isVertical)
        {
            scrollRect.verticalScrollbar = scrollbar;
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        }
        else
        {
            scrollRect.horizontalScrollbar = scrollbar;
            scrollRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
        }
    }
}