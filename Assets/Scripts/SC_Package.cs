using System;
using System.Collections.Generic;
using UnityEngine;

public class SC_Package : MonoBehaviour
{
    [Serializable]
    struct StampValues
    {
        public PackageStampColor Color;
        public PackageStampIcon Icon;
    }

    [Tooltip("Attributes")]
    [SerializeField] private packageTypes _packageType;
    [SerializeField] private PackageDestination _destination;
    [SerializeField] private List<StampValues> _stampValues;

    [Tooltip("Flags")]
    private packageTypes _currentPackageType;
    private List<StampValues> _currentStamps = new List<StampValues>();
    private PackageDestination _currentDestination;

    public Action OnAddStamp;
    public Action OnDestinationSet;


    /// <summary>
    /// Call to check if all Attributes are matching with the Flags.
    /// </summary>
    /// <returns>Returns true if all values are matching.</returns>
    public bool CheckFlags()
    {
        if (_packageType == _currentPackageType) return false;
        if (_destination == _currentDestination) return false;
        if (CheckForStamps()) return false;

        return true;
    }

    /// <summary>
    /// Sets the type of package to be checked later if correct.
    /// </summary>
    /// <param name="type">the Enum that sets the type.</param>
    public void SetPackageType(packageTypes type) => _currentPackageType = type;

    /// <summary>
    /// Sets the destination of package to be checked later if correct.
    /// </summary>
    /// <param name="destination">the Enum that sets the destination.</param>
    public void SetDestination(PackageDestination destination) => _currentDestination = destination;

    /// <summary>
    /// Adds a stamp with a color and icon to be checked later if they are correct.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="icon"></param>
    public void AddStamp(PackageStampColor color, PackageStampIcon icon)
    {
        OnAddStamp?.Invoke();

        StampValues stampValues = new StampValues();
        stampValues.Color = color;
        stampValues.Icon = icon;

        _currentStamps.Add(stampValues);
    }

    private bool CheckForStamps()
    {
        List<StampValues> checksLeft = _currentStamps;

        foreach (var stamp in _stampValues)
        {
            if (checksLeft.Contains(stamp))
                checksLeft.Remove(stamp);
            else
                return false;
        }

        return checksLeft.Count == 0;
    }
}
