using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AnimatedButton: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public float time = 0.06f;
        public float scale = 0.95f;
        public bool overrideScaleTransform = false;

        public Transform transformToScale;
        public bool interactable = true;

        private void MouseDown()
        {
            if (interactable == false)
                return;

            if (!overrideScaleTransform || !transformToScale)
            {
                transformToScale = transform;
            }

            StartCoroutine(ScaleButton());
        }

        private IEnumerator ScaleButton()
        {
            int iterations = (int)(time / Time.fixedDeltaTime);
            float scaleDelta = Mathf.Abs(1f - scale) / (float)iterations;

            for (int i = 0; i < iterations; i++)
            {
                transformToScale.localScale =
                    Vector3.MoveTowards(transformToScale.localScale, new Vector3(scale, scale, scale), scaleDelta);
                yield return new WaitForFixedUpdate();
            }

            transformToScale.localScale = new Vector3(scale, scale, scale);
        }

        private void MouseUp()
        {
            if (interactable == false)
                return;

            if (!overrideScaleTransform || !transformToScale)
            {
                transformToScale = transform;
            }

            StopAllCoroutines();
            transformToScale.localScale = new Vector3(1, 1, 1);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            MouseDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            MouseUp();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseUp();
        }
    }
}