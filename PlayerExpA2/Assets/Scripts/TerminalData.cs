using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalData : MonoBehaviour
{
    public List<string> batteryCells = new List<string>();

    [SerializeField] int batteryCellCount;
    public int numberOfTerminals;

    private void Start()
    {
        for (int i = 0; i < batteryCellCount; i ++)
        {
            batteryCells.Add("empty");
        }
    }
}
