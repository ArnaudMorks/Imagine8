using UnityEngine;

public class SC_PopUpImage : MonoBehaviour
{
    [SerializeField] private GameObject _popUp;
    private bool _isUp = false;

    public void OpenClosePopUp()
    {
        _isUp = !_isUp;
        _popUp.SetActive(_isUp);
    }
}
