using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    private Vector3 tempPos;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float minX, maxX;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (!target)
            return;

        Vector3 targetPosition = target.position + offset;

        tempPos = transform.position;
        tempPos.x = targetPosition.x;

        if (tempPos.x < minX)
            tempPos.x = minX;

        if (tempPos.x > maxX)
            tempPos.x = maxX;

        transform.position = tempPos;

    }
}
