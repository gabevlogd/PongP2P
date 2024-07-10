namespace GV.Utility
{
    using UnityEngine;

    public class BezierVisualizer : MonoBehaviour
    {
        public Vector3[] ControlPoints;
        [Min(2)]
        public int CurveResolution;

    }
}
