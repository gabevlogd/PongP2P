namespace GV.Utility
{
    using UnityEngine;

    public class ObjectPointer
    {
        /// <summary>
        /// Camera from which to calculate the pointed object
        /// </summary>
        private Camera _targetCamera;

        /// <summary>
        /// Pointed object calculated from targetCamera to pointer position with ScreenPointToRay method
        /// </summary>
        public GameObject PointedObject 
        {
            get => GetPointedObject();
            private set => PointedObject = value;
        }

        /// <summary>
        /// Maximum distance from the targetCamera to where the pointer reaches
        /// </summary>
        public float MaxPointingDistance
        {
            get => _maxPointingDistance;
            set => _maxPointingDistance = Mathf.Abs(value);
        }
        private float _maxPointingDistance;

        /// <summary>
        /// Constructor: targetCamera is the camera from which to calculate the pointed object
        /// </summary>
        public ObjectPointer(Camera targetCamera, float maxPointingDistance = Mathf.Infinity)
        {
            _targetCamera = targetCamera;
            _maxPointingDistance = maxPointingDistance;
        }

        /// <summary>
        /// Return the distance between PointedObject and position
        /// </summary>
        public float Distance(Vector3 position) => Vector3.Distance(position, PointedObject.transform.position);

        /// <summary>
        /// Call this method in OnDrawGizmo to visualize the pointer
        /// </summary>
        public void ShowPointerDebug(Color pointerColor)
        {
            Debug.DrawRay(_targetCamera.ScreenPointToRay(Input.mousePosition).origin, _targetCamera.ScreenPointToRay(Input.mousePosition).direction * _maxPointingDistance, pointerColor);
        }

        private GameObject GetPointedObject()
        {
            Ray ray = _targetCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, _maxPointingDistance);
            if (hit.collider != null) return hit.collider.gameObject;
            else return null;
        }
    }
}


