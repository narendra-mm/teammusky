using UnityEngine;

namespace SpaceWalk.GameLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Tool : MonoBehaviour
    {
        [SerializeField] private ToolType _toolType;
        [SerializeField] private float _releaseForce;

        private bool _interacting;
        private Vector3 _prevMousePos;
        private Rigidbody2D _rigidBody;
        // Start is called before the first frame update
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseExit()
        {
            if (!_interacting) return;
            var position = GetMousePositionInWorld();
            var direction = (position - _prevMousePos);
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.AddForce(direction * _releaseForce);
            _interacting = false;
            Debug.Log("Direction: " + direction);
        }
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _interacting = true;
            }
            if (!_interacting) return;
            var position = GetMousePositionInWorld();
            transform.localPosition = position;
            _prevMousePos = position;
        }

        private Vector3 GetMousePositionInWorld()
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0f;
            return position;
        }
    }
    public enum ToolType
    {
        WireDetector,
        PipeDetector,
        NailPistol
    }
}