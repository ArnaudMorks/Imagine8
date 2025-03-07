using System;
using System.Collections.Generic;
using UnityEngine;

public class SC_Package : MonoBehaviour
{
    [Tooltip("Attributes")]
    [SerializeField] private packageTypes _packageType;
    [SerializeField] private List<PackageStampColor> _packageStampColors;

    [Tooltip("Flags")]
    private List<PackageStampColor> _currentStamps = new List<PackageStampColor>();

    public Action OnAddStamp;

    /// <summary>
    /// Call to check if all Attributes are matching with the Flags.
    /// </summary>
    /// <returns>Returns true if all values are matching.</returns>
    public bool CheckFlags()
    {
        bool StampFlag = CheckForStamps();

        if (StampFlag)
        {
            return true;
        }

        return false;
    }

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
