using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalDemo
{
    public class Portal : MonoBehaviour
    {
        public PortalTextures textures;
        public Transform entrancePosition;
        public Transform exitPosition;

        [SerializeField]
        PortalTeleporter teleporter;
        [SerializeField]
        PortalViewport entranceViewport;
        [SerializeField]
        PortalViewport exitViewport;

        //Set where portal should send the player and update both this and target camera textures
        public void UpdatePortalSettings(World pTarget)
        {
            teleporter.reciever = pTarget.exitTarget;
            //TODO close/obscure portal while it changes target
            entranceViewport.SetMaterial(pTarget.portal.textures.exitCameraMat);
            pTarget.portal.UpdateExitViewportMaterial(textures.entranceCameraMat);
        }

        //Update this world's exit portal camera texture
        public void UpdateExitViewportMaterial(Material pMaterial)
        {
            exitViewport.SetMaterial(pMaterial);
        }
    }
}
