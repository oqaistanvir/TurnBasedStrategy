using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private const string IS_OPEN = "IsOpen";
    [SerializeField] private bool isOpen;
    private GridPosition gridPosition;
    private Animator animator;
    private Action onInteractComplete;
    private bool isActive;
    private float timer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        if (isOpen) OpenDoor();
        else CloseDoor();
    }
    private void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isActive = false;
            onInteractComplete();
        }
    }
    public void Interact(Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;
        timer = 0.5f;

        if (!isOpen) OpenDoor();
        else CloseDoor();
    }

    private void OpenDoor()
    {
        isOpen = true;
        animator.SetBool(IS_OPEN, isOpen);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, true);
    }

    private void CloseDoor()
    {
        isOpen = false;
        animator.SetBool(IS_OPEN, isOpen);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, false);
    }
    public bool IsOpen()
    {
        return isOpen;
    }
}
