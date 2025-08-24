using UnityEngine;

public class Rotater : MonoBehaviour
{
    public void Rotate(float direction)
    {
        float rightRotation = 0;
        float leftRotation = 180;

        switch (direction)
        {
            case > 0:
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rightRotation, transform.localEulerAngles.z);
                break;
            case < 0:
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, leftRotation, transform.localEulerAngles.z);
                break;
        }
    }
}
