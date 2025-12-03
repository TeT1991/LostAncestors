using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    protected List<Enemy> _targets;

    protected bool _isActivated = false;
    protected bool _isReadyForUse = true;
    protected Coroutine _coroutine;
    protected ICoroutineRunner _coroutineRunner;
    protected float _tickTime = 1f;
    protected WaitForSeconds _waitForTick;
    protected WaitForSeconds _waitForReload;
    protected float _reloadTime = 4f;
    protected float _duration = 6f;

    public event Action<float> ProgreesChanged;
    public event Action<float> ReloadStarted;
    public event Action Reloaded;
    public event Action<float> Used;

    public Skill(Character character)
    {
        ResetTargets();
        _waitForReload = new(_reloadTime);
        _waitForTick = new(_tickTime);
        _coroutineRunner = character;
    }

    public bool IsActivated => _isActivated;
    public bool IsReadyForUse => _isReadyForUse;

    public float Duration => _duration;

    public virtual void Activate()
    {
        if (_isReadyForUse)
        {
            _isActivated = true;

            if (_coroutine != null)
            {
                _coroutineRunner.StopCoroutine(_coroutine);
            }

            _coroutine = _coroutineRunner.StartCoroutine(Use());
            _isReadyForUse = false;
            Used?.Invoke(_duration);
        }
    }

    public virtual void Deactivate()
    {
        if (_isActivated)
        {
            _isActivated = false;

            if (_coroutine != null)
            {
                _coroutineRunner.StopCoroutine(_coroutine);
            }

            _coroutineRunner.StopCoroutine(_coroutine);
            _coroutine = _coroutineRunner.StartCoroutine(Reload());
        }
    }

    public void AddTarget(Enemy target)
    {
        _targets.Add(target);
    }

    public void RemoveTarget(Enemy target)
    {
        _targets.Remove(target);
    }

    public void ResetTargets()
    {
        _targets = new();
    }
    public virtual void OnTick()
    {
    }

    public virtual IEnumerator Reload()
    {
        yield return _waitForReload;

    }

    public virtual IEnumerator Use()
    {
        yield return _waitForTick;
    }

    protected void NotifyUsed(float value)
    {
        Used?.Invoke(value);
    }

    protected void NotifyReloaded()
    {
        Reloaded?.Invoke();
    }

    protected void NotifyProgressStatus(float value)
    {
        ProgreesChanged?.Invoke(value);
    }

    protected void NotifyReloadStarted(float value)
    {
        ReloadStarted?.Invoke(value);
    }
}
