using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    public Skill(Character character, ICoroutineRunner coroutineRunner)
    {
        ResetTargets();
        _waitForReload = new(_reloadTime);
        _waitForTick = new(_tickTime);
        _coroutineRunner = character;
    }

    public bool IsActivated => _isActivated;
    public bool IsReadyForUse => _isReadyForUse;

    public virtual void Activate()
    {
        if (_isReadyForUse)
        {
            _isActivated = true;
            _coroutine = _coroutineRunner.StartCoroutine(Use());
            _isReadyForUse = false;
        }
    }

    public virtual void Deactivate()
    {
        if (_isActivated)
        {
            _isActivated = false;
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
}
