using UnityEngine;

public interface SC_inIntractable
{
    GameObject gameObject { get; }

    public void Interact(SC_InteractionTypes interactionType);
}
