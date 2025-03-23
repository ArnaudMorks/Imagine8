using System.Collections.Generic;
using UnityEngine;

public class SC_PackageCounter : MonoBehaviour
{
    [SerializeField] private GameObject _visualIndecators;
    private List<GameObject> _indecators;
    private int _packageAmount;

    private SC_PackageSpawner _spawner;


    private void Awake()
    {
        _spawner = FindAnyObjectByType<SC_PackageSpawner>();
        _spawner.OnDeSpawnedPackage += UpdateCounter;

        _indecators = GetChildren();
    }

    private void UpdateCounter(GameObject package)
    {
        _packageAmount = _spawner.packageAmount - 1;
        int deactivateAmount = _indecators.Count / _packageAmount;

        DeActivateObjects(_indecators, deactivateAmount);
    }

    private List<GameObject> GetChildren()
    {
        List<GameObject> children = new List<GameObject>();
        Transform[] allChildren = _visualIndecators.GetComponentsInChildren<Transform>();

        foreach (var child in allChildren)
        {
            if (child.parent == _visualIndecators.transform)
                children.Add(child.gameObject);
        }

        return children;
    }

    private void DeActivateObjects(List<GameObject> objectList, int amount)
    {
        int deactivatedAmount = 0;
        foreach (var item in objectList)
        {
            if (deactivatedAmount >= amount) break;

            if (item.activeSelf)
            {
                item.SetActive(false);
                deactivatedAmount++;

                continue;
            }
        }
    }
}