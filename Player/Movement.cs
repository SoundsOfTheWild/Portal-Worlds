using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {

        [SerializeField]
        private Camera cam;
        [SerializeField]
        float maxUpAngle = 80f;
        [SerializeField]
        float maxDownAngle = 90f;

        private Vector3 velocity = Vector3.zero;
        private float turnAngle = 0f;
        private float tiltAngle = 0f;

        private Rigidbody rb;

        public void SetVelocity(Vector3 pVelocity)
        {
            velocity = pVelocity;
        }

        public void SetTurnAngle(float pTurnAngle)
        {
            turnAngle = pTurnAngle;
        }

        public void SetTiltAngle(float pTiltAngle)
        {
            tiltAngle = pTiltAngle;
        }


        public void Jump(float jumpForce)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        //Movement calculated in physics simulation time
        private void FixedUpdate()
        {
            UpdatePosition();
            UpdateTurnRotation();
            UpdateTiltRotation();
        }

        private void UpdatePosition()
        {
            if (velocity != Vector3.zero)
            {
                //Attempts movement with physics checks
                rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime));
            }
        }

        private void UpdateTurnRotation()
        {
            Vector3 euler = (rb.rotation).eulerAngles;
            euler.y = turnAngle;

            //Attempts movement with physics checks           
            rb.MoveRotation(Quaternion.Euler(euler));
        }

        private void UpdateTiltRotation()
        {
            cam.transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
        }


        
    }
}
