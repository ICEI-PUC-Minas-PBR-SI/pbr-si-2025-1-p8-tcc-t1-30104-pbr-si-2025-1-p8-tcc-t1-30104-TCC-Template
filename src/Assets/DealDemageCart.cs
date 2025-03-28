using UnityEngine;

public class DealDemageCart : MonoBehaviour
{

    [SerializeField]
    public float attackRadius = 1f;
    [SerializeField]
    public float damage = 10f;

    public bool hasDamaged = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRadius);

        if(hitEnemies.Length > 0 )
        {
            if (hitEnemies[0].CompareTag("Cart") && !hasDamaged)
            {

                HealthSystem damageable = hitEnemies[0].GetComponent<HealthSystem>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                    hasDamaged = true;
                }
            }
        }


   
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
