using _Project.Develop.Gameplay.Characters;
using UnityEngine;

namespace _Project.Develop.Gameplay.Animations
{
    public class CharacterAnimationEventHandler : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private AgentCharacter _character = null;

        public void CharacterResumeMove()
            => _character.ResumeMove();

        public void CharacterStopMove()
            => _character.StopMove();

        public void DyingAnimationComplete()
            => _character.StartSpawnTimer();
    }
}