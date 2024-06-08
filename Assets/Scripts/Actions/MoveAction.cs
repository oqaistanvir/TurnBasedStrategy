using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;
    [SerializeField] private int maxMoveDistance = 4;
    private Vector3 targetPosition;
    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

        OnStartMoving?.Invoke(this, EventArgs.Empty);

        ActionStart(onActionComplete);
    }

    private void Update()
    {
        if (!isActive) return;
        float moveSpeed = 4f;
        float stoppingDistance = 0.1f;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
        {
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
        }
        else
        {
            OnStopMoving?.Invoke(this, EventArgs.Empty);
            ActionComplete();
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                // Checking out of bounds movement
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

                // Unit cannot move to the same grid position it was in
                if (unitGridPosition == testGridPosition) continue;

                //Grid Position already occupied
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                validGridPositionList.Add(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = targetCountAtGridPosition * 10,
        };
    }
}
