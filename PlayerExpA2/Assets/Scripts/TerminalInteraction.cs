using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalInteraction : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject terminalCanvas;
    [SerializeField] GameObject player;

    GrappleMovement grappleMovement;

    public bool interacting;

    void Start()
    {
        terminalCanvas.SetActive(false);

        interacting = false;


        grappleMovement = player.GetComponent<GrappleMovement>();
    }

    public void Interact()
    {
        terminalCanvas.SetActive(true);

        interacting = true;

        grappleMovement.Freeze();
        grappleMovement.HaltMovement();
    }

    public void Exit()
    {
        terminalCanvas.SetActive(false);

        interacting = false;

        grappleMovement.ContinueMovement();
    }
}
