using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public event Action<GameObject, GameObject> OnInteract;
    public GameObject InteractionGameObject { get { return _interactionGameObject; } set { _interactionGameObject = InteractionGameObject; }  }
    private InputSystem _inputSystem;
    private GameObject _interactionGameObject;
    private void Awake()
    {

        _inputSystem = new InputSystem();
        _inputSystem.Interaction.Interact.performed += context => Interacted();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
    }
    private void OnDisable()
    {
        _inputSystem.Disable();
    }

    private void Interacted()
    {
        OnInteract?.Invoke(gameObject, _interactionGameObject);
    }
}
