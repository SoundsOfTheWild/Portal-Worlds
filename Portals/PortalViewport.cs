using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PortalDemo
{
    public class PortalViewport : MonoBehaviour
    {
        MeshRenderer meshRenderer;

        //Update material for this portal when a new target/origin destination is set
        public void SetMaterial(Material pMaterial)
        {
            if(meshRenderer == null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }
            meshRenderer.material = pMaterial;
        }
    }
}
