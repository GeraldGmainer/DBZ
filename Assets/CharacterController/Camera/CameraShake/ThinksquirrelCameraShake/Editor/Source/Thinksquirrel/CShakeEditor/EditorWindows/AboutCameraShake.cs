// AboutCameraShake.cs
// Copyright (c) 2012-2015 Thinksquirrel Software, LLC.

using Thinksquirrel.CShake.Internal;
using UnityEditor;
using UnityEngine;

namespace Thinksquirrel.CShakeEditor
{
    public sealed class AboutCameraShake : EditorWindow
    {
        #region Instance Fields
        double m_LastTime;
        Vector2 m_ScrollPosition = Vector2.zero;
        double m_deltaTime;
        #endregion

        #region Static and Constant Fields
        static Vector2 m_MinSize = new Vector2(350, 300);
        static Vector2 m_MaxSize = new Vector2(350, 300);
        static GUIContent m_Logo;
        static GUIStyle m_HorizontalScrollbarStyle;
        static GUIStyle m_VerticalScrollbarStyle;
        #endregion

        #region Public API
        [MenuItem(CameraShakeMenuItems.menuToolsLocation + "/Camera Shake/About Camera Shake", false, 1203)]
        [MenuItem(CameraShakeMenuItems.menuWindowLocation + "/Camera Shake/About Camera Shake", false, 1203)]
        public static void Open()
        {
            GetWindow<AboutCameraShake>(true, "About Camera Shake");
        }
        #endregion

        #region Unity Methods
        void OnGUI()
        {
            if (m_Logo == null)
            {
                var guids = AssetDatabase.FindAssets("camerashake_logo");
                Texture logo = null;

                foreach (var guid in guids)
                {
                    logo = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Texture)) as Texture;

                    if (logo)
                        break;
                }
                m_Logo = logo ? new GUIContent(logo) : GUIContent.none;
                m_HorizontalScrollbarStyle = new GUIStyle(GUI.skin.horizontalScrollbar);
                m_VerticalScrollbarStyle = new GUIStyle(GUI.skin.verticalScrollbar);
                m_HorizontalScrollbarStyle.fixedWidth = 0;
                m_HorizontalScrollbarStyle.fixedHeight = 0;
                m_VerticalScrollbarStyle.fixedWidth = 0;
                m_VerticalScrollbarStyle.fixedHeight = 0;
            }
            GUILayout.BeginHorizontal(GUILayout.Height(128));
            GUILayout.Label(m_Logo);
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Version " + VersionInfo.version, EditorStyles.largeLabel);
            if (VersionInfo.isPreRelease)
            {
                GUILayout.Label(VersionInfo.version.Contains("a") ? "Alpha" : "Beta" + " Release", EditorStyles.miniLabel);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            m_ScrollPosition = GUILayout.BeginScrollView(m_ScrollPosition, false, false, m_HorizontalScrollbarStyle,
                                                         m_VerticalScrollbarStyle, GUILayout.Height(40));
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("Development/Programming");
            CreditsLabel("Josh Montoute");
            CreditsLabel("");
            CreditsLabel("Additional Art/Design Content");
            CreditsLabel("Charlie Samways");
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("");
            CreditsLabel("");
            GUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
            var contentLink = CameraShakeEditorHelpers.ContentLink();
            if (!VersionInfo.isPreRelease && !string.IsNullOrEmpty(contentLink))
            {
                if (GUILayout.Button("Register Camera Shake to receive update notifications"))
                {
                    CameraShakeMenuItems.RegisterCameraShake();
                }
                if (GUILayout.Button("Rate this package!"))
                {
                    Application.OpenURL("com.unity3d.kharma:" + contentLink);
                }
            }
            GUILayout.FlexibleSpace();
            GUI.color = new Color(0, .5f, .75f, 1);
            if (GUILayout.Button("Open source acknowledgments", EditorStyles.whiteLabel))
            {
                Application.OpenURL("https://support.thinksquirrel.com/hc/articles/202987764");
            }
            if (GUILayout.Button("Visit Thinksquirrel", EditorStyles.whiteLabel))
            {
                Application.OpenURL("https://www.thinksquirrel.com");
            }
            GUILayout.FlexibleSpace();
            GUI.color = Color.white;
            //GUILayout.Label("License: " + VersionInfo.license);
            GUILayout.Label(VersionInfo.copyright, EditorStyles.miniLabel);

            GUILayout.Space(14);

            minSize = m_MinSize;
            maxSize = m_MaxSize;
        }
        void Update()
        {
            m_deltaTime = EditorApplication.timeSinceStartup - m_LastTime;
            m_LastTime = EditorApplication.timeSinceStartup;

            m_ScrollPosition = new Vector2(0, m_ScrollPosition.y + 5 * (float)m_deltaTime);
            if (m_ScrollPosition.y >= 150)
            {
                m_ScrollPosition = Vector2.zero;
            }

            Repaint();
        }
        #endregion

        void CreditsLabel(string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, EditorStyles.miniLabel, GUILayout.ExpandWidth(false));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}