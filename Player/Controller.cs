using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalDemo
{
    [RequireComponent(typeof(Movement))]
    public class Controller : MonoBehaviour
    {

        [SerializeField]
        float moveSpeed = 5f;
        [SerializeField]
        float mouseSensitivityX = 3f;
        [SerializeField]
        float mouseSensitivityY = 2f;

        [SerializeField]
        float jumpForce = 5f;

        [SerializeField]
        float maxUpAngle = 80f;
        [SerializeField]
        float maxDownAngle = 90f;

        float turnAngle;

        float tiltAngle = 0f;

        Movement mover;
        
        private void Start()
        {
            //Set all to zero
            mover = GetComponent<Movement>();
            turnAngle = transform.rotation.eulerAngles.y;

            mover.SetVelocity(Vector3.zero);
            mover.SetTurnAngle(turnAngle);
            mover.SetTiltAngle(tiltAngle);

            //Hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            UpdateMovement();
            UpdateRotation();
            UpdateLockMode();
        }


        private void UpdateMovement()
        {
            //Calculate movement vector
            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");

            //Strafing movement
            Vector3 strafeMove = transform.right * xInput;
            //Forward/backward movement
            Vector3 walkMove = transform.forward * yInput;

            Vector3 velocity = (strafeMove + walkMove);

            //Limit velocity's magnitude without preventing slower movements
            if (velocity.sqrMagnitude > 1f)
            {
                velocity.Normalize();
            }

            velocity *= moveSpeed;

            //Send input movement to Mover
            mover.SetVelocity(velocity);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                mover.Jump(jumpForce);
            }
        }

        private void UpdateRotation()
        {
            //Calculate rotation vector
            float yAxisRotation = Input.GetAxisRaw("Mouse X");

            turnAngle += yAxisRotation * mouseSensitivityX;

            //Send rotation angle to Mover
            mover.SetTurnAngle(turnAngle);

            //Calculate camera tilt rotation vector
            float xAxisRotation = -Input.GetAxisRaw("Mouse Y");

            tiltAngle += xAxisRotation * mouseSensitivityY;

            tiltAngle = Mathf.Clamp(tiltAngle, -maxUpAngle, maxDownAngle);

            //Send camera tilt rotation to Mover
            mover.SetTiltAngle(tiltAngle);
        }

        //Change cursor restrictions if esc pressed
        private void UpdateLockMode()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if(Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
