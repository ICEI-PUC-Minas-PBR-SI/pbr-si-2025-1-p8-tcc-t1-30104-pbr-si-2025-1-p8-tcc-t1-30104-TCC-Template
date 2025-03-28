using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public bool canAttackInScene = false;

    private int _animIDAttack;
    private Animator _animator;
    private bool _hasAnimator;

    [HideInInspector]
    public bool isAttacking;

    void Start()
    {
        _animIDAttack = Animator.StringToHash("Attack");
        _hasAnimator = TryGetComponent(out _animator);
    }


    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && _animator.GetBool("Grounded") && canAttackInScene)
        {
            _animator.SetTrigger(_animIDAttack);
            Debug.Log("Botão esquerdo do mouse pressionado!");
            isAttacking = true;
        }
    }


    public void ResetAttack()
    {
        isAttacking = false;
    }


}
