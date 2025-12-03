using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vampirism : Skill
{
    private readonly Character _character;

    public Vampirism(Character character) : base(character)
    {
        _character = character;
    }

    public override void OnTick()
    {
        float damage = 10;

        var targetsSnapshot = new List<Enemy>(_targets);

        foreach (Enemy target in targetsSnapshot)
        {
            if (target != null)
            {
                target.TakeDamage(damage);
                _character.Health.IncreaseHealth(damage);
            }
        }
    }

    public override IEnumerator Use()
    {
        float time = 0;

        while (_isActivated)
        {
            yield return _waitForTick;

            time += _tickTime;

            if (time < _duration)
            {
                OnTick();
                NotifyProgressStatus(_duration - time);
            }
            else
            {
                NotifyProgressStatus(_duration - time);
                Deactivate();
                yield break;
            }
        }
    }

    public override IEnumerator Reload()
    {
        float time = 0;
        NotifyReloadStarted(_reloadTime);

        while (_isReadyForUse == false)
        {
            time += Time.deltaTime;

            if (time >= _reloadTime)
            {
                _isReadyForUse = true;
            }
 
            float progress = time;
            NotifyProgressStatus(progress);
            yield return null;
        }

        NotifyReloaded();
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
