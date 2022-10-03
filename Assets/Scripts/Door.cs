using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    private Animator _animator;
    private Interactive _interactive = null;
    private UnityEvent _opened = new UnityEvent();

    public event UnityAction Opened
    {
        add => _opened.AddListener(value);
        remove => _opened.RemoveListener(value);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsOpen = false;
    }

    private void ChangeState()
    {
        IsOpen = IsOpen ? false : true;
        _opened.Invoke();
        _animator.SetBool("isOpen", IsOpen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactive>(out _interactive))
        {
            _interactive.Used += ChangeState;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactive>(out _interactive))
        {
            _interactive.Used -= ChangeState;
            _interactive = null;
        }
    }
}