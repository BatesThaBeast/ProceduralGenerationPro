using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalk Parameters_", menuName = "PCG/SimpleRandomWalk Data")]
public class SCR_SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
