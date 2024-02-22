#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

public class TimeTrackerWindow : EditorWindow
{
    [MenuItem("tzdevil/Time Tracker", false, 10)]
    public static void CreateNewLabelBlocks()
            => GetWindow(typeof(TimeTrackerWindow), false, "Time Tracker")
                        .position = new Rect(360, 120, 280, 140);

    void OnEnable()
    {
        EditorApplication.update += UpdateCallback;
    }

    void OnDisable()
    {
        EditorApplication.update -= UpdateCallback;
    }

    void UpdateCallback()
    {
        Repaint();
    }

    private void OnGUI()
    {
        TimeTracker.TrackTime();

        TimeSpan totalTimeSpent = TimeSpan.FromSeconds(TimeTracker.TotalTimeSpent);
        var totalTimeSpentString = $"<size=20>{totalTimeSpent.Hours:D2}</size>hr <size=20>{totalTimeSpent.Minutes:D2}</size>min <size=20>{totalTimeSpent.Seconds:D2}</size>sec";

        TimeSpan timeSpentThisSession = TimeSpan.FromSeconds(TimeTracker.TimeSpentThisSession);
        var timeSpentThisSessionString = $"<size=20>{timeSpentThisSession.Hours:D2}</size>hr <size=20>{timeSpentThisSession.Minutes:D2}</size>min <size=20>{timeSpentThisSession.Seconds:D2}</size>sec";

        GUIStyle style = new()
        {
            richText = true,
            alignment = TextAnchor.MiddleCenter,
            fontSize = 16,
            padding = new RectOffset(6, 0, 4, 0),
            normal = new GUIStyleState
            {
                textColor = new Color32(77, 162, 101, 255)
            }
        };

        GUILayout.Label($"<b>{GetProjectName()}</b>", style);

        GUILayout.Space(6);

        style.normal.textColor = Color.white;
        style.fontSize = 14;
        style.alignment = default;
        GUILayout.Label($"<b><size=15>Total time spent:</size></b>\n{totalTimeSpentString}", style);

        GUILayout.Space(6);

        GUILayout.Label($"<b><size=15>Time spent this session:</size></b>\n{timeSpentThisSessionString}", style);
    }

    string GetProjectName()
    {
        string[] pathParts = Application.dataPath.Split('/');
        string projectName = pathParts[^2];
        return projectName;
    }
}
#endif