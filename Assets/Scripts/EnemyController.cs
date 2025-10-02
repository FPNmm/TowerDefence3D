using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;


    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
