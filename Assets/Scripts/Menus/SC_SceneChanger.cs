using TimeSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_SceneChanger : MonoBehaviour
{
    private SC_TimeManager _timeManager;


    public void ChangeScene(string scene)
    {
        TryToAssignTimeManager();
        ResetSigletonsValues();

        SceneManager.LoadScene(scene);
    }

    public void Quit() => Application.Quit();


    private void TryToAssignTimeManager()
    {
        if (SC_TimeManager.Instance == null)
        {
            Debug.LogWarning("TimeManager instance is missing!");
            return;
        }

        _timeManager = SC_TimeManager.Instance;
    }

    private void ResetSigletonsValues()
    {
        _timeManager.RunningIngameTime(true);

        //Expand...
    }
}
