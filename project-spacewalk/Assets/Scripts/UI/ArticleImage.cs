using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceWalk.UI
{
    [RequireComponent(typeof(Image))]
    public class ArticleImage : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _listOfArticles;
        private Image _image;
        // Start is called before the first frame update
        void Awake()
        {
            _image = GetComponent<Image>();
        }
        private void OnEnable()
        {
            var random = Random.Range(0, _listOfArticles.Count);
            _image.sprite = _listOfArticles[random];
        }

    }
}