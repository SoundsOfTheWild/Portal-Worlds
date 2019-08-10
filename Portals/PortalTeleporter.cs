using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalDemo
{
    public class PortalTeleporter : MonoBehaviour
    {

        public Transform player;
        public TeleportTarget reciever;
        PlayerLocalisation playerLocal;

        private bool playerIsOverlapping = false;

        private void Start()
        {
            playerLocal = player.gameObject.GetComponent<PlayerLocalisation>();
        }

        void Update()
        {
            // 1st Condition for player to be teleported: overlapping with portal trigger
            if (playerIsOverlapping)
            {

                //2nd condition for player to be teleported: player is outside the portal moving inwards (prevents teleportation backwards after player has been teleported)
                Vector3 portalToPlayer = player.position - transform.position;
                float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

                // If this is true: The player has moved across the portal
                if (dotProduct > 0f)
                {
                    // Teleport player
                    float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.transform.rotation);
                    rotationDiff += 180;
                    player.Rotate(Vector3.up, rotationDiff);

                    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                    player.position = reciever.transform.position + positionOffset;

                    playerIsOverlapping = false;

                    playerLocal.ChangePlayerWorld(reciever.world);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                playerIsOverlapping = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                playerIsOverlapping = false;
            }
        }
    }
}
