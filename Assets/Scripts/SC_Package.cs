using System;
using System.Collections.Generic;
using UnityEngine;

public class SC_Package : MonoBehaviour
{
    [Tooltip("Settings")]
    [SerializeField] private packageTypes g_packageType;
    [SerializeField] private List<PackageStampColor> g_packageStampColors;

    [Tooltip("Flags")]
    private List<PackageStampColor> g_currentStamps = new List<PackageStampColor>();

    public Action OnAddStamp;


    private void AddStamp(PackageStampColor color)
    {
        OnAddStamp?.Invoke();

        g_currentStamps.Add(color);
    }

    private bool CheckForStamps()
    {
        List<PackageStampColor> checksLeft = g_currentStamps;

        foreach (var stamp in g_packageStampColors)
        {
            if (checksLeft.Contains(stamp))
                checksLeft.Remove(stamp);
            else
                return false;
        }

        return checksLeft.Count == 0;
    }
}
