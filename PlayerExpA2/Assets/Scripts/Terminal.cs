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

    private void Start()
    {
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
        textBoxCol1.text += "\n> " + inputField.text;
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
                textBoxCol1.text += "\ntype in a directory name to list";
            }
            else
            {
                if (inputIndcFunc[1] == "celldir")
                {
                    textBoxCol1.text += "\ncell 1   --  terminal 1\ncell 2   --  terminal 1";
                    textBoxCol2.text += "\ncell 2   --  \ncell 3   --  terminal 1";
                }
                else if (inputIndcFunc[1] == "powerdir")
                {
                    textBoxCol1.text += "\ncell 1   --  5kW-h\ncell 2   --  0kW-h\ntotal power draw: 10kW-h";
                    textBoxCol2.text += "\ncell 2   --  0kW-h\ncell 3   --  5kW-h";
                }
                else
                {
                    textBoxCol1.text += "\nthis directory does not exist";
                }
            }
        }
        else if (inputIndcFunc[0] == "clr")
        {
            textBoxCol1.text += "\nthis is not yet implemented";
        }
        else if (inputIndcFunc[0] == "link")
        {
            textBoxCol1.text += "\nthis is not yet implemented";
        }
        else 
        {
            textBoxCol1.text += "\nThe term" + " '" + inputFunction + "' " + "is not recognized try typing help for avaliable functions";
        }
    }
}
