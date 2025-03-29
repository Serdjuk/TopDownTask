using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform target;

    Vector3 m_targetPosition;

    void Update()
    {
        if (target != null)
        {
            m_targetPosition = new Vector3(target.position.x, target.position.y, -10.0f);
        }

        transform.position = Vector3.Lerp(transform.position, m_targetPosition, Time.deltaTime * 10.0f);
    }
}