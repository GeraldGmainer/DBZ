// CameraShakeFirstRunWindow.cs
// Copyright (c) 2012-2015 Thinksquirrel Software, LLC.

using Thinksquirrel.CShake.Internal;
using UnityEditor;
using UnityEngine;

namespace Thinksquirrel.CShakeEditor
{
    public class CameraShakeWelcomeWindow : EditorWindow
    {
        #region Instance Fields
        [SerializeField] Vector2 m_Dimensions = new Vector2(400, 300);
        [SerializeField] GUIContent m_Logo;
        [SerializeField] GUIContent m_Facebook;
        [SerializeField] GUIContent m_Tumblr;
        [SerializeField] GUIContent m_Twitter;
        [SerializeField] GUIContent m_GooglePlus;
        [SerializeField] GUIContent m_YouTube;
        [SerializeField] GUIStyle m_BoxStyle;
        [SerializeField] GUIStyle m_LabelStyle;
        [SerializeField] GUIStyle m_ToggleStyle;
        [SerializeField] bool m_PositionSet;
        [SerializeField] GUISkin m_CachedSkin;
        #endregion
       
        #region Unity Methods
        void OnEnable()
        {         
            if (!m_PositionSet)
            {
                float w = Screen.currentResolution.width;
                float h = Screen.currentResolution.height;

                var r = new Rect(
                    (w * .5f) - (m_Dimensions.x * .5f),
                    (h * .5f) - (m_Dimensions.y * .5f),
                    m_Dimensions.x,
                    m_Dimensions.y);

                position = r;
                minSize = m_Dimensions;
                maxSize = m_Dimensions;
                m_PositionSet = true;
            }

            EditorApplication.update += Repaint;
        }
        void OnDestroy()
        {
            EditorApplication.update -= Repaint;
        }

        void OnGUI()
        {
            if (m_Logo == null || m_Facebook == null || m_Tumblr == null || m_Twitter == null || m_GooglePlus == null || m_YouTube == null)
            {
                var guids = AssetDatabase.FindAssets("camerashake_");

                foreach (var guid in guids)
                {
                    var tex = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Texture)) as Texture;

                    if (!tex)
                        continue;

                    switch (tex.name)
                    {
                        case "camerashake_logo":
                            m_Logo = new GUIContent(tex);
                            break;
                        case "camerashake_social_facebook":
                            m_Facebook = new GUIContent(string.Empty, tex);
                            break;
                        case "camerashake_social_tumblr":
                            m_Tumblr = new GUIContent(string.Empty, tex);
                            break;
                        case "camerashake_social_twitter":
                            m_Twitter = new GUIContent(string.Empty, tex);
                            break;
                        case "camerashake_social_googleplus":
                            m_GooglePlus = new GUIContent(string.Empty, tex);
                            break;
                        case "camerashake_social_youtube":
                            m_YouTube = new GUIContent(string.Empty, tex);
                            break;
                    }
                }

                if (m_Logo == null) m_Logo = GUIContent.none;
                if (m_Facebook == null) m_Facebook = GUIContent.none;
                if (m_Tumblr == null) m_Tumblr = GUIContent.none;
                if (m_Twitter == null) m_Twitter = GUIContent.none;
                if (m_GooglePlus == null) m_GooglePlus = GUIContent.none;
                if (m_YouTube == null) m_YouTube = GUIContent.none;
            }

            if (GUI.skin != m_CachedSkin)
            {
                m_BoxStyle = null;
                m_LabelStyle = null;
                m_ToggleStyle = null;
                m_CachedSkin = GUI.skin;
            }

            if (m_BoxStyle == null)
            {
                m_BoxStyle = new GUIStyle(EditorStyles.textField)
                {
                    stretchWidth = true,
                    stretchHeight = true,
                    fixedWidth = 0,
                    fixedHeight = 0,
                    border = new RectOffset(0, 0, 0, 0),
                    padding = new RectOffset(0, 0, 0, 0),
                    margin = new RectOffset(0, 0, 0, 0)
                };
            }

            if (m_LabelStyle == null)
            {
                m_LabelStyle = new GUIStyle(EditorStyles.miniLabel)
                {
                    wordWrap = true
                };
            }

