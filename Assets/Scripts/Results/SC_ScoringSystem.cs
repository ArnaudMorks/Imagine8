using System.Collections.Generic;
using UnityEngine;

public class SC_ScoringSystem : MonoBehaviour
{
    private SC_PackageSpawner _packageSpawner;

    private Dictionary<SC_Package, bool> _currentScore = new Dictionary<SC_Package, bool>();

    private void Awake()
    {
        _packageSpawner = FindFirstObjectByType<SC_PackageSpawner>();
        _packageSpawner.OnDeSpawnedPackage += AddScoring;
    }

    /// <summary>
    /// Gets the amount of correct guessed packages.
    /// </summary>
    /// <returns> Amount of "True" statements in the score.</returns>
    public int GetScoring()
    {
        int correctAmount = 0;

        foreach (var score in _currentScore)
        {
            if (score.Value == true) correctAmount++;
        }

        return correctAmount;
    }

    /// <summary>
    /// Gets the amount of packages checked.
    /// </summary>
    /// <returns></returns>
    public int GetMaxScoring() => _currentScore.Count;

    /// <summary>
    /// Clears the dictionary that tracks the score data.
    /// </summary>
    public void ResetScoring() => _currentScore.Clear();

    private void AddScoring(GameObject packageObject)
    {
        SC_Package package = packageObject.GetComponent<SC_Package>();
        if (package == null) return;

        Debug.Log("ADDED");
        var correctness = package.CheckFlags();
        _currentScore.Add(package, correctness);
    }
}