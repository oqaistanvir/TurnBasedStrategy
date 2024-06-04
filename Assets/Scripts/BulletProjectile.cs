using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVFXPrefab;
    private Vector3 targetPosition;
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float moveSpeed = 200f;
        float distanceBeforeMoving = Vector3.Distance(targetPosition, transform.position);
        transform.position += moveSpeed * Time.deltaTime * moveDir;
        float distanceAfterMoving = Vector3.Distance(targetPosition, transform.position);
        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = targetPosition;
            trailRenderer.transform.parent = null;
            Destroy(gameObject);

            Instantiate(bulletHitVFXPrefab, targetPosition, Quaternion.identity);
        }
    }
}
