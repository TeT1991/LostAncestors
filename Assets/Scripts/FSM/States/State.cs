using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class State 
{
    public virtual void Enter()
    {
        Debug.Log("Enter " + this.GetType());
    }

    public virtual void Update()
    {
        Debug.Log("Update " + this.GetType());
    }

    public virtual void Exit()
    {
        Debug.Log("Exit " + this.GetType());
    }
}
