<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> b9303f46096a31d8213b5436b541e36359e917f9
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _controlableObject;
<<<<<<< HEAD
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
=======

    public void Interact()
    {
        if (_controlableObject.TryGetComponent<IControlable>(out IControlable controlable))
        {
            controlable.PerfomAction();
            Destroy(this);
            Destroy(GetComponent<Collider2D>());
        }
    }
>>>>>>> b9303f46096a31d8213b5436b541e36359e917f9
}
