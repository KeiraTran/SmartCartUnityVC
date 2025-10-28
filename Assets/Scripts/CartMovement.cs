using UnityEngine;
using UnityEngine.AI;

public class CartMovement : MonoBehaviour
{
    public Camera mainCamera;           // Reference to your third-person camera
    public float distance = 0.035f;         // How far in front of the cart the target point should be

    private NavMeshAgent agent;

    private void Start()
    {
        // Get NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Auto-assign if not set
        }
    }

    private void Update()
    {
        // Ray from the camera to where the mouse is pointing
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Assume floor is at y=0
        Plane floor = new Plane(Vector3.up, Vector3.zero);

        if (floor.Raycast(ray, out float enter))
        {
            // World point where mouse ray hits the floor
            Vector3 hitPoint = ray.GetPoint(enter);

            // Compute a target position a little behind the mouse point
            Vector3 direction = (hitPoint - transform.position).normalized;
            Vector3 targetPos = hitPoint - direction * distance;

            // Tell the NavMeshAgent to move there
            agent.SetDestination(targetPos);
        }
    }
}
