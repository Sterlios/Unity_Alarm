using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class Alarm: MonoBehaviour
{
    private AudioSource _audio;
    private Interactive _interactive;
    private Coroutine _playJob = null;
    private float _step = 0.05f;
    private float _maxValue = 1f;
    private float _minValue = 0f;
    private bool _isPlaying = false;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = 0;
        _audio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactive>(out _interactive))
        {
            _interactive.Used += OnUsed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactive>(out _interactive))
        {
            _interactive.Used -= OnUsed;
        }
    }

    private void OnUsed()
    {
        if (_playJob != null)
        {
            StopCoroutine(_playJob);
        }

        _isPlaying = !_isPlaying;
        float target = _isPlaying == true ? _maxValue : _minValue;

        _playJob = StartCoroutine(PlayCoroutine(target));
    }

    private IEnumerator PlayCoroutine(float target)
    {
        while (_audio.volume != target)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, target, _step * Time.deltaTime);
            yield return null;
        }
    }
}
