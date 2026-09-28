using UnityEngine;

namespace _Project.Develop.Gameplay.Features.MovementFeatures.DestinationPointer
{
    public class MoveDestinationPointerView : MonoBehaviour
    {
        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);

        public void SetPosition(Vector3 position)
            => transform.position = position;
    }
}