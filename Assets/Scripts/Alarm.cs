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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactive>(out _interactive))
        {
            _interactive.Used += Play;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactive>(out _interactive))
        {
            _interactive.Used -= Play;
        }
    }

    private void Play()
    {
        if (_playJob != null)
        {
            StopCoroutine(_playJob);
        }

        _playJob = StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        if (_isPlaying == false)
        {
            _audio.Play();
            _isPlaying = true;

            while (_audio.volume < _maxValue)
            {
                _audio.volume += Mathf.MoveTowards(_minValue, _maxValue, _step * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            _isPlaying = false;

            while (_audio.volume > _minValue)
            {
                _audio.volume -= Mathf.MoveTowards(_minValue, _maxValue, _step * Time.deltaTime);
                yield return null;
            }

            _audio.Stop();
        }
    }
}
