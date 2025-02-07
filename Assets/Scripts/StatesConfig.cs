using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatesConsfig", menuName = "Configs/StatesConfig")]
public class StatesConfig : ScriptableObject
{
    [SerializeField] public List<States> States;
}