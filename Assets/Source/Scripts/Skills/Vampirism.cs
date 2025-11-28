using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : Skill
{
    private readonly Character _character;
    private UIValueBarsHolder _progressBar;

    public Vampirism(Character character, ICoroutineRunner coroutineRunner, UIValueBarsHolder progressBar) : base(character, coroutineRunner)
    {
        _character = character;
        _progressBar = progressBar;
        _progressBar.Init(_duration, _duration);
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
            OnTick();
            _progressBar.ChangeValue(_duration - time);

            yield return _waitForTick;

            time += _tickTime;

            if (time <= _duration)
            {
                OnTick();
                _progressBar.ChangeValue(_duration - time);
            }

            else
            {
                Deactivate();
            }
        }
    }

    public override IEnumerator Reload()
    {
        float time = 0;

        yield return null;

        while (_isReadyForUse == false)
        {
            _progressBar.ChangeValue(_duration - time);

            time += Time.deltaTime;

            if (time <= _reloadTime)
            {
                _progressBar.ChangeValue(_duration - time);
            }
            else
            {
                _isReadyForUse = true;
            }
        }
    }

    public override void Activate()
    {
        base.Activate();
        _progressBar.SetMaxValue(_duration);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _progressBar.SetMaxValue(_reloadTime);
    }
}