            if (m_ToggleStyle == null)
            {
                var shurikenToggle = (GUIStyle)"ShurikenToggle";

                m_ToggleStyle = new GUIStyle(shurikenToggle)
                {
                    font = EditorStyles.miniLabel.font,
                    fontSize = EditorStyles.miniLabel.fontSize,
                    contentOffset = new Vector2(13, -1),
                    active = { background = shurikenToggle.active.background, textColor = EditorStyles.miniLabel.normal.textColor },
                    hover = { background = shurikenToggle.hover.background, textColor = EditorStyles.miniLabel.normal.textColor },
                    normal = { background = shurikenToggle.normal.background, textColor = EditorStyles.miniLabel.normal.textColor },
                    focused = { background = shurikenToggle.focused.background, textColor = EditorStyles.miniLabel.normal.textColor },
                    onActive = { background = shurikenToggle.onActive.background, textColor = EditorStyles.miniLabel.normal.textColor },
                    onHover = { background = shurikenToggle.onHover.background, textColor = EditorStyles.miniLabel.normal.textColor },
                    onNormal = { background = shurikenToggle.onNormal.background, textColor = EditorStyles.miniLabel.normal.textColor },
                    onFocused = { background = shurikenToggle.onFocused.background, textColor = EditorStyles.miniLabel.normal.textColor },
                };
            }
            
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            var col = new Color32(45, 142, 205, 192);
            GUI.backgroundColor = col;
            GUILayout.BeginVertical(m_BoxStyle);
            GUI.backgroundColor = Color.white;

            GUILayout.FlexibleSpace();

            // Logo
            GUILayout.Space(-32);
            GUILayout.BeginHorizontal(GUILayout.Height(128));
            GUILayout.FlexibleSpace();
            GUILayout.Label(m_Logo);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(-32);

            GUILayout.FlexibleSpace();

            // Welcome message
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label(string.Format("Thank you for installing {0}!", VersionInfo.license), EditorStyles.largeLabel);

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label("");

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();

            // Links
            var scene = CameraShakeEditorHelpers.ExampleScene();
            var sceneObj = AssetDatabase.LoadAssetAtPath(scene, typeof(Object));

            EditorGUI.BeginDisabledGroup(!sceneObj);
            if (DrawButton("Example Scene", "Open the Camera Shake example scene"))
            {
                if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
                {
                    EditorApplication.OpenScene(scene);
                    Close();
                    EditorApplication.update += ReopenWindow;
                }
            }
            EditorGUI.EndDisabledGroup();
            if (DrawButton("Other Extensions", "Check out other Thinksquirrel extensions")) Application.OpenURL(CameraShakeEditorHelpers.SearchLink());
            if (DrawButton("Community", "Browse the Camera Shake community")) CameraShakeMenuItems.SupportForumWindow();
            if (DrawButton("Rate this package", "Rate this package in the Asset Store")) Application.OpenURL("com.unity3d.kharma:" + CameraShakeEditorHelpers.ContentLink());
            if (DrawButton("Subscribe", "Subscribe for product updates")) Application.OpenURL("https://www.thinksquirrel.com/#subscribe");
            if (DrawButton("Help", "Browse through the Reference Manual")) CameraShakeMenuItems.HelpWindow();

            GUILayout.FlexibleSpace();

            EditorGUI.BeginChangeCheck();
            var toggle = DrawToggle(EditorPrefs.GetBool("Thinksquirrel.CShakeEditor.WelcomeWindow", true), "Show this window on startup");

            if (EditorGUI.EndChangeCheck())
                EditorPrefs.SetBool("Thinksquirrel.CShakeEditor.WelcomeWindow", toggle);

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();

            DrawSocialMediaIcons();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
        #endregion

        static void ReopenWindow()
        {
            EditorApplication.update -= ReopenWindow;
            CameraShakeMenuItems.WelcomeWindow();
        }
        bool DrawButton(string label, string description)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(4);
            var col = new Color32(45, 142, 205, 255);
            GUI.backgroundColor = col;
            var result = GUILayout.Button(label, EditorStyles.toolbarButton, GUILayout.Width(116), GUILayout.Height(24));
            GUI.backgroundColor = Color.white;
            GUILayout.BeginVertical();
            GUILayout.Space(-1);
            GUILayout.Label(description, m_LabelStyle);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            return result;
        }

        bool DrawToggle(bool value, string label)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var toggle = GUILayout.Toggle(value, label, m_ToggleStyle);
            GUILayout.Space(16);
            GUILayout.EndHorizontal();

            return toggle;
        }

        void DrawSocialMediaIcons()
        {
            var col = new Color32(45, 142, 205, 255);
            GUI.backgroundColor = col;
            GUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            var col2 = Color.white;
            col2.a = 0.65f;
            GUI.contentColor = col2;
            if (GUILayout.Button(m_Facebook, EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) Application.OpenURL("https://www.facebook.com/thinksquirrel");
            if (GUILayout.Button(m_Tumblr, EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) Application.OpenURL("https://blog.thinksquirrel.com");
            if (GUILayout.Button(m_Twitter, EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) Application.OpenURL("https://www.twitter.com/thinksquirrel");
            if (GUILayout.Button(m_GooglePlus, EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) Application.OpenURL("https://plus.google.com/+thinksquirrel");
            if (GUILayout.Button(m_YouTube, EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))) Application.OpenURL("https://www.youtube.com/thinksquirrel");
            GUI.contentColor = Color.white;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            GUILayout.Space(7);
        }
    }
}
