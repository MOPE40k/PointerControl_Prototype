using System.Collections;
using _Project.Develop.Gameplay.Animations;
using _Project.Develop.Gameplay.Features.MovementFeatures.DestinationPointer;
using UnityEngine;

namespace _Project.Develop.Gameplay.Characters
{
    public class AgentCharacterView : MonoBehaviour
    {
        // Consts
        private const float FootstepsSoundInterval = 0.4f;
        private const float VelocityThresholdForFootstepsSound = 0.05f;
        private const string MaterialEdgeKey = "_Edge";

        [Header("References:")]
        [SerializeField] private AgentCharacter _character = null;
        [SerializeField] private Animator _animator = null;
        [SerializeField] private MoveDestinationPointerView _moveDestinationPointerView = null;
        [SerializeField] private CharacterSounds _sounds = null;

        // References
        private CharacterAnimationHandler _animationHandler = null;
        private MoveDestinationPointer _moveDestinationPointer = null;
        private Coroutine _footstepsRoutine = null;
        private SkinnedMeshRenderer[] _meshRenderers = null;

        private void Awake()
        {
            _animationHandler = new CharacterAnimationHandler(_character, _animator);

            _moveDestinationPointer = new MoveDestinationPointer(_character, _moveDestinationPointerView);

            _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        private void Start()
            => _footstepsRoutine = StartCoroutine(FootstepsSoundsRoutine());

        private void Update()
        {
            if (_character.InSpawnProcess(out float elapsedTime))
                if (_character.IsDead)
                    Despawn(elapsedTime);
                else
                    Spawn(elapsedTime);

            _animationHandler.UpdateTick();

            _moveDestinationPointer.UpdateTick();
        }

        private void Spawn(float elapsedTime)
            => SetFloatFor(_meshRenderers, MaterialEdgeKey, 1f - elapsedTime / _character.TimeToSpawn);

        private void Despawn(float elapsedTime)
            => SetFloatFor(_meshRenderers, MaterialEdgeKey, elapsedTime / _character.TimeToSpawn);

        public void TakeDamage()
        {
            if (_character.IsDead)
            {
                _animationHandler.DyingAnimation();

                _sounds.Die();
            }
            else
            {
                _animationHandler.DamageAnimation();

                _sounds.TakeDamage();
            }
        }

        private IEnumerator FootstepsSoundsRoutine()
        {
            while (_character.IsDead == false)
            {
                if (CanFootstepsSoundPlay())
                {
                    _sounds.Footsteps();

                    yield return new WaitForSeconds(FootstepsSoundInterval);
                }
                else
                {
                    yield return null;
                }
            }
        }

        private bool CanFootstepsSoundPlay()
            => _character.CurrentVelocity.magnitude > VelocityThresholdForFootstepsSound
                && _character.InJumpProcess == false;

        private void SetFloatFor(SkinnedMeshRenderer[] renderers, string key, float value)
        {
            foreach (SkinnedMeshRenderer renderer in renderers)
                renderer.material.SetFloat(key, value);
        }

        private void OnDestroy()
        {
            if (_footstepsRoutine != null)
            {
                StopCoroutine(_footstepsRoutine);

                _footstepsRoutine = null;
            }
        }
    }
}