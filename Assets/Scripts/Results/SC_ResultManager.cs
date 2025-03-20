using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SC_ScoringSystem))]
public class SC_ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject _resultScreen;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _DurationText;

    private SC_ScoringSystem _ScoringSystem;
    private SC_PackageSpawner _PackageSpawner;

    public Action OnResultScreenEnabled;
    public Action OnResultScreenDisabled;

    private void Awake()
    {
        _resultScreen.SetActive(false);

        _ScoringSystem = GetComponent<SC_ScoringSystem>();

        _PackageSpawner = FindFirstObjectByType<SC_PackageSpawner>();
        _PackageSpawner.OnFinishList += ShowResults;
    }

    private void ShowResults()
    {
        SetScore();
        SetResultScreen(true);
    }
    private void SetScore()
    {
        int scoreAmount = _ScoringSystem.GetScoring();
        int maxScoreAmount = _ScoringSystem.GetMaxScoring();

        string score = $"Score: {scoreAmount}/{maxScoreAmount}";
        _scoreText.text = score;
    }

    private void SetResultScreen(bool state)
    {
        _resultScreen.SetActive(state);

        if (state) OnResultScreenEnabled?.Invoke();
        else OnResultScreenDisabled?.Invoke();
    }

}
