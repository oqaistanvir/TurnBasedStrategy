using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        BaseAction.ResetStaticData();
        DestructibleCrate.ResetStaticData();
        GrenadeProjectile.ResetStaticData();
        Unit.ResetStaticData();
        ShootAction.ResetStaticData();
        SwordAction.ResetStaticData();
    }
}
