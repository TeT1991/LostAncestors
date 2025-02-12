using UnityEngine;

public class Mover : MonoBehaviour
{
    public void Move(float speed)
    {
        transform.Translate(new Vector3(speed, 0, 0));
    }
}
