using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    using Utils.TransformExtenstion;
    public interface IHaveBounds
    {
        public Transform transform { get; }
    }
    
    public abstract class GroupHorizontal<T> : MonoBehaviour where T : IHaveBounds
    {
        [SerializeField] private Transform container;
        [SerializeField] private float itemOffsetX = 2f;
        [SerializeField] private float limitSize;

        protected List<T> items = new List<T>();
        private float nextItemPositionX;

        public void Reset()
        {
            items.Clear();
            container.ClearChildren();
            nextItemPositionX = 0;
        }

        public void AddToGroup(T item)
        {
            items.Add(item);
            item.transform.SetParent(container);
            item.transform.localRotation = Quaternion.identity;
            UpdateItemPosition(item);
            if (limitSize != 0f && nextItemPositionX > limitSize) UpdateItemsOffset();
            OnItemAdded(item);
        }

        protected virtual void OnItemAdded(T item)
        {
        }

        private void OnValidate()
        {
            if (items.Count == 0) return;
            UpdateItemsOffset();
        }

        private void UpdateItemPosition(T item)
        {
            item.transform.localPosition = GetNextItemTargetPosition();
            nextItemPositionX += itemOffsetX;
        }

        private void UpdateItemsOffset()
        {
            nextItemPositionX = 0;
            itemOffsetX = limitSize / items.Count;
            items.ForEach(UpdateItemPosition);
        }

        public Vector3 GetNextItemTargetPosition()
        {
            return Vector3.right * nextItemPositionX;
        }
    }
}