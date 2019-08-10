using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalDemo
{
    public class PortalTextures : MonoBehaviour
    {

        [SerializeField]
        Camera entranceCamera;
        [SerializeField]
        Camera exitCamera;

        public Material entranceCameraMat;
        public Material exitCameraMat;

        void Start()
        {
            CalculateTextureSize(entranceCamera, entranceCameraMat);
            CalculateTextureSize(exitCamera, exitCameraMat);
        }

        //Set textures to be correct resolution for screen size
        private void CalculateTextureSize(Camera cam, Material cameraMaterial)
        {
            if (cam.targetTexture != null)
            {
                cam.targetTexture.Release();
            }
            cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameraMaterial.mainTexture = cam.targetTexture;
        }

    }
}
