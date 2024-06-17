using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScripting : MonoBehaviour
{
    [SerializeField] private GameObject[] part1GameObjects;
    [SerializeField] private GameObject[] part2GameObjects;
    [SerializeField] private GameObject part1Hider;
    [SerializeField] private GameObject part2Hider;
    [SerializeField] private Door door;
    private List<Unit> friendlyUnits;

    private void Start()
    {
        PartVisibility(false, part1GameObjects, part1Hider);
        PartVisibility(false, part2GameObjects, part2Hider);
    }
    private void Update()
    {
        friendlyUnits = UnitManager.Instance.GetFriendlyUnitList();
        foreach (Unit unit in friendlyUnits)
        {
            GridPosition gridPosition = unit.GetGridPosition();
            if (gridPosition.x <= 9 && gridPosition.z >= 6)
            {
                PartVisibility(true, part1GameObjects, part1Hider);
            }
        }
        if (door.IsOpen()) PartVisibility(true, part2GameObjects, part2Hider);
    }
    private void PartVisibility(bool isVisible, GameObject[] partGameObjects, GameObject partHider)
    {
        foreach (GameObject gameObject in partGameObjects)
        {
            if (gameObject == null) continue;
            gameObject.SetActive(isVisible);
        }
        partHider.SetActive(!isVisible);
    }

}
