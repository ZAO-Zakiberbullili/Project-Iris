using UnityEngine;

public class InteractionTerritoryDetecter : MonoBehaviour
{
    [SerializeField] private Interaction _interaction;
    [SerializeField] private InteractionHandler _interactionHandler;
    private bool _alreadyInteract = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!_alreadyInteract)
            {
                _interaction.OnInteract += _interactionHandler.Interaction;
                _alreadyInteract = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_alreadyInteract)
            {
                _interaction.OnInteract -= _interactionHandler.Interaction;
                _alreadyInteract = false;
            }
        }
    }
}
