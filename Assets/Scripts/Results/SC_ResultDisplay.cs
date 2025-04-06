using CameraSystem;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SC_ScoringManager))]
public class SC_ResultDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _resultScreen;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _DurationText;

    private SC_ScoringManager _ScoringSystem;
    private SC_PackageSpawner _PackageSpawner;

    public Action OnResultScreenEnabled;
    public Action OnResultScreenDisabled;

    private void Awake()
    {
        _resultScreen.SetActive(false);

        _ScoringSystem = GetComponent<SC_ScoringManager>();

        _PackageSpawner = FindFirstObjectByType<SC_PackageSpawner>();
        _PackageSpawner.OnFinishList += ShowResults;
    }

    /// <summary>
    /// Sets the final state of the game.
    /// </summary>
    /// <param name="state"></param>
    public void SetResultScreen(bool state)
    {
        SC_CameraManager.Instance.ViewerToMainView();

        _resultScreen.SetActive(state);

        if (state) OnResultScreenEnabled?.Invoke();
        else OnResultScreenDisabled?.Invoke();
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
}
