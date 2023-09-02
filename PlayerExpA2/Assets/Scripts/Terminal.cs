using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Terminal : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text textBoxCol1;
    [SerializeField] TMP_Text textBoxCol2;

    [SerializeField] string terminalName;

    TerminalData terminalData;

    private void Start()
    {
        terminalData = GetComponent<TerminalData>();

        inputField.caretWidth = 20;

        inputField.ActivateInputField();
    }

    private void Update()
    {
        inputField.ActivateInputField();

        if (Input.GetKeyUp(KeyCode.Return)) 
        { 
            SubmitText(); 
        }
    }


    public void SubmitText()
    {
        MoveUpLine();
        textBoxCol1.text += "> " + inputField.text;
        textBoxCol2.text += "\n";

        SearchForFunction(inputField.text);

        inputField.text = null;
    }

    void SearchForFunction(string inputFunction)
    {
        string[] inputIndcFunc = inputFunction.Split(' ');

        if (inputIndcFunc[0] == "help")
        {
            textBoxCol1.text += "\nls\ncelldir\npowerdir";
            textBoxCol2.text += "\nadjst\nclr\nlink";
        }
        else if (inputIndcFunc[0] == "ls")
        {
            if (!(inputIndcFunc.Length > 1))
            {
                MoveUpLine();
                textBoxCol1.text += "type in a directory name to list";
            }
            else
            {
                if (inputIndcFunc[1] == "celldir")
                {
                    CellDirectory();
                }
                else if (inputIndcFunc[1] == "powerdir")
                {
                    textBoxCol1.text += "\ncell 1   --  5kW-h\ncell 2   --  0kW-h\ntotal power draw: 10kW-h";
                    textBoxCol2.text += "\ncell 2   --  0kW-h\ncell 3   --  5kW-h";
                }
                else
                {
                    MoveUpLine();
                    textBoxCol1.text += "this directory does not exist";
                }
            }
        }
        else if (inputIndcFunc[0] == "clr")
        {
            ClearFunction(inputIndcFunc);
        }
        else if (inputIndcFunc[0] == "link")
        {
            LinkFunction(inputIndcFunc);
        }
        else
        {
            MoveUpLine();
            textBoxCol1.text += "The term" + " '" + inputFunction + "' " + "is not recognized try typing help for avaliable functions";
        }
    }

    void CellDirectory()
    {
        int numPerCol = terminalData.batteryCells.Count / 2;   

        for (int i = 0; i < numPerCol; i ++)
        {
            MoveUpLine();
            textBoxCol1.text += "cell " + i + "   --   " + terminalData.batteryCells[i];
            textBoxCol2.text += "cell " + (i + numPerCol) + "   --   " + terminalData.batteryCells[i + numPerCol];
        }
    }
       
    void LinkFunction(string[] inputIndcFunc)
    {
        if (inputIndcFunc[1].Substring(0, 3) == "trm")
        {
            int terminalNumber = int.Parse(inputIndcFunc[1].Substring(inputIndcFunc[1].Length - 1, 1));
            int cellNumber = int.Parse(inputIndcFunc[2].Substring(4, 1));

            if (inputIndcFunc[2].Substring(0, 1) == "[" && inputIndcFunc[2].Substring(5, 1) == "]" && inputIndcFunc[2].Substring(1, 3) == "cll")
            {
                if (terminalNumber <= terminalData.numberOfTerminals)
                {
                    if (cellNumber <= terminalData.batteryCells.Count)
                    {
                        terminalData.batteryCells[cellNumber] = "Terminal " + terminalNumber;
                    }
                    else
                    {
                        MoveUpLine();
                        textBoxCol1.text += "Cell " + cellNumber + " does not exist";
                    }
                }
                else
                {
                    MoveUpLine();
                    textBoxCol1.text += "Terminal " + terminalNumber + " does not exist";
                }
            }
            else
            {
                MoveUpLine();
                textBoxCol1.text += "Incorrectly refferencing a cell";
            }
        }
        else
        {
            MoveUpLine();
            textBoxCol1.text += inputIndcFunc[1] + " cannot be connected to a battery cell";
        }
    }
        
    void ClearFunction(string[] inputIndcFunc)
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
                MoveUpLine();
                textBoxCol1.text += "Cell " + cellNumber + " does not exist";
            }
        }
        else
        {
            MoveUpLine();
            textBoxCol1.text += "Incorrectly refferencing a cell";
        }
    }

    void MoveUpLine()
    {
        textBoxCol1.text += "\n";
        textBoxCol2.text += "\n";
    }
}
