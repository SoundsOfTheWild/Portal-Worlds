using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalDemo
{
    public class PortalCamera : MonoBehaviour
    {

        public Transform portal;
        public static Portal otherPortal;
        public static Vector3 playerCameraPosition;
        public static Vector3 playerCameraForward;

        [SerializeField]
        bool isEntranceCamera = false;

        //Gets player position and rotation information for all portals
        public static void UpdatePlayerCameraDetails(Vector3 pPos,Vector3 pForward)
        {
            playerCameraPosition = pPos;
            playerCameraForward = pForward;
        }

        // Update's portal camera positions relative to their respective portal to immitate player's postion relative to a viewport
        void LateUpdate()
        {
            Transform otherPortalTransform = (isEntranceCamera) ? otherPortal.exitPosition : otherPortal.entrancePosition;
            Vector3 playerOffsetFromPortal = playerCameraPosition - otherPortalTransform.position;
            transform.position = portal.position + playerOffsetFromPortal;

            float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortalTransform.rotation) + 180f;

            Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
            Vector3 newCameraDirection = portalRotationalDifference * playerCameraForward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
    }
}
