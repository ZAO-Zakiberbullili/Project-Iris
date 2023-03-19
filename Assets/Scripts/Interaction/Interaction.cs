using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public event Action OnInteract;

    private InputSystem _inputSystem;

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
        OnInteract?.Invoke();
    }
}
