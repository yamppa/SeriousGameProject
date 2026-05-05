using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    [SerializeField] private Transform target;


    // Rotate only around the y-axis

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Keep the y-axis rotation only
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion targetRotation = lookRotation * Quaternion.Euler(0, 180, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
