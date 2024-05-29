using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;
    private Vector3 targetPosition;
    private Unit unit;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    private void Update()
    {
        float moveSpeed = 4f;
        float rotateSpeed = 10f;
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            unitAnimator.SetBool(IS_WALKING, true);
        }
        else
        {
            unitAnimator.SetBool(IS_WALKING, false);
        }
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
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
}
