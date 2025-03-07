using System.Collections.Generic;
using UnityEngine;

public class SC_PackageList : MonoBehaviour
{
    [SerializeField] private List<SC_Package> packages;

    /// <summary>
    /// Gets the package list.
    /// </summary>
    /// <returns>package list</returns>
    public List<SC_Package> GetPackages()
    {
        return packages;
    }
}
