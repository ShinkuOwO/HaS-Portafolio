using UnityEngine;

namespace S_Characters.S_Player
{
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private IInput _input;
        [Header("Player Settings")]
        public float playerSpeed = 2.0f;
        public float playerSprint = 4.0f;
        public float jumpHeight = 1.0f;
        public float gravityValue = -9.81f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Inputs playerInputs = new Inputs();
            _input = new InputManager(playerInputs);
            _input.OnEnable();
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            IsMove();
        }

        private void IsMove()
        {
            Vector2 movement = _input.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x,0,movement.y);

            move = transform.TransformDirection(move);
            
            _rigidbody.MovePosition(_rigidbody.position + move * (playerSpeed * Time.fixedDeltaTime));

        }
    }
}
