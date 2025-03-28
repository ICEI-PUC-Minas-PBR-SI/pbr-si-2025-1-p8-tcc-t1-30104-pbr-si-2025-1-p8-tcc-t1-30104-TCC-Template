using UnityEngine;

public class DealDamage : MonoBehaviour
{

    [SerializeField] private float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            HealthSystem enemy = other.GetComponent<HealthSystem>();
            EnemyController enemyController = other.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
            StartCoroutine(enemyController.BlinkEffect());
        }
    }

}
