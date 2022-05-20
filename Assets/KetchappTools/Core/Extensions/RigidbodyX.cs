using UnityEngine;

namespace KetchappTools.Core.Extensions
{
    public static class RigidbodyX
    {
        // 5 fundamental kinematic equations
        // refs:
        //      - https://youtu.be/v1V3T5BPd7E
        //      - https://youtu.be/phMZQNu0ZFM
        //      - https://youtu.be/IvT8hjy6q4o
        //
        // equation 1: s = (u + v) / 2 * t
        // equation 2: v = u + a * t
        // equation 3: s = u * t + a * t² / 2
        // equation 4: s = v * t - a * t² / 2
        // equation 5: v² = u² + 2 * a * s

        // u = initial velocity
        // v = final velocity
        // s = displacement
        // t = time
        // a = acceleration



        /// <summary>
        /// Returns the displacement vector during an elasped time
        /// </summary>
        public static Vector3 GetDisplacement(this Rigidbody rigidbody, float elapsedTime, Vector3? customGravity = null)
        {
            // equation 3: s = u * t + a * t² / 2

            Vector3 gravity = customGravity ?? Physics.gravity;
            return rigidbody.velocity * elapsedTime + gravity * elapsedTime * elapsedTime / 2f;
        }

        /// <summary>
        /// Returns the future position after an elapsed time
        /// </summary>
        public static Vector3 GetFuturePosition(this Rigidbody rigidbody, float elapsedTime, Vector3? customGravity = null)
        {
            return rigidbody.position + rigidbody.GetDisplacement(elapsedTime, customGravity);
        }

        /// <summary>
        /// Returns the initial velocity needed by the Rigidbody to reach the target position after a duration
        /// </summary>
        public static Vector3 GetInitialVelocity(this Rigidbody rigidbody, Vector3 targetPosition, float duration, Vector3? customGravity = null)
        {
            // equation 3:  s = u * t + a * t² / 2
            //              u * t = s - a * t² / 2
            //              u = (s - a * t² / 2) / t

            Vector3 gravity = customGravity ?? Physics.gravity;

            Vector3 displacement = targetPosition - rigidbody.position;

            float xInitialVelocity = (displacement.x - gravity.x * duration * duration / 2f) / duration;
            float yInitialVelocity = (displacement.y - gravity.y * duration * duration / 2f) / duration;
            float zInitialVelocity = (displacement.z - gravity.z * duration * duration / 2f) / duration;

            return new Vector3(xInitialVelocity, yInitialVelocity, zInitialVelocity);
        }

        /// <summary>
        /// Solves a quadratic equation (ax² + bx + c = 0), returns the 2 results of the equation
        /// </summary>
        public static float[] SolveQuadraticEquation(float a, float b, float c)
        {
            return new float[]
            {
                (-b - MathX.Sqrt(b * b - 4 * a * c)) / (2 * a),
                (-b + MathX.Sqrt(b * b - 4 * a * c)) / (2 * a)
            };
        }

        /// <summary>
        /// Returns the delay before reaching target value
        /// </summary>
        public static float GetDelayBeforeReachingTarget(float start, float velocity, float target, float acceleration = 0f)
        {
            // equation 3:  s = u * t + a * t² / 2
            //              a * t² / 2 + u * t - s = 0
            //              a * t² + 2 * u * t - 2 * s = 0

            float s = target - start;
            float u = velocity;
            float a = acceleration;

            float[] t = SolveQuadraticEquation(a, 2 * u, -2 * s);

            return t[0] > 0 ? t[0] : t[1];
        }

        /// <summary>
        /// Returns the delay before reaching target X value
        /// </summary>
        public static float GetDelayBeforeReachingX(this Rigidbody rigidbody, float targetX, float? customGravityX = null)
        {
            return GetDelayBeforeReachingTarget(rigidbody.position.x, rigidbody.velocity.x, targetX, customGravityX ?? Physics.gravity.x);
        }

        /// <summary>
        /// Returns the delay before reaching target Y value
        /// </summary>
        public static float GetDelayBeforeReachingY(this Rigidbody rigidbody, float targetY, float? customGravityY = null)
        {
            return GetDelayBeforeReachingTarget(rigidbody.position.y, rigidbody.velocity.y, targetY, customGravityY ?? Physics.gravity.y);
        }

        /// <summary>
        /// Returns the delay before reaching target Z value
        /// </summary>
        public static float GetDelayBeforeReachingZ(this Rigidbody rigidbody, float targetZ, float? customGravityZ = null)
        {
            return GetDelayBeforeReachingTarget(rigidbody.position.z, rigidbody.velocity.z, targetZ, customGravityZ ?? Physics.gravity.z);
        }
    }
}