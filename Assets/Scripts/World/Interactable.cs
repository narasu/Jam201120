using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IPressable
{
    
    [HideInInspector] public SpriteRenderer spriteRenderer;
    protected InteractableFSM fsm;

    public List<GameObject> highlightObjects;
    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        fsm = new InteractableFSM();
        foreach (GameObject o in highlightObjects) o.SetActive(false);
    }

    void Start()
    {
        fsm.Initialize(this);

        fsm.AddState(InteractableStateType.Off, new OffState());
        fsm.AddState(InteractableStateType.Highlighted, new HighlightedState());
        fsm.AddState(InteractableStateType.Interacting, new InteractingState());

        fsm.GotoState(InteractableStateType.Off);
    }

    void Update()
    {
        fsm.UpdateState();
    }

    public void Highlight()
    {
        if (fsm.CurrentStateType==InteractableStateType.Off)
        {
            fsm.GotoState(InteractableStateType.Highlighted);
        }
    }

    public virtual void HandleInteraction()
    {

    }

}
