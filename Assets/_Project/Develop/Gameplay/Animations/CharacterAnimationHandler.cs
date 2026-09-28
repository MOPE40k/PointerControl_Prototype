using _Project.Develop.Gameplay.Characters;
using UnityEngine;

namespace _Project.Develop.Gameplay.Animations
{
    public class CharacterAnimationHandler
    {
        // Consts
        private const string BaseLayerName = "Base Layer";
        private const string InjuredLayerName = "Injured Layer";
        private const string HitAnimationName = "Kick To The Groin";

        private const float InjuredStateBound = 0.3f;
        private const float MaxWeightValue = 1.0f;
        private const float MinWeightValue = 0.0f;
        private const float AnimationAcceleration = 2f;

        private readonly int VelocityKey = Animator.StringToHash("Velocity");
        private readonly int HitKey = Animator.StringToHash("Hit");
        private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");
        private readonly int DyingKey = Animator.StringToHash("Dying");

        // Settings
        private readonly int _injuredLayerIndex = 0;
        private readonly int _baseLayerIndex = 0;

        // References
        private readonly AgentCharacter _character = null;
        private readonly Animator _animator = null;

        // Runtime
        private float _animationSpeed = 0f;

        public CharacterAnimationHandler(AgentCharacter character, Animator animator)
        {
            _character = character;
            _animator = animator;

            _injuredLayerIndex = _animator.GetLayerIndex(InjuredLayerName);
            _baseLayerIndex = _animator.GetLayerIndex(BaseLayerName);
        }

        public void UpdateTick()
        {
            CalculateLayersWeight();

            CalculateAnimationSpeed();

            VelocitySetValue(_animationSpeed);

            InJumpProcessSetState(_character.InJumpProcess);
        }

        private void VelocitySetValue(float value)
            => _animator.SetFloat(VelocityKey, value);

        private void InJumpProcessSetState(bool state)
            => _animator.SetBool(InJumpProcessKey, state);

        public void DamageAnimation()
        {
            if (IsAnimationStatePlaying(_baseLayerIndex, HitAnimationName) == false)
                _animator.SetTrigger(HitKey);
        }

        public void DyingAnimation()
            => _animator.SetTrigger(DyingKey);

        private void CalculateLayersWeight()
        {
            float normalizedHealth = Mathf.Clamp01(_character.CurrentHealth / _character.MaxHealth);

            if (normalizedHealth <= InjuredStateBound)
            {
                _animator.SetLayerWeight(_injuredLayerIndex, MaxWeightValue);
                _animator.SetLayerWeight(_baseLayerIndex, MinWeightValue);
            }
            else
            {
                _animator.SetLayerWeight(_baseLayerIndex, MaxWeightValue);
                _animator.SetLayerWeight(_injuredLayerIndex, MinWeightValue);
            }
        }

        private void CalculateAnimationSpeed()
        {
            Vector3 currentVelocity = _character.CurrentVelocity;
            currentVelocity.y = 0f;

            float currentSpeed = currentVelocity.magnitude;
            float normalizedSpeed = Mathf.Clamp01(currentSpeed / _character.MoveSpeed);

            _animationSpeed = Mathf.MoveTowards(_animationSpeed, normalizedSpeed, AnimationAcceleration * Time.deltaTime);
        }

        private bool IsAnimationStatePlaying(int layerIndex, string stateName)
            => _animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
    }
}