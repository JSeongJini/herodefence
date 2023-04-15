using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFailState : IState<Hero>
{
    private Animator animator;

    #region Cache
    private int hashFail = Animator.StringToHash("Fail");
    #endregion

    public HeroFailState(Hero _hero)
    {
        if (animator == null) animator = _hero.GetComponent<Animator>();
    }

    public void Enter(Hero _hero)
    {
        animator.speed = 1f;
        animator.SetTrigger(hashFail);
    }

    public void Update(Hero _hero)
    {
    }

    public void End(Hero _hero)
    {
    }
}
