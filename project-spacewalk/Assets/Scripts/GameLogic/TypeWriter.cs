using System.Collections;
using TMPro;
using UnityEngine;

namespace SpaceWalk.GameLogic
{
    [RequireComponent(typeof(TMP_Text))]
    public class TypeWriter : MonoBehaviour
    {
        [Range(0,1)]
        [SerializeField] private float _progress;
        [SerializeField] private float _animationDelay;

        private TMP_Text _text;
        private string _initialText;

        private Coroutine _animateCoroutine;
        // Start is called before the first frame update
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _initialText = _text.text;
        }

        // Update is called once per frame
        void Update()
        {
            var endIndex = _progress * _initialText.Length;
            _text.text = _initialText.Substring(0, (int)endIndex);
        }
            
        [ContextMenu("Animate")]
        public void Animate()
        {
            if (_animateCoroutine != null)
            {
                StopCoroutine(_animateCoroutine);
            }
           _animateCoroutine = StartCoroutine(AnimateRoutine());
        }

        private IEnumerator AnimateRoutine()
        {
            var wait = new WaitForSeconds(_animationDelay);
            for (var i = 0; i < _initialText.Length; i++)
            {
                _progress = (float)i / _initialText.Length;
                yield return wait;
            }
   
        }
    }
}