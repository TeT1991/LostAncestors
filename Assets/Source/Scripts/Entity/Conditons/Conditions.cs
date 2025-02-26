using System.Collections.Generic;

public class Conditions 
{
    private List<bool> _conditions;

    public bool IsConditionsCompleted()
    {
        bool conditinsCompleted = false;

        foreach (var condition in _conditions)
        {
            conditinsCompleted = !condition;

            if (conditinsCompleted == false)
            {
                break;
            }
        }

        return conditinsCompleted;
    }

    public void UpdateConditionsStatus(params bool[] values)
    {
        _conditions = new List<bool>(values);

        foreach (var value in values)
        {
            _conditions.Add(value);
        }
    }
}
