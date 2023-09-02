using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalFunctions
{
    public void CellDirectory(TerminalData terminalData, TMP_Text textBoxCol1, TMP_Text textBoxCol2)
    {
        int numPerCol = terminalData.batteryCells.Count / 2;

        for (int i = 0; i < numPerCol; i++)
        {
            MoveUpLine(textBoxCol1, textBoxCol2);
            textBoxCol1.text += "cell " + i + "   --   " + terminalData.batteryCells[i];
            textBoxCol2.text += "cell " + (i + numPerCol) + "   --   " + terminalData.batteryCells[i + numPerCol];
        }

        textBoxCol2.text += "\n";
    }

    public void LinkFunction(string[] inputIndcFunc, TerminalData terminalData, TMP_Text textBoxCol1, TMP_Text textBoxCol2)
    {
        if (inputIndcFunc[1].Substring(0, 3) == "trm")
        {
            int terminalNumber = int.Parse(inputIndcFunc[1].Substring(inputIndcFunc[1].Length - 1, 1));
            int cellNumber = int.Parse(inputIndcFunc[2].Substring(4, 1));

            if (inputIndcFunc[2].Substring(0, 1) == "[" && inputIndcFunc[2].Substring(5, 1) == "]" && inputIndcFunc[2].Substring(1, 3) == "cll")
            {
                if (terminalNumber <= terminalData.terminals.Count)
                {
                    if (cellNumber <= terminalData.batteryCells.Count)
                    {
                        terminalData.batteryCells[cellNumber] = "terminal " + terminalNumber;
                        terminalData.terminals[terminalNumber-1].GetComponent<TerminalData>().GiveCell("cell " + cellNumber);
                    }
                    else
                    {
                        MoveUpLine(textBoxCol1, textBoxCol2);
                        textBoxCol1.text += "cell " + cellNumber + " does not exist";
                    }
                }
                else
                {
                    MoveUpLine(textBoxCol1, textBoxCol2);
                    textBoxCol1.text += "terminal " + terminalNumber + " does not exist";
                }
            }
            else
            {
                MoveUpLine(textBoxCol1, textBoxCol2);
                textBoxCol1.text += "incorrectly refferencing a cell";
            }
        }
        else
        {
            MoveUpLine(textBoxCol1, textBoxCol2);
            textBoxCol1.text += inputIndcFunc[1] + " cannot be connected to a battery cell";
        }
    }

    public void ClearFunction(string[] inputIndcFunc, TerminalData terminalData, TMP_Text textBoxCol1, TMP_Text textBoxCol2)
    {
        if (inputIndcFunc[1].Substring(0, 1) == "[" && inputIndcFunc[1].Substring(5, 1) == "]" && inputIndcFunc[1].Substring(1, 3) == "cll")
        {
            int cellNumber = int.Parse(inputIndcFunc[1].Substring(4, 1));
            if (cellNumber <= terminalData.batteryCells.Count)
            {
                terminalData.batteryCells[cellNumber] = "empty";
            }
            else
            {
                MoveUpLine(textBoxCol1, textBoxCol2);
                textBoxCol1.text += "cell " + cellNumber + " does not exist";
            }
        }
        else
        {
            MoveUpLine(textBoxCol1, textBoxCol2);
            textBoxCol1.text += "incorrectly refferencing a cell";
        }
    }

    public void MoveUpLine(TMP_Text textBoxCol1, TMP_Text textBoxCol2)
    {
        textBoxCol1.text += "\n";
        textBoxCol2.text += "\n";
    }

    public void AdjustFunction(string[] inputIndcFunc, TerminalData terminalData, TMP_Text textBoxCol1, TMP_Text textBoxCol2)
    { 
        int powerChange;

        if (int.TryParse(inputIndcFunc[1], out powerChange))
        {
            int cellNumber = int.Parse(inputIndcFunc[2].Substring(4, 1));

            Debug.Log("This is the cell number: " + cellNumber);
            Debug.Log("This is the power change: " + powerChange);

            if (inputIndcFunc[2].Substring(0, 1) == "[" && inputIndcFunc[2].Substring(5, 1) == "]" && inputIndcFunc[2].Substring(1, 3) == "cll")
            {
                for (int i = 0; i < terminalData.batteryCellsGiven.Count; i++)
                {
                    if (int.Parse(terminalData.batteryCellsGiven[i].Substring(terminalData.batteryCellsGiven[i].Length - 1, 1)) == cellNumber)
                    {
                        if (terminalData.cellSysConnection[i] != "empty")
                        {
                            terminalData.cellPower[i] = powerChange;
                        }
                        else
                        {
                            MoveUpLine(textBoxCol1, textBoxCol2);
                            textBoxCol1.text += "cell " + cellNumber + " does not have a system attached";
                        }
                    }
                    else
                    {
                        MoveUpLine(textBoxCol1, textBoxCol2);
                        textBoxCol1.text += "cell " + cellNumber + " does not exist";
                    }
                }
            }
            else
            {
                MoveUpLine(textBoxCol1, textBoxCol2);
                textBoxCol1.text += "incorrectly refferencing a cell";
            }
        }
        else
        {
            MoveUpLine(textBoxCol1, textBoxCol2);
            textBoxCol1.text += inputIndcFunc[1] + " cannot be connected to a battery cell";
        }
    }

    public void LinkSystemFunction(string[] inputIndcFunc, TerminalData terminalData, TMP_Text textBoxCol1, TMP_Text textBoxCol2)
    {
        if (inputIndcFunc[1].Substring(0, 3) == "sys")
        {
            int systemNumber = int.Parse(inputIndcFunc[1].Substring(inputIndcFunc[1].Length - 1, 1));
            int cellNumber = int.Parse(inputIndcFunc[2].Substring(4, 1));

            if (inputIndcFunc[2].Substring(0, 1) == "[" && inputIndcFunc[2].Substring(5, 1) == "]" && inputIndcFunc[2].Substring(1, 3) == "cll")
            {
                if (systemNumber <= terminalData.numberOfSystems)
                {
                    if (cellNumber <= terminalData.batteryCellsGiven.Count)
                    {
                        for (int i = 0; i < terminalData.batteryCellsGiven.Count; i ++)
                        {
                            if (int.Parse(terminalData.batteryCellsGiven[i].Substring(terminalData.batteryCellsGiven[i].Length - 1, 1)) == cellNumber)
                            {
                                Debug.Log("got to the end");
                                terminalData.cellSysConnection[i] = "system " + systemNumber;
                                terminalData.unconnectedSystems.Remove("system " + systemNumber);
                            }
                        }
                    }
                    else
                    {
                        MoveUpLine(textBoxCol1, textBoxCol2);
                        textBoxCol1.text += "cell " + cellNumber + " does not exist";
                    }
                }
                else
                {
                    MoveUpLine(textBoxCol1, textBoxCol2);
                    textBoxCol1.text += "system " + systemNumber + " does not exist";
                }
            }
            else
            {
                MoveUpLine(textBoxCol1, textBoxCol2);
                textBoxCol1.text += "incorrectly referencing a cell";
            }
        }
        else
        {
            MoveUpLine(textBoxCol1, textBoxCol2);
            textBoxCol1.text += inputIndcFunc[1] + " cannot be connected to a battery cell";
        }
    }
}
