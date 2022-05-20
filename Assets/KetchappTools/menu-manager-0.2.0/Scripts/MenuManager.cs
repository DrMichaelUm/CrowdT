using KetchappTools.Core.DesignPatterns;
using KetchappTools.Menus.Transitions;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KetchappTools.Menus
{
    public class MenuManager : Singleton<MenuManager>
    {
        #region Properties

#if UNITY_EDITOR

        [UnityEditor.MenuItem("GameObject/KetchappTools/Menus/Manager", false, 1)]
        public static void CreateMenuManager()
        {
            GameObject menuManagerGameObject = new GameObject(typeof(MenuManager).Name);
            UnityEditor.Undo.RegisterCreatedObjectUndo(menuManagerGameObject, "Create " + typeof(MenuManager).Name);
            menuManagerGameObject.AddComponent<MenuManager>();
            UnityEditor.Selection.activeGameObject = menuManagerGameObject;
        }

        [SerializeField, FoldoutGroup("Editor"), LabelText("Name")]
        private string newMenuName = "";

        [SerializeField, FoldoutGroup("Editor"), HideInInspector]
        private string newMenuClassName = "";

        [SerializeField, FoldoutGroup("Editor"), HideInInspector]
        private bool generatePrefabAfterReload = false;

        [SerializeField, FoldoutGroup("Editor"), LabelText("Add To MenuList")]
        private bool newMenuAddToMenuList = true;

        [SerializeField, FoldoutGroup("Editor"), LabelText("Menu List Path"), ShowIf("ShowIfNewMenuAddToMenuListPath"), FolderPath]
        private string newMenuAddToMenuListPath = "Assets/Scripts/Menus/MenuLists";
        private bool ShowIfNewMenuAddToMenuListPath()
        {
            return newMenuAddToMenuList && !newMenuScript;
        }

        [SerializeField, FoldoutGroup("Editor"), LabelText("Script")]
        private bool newMenuScript = true;

        [SerializeField, FoldoutGroup("Editor"), LabelText("Script Path"), ShowIf("newMenuScript", true), FolderPath]
        private string newMenuScriptPath = "Assets/Scripts/Menus";

        [SerializeField, FoldoutGroup("Editor"), LabelText("Prefab")]
        private bool newMenuPrefab = true;

        [SerializeField, FoldoutGroup("Editor"), LabelText("Prefab Path"), ShowIf("newMenuPrefab", true), FolderPath]
        private string newMenuPrefabPath = "Assets/Prefabs/Menus";

        [SerializeField, FoldoutGroup("Editor"), LabelText("Safe Area")]
        private bool newMenuSafeArea = true;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Render Mode")]
        private RenderMode newMenuCanvasRenderMode = RenderMode.ScreenSpaceOverlay;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Scale Mode")]
        private CanvasScaler.ScaleMode newMenuCanvasScalerScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Scale Mode"), ShowIf("newMenuCanvasScalerScaleMode", CanvasScaler.ScaleMode.ConstantPixelSize)]
        private float newMenuCanvasScaleFactor = 1f;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Reference Resolution"), ShowIf("newMenuCanvasScalerScaleMode", CanvasScaler.ScaleMode.ScaleWithScreenSize)]
        private Vector2 newMenuCanvasReferenceResolution = new Vector2(1500, 1500);

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Screen Match Mode"), ShowIf("newMenuCanvasScalerScaleMode", CanvasScaler.ScaleMode.ScaleWithScreenSize)]
        private CanvasScaler.ScreenMatchMode newMenuCanvasScreenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Match Width Or Height"), ShowIf("newMenuCanvasScalerScaleMode", CanvasScaler.ScaleMode.ScaleWithScreenSize)]
        private float newMenuCanvasMatchWidthOrHeight = 0.75f;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Physical Unit"), ShowIf("newMenuCanvasScalerScaleMode", CanvasScaler.ScaleMode.ConstantPhysicalSize)]
        private CanvasScaler.Unit newMenuCanvasPhysicalUnit = CanvasScaler.Unit.Points;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Fallback Screen DPI"), ShowIf("newMenuCanvasScalerScaleMode", CanvasScaler.ScaleMode.ConstantPhysicalSize)]
        private float newMenuCanvasFallbackScreenDPI = 96f;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Default Sprite DPI"), ShowIf("newMenuCanvasScalerScaleMode", CanvasScaler.ScaleMode.ConstantPhysicalSize)]
        private float newMenuCanvasDefaultSpriteDPI = 96f;

        [SerializeField, FoldoutGroup("Editor/Canvas Settings"), LabelText("Reference Pixels Per Unit")]
        private float newMenuCanvasReferencePixelsPerUnit = 100f;

        [Button, FoldoutGroup("Editor")]
        private void CreateNewMenu()
        {
            // Check if a menu with the same name exist
            Menu[] childrenMenus = GetComponentsInChildren<Menu>(true);
            if (newMenuName == "")
            {
                UnityEditor.EditorUtility.DisplayDialog("Error!", "Your menu need a name.", "Ok");
                return;
            }
            if (childrenMenus.FirstOrDefault(x => x.gameObject.name == newMenuName) != null)
            {
                UnityEditor.EditorUtility.DisplayDialog(newMenuName + " already exist!", "A menu with the same name already exist.", "Ok");
                return;
            }

            // Set variables
            newMenuClassName = SanitizeClassName(newMenuName);

            // Display progress bar
            UnityEditor.EditorUtility.DisplayProgressBar("Create New Menu", "Starting", 0f);

            // Create the script
            if (newMenuScript)
            {
                CheckIfPathExist(newMenuScriptPath);
                File.WriteAllText(UnityEditor.AssetDatabase.GenerateUniqueAssetPath(newMenuScriptPath + "/" + newMenuClassName + ".cs"), CreateClass(newMenuClassName, newMenuAddToMenuList));
                generatePrefabAfterReload = true;
                UnityEditor.EditorUtility.DisplayProgressBar("Create New Menu", "Compiling menu class", 0.1f);
                UnityEditor.AssetDatabase.Refresh();
            }
            else if(newMenuAddToMenuList)
            {
                CheckIfPathExist(newMenuAddToMenuListPath);
                File.WriteAllText(UnityEditor.AssetDatabase.GenerateUniqueAssetPath(newMenuAddToMenuListPath + "/MenuList" + newMenuClassName + ".cs"), CreateMenuList("Menu", newMenuClassName, true));
                generatePrefabAfterReload = true;
                UnityEditor.EditorUtility.DisplayProgressBar("Regenerating Menu List", "Adding new menu to the menu list", 0.1f);
                UnityEditor.AssetDatabase.Refresh();
            }
            else
                CreatePrefab();
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void DidReloadScripts()
        {
            MenuManager menuManager = FindObjectOfType<MenuManager>();
            if(menuManager && menuManager.generatePrefabAfterReload)
            {
                menuManager.generatePrefabAfterReload = false;
                menuManager.CreatePrefab();
            }
        }

        private void CreatePrefab()
        {
            UnityEditor.EditorUtility.DisplayProgressBar("Create New Menu", "Creating prefab", 0.5f);

            Type menuType = typeof(KetchappTools.Menus.Menu);
            if(newMenuScript)
            {
                string typeName = "Menus." + newMenuClassName;
                System.Type bMenuType = System.Type.GetType(typeName);
                if (bMenuType != null)
                    menuType = bMenuType;
            }

            // Create menu gameobject
            GameObject menuGameObject = new GameObject(newMenuName);
            menuGameObject.transform.SetParent(transform);
            Menu menu = menuGameObject.AddComponent(menuType) as Menu;
            UnityEditor.Selection.activeGameObject = menuGameObject;

            // Setup canvas
            Canvas canvas = menuGameObject.GetComponent<Canvas>();
            canvas.renderMode = newMenuCanvasRenderMode;
            CanvasScaler canvasScaler = menuGameObject.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = newMenuCanvasScalerScaleMode;
            canvasScaler.scaleFactor = newMenuCanvasScaleFactor;
            canvasScaler.referenceResolution = newMenuCanvasReferenceResolution;
            canvasScaler.screenMatchMode = newMenuCanvasScreenMatchMode;
            canvasScaler.matchWidthOrHeight = newMenuCanvasMatchWidthOrHeight;
            canvasScaler.physicalUnit = newMenuCanvasPhysicalUnit;
            canvasScaler.fallbackScreenDPI = newMenuCanvasFallbackScreenDPI;
            canvasScaler.defaultSpriteDPI = newMenuCanvasDefaultSpriteDPI;
            canvasScaler.referencePixelsPerUnit = newMenuCanvasReferencePixelsPerUnit;

            // Create body gameobject
            GameObject bodyGameObject = new GameObject("Body");
            RectTransform bodyRectTransform = AddFullSizeRectTransform(bodyGameObject, menuGameObject.transform);
            Body body = bodyGameObject.AddComponent<Body>();
            menu.EditorSetBody(body);

            // Create safearea gameobject
            if (newMenuSafeArea)
            {
                GameObject safeAreaGameObject = new GameObject("SafeArea");
                safeAreaGameObject.AddComponent<KetchappTools.Menus.Utils.SafeArea>();
                RectTransform safeAreaRectTransform = AddFullSizeRectTransform(safeAreaGameObject, bodyRectTransform);
            }

            UnityEditor.EditorUtility.DisplayProgressBar("Create New Menu", "Saving prefab", 0.6f);

            // Create prefab
            if (newMenuPrefab)
            {
                CheckIfPathExist(newMenuPrefabPath);
                GameObject prefabMenuGameObject = UnityEditor.PrefabUtility.SaveAsPrefabAsset(menuGameObject, UnityEditor.AssetDatabase.GenerateUniqueAssetPath(newMenuPrefabPath + "/" + newMenuName + ".prefab"));
                GameObject prefabMenuGameObjectInstanciated = UnityEditor.PrefabUtility.InstantiatePrefab(prefabMenuGameObject) as GameObject;
                prefabMenuGameObjectInstanciated.transform.SetParent(menuGameObject.transform.parent);
                GameObject.DestroyImmediate(menuGameObject);
                UnityEditor.Selection.activeGameObject = prefabMenuGameObjectInstanciated;
                // Update menu variables
                menu = prefabMenuGameObjectInstanciated.GetComponent(menuType) as Menu;
            }

            // Set the menu in the menu list
            if (newMenuAddToMenuList)
            {
                menuList.EditorSetMenu(menu, newMenuClassName);
                UnityEditor.EditorUtility.SetDirty(this);
            }

            UnityEditor.EditorUtility.ClearProgressBar();
        }

        private static void CheckIfPathExist(string path)
        {
            if (!Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
            }
        }

        private static string CreateClass(string className, bool addToMenuList)
        {
            string output = "";

            output += "using UnityEngine;\n";
            output += "using System;\n";
            output += "using System.Collections;\n";
            output += "using System.Collections.Generic;\n";
            output += "\n";
            output += "namespace Menus\n";
            output += "{\n";
            output += "\tpublic class " + className + " : KetchappTools.Menus.Menu\n";
            output += "\t{\n";
            output += "\t\t#region Properties\n";
            output += "\t\t\n";
            output += "\t\t#endregion\n";
            output += "\t\t\n";
            output += "\t\t#region Menu Methods\n";
            output += "\t\t\n";
            output += "\t\t// Call at the start of the show animation\n";
            output += "\t\tprotected override void OnShowStart()\n";
            output += "\t\t{\n";
            output += "\t\t\tbase.OnShowStart();\n";
            output += "\t\t}\n";
            output += "\t\t\n";
            output += "\t\t// Call at the end of the show animation\n";
            output += "\t\tprotected override void OnShowEnd()\n";
            output += "\t\t{\n";
            output += "\t\t\tbase.OnShowEnd();\n";
            output += "\t\t}\n";
            output += "\t\t\n";
            output += "\t\t// Call at the start of the hide animation\n";
            output += "\t\tprotected override void OnHideStart()\n";
            output += "\t\t{\n";
            output += "\t\t\tbase.OnHideStart();\n";
            output += "\t\t}\n";
            output += "\t\t\n";
            output += "\t\t// Call at the end of the hide animation\n";
            output += "\t\tprotected override void OnHideEnd()\n";
            output += "\t\t{\n";
            output += "\t\t\tbase.OnHideEnd();\n";
            output += "\t\t}\n";
            output += "\t\t\n";
            output += "\t\t#endregion\n";
            output += "\t}\n";
            output += "}";

            if(addToMenuList)
                output += CreateMenuList("global::Menus." + className, className);

            return output;
        }

        private static string CreateMenuList(string className, string menuName, bool addUsing = false)
        {
            string menuNamePrivate = char.ToLower(menuName[0]) + menuName.Substring(1);
            string menuNamePublic = char.ToUpper(menuName[0]) + menuName.Substring(1);

            string output = "";

            if(addUsing)
            {
                output += "using UnityEngine;\n";
            }

            output += "\n";
            output += "namespace KetchappTools.Menus\n";
            output += "{\n";
            output += "\tpublic partial class MenuList\n";
            output += "\t{\n";
            output += "\t\t[SerializeField]\n";
            output += "\t\tprivate " + className + " " + menuNamePrivate + ";\n";
            output += "\t\tpublic " + className + " " + menuNamePublic + " => " + menuNamePrivate + ";\n";
            output += "\t}\n";
            output += "}\n";

            return output;
        }

        private static RectTransform AddFullSizeRectTransform(GameObject gameObject, Transform parent = null)
        {
            RectTransform bRectTransform = gameObject.AddComponent<RectTransform>();
            bRectTransform.SetParent(parent);
            bRectTransform.anchorMin = Vector2.zero;
            bRectTransform.anchorMax = Vector2.one;
            bRectTransform.pivot = Vector2.one * .5f;
            bRectTransform.offsetMin = Vector2.zero;
            bRectTransform.offsetMax = Vector2.zero;
            return bRectTransform;
        }

        private static string SanitizeClassName(string value)
        {
            string output = "";
            bool nextUpper = true;
            int startByNumber = 0;

            // Remove special characters
            for (int i = 0; i < value.Length; i++)
            {
                bool nextUpperBuffer = nextUpper;
                nextUpper = false;

                if (value[i] == ' ' || value[i] == '-' || value[i] == '_')
                {
                    nextUpper = true;
                    continue;
                }

                if (startByNumber == i && (value[i] == '0' || value[i] == '1' || value[i] == '2' || value[i] == '3' || value[i] == '4' || value[i] == '5' || value[i] == '6' || value[i] == '7' || value[i] == '8' || value[i] == '9'))
                    startByNumber++;

                if (nextUpperBuffer)
                    output += Char.ToUpper(value[i]);
                else
                    output += value[i];
            }

            // Move number at end if there at beginning
            if (startByNumber > 0)
                output = output.Substring(startByNumber, output.Length - startByNumber) + output.Substring(0, startByNumber);

            return output;
        }

        [Button("Hide All Menu"), FoldoutGroup("Debug", order: 100), DisableInEditorMode]
        private void EditorHideAllMenus()
        {
            HideAllMenus();
        }

        [Button("Hide All Menu Immediately"), FoldoutGroup("Debug", order: 100), DisableInEditorMode]
        private void EditorHideAllMenusImmediately()
        {
            HideAllMenusImmediately();
        }

#endif

        protected Dictionary<string, Menu> menusInDictionnary = new Dictionary<string, Menu>();
        protected List<Menu> menusInList = new List<Menu>();
        public Dictionary<string, Menu> Menus => menusInDictionnary;

        [SerializeField, FoldoutGroup("Transitions")]
        protected Transition transitionIn = null;
        public Transition TransitionIn => transitionIn;

        [SerializeField, FoldoutGroup("Transitions")]
        protected Transition transitionOut = null;
        public Transition TransitionOut => transitionOut;

        [SerializeField, FoldoutGroup("Menu List")]
        protected MenuList menuList = null;
        public MenuList MenuList => menuList;

        protected GameObject blocker;
        protected int block = 0;
        protected bool isBlocked { get { return block > 0; } }
        public bool IsBlocked { get { return isBlocked; } }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            // Find all the children menu
            Menu[] childrenMenus = GetComponentsInChildren<Menu>(true);
            foreach (Menu iMenu in childrenMenus)
                AddMenu(iMenu);

            // Generate blocker
            blocker = new GameObject("Blocker");
            blocker.transform.SetParent(transform);
            Canvas blockerCanvas = blocker.AddComponent<Canvas>();
            blockerCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            blockerCanvas.sortingOrder = 999999;
            GraphicRaycaster blockerGraphicRaycaster = blocker.AddComponent<GraphicRaycaster>();
            Image blockerImage = blocker.AddComponent<Image>();
            blockerImage.color = new Color(0f, 0f, 0f, 0f);
            blocker.SetActive(false);
        }

        #endregion

        #region Menu Methods

        public Menu GetMenu(string name)
        {
            return menusInDictionnary[name];
        }

        public void AddMenu(Menu menu)
        {
            // Add to the dictionnary
            menusInDictionnary.Add(menu.ID, menu);
            // Add to the list
            menusInList.Add(menu);

            // De sure it's placed at the good position
            ((RectTransform)menu.transform).anchoredPosition = Vector3.zero;

            // Init and hide the menu
            menu.Initialize();
        }

        public void RemoveMenu(Menu menu)
        {
            // Remove from the dictionnary
            menusInDictionnary.Remove(menu.ID);
            // Remove from the list
            menusInList.Remove(menu);
        }

        public List<Menu> DisplayedMenus()
        {
            return menusInList.Where((menu => menu.IsDisplayed == true)).ToList();
        }

        public List<Menu> ShownOrAlmostMenus()
        {
            return menusInList.Where((menu => menu.IsShownOrAlmost == true)).ToList();
        }

        public List<Menu> HiddenOrAlmostMenus()
        {
            return menusInList.Where((menu => menu.IsHiddenOrAlmost == true)).ToList();
        }

        public void HideAllMenusImmediately()
        {
            foreach (Menu iMenu in menusInList)
            {
                if (iMenu.IsShownOrAlmost)
                    iMenu.HideImmediately();
            }
        }

        public void HideAllMenus(Action callback = null)
        {
            int menuToHide = 0;
            foreach (Menu iMenu in menusInList)
            {
                if(iMenu.IsShownOrAlmost)
                {
                    menuToHide++;
                    iMenu.Hide(()=>
                    {
                        menuToHide--;
                        if(menuToHide <= 0)
                        {
                            callback?.Invoke();
                            callback = null;
                        }
                    });
                }
            }
        }

        #endregion

        #region Blocker Methods

        public void Block()
        {
            block++;
            blocker.SetActive(true);
        }

        public void Unblock(bool force = false)
        {
            block--;
            if(block <= 0 || force)
            {
                block = 0;
                blocker.SetActive(false);
            }
        }

        #endregion
    }
}