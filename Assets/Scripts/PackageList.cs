using System.Collections.Generic;
using UnityEngine;

public class PackageList : MonoBehaviour
{
    [SerializeField] private List<Package> packages;

    /// <summary>
    /// Gets the package list.
    /// </summary>
    /// <returns>package list</returns>
    public List<Package> GetPackages()
    {
        return packages;
    }
}
