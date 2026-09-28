using System;
using System.Collections;
using UnityEngine;

namespace _Project.Develop.Utils.CoroutineManagment
{
    public class CoroutineTimer : IDisposable
    {
        // References
        private readonly MonoBehaviour _coroutineRunner = null;

        // Settings
        private float _timeLimit = 0f;
        private float _timeElapsed = 0f;

        // Runtime
        private Coroutine _routine = null;

        public CoroutineTimer(MonoBehaviour coroutineRunner)
            => _coroutineRunner = coroutineRunner;

        public float TimeLimit => _timeLimit;

        public bool InProcess(out float elapsedTime)
        {
            if (_routine == null)
            {
                elapsedTime = _timeLimit;

                return false;
            }

            elapsedTime = _timeElapsed;

            return true;
        }

        public void StartTimer(float timeLimit)
        {
            if (_routine != null)
            {
                _coroutineRunner.StopCoroutine(_routine);

                _routine = null;
            }

            _timeLimit = timeLimit;

            _routine = _coroutineRunner.StartCoroutine(Process());
        }

        private IEnumerator Process()
        {
            _timeElapsed = 0f;

            while (_timeElapsed < _timeLimit)
            {
                _timeElapsed += Time.deltaTime;

                if (_timeElapsed >= _timeLimit)
                    _timeElapsed = _timeLimit;

                yield return null;
            }

            _routine = null;
        }

        public void Dispose()
        {
            if (_routine != null)
            {
                _coroutineRunner.StopCoroutine(_routine);

                _routine = null;
            }
        }
    }
}