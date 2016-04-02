using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public static class CliBuilder {

    static string folder = Path.Combine(Application.dataPath, "../builds/");

    static string[] GetScenes(){
        var scenes = (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray();
        Debug.Log(scenes.Length + " scenes found");
        return scenes;
    }

    [MenuItem ("Builds/ALL")]
    public static void ALL(){
        WebGL();
        Win64();
        OSX();
        Linux();
    }

    public static void BuildPlayer(string buildName, BuildTarget buildTarget){
        ClearConsole();
        Debug.Log("\nBuilding " + buildName + "...");
        var result = BuildPipeline.BuildPlayer(GetScenes(), folder + buildName, buildTarget, BuildOptions.None);
        Debug.Log("Done: " + result);
    }

    [MenuItem ("Builds/WebGL")]
    public static void WebGL(){
        BuildPlayer("webgl", BuildTarget.WebGL);
    }

    [MenuItem ("Builds/Win64")]
    public static void Win64(){
        BuildPlayer("win64/art-of-dreams.exe", BuildTarget.StandaloneWindows64);
    }

    [MenuItem ("Builds/OSX")]
    public static void OSX(){
        BuildPlayer("osx/art-of-dreams", BuildTarget.StandaloneOSXUniversal);
    }

    [MenuItem ("Builds/Linux")]
    public static void Linux(){
        BuildPlayer("linux/art-of-dreams", BuildTarget.StandaloneLinuxUniversal);
    }

    [MenuItem ("Tools/Clear Console")]
    static void ClearConsole () {
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null,null);
    }
}
