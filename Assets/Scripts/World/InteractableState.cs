using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableStateType { Off, Highlighted, Interacting }

public abstract class InteractableState
{
    protected InteractableFSM owner;
    protected Interactable interactable;

    public void Initialize(InteractableFSM owner)
    {
        this.owner = owner;
        interactable = owner.owner;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class OffState : InteractableState 
{
    public override void Enter()
    {
        //interactable.spriteRenderer.sprite = interactable.normalSprite;
    }
    public override void Update()
    {
        
    }
    public override void Exit()
    {
        
    }
}
public class HighlightedState : InteractableState
{
    public override void Enter()
    {
        Debug.Log("enter highlighted");
        //interactable.spriteRenderer.sprite = interactable.highlightedSprite;
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            owner.GotoState(InteractableStateType.Interacting);
        }
    }
    public override void Exit()
    {
        Debug.Log("exit highlighted");
    }
}
public class InteractingState : InteractableState
{
    public override void Enter()
    {
        interactable.HandleInteraction();
    }
    public override void Update()
    {
        
    }
    public override void Exit()
    {
        
    }
}