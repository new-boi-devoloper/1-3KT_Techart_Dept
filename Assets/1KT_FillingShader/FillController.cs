using System.Collections;
using UnityEngine;

namespace _1KT_FillingShader
{
    public class FillController : MonoBehaviour
    {
        public float fillSpeed = 0.5f;
        public float unfillSpeed = 0.3f;
        public string fillProperty = "_FillAmount";
    
        private Material material;
        private bool isMouseOver = false;
        private float targetFill = 0f;

        void Start()
        {
            material = GetComponent<Renderer>().material;
            material.SetFloat(fillProperty, 0f);
        }

        void OnMouseEnter()
        {
            isMouseOver = true;
            StopAllCoroutines();
            StartCoroutine(ChangeFill(1f, fillSpeed));
        }

        void OnMouseExit()
        {
            isMouseOver = false;
            StopAllCoroutines();
            StartCoroutine(ChangeFill(0f, unfillSpeed));
        }

        IEnumerator ChangeFill(float targetValue, float speed)
        {
            float startFill = material.GetFloat(fillProperty);
            float elapsedTime = 0f;
        
            while (elapsedTime < 1f / speed)
            {
                elapsedTime += Time.deltaTime;
                float newFill = Mathf.Lerp(startFill, targetValue, elapsedTime * speed);
                material.SetFloat(fillProperty, newFill);
                yield return null;
            }
        
            material.SetFloat(fillProperty, targetValue);
        }

        void OnDestroy()
        {
            if (material != null)
                DestroyImmediate(material);
        }
    }
}