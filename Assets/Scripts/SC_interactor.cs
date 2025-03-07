using UnityEngine;
using UnityEngine.InputSystem;

public class SC_interactor : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            TryInteract(SC_InteractionTypes.LeftButton);
        }

        if (Mouse.current.rightButton.IsPressed())
        {
            TryInteract(SC_InteractionTypes.RightButton);
        }
    }

    private void TryInteract(SC_InteractionTypes interactionType)
    {
        Vector2 rayOrigin = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).origin;
        Debug.Log(rayOrigin);
        RaycastHit2D hit;

        if (hit = Physics2D.Raycast(rayOrigin, Vector3.forward))
        {
            Debug.Log("HIT!");
            SC_inIntractable item;
            hit.transform.gameObject.TryGetComponent(out item);

            if (item == null)
                return;

            Interact(item, interactionType);
        }
    }

    private void Interact(SC_inIntractable item, SC_InteractionTypes interactionType) => item.Interact(interactionType);
}
