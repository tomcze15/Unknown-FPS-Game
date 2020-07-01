#define UNITY_EDITOR
#if     UNITY_EDITOR
#define RAY
#endif

using System;
using UnityEngine;

namespace CharacterController.FirstPerson
{
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public class FirstCharacterController : MonoBehaviour
    {
        [Serializable]
        public class MovementSettings 
        {
            [Header("Movement")]
            public bool MoveForward;
            public bool MoveBack;
            public bool MoveLeft;
            public bool MoveRight;
            public bool Jump;
            public bool Run;

            [Header("Other")]
            public bool isGrounded;
            public float Horizontal;
            public float Vertical;
            public float Speed;
            public float JumpHeight;
            public float RunMultiplier;
            public float BackMultiplier;
            public float SideMultiplier;
            public AnimationCurve SlopeCurveModifier;
        }

        [Serializable] 
        public class AdvancedSettings
        {
            public float Gravity;
            public float GravityMultiplier;
            public float AngleSlope;
            public Vector3 Velocity;

            public AdvancedSettings()
            {
                Velocity = Vector3.zero;
                GravityMultiplier = 1f;
                Gravity = -9.18f;
            }
        }

        [SerializeField] UnityEngine.CharacterController CharacterController;
        
        public MovementSettings             movementSettings = new MovementSettings();
        [SerializeField] AdvancedSettings   advancedSettings = new AdvancedSettings();

        [SerializeField] float      MaxGroundDetectionDistance  = 0f;
        [SerializeField] String     TurnOnSlopeByTag            = "";
        [SerializeField] LayerMask  GroundMask                  = 0;

        private Vector3     _curve;
        private Vector3     _moveForce;
        private Vector3     _slopeDirection;
        private GameObject  _currentHitObj;
        private float       _curretnHitDistance;

        // Start is called before the first frame update
        void Start()
        {
            if (!CharacterController)
                CharacterController = this.GetComponent<UnityEngine.CharacterController>();
        }

        private void FixedUpdate()
        {
            UpdateData();
            UpdateCheckGround();
            UpdateSlopeStatus();
            UpdateDirectionalMove();

            if (movementSettings.Jump && movementSettings.isGrounded)
                Jump();

            UpdateGravity();
            Move();
        }

        private void Move()
        {
            CharacterController.Move(_moveForce * Time.deltaTime);
            CharacterController.Move(advancedSettings.Velocity * Time.deltaTime);
        }

        private void Jump() 
            => advancedSettings.Velocity.y += Mathf.Sqrt(movementSettings.JumpHeight * -2f * advancedSettings.Gravity);

        private void UpdateDirectionalMove()
        {
            _moveForce = this.transform.right * movementSettings.Horizontal + this.transform.forward * movementSettings.Vertical * movementSettings.Speed;

            if (movementSettings.Run)
                _moveForce *= movementSettings.RunMultiplier;

            if (_currentHitObj)
            { 
                if (_currentHitObj.tag.Equals(TurnOnSlopeByTag)) 
                    _moveForce *= SlopeMultiplier();
            } 
        }

        private void UpdateGravity() 
            => advancedSettings.Velocity.y += advancedSettings.Gravity * advancedSettings.GravityMultiplier * Time.deltaTime;

        private void UpdateCheckGround()
        {
            movementSettings.isGrounded = Physics.CheckSphere(_curve - new Vector3(0, 0.1f, 0), CharacterController.radius, GroundMask);
            if (movementSettings.isGrounded && advancedSettings.Velocity.y < 0)
            {
                advancedSettings.Velocity.y = 0;
            }
        }

        private void UpdateSlopeStatus()
        {
            RaycastHit hitInfo;
            if(Physics.SphereCast(_curve, CharacterController.radius, -Vector3.up, out hitInfo, MaxGroundDetectionDistance, GroundMask, QueryTriggerInteraction.UseGlobal))
            {
                _currentHitObj = hitInfo.transform.gameObject;
                _curretnHitDistance = hitInfo.distance;
                _slopeDirection = Vector3.Cross(this.transform.right, hitInfo.normal);
                // Road inclination angle
                advancedSettings.AngleSlope = Vector3.Angle(-Vector3.up, _slopeDirection);
            }
            else 
            {
                _curretnHitDistance = MaxGroundDetectionDistance;
                _currentHitObj = null;
            }
#if RAY
            Vector3 origin = this.transform.position;
            origin.y += +CharacterController.center.y;
            Debug.DrawLine(origin, origin + _slopeDirection * 5, Color.blue);
            Debug.DrawLine(_curve, _curve - Vector3.up * _curretnHitDistance, Color.gray);
#endif
        }

        private void UpdateData()
        {
            var bottom = CharacterController.bounds.center - (Vector3.up * CharacterController.bounds.extents.y);
            _curve = bottom + (Vector3.up * CharacterController.radius);

            movementSettings.Horizontal = Input.GetAxis("Horizontal");
            movementSettings.Vertical = Input.GetAxis("Vertical");
        }

        private float SlopeMultiplier()
            => movementSettings.SlopeCurveModifier.Evaluate(advancedSettings.AngleSlope);

        private void OnDrawGizmosSelected()
        {
#if RAY
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_curve - Vector3.up * _curretnHitDistance, CharacterController.radius);
#endif
        }
    }
}