//
// CShakeEditorHelpers.cs
//
// Author(s):
//       Josh Montoute <josh@thinksquirrel.com>
//
// Copyright (c) 2012-2015 Thinksquirrel Software, LLC
//
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Thinksquirrel.CShake.Internal;
using UnityEditor;
using UnityEngine;

//! \cond PRIVATE
namespace Thinksquirrel.CShakeEditor
{
    static class CameraShakeEditorHelpers
    {
        /// <summary>
        /// Gets the Camera Shake reference manual URL.
        /// </summary>
        public static string ReferenceManualUrl()
        {
            return string.Format("https://docs.thinksquirrel.com/camera-shake/{0}/reference/", VersionInfo.version);
        }

        /// <summary>
        /// Gets the Camera Shake support forum URL.
        /// </summary>
        public static string SupportForumUrl()
        {
            return "https://support.thinksquirrel.com/hc/communities/public/topics/200145264-Camera-Shake";
        }

        /// <summary>
        /// Gets the Camera Shake reference manual archive URL.
        /// </summary>
        public static string ArchiveUrl()
        {
            return string.Format("https://docs.thinksquirrel.com/camera-shake/archives/{0}.zip", VersionInfo.version);
        }

        /// <summary>
        /// Gets the example scene path.
        /// </summary>
        public static string ExampleScene()
        {
            return "Assets/Camera Shake/Camera Shake Example/VersionSpecific/Scenes/CameraShakeExample.unity";
        }

        /// <summary>
        /// Gets the Camera Shake manual URL for the specified component.
        /// </summary>
        public static string ComponentUrl(System.Type type, bool includeBaseUrl = true)
        {
            string fullTypeName = type.ToString();

            if (!fullTypeName.Contains("Thinksquirrel"))
            {
                var attribute = System.Attribute.GetCustomAttribute(type, typeof(CameraShakeDocumentationName));

                if (attribute != null)
                {
                    var typedAttribute = (CameraShakeDocumentationName)attribute;

                    fullTypeName = typedAttribute.name;
                }
                else
                {
                    return null;
                }
            }

            string[] names = fullTypeName.Split('.');
            fullTypeName = names[names.Length - 1];
            var sb = new StringBuilder();
            sb.Append(fullTypeName.ToLowerInvariant());
            sb.Append(".html");

            return includeBaseUrl ? ReferenceManualUrl() + sb : sb.ToString();
        }

        /// <summary>
        /// Gets the Camera Shake Scripting API URL for the specified type.
        /// </summary>
        public static string ScriptingUrl(System.Type type, bool includeBaseUrl = true)
        {
            string fullTypeName = type.ToString();

            if (!fullTypeName.Contains("Thinksquirrel"))
            {
                var attribute = System.Attribute.GetCustomAttribute(type, typeof(CameraShakeDocumentationName));

                if (attribute != null)
                {
                    var typedAttribute = (CameraShakeDocumentationName)attribute;

                    fullTypeName = typedAttribute.name;
                }
                else
                {
                    return null;
                }
            }

            fullTypeName = fullTypeName.Replace(".", "_1_1_");
            var sb = new StringBuilder();
            sb.Append("class_");
            sb.Append(HumanizeString(fullTypeName).Replace(' ', '_').ToLowerInvariant());
            sb.Append(".html");

            return includeBaseUrl ? ReferenceManualUrl() + sb : sb.ToString();
        }

        /// <summary>
        /// Gets the Asset Store content link for the current Camera Shake version.
        /// </summary>
        /// <returns>
        /// A relative URL to the Asset Store.
        /// </returns>
        public static string ContentLink()
        {
            return "content/3563";
        }

        public static string SearchLink()
        {
            return "https://www.assetstore.unity3d.com/en/#!/publisher/578";
        }

