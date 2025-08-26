using UnityEngine;

public class EntityRotater : MonoBehaviour
{
    private readonly Entity _entity;
    public EntityRotater(Entity entity)
    {
        _entity = entity;
    }

    public void Rotate(int direction)
    {
        float positiveRotation = 0f;
        float negativeRotation = 180f;

        float yRotation = direction >= 0 ? positiveRotation : negativeRotation;
        _entity.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
