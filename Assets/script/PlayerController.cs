using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _turnSpeed = 360f;
    [SerializeField] private float _speed = 5f;
    private Vector3 _input;
    private void Update()
    {
        GatherInput(); 
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GatherInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _input = new Vector3(x, 0, y).normalized;

        if (x != 0 || y != 0)
            _anim.SetBool("IsRunning", true);
        else
            _anim.SetBool("IsRunning", false);
    }

    void Look()
    {
        if (_input != Vector3.zero)
        {

            var relative = (transform.position + _input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }

    void Move()
    {

        _rb.MovePosition(transform.position + _input.ToIso() * _speed * Time.fixedDeltaTime);
    }
}
