using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _controlableObject;

    public void Interact()
    {
        if (_controlableObject.TryGetComponent<IControlable>(out IControlable controlable))
        {
            controlable.PerfomAction();
            Destroy(this);
            Destroy(GetComponent<Collider2D>());
        }
    }
}
