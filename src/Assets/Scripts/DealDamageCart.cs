using System.Collections;
using UnityEngine;

public class DealDamageCart : MonoBehaviour
{
    [SerializeField]
    public float damage;

    [SerializeField]
    public float attackInterval = 2f;

    [SerializeField]
    public float attackRange = 2f;


    private bool isAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Dano no carrinho");
        if (collision.gameObject.CompareTag("Cart"))
        {
            StartCoroutine(ApplyDamage(collision.gameObject));
        }
    }
    private IEnumerator ApplyDamage(GameObject cart)
    {
        while (true)
        {
           // yield return new WaitForSeconds(damageInterval);
            cart.GetComponent<HealthSystem>()?.TakeDamage(damage);
        }
    }
}
