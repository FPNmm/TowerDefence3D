using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private float FireRate = 1.0f;
    [SerializeField] private float damage;

    private float cooldown;

    private void Start()
    {
        cooldown = FireRate;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (cooldown <= 0f && enemy)
        {
            enemy.KillEnemy();
            cooldown = FireRate;
        }
    }
}
