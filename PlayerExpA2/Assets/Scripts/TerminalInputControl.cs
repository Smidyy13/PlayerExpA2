using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerminalInputControl : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text textBoxCol1;
    [SerializeField] TMP_Text textBoxCol2;

    [SerializeField] string terminalName;

    List<string> terminalSystems = new List<string>();

    TerminalData terminalData;
    TerminalInteraction terminalInteraction;

    TerminalFunctions terminalFunctions = new TerminalFunctions();

    float totalPowerDraw;

    private void Start()
    {
        terminalData = GetComponent<TerminalData>();

        terminalInteraction = GetComponent<TerminalInteraction>();

        inputField.caretWidth = 20;

        inputField.ActivateInputField();
    }

    void Update()
    {
        inputField.ActivateInputField();

        if (Input.GetKeyDown(KeyCode.Return) && terminalInteraction.interacting)
        {
            SubmitText();
        }
    }


    public void SubmitText()
    {
        MoveUpLine();
        textBoxCol1.text += "> " + inputField.text;

        SearchForFunction(inputField.text);

        inputField.text = null;
    }

    void SearchForFunction(string inputFunction)
    {
        string[] inputIndcFunc = inputFunction.Split(' ');

        if (inputIndcFunc[0] == "help")
        {
            textBoxCol1.text += "\nls\ncelldir\npowerdir\n";
            textBoxCol2.text += "\nadjst\nclr\nlink\nexit\n";
        }
        else if (inputIndcFunc[0] == "exit")
        {
            terminalInteraction.Exit();
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
                    PowerDirectory();
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
            terminalFunctions.ClearFunction(inputIndcFunc, terminalData, textBoxCol1, textBoxCol2);
        }
        else if (inputIndcFunc[0] == "link")
        {
            terminalFunctions.LinkSystemFunction(inputIndcFunc, terminalData, textBoxCol1, textBoxCol2);
        }
        else if (inputIndcFunc[0] == "adjst")
        {
            terminalFunctions.AdjustFunction(inputIndcFunc, terminalData, textBoxCol1, textBoxCol2);
        }
        else
        {
            if (inputFunction != "")
            {
                MoveUpLine();
                textBoxCol1.text += "The term" + " '" + inputFunction + "' " + "is not recognized try typing help for avaliable functions";
            }
        }
    }

    public void CellDirectory()
    {
        if (terminalData.batteryCellsGiven.Count > 4)
        {
            int numPerCol = terminalData.batteryCellsGiven.Count / 2;

            for (int i = 0; i < numPerCol; i++)
            {
                MoveUpLine();
                textBoxCol1.text += terminalData.batteryCellsGiven[i] + "   --   " + terminalData.cellSysConnection[i];
                textBoxCol2.text += terminalData.batteryCellsGiven[i + numPerCol] + "   --   " + terminalData.cellSysConnection[i + numPerCol];
            }
        }
        else
        {
            for (int i = 0; i < terminalData.batteryCellsGiven.Count; i++)
            {
                MoveUpLine();
                textBoxCol1.text += terminalData.batteryCellsGiven[i] + "   --   " + terminalData.cellSysConnection[i];
            }
        }

        textBoxCol1.text += "\n.......................................\nunassigned systems:";

        for (int i = 0; i < terminalData.unconnectedSystems.Count; i++)
        {
            textBoxCol1.text += "\n" + terminalData.unconnectedSystems[i];
        }

        textBoxCol2.text += "\n";
    }

    public void PowerDirectory()
    {
        if (terminalData.batteryCellsGiven.Count > 4)
        {
            int numPerCol = terminalData.batteryCellsGiven.Count / 2;

            for (int i = 0; i < numPerCol; i++)
            {
                MoveUpLine();
                textBoxCol1.text += terminalData.batteryCellsGiven[i] + "   --   " + terminalData.cellPower[i] + " kW-h";
                textBoxCol2.text += terminalData.batteryCellsGiven[i + numPerCol] + "   --   " + terminalData.cellPower[i + numPerCol] + " kW-h";
            }
        }
        else
        {
            for (int i = 0; i < terminalData.batteryCellsGiven.Count; i++)
            {
                MoveUpLine();
                textBoxCol1.text += terminalData.batteryCellsGiven[i] + "   --   " + terminalData.cellPower[i] + " kW-h";
            }
        }

        for (int i = 0; i < terminalData.cellPower.Count; i++)
        {
            totalPowerDraw += terminalData.cellPower[i];
        }

        textBoxCol1.text += "\n.......................................\ntotal terminal power draw: " + totalPowerDraw + " kW-h";

        textBoxCol2.text += "\n";
    }

    void MoveUpLine()
    {
        textBoxCol1.text += "\n";
        textBoxCol2.text += "\n";
    }
}