        static string HumanizeString(string input)
        {
            var sb = new StringBuilder();

            char last = char.MinValue;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (char.IsLower(last) && char.IsUpper(c))
                {
                    sb.Append(' ');
                }
                sb.Append(c);
                last = c;
            }
            return sb.ToString();
        }
    }
    [InitializeOnLoad]
    static class JavaScriptInstaller
    {
        static JavaScriptInstaller()
        {
            if (EditorPrefs.GetBool("Thinksquirrel.CameraShakeEditor.InstallForJavaScript", false))
                EditorApplication.update += DoJavaScriptInstaller;
        }

        static void DoJavaScriptInstaller()
        {
            EditorApplication.update -= DoJavaScriptInstaller;
            EditorPrefs.DeleteKey("Thinksquirrel.CameraShakeEditor.InstallForJavaScript");

            bool isWindows = Application.platform == RuntimePlatform.WindowsEditor;
            string dataPath, plugins, pluginsCameraShake, pluginsCameraShakeRel, cameraShakeMainRel, error;

            // Get paths
            dataPath = isWindows ? Application.dataPath.Replace("/", "\\") : Application.dataPath;
            plugins = Path.Combine(dataPath, "Plugins");
            pluginsCameraShake = Path.Combine(plugins, "Camera Shake");
            cameraShakeMainRel = "Assets/Camera Shake/_Main/";
            pluginsCameraShakeRel = "Assets/Plugins/Camera Shake/";

            // Delete any old CameraShake source files in the plugins folder
            if (Directory.Exists(pluginsCameraShake))
            {
                bool del = false;

                var dir = Path.Combine(pluginsCameraShake, "Source");

                if (Directory.Exists(dir))
                {
                    var meta = dir + ".meta";
                    del = true;
                    Directory.Delete(dir);
                    if (File.Exists(meta)) File.Delete(meta);
                }

                if (del)
                    AssetDatabase.Refresh();
            }

            // Check to see if the plugins folder exists
            if (!Directory.Exists(plugins))
            {
                AssetDatabase.CreateFolder("Assets", "Plugins");
            }

            // Check to see if the CameraShake folder exists under the plugins folder
            if (!Directory.Exists(pluginsCameraShake))
            {
                AssetDatabase.CreateFolder("Assets/Plugins", "Camera Shake");
            }

            error = AssetDatabase.MoveAsset(cameraShakeMainRel + "Source", pluginsCameraShakeRel + "Source");

            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError("Unable to move Camera Shake runtime source files: " + error);
            }

            AssetDatabase.Refresh();
        }
    }
}
#region For batch mode source code building only
static class CameraShakeBatchMode
{
    static bool CheckLog(bool clear)
    {
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var logEntries = assembly.GetType("UnityEditorInternal.LogEntries");

        if (clear)
        {
            logEntries.GetMethod("Clear").Invoke(new object(), null);
        }

        var count = (int)logEntries.GetMethod("GetCount").Invoke(new object(), null);

        return count == 0;
    }

    static void ExportPackageInternal()
    {
        EditorApplication.NewScene();

        if (!CheckLog(true))
        {
            Debug.LogError(string.Format("[{0}] Compilation errors present! Aborting...", typeof(CameraShakeBatchMode).Name));
            EditorApplication.Exit(1);
            return;
        }

        var packagePaths = new[]
        {
            "Assets/Camera Shake/_Main/Source",
            "Assets/Camera Shake/Editor/Source",
            "Assets/Camera Shake/Camera Shake Example"
        };

        string packageName, asInstaller, edition;
        
        packageName = "CameraShake";
        asInstaller = "ASInstaller_CameraShake";
        edition = "Camera Shake";

        var name = string.Format("{0}.unitypackage", packageName);

        // TODO: Pass this in the command line
#if UNITY_4_6
        const string platform = "4.6.0";
#else
        const string platform = "5.0.0";
#endif

        var finalProjectRoot =
            new DirectoryInfo(Path.Combine(Application.dataPath, "../../../AssetStoreProjects")).FullName;

        var finalProjectPath = Path.Combine(Path.Combine(finalProjectRoot, platform), edition.Replace(":", "-"));

        var dataPathFinal = Path.Combine(finalProjectPath, "Assets");
        var destinationPath = Path.Combine(dataPathFinal, asInstaller);

        AssetDatabase.ExportPackage(packagePaths, Path.Combine(destinationPath, name), ExportPackageOptions.Recurse);
        
        if (CheckLog(false)) return;

        Debug.LogError(string.Format("[{0}] Errors present! Aborting...", typeof(CameraShakeBatchMode).Name));
        EditorApplication.Exit(1);
    }   
}
#endregion
//! \endcond

