using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue", fileName = "New Dialogue")]
public class Dialogue : ScriptableObject
{
    public Dialogue next;
    public string[] message;
    public string startFunction;
    public string objective;
}
