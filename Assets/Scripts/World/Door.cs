using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    

    public override void HandleInteraction()
    {
        base.HandleInteraction();
        Debug.Log("interacting with door");
        // goto next level / win screen
        SceneManager.LoadScene("Win");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && fsm.CurrentStateType == InteractableStateType.Off)
        {
            Highlight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && fsm.CurrentStateType == InteractableStateType.Highlighted)
        {
            fsm.GotoState(InteractableStateType.Off);
        }
    }
}
