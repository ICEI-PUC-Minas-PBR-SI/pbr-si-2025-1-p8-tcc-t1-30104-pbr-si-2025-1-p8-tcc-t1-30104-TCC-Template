using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float attackInterval = 1.5f; 
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float blinkDuration = 0.1f;

    private bool isAttacking = false;
    private Animator _animator;
    public Transform target;

    private NavMeshAgent agent;
    private bool canAttack = true;
    private Color defaultColor;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        defaultColor = skinnedMeshRenderer.material.color;

        GameObject cart = GameObject.FindGameObjectWithTag("Cart");
        if (cart != null)
        {
            target = cart.transform;
        }
    }

    void Update()
    {
        MoveToTarget();
        DetectCartAndAttack();
        UpdateAnimation();
    }

    void DetectCartAndAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Cart") && canAttack)
            {
                Attack();
                return;
            }
        }
    }

    void UpdateAnimation()
    {
        _animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void MoveToTarget()
    {
        if (target != null && !isAttacking)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }

    void Attack()
    {
        canAttack = false;
        isAttacking = true;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.ResetPath();

        _animator.SetTrigger("Attack");


    }

    public void ResetAttack()
    {
        DealDemageCart script = weapon.GetComponent<DealDemageCart>();
        script.hasDamaged = false;
    }
    public void OnAttackEnd()
    {

        agent.isStopped = false;
        isAttacking = false;

        StartCoroutine(OnTriggerAttackInterval());
    }

    public IEnumerator OnTriggerAttackInterval()
    {
        yield return new WaitForSeconds(attackInterval);

        canAttack = true;
    }

    public IEnumerator BlinkEffect()
    {
        if (skinnedMeshRenderer != null && skinnedMeshRenderer.material)
        {
            skinnedMeshRenderer.material.color = Color.white * 4f;
            yield return new WaitForSeconds(blinkDuration);
            skinnedMeshRenderer.material.color = defaultColor;
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
