using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private readonly float totalSpinAmountMax = 360f;
    private float totalSpinAmount;
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        totalSpinAmount = totalSpinAmountMax;
    }

    private void Update()
    {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount -= spinAddAmount;
        if (totalSpinAmount <= 0f)
        {
            ActionComplete();
        }
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }
}
