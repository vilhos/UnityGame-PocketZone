using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private JoystickController joystick;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private Vector2 movement;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        // Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector2 direction = joystick.GetDirection();
        movement = direction * Time.deltaTime * moveSpeed;

        if (direction.x > 0)
        {
            anim.SetInteger("State", 2);
            transform.right = Vector3.right;    // Поворот вправо
        }
        else if (direction.x < 0)
        {
            anim.SetInteger("State", 2);
            transform.right = Vector3.left;     // Поворот влево
            movement.x = -movement.x;           // Инвертируем направление по X для движения влево
        }
        else if (direction.x == 0)
        {
            anim.SetInteger("State", 1);
        }

        transform.Translate(movement);
    }
    public Vector3 PlayerPosition
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
}
