using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField] private int _pointId;
    public int PointId => _pointId;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
