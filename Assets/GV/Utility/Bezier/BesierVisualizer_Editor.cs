namespace GV.Utility
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(BezierVisualizer))]
    public class BesierVisualizer_Editor : Editor
    {
        private BezierVisualizer _this;
        private int _selectedIndex;

        private void Awake() => _this = target as BezierVisualizer;

        private void OnSceneGUI()
        {

            DrawBezierCurve();


        }

        public void DrawBezierCurve()
        {
            if (_this.ControlPoints == null || _this.ControlPoints.Length == 0) return;



            for (int i = 0; i < _this.ControlPoints.Length; i++)
            {
                Handles.color = Color.white;
                if (Handles.Button(_this.ControlPoints[i], Quaternion.identity, 0.5f, 1f, Handles.DotHandleCap))
                    _selectedIndex = i;

                Handles.color = Color.gray;
                if (i != _this.ControlPoints.Length - 1)
                    Handles.DrawLine(_this.ControlPoints[i], _this.ControlPoints[i + 1]);
            }

            _this.ControlPoints[_selectedIndex] = Handles.DoPositionHandle(_this.ControlPoints[_selectedIndex], Quaternion.identity);

            Handles.color = Color.green;
            List<Vector3> bezierCurvePoints = BezierCurveCalculator.CalculateBezierCurvePoints(_this.CurveResolution, _this.ControlPoints);

            for (int i = 0; i < bezierCurvePoints.Count - 1; i++)
                Handles.DrawLine(bezierCurvePoints[i], bezierCurvePoints[i + 1]);

        }
    }
}

