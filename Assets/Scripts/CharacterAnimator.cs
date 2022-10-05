using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool move;
    [SerializeField] bool attack;

    private void Awake()
    {
        animator  = GetComponent<Animator>();
    }

    public void StartMoving()
    { 
        move = true;
        animator.SetBool("Move", move);
    }

    public void StopMoving()
    { 
        move =false;
        animator.SetBool("Move", move);
    }

    public void Attack()
    { 
        attack = true;
        animator.SetBool("Attack", attack);
    }

    private void Update()
    {
        animator.SetBool("Move", move);
        animator.SetBool("Attack", attack);
    }

    private void LateUpdate()
    {
        if (attack == true)
        {
        attack = false;
        animator.SetBool("Attack", attack);
        }
    }
}
