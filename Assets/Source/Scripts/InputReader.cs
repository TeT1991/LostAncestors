using UnityEngine;

public class InputReader 
{
    public KeyCode GetPressedButton()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }
}
