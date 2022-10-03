using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactive : MonoBehaviour
{
    private UnityEvent _used = new UnityEvent();

    public event UnityAction Used
    {
        add => _used.AddListener(value);
        remove => _used.RemoveListener(value);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _used.Invoke();
        }
    }
}
