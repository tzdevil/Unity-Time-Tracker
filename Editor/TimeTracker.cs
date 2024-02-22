#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class TimeTracker
{
    private static string _projectName;

    public static double TotalTimeSpent { get; private set; }
    public static double TimeSpentThisSession { get; private set; }

    private static double _totalTimeSpentInEditorTillThisSession;

    static TimeTracker()
    {
        EditorApplication.quitting += Quit;

        _projectName = GetProjectName();

        TotalTimeSpent = EditorPrefs.GetInt(_projectName, 0);
        _totalTimeSpentInEditorTillThisSession = TotalTimeSpent;
    }

    public static void TrackTime()
    {
        TimeSpentThisSession = EditorApplication.timeSinceStartup;
        TotalTimeSpent = TimeSpentThisSession + _totalTimeSpentInEditorTillThisSession;
    }

    private static void SaveTotalTimeSpentInEditorToEditorPrefs()
    {
        EditorPrefs.SetInt(_projectName, (int)TotalTimeSpent);
    }

    static void Quit()
    {
        SaveTotalTimeSpentInEditorToEditorPrefs();
    }

    static string GetProjectName()
    {
        string[] pathParts = Application.dataPath.Split('/');
        string projectName = pathParts[^2];
        return projectName;
    }
}
#endif