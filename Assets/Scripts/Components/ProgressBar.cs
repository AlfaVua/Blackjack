using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private RectTransform baseRect;
        [SerializeField] private RectMask2D mask;

        private float maxWidth;

        private void Awake()
        {
            maxWidth = baseRect.rect.width;
        }

        public void SetValue(float percentage)
        {
            mask.padding = new Vector4(mask.padding.x, mask.padding.y, (1 - percentage) * maxWidth , mask.padding.w);
        }
    }
}