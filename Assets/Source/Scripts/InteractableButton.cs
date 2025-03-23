using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _controlableObject;
    [SerializeField] private SpriteRenderer _messageObject;

    private bool _isUsed = false;

    public void Interact()
    {
        if (_controlableObject != null && _controlableObject.TryGetComponent<IControlable>(out IControlable controlable))
        {
            controlable.PerfomAction();
            _controlableObject = null;
            Destroy(this);
            Destroy(GetComponent<Collider2D>());
        }
    }

    public void ShowMessage()
    {
        _messageObject.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        _messageObject.gameObject.SetActive(false);
    }
}
