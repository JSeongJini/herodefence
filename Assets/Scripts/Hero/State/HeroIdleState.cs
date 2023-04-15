using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleState : IState<Hero>
{
    private Animator animator;

    public HeroIdleState(Hero _hero)
    {
        if (animator == null) animator = _hero.GetComponent<Animator>();
    }


    public void Enter(Hero _hero)
    {
        animator.speed = 1f;
    }

    public void Update(Hero _hero)
    {
    }

    public void End(Hero _hero)
    {
    }
}
