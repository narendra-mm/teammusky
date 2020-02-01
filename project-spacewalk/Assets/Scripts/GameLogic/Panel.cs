using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceWalk.GameLogic
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Panel : MonoBehaviour
    {
        [SerializeField] private Transform _scanLight;
        private Material _panelMaterial;
        // Start is called before the first frame update
        void Start()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            _panelMaterial = meshRenderer.material;
        }

        public void SetScanMode(ToolType type)
        {
            _panelMaterial.SetInt("_Mask", (int)type);
        }

        public void AttachLightToTransform(Transform t)
        {
            _scanLight.parent = t;
        }
        public void DetachLight()
        {
            _scanLight.parent = transform;
        }
    }
}
