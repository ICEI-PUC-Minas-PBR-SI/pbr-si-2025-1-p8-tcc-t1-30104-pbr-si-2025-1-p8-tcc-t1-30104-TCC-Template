using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public float health;
    


    public float maxHealth;
    
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !gameObject.CompareTag("Cart"))
        {
            StopAllCoroutines();
            Destroy(gameObject);         
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Damage " + damage);
    }

  


}
