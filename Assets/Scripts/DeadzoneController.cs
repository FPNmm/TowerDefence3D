using UnityEngine;

public class DeadzoneController : MonoBehaviour
{
    [SerializeField] private int health;

    private int currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();

        if (enemy)
        {
            currentHealth -= 1;
            enemy.KillEnemy();
            if (currentHealth == 0) Debug.Log("You lose");
        }
    }
}
