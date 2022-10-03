using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    [SerializeField] private int _speed = 1;

    private bool _isEntered = false;
    private Animator _animator;
    private Door _door;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(_isEntered == false)
        {
            float direction = Input.GetAxis("Horizontal");
            _animator.SetBool("isWalk", direction != 0);
            _spriteRenderer.flipX = direction < 0;
            transform.position += new Vector3(direction * _speed * Time.deltaTime, 0, 0);
        }
    }

    private void EnterInHouse()
    {
        _isEntered = _isEntered ? false : true;
        _spriteRenderer.enabled = !_isEntered;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Door>(out _door))
        {
            _door.Opened += EnterInHouse;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Door>(out _door))
        {
            _door.Opened -= EnterInHouse;
        }
    }
}
