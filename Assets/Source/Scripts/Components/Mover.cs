using UnityEngine;
public class Mover : MonoBehaviour
{
    public void MoveHorizontal(float speed)
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void MoveVecrtiacal(float speed)
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
