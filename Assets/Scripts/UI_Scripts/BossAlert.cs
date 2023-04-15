using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAlert : MyUIBase
{
    [SerializeField] private AudioClip alertSFX = null;
    [SerializeField] private Bounce bounce = null;

    protected override void Awake()
    {
        base.Awake();
        if (!bounce) bounce = GetComponent<Bounce>();
    }

    public override void Show()
    {
        base.Show();
        bounce.StartBounce();
        SoundManager.Instance.PlayClip(alertSFX);
    }
}
