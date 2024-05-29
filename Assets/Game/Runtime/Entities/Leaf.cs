using UnityEngine;

namespace Iris
{
    public enum PlayerState
    {
        Idle,
        Walk
    }

    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    internal sealed class Leaf : Character
    {
        private Rigidbody2D m_Rigidbody2D;
        private Animator m_Animator;

        private Vector2 m_Input = Vector2.zero;
        private Vector2 m_Target = Vector2.zero;

        private const float kWalkSpeed = 4f;
        private const float kGridSize = 1f;

        private static int s_Horizontal = Animator.StringToHash(kHorizontalAxis);
        private static int s_Vertical = Animator.StringToHash(kVerticalAxis);
        //private static int s_Idle = Animator.StringToHash(kIdleState);
        //private static int s_Walk = Animator.StringToHash(kWalkState);

        private const string kHorizontalAxis = "Horizontal";
        private const string kVerticalAxis = "Vertical";
        //private const string kIdleState = "Idle";
        //private const string kWalkState = "Walk";

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            GridBasedMovement();

            if (Vector2.Distance(transform.position, m_Target) <= float.Epsilon)
            {
                CaptureInputAndClampAxis();
                SetAnimatorParameters();
                CalculateMovementTarget();
            }
        }

        private void GridBasedMovement()
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Target, kWalkSpeed * Time.deltaTime);
        }

        private void SetAnimatorParameters()
        {
            bool isMoving = m_Input.sqrMagnitude != 0f;

            m_Animator.SetBool("IsMoving", isMoving);

            if (isMoving)
            {
                m_Animator.SetFloat(s_Horizontal, m_Input.x);
                m_Animator.SetFloat(s_Vertical, m_Input.y);
            }
        }

        private void CaptureInputAndClampAxis()
        {
            m_Input = new Vector2(Input.GetAxisRaw(kHorizontalAxis), Input.GetAxisRaw(kVerticalAxis));

            if (m_Input.y != 0f)
            {
                m_Input.x = 0f;
            }
        }

        private void CalculateMovementTarget()
        {
            m_Target = new Vector3(transform.position.x + (m_Input.x * kGridSize), transform.position.y + (m_Input.y * kGridSize));
        }
    }
}
