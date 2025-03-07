using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// What does it need
/// 
/// stamp Image
/// currentColor color
/// 
/// detect ink pad to change color
/// 
/// Move-able
///     Left click to drag & Stamp something
///     Right click to return to original pos
/// return position
/// 
/// 
/// </summary>
public class SC_Stamp : MonoBehaviour, SC_inIntractable
{
    [SerializeField] private Texture2D _stampIcon;
    private Color _currentStampColor;
    private float _stampAmount = 1;

    private Vector3 _originalPosition;
    private GameObject _collidingObject;

    private bool _isMoving = false;
    private bool _isStamping = false;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    public void Interact(SC_InteractionTypes interactionType)
    {
        if (interactionType == SC_InteractionTypes.LeftButton && _isMoving)
        {
            Stamp();
            return;
        }

        if (interactionType == SC_InteractionTypes.LeftButton)
        {
            MoveStamp();
            return;
        }

        if (interactionType == SC_InteractionTypes.RightButton)
        {
            ReturnStamp();
            return;
        }
    }

    public void ChangeColor(Color color)
    {

    }

    private void Stamp()
    {
        if (_collidingObject.GetComponent<SC_InkPad>() != null)
        {

        }
    }

    private void MoveStamp()
    {
        Vector2 mousePosition = Mouse.current.position.value;
        transform.position = mousePosition;
    }

    private void ReturnStamp()
    {
        transform.position = _originalPosition;
    }

    private void OnTriggerEnter(Collider other) => _collidingObject = other.gameObject;

    private void OnTriggerExit(Collider other) => _collidingObject = null;
}
