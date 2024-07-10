namespace GV.Utility
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BezierCurveCalculator
    {

        /// <summary>
        /// Calculates points on the Bezier curve with a variable number of control points
        /// </summary>
        public static List<Vector3> CalculateBezierCurvePoints(int curveResolution, Vector3[] controlPoints)
        {
            List<Vector3> curvePoints = new List<Vector3>();

            for (int i = 0; i < curveResolution; i++)
            {
                float t = i / (float)(curveResolution - 1);
                Vector3 curvePoint = CalculateBezierPoint(t, controlPoints);
                curvePoints.Add(curvePoint);
            }

            return curvePoints;
        }

        /// <summary>
        /// Calculates a point on the Bezier curve with a variable number of control points
        /// </summary>
        public static Vector3 CalculateBezierPoint(float t, Vector3[] controlPoints)
        {
            int n = controlPoints.Length - 1;
            Vector3 result = new Vector3();
            for (int i = 0; i <= n; ++i)
            {
                float factor = Bernstein(n, i, t);
                result.x += factor * controlPoints[i].x;
                result.y += factor * controlPoints[i].y;
                result.z += factor * controlPoints[i].z;
            }
            return result;
        }


        /// <summary>
        /// Calculates the binomial coefficient
        /// </summary>
        private static int BinomialCoefficient(int a, int b)
        {
            if (b > a) return 0;
            if (b == 0 || b == a) return 1;

            long result = 1;
            for (int i = 1; i <= b; i++)
            {
                result *= a--;
                result /= i;
            }

            return (int)result;
        }


        /// <summary>
        /// Calculates Bernstein's polynomial
        /// </summary>
        private static float Bernstein(int n, int i, float t) => BinomialCoefficient(n, i) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
    }
}

