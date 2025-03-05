using System;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    [Tooltip("Settings")]
    [SerializeField] private packageTypes _packageType;
    [SerializeField] private List<PackageStampColor> _packageStampColors;

    [Tooltip("Flags")]
    private List<PackageStampColor> _currentStamps = new List<PackageStampColor>();

    private Action OnAddStamp;

    private void AddStamp(PackageStampColor color)
    {
        OnAddStamp?.Invoke();

        _currentStamps.Add(color);
    }

    private bool CheckForStamps()
    {
        List<PackageStampColor> checksLeft = _currentStamps;

        foreach (var stamp in _packageStampColors)
        {
            if (checksLeft.Contains(stamp))
                checksLeft.Remove(stamp);
            else
                return false;
        }

        return checksLeft.Count == 0;
    }
}
