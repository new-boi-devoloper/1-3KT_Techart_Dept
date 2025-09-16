using UnityEngine;

namespace WorkWithHLSL
{
    public class DissolveController : MonoBehaviour
    {
        public Material dissolveMaterial;
        public float dissolveSpeed = 0.5f;

        private float currentThreshold = 0f;

        private static readonly int DissolveThreshold = Shader.PropertyToID("_DissolveThreshold");

        void Update()
        {
            currentThreshold += dissolveSpeed * Time.deltaTime;
            dissolveMaterial.SetFloat(DissolveThreshold, currentThreshold);

            // Reset when complete
            if (currentThreshold > 1f)
            {
                currentThreshold = 0f;
            }
        }
    }
}