/* Copyright (C) <2009-2011> <Thorben Linneweber, Jitter Physics>
* 
*  This software is provided 'as-is', without any express or implied
*  warranty.  In no event will the authors be held liable for any damages
*  arising from the use of this software.
*
*  Permission is granted to anyone to use this software for any purpose,
*  including commercial applications, and to alter it and redistribute it
*  freely, subject to the following restrictions:
*
*  1. The origin of this software must not be misrepresented; you must not
*      claim that you wrote the original software. If you use this software
*      in a product, an acknowledgment in the product documentation would be
*      appreciated but is not required.
*  2. Altered source versions must be plainly marked as such, and must not be
*      misrepresented as being the original software.
*  3. This notice may not be removed or altered from any source distribution. 
*/

using System;

namespace Fix
{
    /// <summary>
    /// A vector structure.
    /// </summary>
    [Serializable]
    public struct FixVector3
    {

        private static fixed64 ZeroEpsilonSq = FixMath.Epsilon;
        internal static FixVector3 InternalZero;
        internal static FixVector3 Arbitrary;

        /// <summary>The X component of the vector.</summary>
        public fixed64 x;
        /// <summary>The Y component of the vector.</summary>
        public fixed64 y;
        /// <summary>The Z component of the vector.</summary>
        public fixed64 z;

        #region Static readonly variables
        /// <summary>
        /// A vector with components (0,0,0);
        /// </summary>
        public static readonly FixVector3 zero;
        /// <summary>
        /// A vector with components (-1,0,0);
        /// </summary>
        public static readonly FixVector3 left;
        /// <summary>
        /// A vector with components (1,0,0);
        /// </summary>
        public static readonly FixVector3 right;
        /// <summary>
        /// A vector with components (0,1,0);
        /// </summary>
        public static readonly FixVector3 up;
        /// <summary>
        /// A vector with components (0,-1,0);
        /// </summary>
        public static readonly FixVector3 down;
        /// <summary>
        /// A vector with components (0,0,-1);
        /// </summary>
        public static readonly FixVector3 back;
        /// <summary>
        /// A vector with components (0,0,1);
        /// </summary>
        public static readonly FixVector3 forward;
        /// <summary>
        /// A vector with components (1,1,1);
        /// </summary>
        public static readonly FixVector3 one;
        /// <summary>
        /// A vector with components 
        /// (fixed64.MinValue,fixed64.MinValue,fixed64.MinValue);
        /// </summary>
        public static readonly FixVector3 MinValue;
        /// <summary>
        /// A vector with components 
        /// (fixed64.MaxValue,fixed64.MaxValue,fixed64.MaxValue);
        /// </summary>
        public static readonly FixVector3 MaxValue;
        #endregion

        #region Private static constructor
        static FixVector3()
        {
            one = new FixVector3(1, 1, 1);
            zero = new FixVector3(0, 0, 0);
            left = new FixVector3(-1, 0, 0);
            right = new FixVector3(1, 0, 0);
            up = new FixVector3(0, 1, 0);
            down = new FixVector3(0, -1, 0);
            back = new FixVector3(0, 0, -1);
            forward = new FixVector3(0, 0, 1);
            MinValue = new FixVector3(fixed64.MinValue);
            MaxValue = new FixVector3(fixed64.MaxValue);
            Arbitrary = new FixVector3(1, 1, 1);
            InternalZero = zero;
        }
        #endregion

        public static FixVector3 Abs(FixVector3 other) {
            return new FixVector3(fixed64.Abs(other.x), fixed64.Abs(other.y), fixed64.Abs(other.z));
        }

        /// <summary>
        /// Gets the squared length of the vector.
        /// </summary>
        /// <returns>Returns the squared length of the vector.</returns>
        public fixed64 sqrMagnitude {
            get { 
                return (((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
            }
        }

        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        /// <returns>Returns the length of the vector.</returns>
        public fixed64 magnitude {
            get {
                fixed64 num = ((this.x * this.x) + (this.y * this.y)) + (this.z * this.z);
                return fixed64.Sqrt(num);
            }
        }

        public static FixVector3 ClampMagnitude(FixVector3 vector, fixed64 maxLength) {
            return Normalize(vector) * maxLength;
        }

        /// <summary>
        /// Gets a normalized version of the vector.
        /// </summary>
        /// <returns>Returns a normalized version of the vector.</returns>
        public FixVector3 normalized {
            get {
                FixVector3 result = new FixVector3(this.x, this.y, this.z);
                result.Normalize();

                return result;
            }
        }

        /// <summary>
        /// Constructor initializing a new instance of the structure
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>

        public FixVector3(int x,int y,int z)
		{
			this.x = (fixed64)x;
			this.y = (fixed64)y;
			this.z = (fixed64)z;
		}

		public FixVector3(fixed64 x, fixed64 y, fixed64 z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Multiplies each component of the vector by the same components of the provided vector.
        /// </summary>
        public void Scale(FixVector3 other) {
            this.x = x * other.x;
            this.y = y * other.y;
            this.z = z * other.z;
        }

        /// <summary>
        /// Sets all vector component to specific values.
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public void Set(fixed64 x, fixed64 y, fixed64 z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Constructor initializing a new instance of the structure
        /// </summary>
        /// <param name="xyz">All components of the vector are set to xyz</param>
        public FixVector3(fixed64 xyz)
        {
            this.x = xyz;
            this.y = xyz;
            this.z = xyz;
        }

		public static FixVector3 Lerp(FixVector3 from, FixVector3 to, fixed64 percent) {
			return from + (to - from) * percent;
		}

        /// <summary>
        /// Builds a string from the JVector.
        /// </summary>
        /// <returns>A string containing all three components.</returns>
        #region public override string ToString()
        public override string ToString() {
            return string.Format("({0:f1}, {1:f1}, {2:f1})", x.AsFloat(), y.AsFloat(), z.AsFloat());
        }
        #endregion

        /// <summary>
        /// Tests if an object is equal to this vector.
        /// </summary>
        /// <param name="obj">The object to test.</param>
        /// <returns>Returns true if they are euqal, otherwise false.</returns>
        #region public override bool Equals(object obj)
        public override bool Equals(object obj)
        {
            if (!(obj is FixVector3)) return false;
            FixVector3 other = (FixVector3)obj;

            return (((x == other.x) && (y == other.y)) && (z == other.z));
        }
        #endregion

        /// <summary>
        /// Multiplies each component of the vector by the same components of the provided vector.
        /// </summary>
        public static FixVector3 Scale(FixVector3 vecA, FixVector3 vecB) {
            FixVector3 result;
            result.x = vecA.x * vecB.x;
            result.y = vecA.y * vecB.y;
            result.z = vecA.z * vecB.z;

            return result;
        }

        /// <summary>
        /// Tests if two JVector are equal.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>Returns true if both values are equal, otherwise false.</returns>
        #region public static bool operator ==(JVector value1, JVector value2)
        public static bool operator ==(FixVector3 value1, FixVector3 value2)
        {
            return (((value1.x == value2.x) && (value1.y == value2.y)) && (value1.z == value2.z));
        }
        #endregion

        /// <summary>
        /// Tests if two JVector are not equal.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>Returns false if both values are equal, otherwise true.</returns>
        #region public static bool operator !=(JVector value1, JVector value2)
        public static bool operator !=(FixVector3 value1, FixVector3 value2)
        {
            if ((value1.x == value2.x) && (value1.y == value2.y))
            {
                return (value1.z != value2.z);
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Gets a vector with the minimum x,y and z values of both vectors.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>A vector with the minimum x,y and z values of both vectors.</returns>
        #region public static JVector Min(JVector value1, JVector value2)

        public static FixVector3 Min(FixVector3 value1, FixVector3 value2)
        {
            FixVector3 result;
            FixVector3.Min(ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Gets a vector with the minimum x,y and z values of both vectors.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="result">A vector with the minimum x,y and z values of both vectors.</param>
        public static void Min(ref FixVector3 value1, ref FixVector3 value2, out FixVector3 result)
        {
            result.x = (value1.x < value2.x) ? value1.x : value2.x;
            result.y = (value1.y < value2.y) ? value1.y : value2.y;
            result.z = (value1.z < value2.z) ? value1.z : value2.z;
        }
        #endregion

        /// <summary>
        /// Gets a vector with the maximum x,y and z values of both vectors.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns>A vector with the maximum x,y and z values of both vectors.</returns>
        #region public static JVector Max(JVector value1, JVector value2)
        public static FixVector3 Max(FixVector3 value1, FixVector3 value2)
        {
            FixVector3 result;
            FixVector3.Max(ref value1, ref value2, out result);
            return result;
        }
		
		public static fixed64 Distance(FixVector3 v1, FixVector3 v2) {
			return fixed64.Sqrt ((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z));
		}

        /// <summary>
        /// Gets a vector with the maximum x,y and z values of both vectors.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="result">A vector with the maximum x,y and z values of both vectors.</param>
        public static void Max(ref FixVector3 value1, ref FixVector3 value2, out FixVector3 result)
        {
            result.x = (value1.x > value2.x) ? value1.x : value2.x;
            result.y = (value1.y > value2.y) ? value1.y : value2.y;
            result.z = (value1.z > value2.z) ? value1.z : value2.z;
        }
        #endregion

        /// <summary>
        /// Sets the length of the vector to zero.
        /// </summary>
        #region public void MakeZero()
        public void MakeZero()
        {
            x = fixed64.Zero;
            y = fixed64.Zero;
            z = fixed64.Zero;
        }
        #endregion

        /// <summary>
        /// Checks if the length of the vector is zero.
        /// </summary>
        /// <returns>Returns true if the vector is zero, otherwise false.</returns>
        #region public bool IsZero()
        public bool IsZero()
        {
            return (this.sqrMagnitude == fixed64.Zero);
        }

        /// <summary>
        /// Checks if the length of the vector is nearly zero.
        /// </summary>
        /// <returns>Returns true if the vector is nearly zero, otherwise false.</returns>
        public bool IsNearlyZero()
        {
            return (this.sqrMagnitude < ZeroEpsilonSq);
        }
        #endregion

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The vector to transform.</param>
        /// <param name="matrix">The transform matrix.</param>
        /// <returns>The transformed vector.</returns>
        #region public static JVector Transform(JVector position, JMatrix matrix)
        public static FixVector3 Transform(FixVector3 position, FixMatrix3x3 matrix)
        {
            FixVector3 result;
            FixVector3.Transform(ref position, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The vector to transform.</param>
        /// <param name="matrix">The transform matrix.</param>
        /// <param name="result">The transformed vector.</param>
        public static void Transform(ref FixVector3 position, ref FixMatrix3x3 matrix, out FixVector3 result)
        {
            fixed64 num0 = ((position.x * matrix.M11) + (position.y * matrix.M21)) + (position.z * matrix.M31);
            fixed64 num1 = ((position.x * matrix.M12) + (position.y * matrix.M22)) + (position.z * matrix.M32);
            fixed64 num2 = ((position.x * matrix.M13) + (position.y * matrix.M23)) + (position.z * matrix.M33);

            result.x = num0;
            result.y = num1;
            result.z = num2;
        }

        /// <summary>
        /// Transforms a vector by the transposed of the given Matrix.
        /// </summary>
        /// <param name="position">The vector to transform.</param>
        /// <param name="matrix">The transform matrix.</param>
        /// <param name="result">The transformed vector.</param>
        public static void TransposedTransform(ref FixVector3 position, ref FixMatrix3x3 matrix, out FixVector3 result)
        {
            fixed64 num0 = ((position.x * matrix.M11) + (position.y * matrix.M12)) + (position.z * matrix.M13);
            fixed64 num1 = ((position.x * matrix.M21) + (position.y * matrix.M22)) + (position.z * matrix.M23);
            fixed64 num2 = ((position.x * matrix.M31) + (position.y * matrix.M32)) + (position.z * matrix.M33);

            result.x = num0;
            result.y = num1;
            result.z = num2;
        }
        #endregion

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>Returns the dot product of both vectors.</returns>
        #region public static fixed64 Dot(JVector vector1, JVector vector2)
        public static fixed64 Dot(FixVector3 vector1, FixVector3 vector2)
        {
            return FixVector3.Dot(ref vector1, ref vector2);
        }


        /// <summary>
        /// Calculates the dot product of both vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>Returns the dot product of both vectors.</returns>
        public static fixed64 Dot(ref FixVector3 vector1, ref FixVector3 vector2)
        {
            return ((vector1.x * vector2.x) + (vector1.y * vector2.y)) + (vector1.z * vector2.z);
        }
        #endregion

        // Projects a vector onto another vector.
        public static FixVector3 Project(FixVector3 vector, FixVector3 onNormal)
        {
            fixed64 sqrtMag = Dot(onNormal, onNormal);
            if (sqrtMag < FixMath.Epsilon)
                return zero;
            else
                return onNormal * Dot(vector, onNormal) / sqrtMag;
        }

        // Projects a vector onto a plane defined by a normal orthogonal to the plane.
        public static FixVector3 ProjectOnPlane(FixVector3 vector, FixVector3 planeNormal)
        {
            return vector - Project(vector, planeNormal);
        }


        // Returns the angle in degrees between /from/ and /to/. This is always the smallest
        public static fixed64 Angle(FixVector3 from, FixVector3 to)
        {
            return FixMath.Acos(FixMath.Clamp(Dot(from.normalized, to.normalized), -fixed64.ONE, fixed64.ONE)) * FixMath.Rad2Deg;
        }

        // The smaller of the two possible angles between the two vectors is returned, therefore the result will never be greater than 180 degrees or smaller than -180 degrees.
        // If you imagine the from and to vectors as lines on a piece of paper, both originating from the same point, then the /axis/ vector would point up out of the paper.
        // The measured angle between the two vectors would be positive in a clockwise direction and negative in an anti-clockwise direction.
        public static fixed64 SignedAngle(FixVector3 from, FixVector3 to, FixVector3 axis)
        {
            FixVector3 fromNorm = from.normalized, toNorm = to.normalized;
            fixed64 unsignedAngle = FixMath.Acos(FixMath.Clamp(Dot(fromNorm, toNorm), -fixed64.ONE, fixed64.ONE)) * FixMath.Rad2Deg;
            fixed64 sign = FixMath.Sign(Dot(axis, Cross(fromNorm, toNorm)));
            return unsignedAngle * sign;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The sum of both vectors.</returns>
        #region public static void Add(JVector value1, JVector value2)
        public static FixVector3 Add(FixVector3 value1, FixVector3 value2)
        {
            FixVector3 result;
            FixVector3.Add(ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Adds to vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The sum of both vectors.</param>
        public static void Add(ref FixVector3 value1, ref FixVector3 value2, out FixVector3 result)
        {
            fixed64 num0 = value1.x + value2.x;
            fixed64 num1 = value1.y + value2.y;
            fixed64 num2 = value1.z + value2.z;

            result.x = num0;
            result.y = num1;
            result.z = num2;
        }
        #endregion

        /// <summary>
        /// Divides a vector by a factor.
        /// </summary>
        /// <param name="value1">The vector to divide.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>Returns the scaled vector.</returns>
        public static FixVector3 Divide(FixVector3 value1, fixed64 scaleFactor) {
            FixVector3 result;
            FixVector3.Divide(ref value1, scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Divides a vector by a factor.
        /// </summary>
        /// <param name="value1">The vector to divide.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="result">Returns the scaled vector.</param>
        public static void Divide(ref FixVector3 value1, fixed64 scaleFactor, out FixVector3 result) {
            result.x = value1.x / scaleFactor;
            result.y = value1.y / scaleFactor;
            result.z = value1.z / scaleFactor;
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The difference of both vectors.</returns>
        #region public static JVector Subtract(JVector value1, JVector value2)
        public static FixVector3 Subtract(FixVector3 value1, FixVector3 value2)
        {
            FixVector3 result;
            FixVector3.Subtract(ref value1, ref value2, out result);
            return result;
        }

        /// <summary>
        /// Subtracts to vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The difference of both vectors.</param>
        public static void Subtract(ref FixVector3 value1, ref FixVector3 value2, out FixVector3 result)
        {
            fixed64 num0 = value1.x - value2.x;
            fixed64 num1 = value1.y - value2.y;
            fixed64 num2 = value1.z - value2.z;

            result.x = num0;
            result.y = num1;
            result.z = num2;
        }
        #endregion

        /// <summary>
        /// The cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product of both vectors.</returns>
        #region public static JVector Cross(JVector vector1, JVector vector2)
        public static FixVector3 Cross(FixVector3 vector1, FixVector3 vector2)
        {
            FixVector3 result;
            FixVector3.Cross(ref vector1, ref vector2, out result);
            return result;
        }

        /// <summary>
        /// The cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <param name="result">The cross product of both vectors.</param>
        public static void Cross(ref FixVector3 vector1, ref FixVector3 vector2, out FixVector3 result)
        {
            fixed64 num3 = (vector1.y * vector2.z) - (vector1.z * vector2.y);
            fixed64 num2 = (vector1.z * vector2.x) - (vector1.x * vector2.z);
            fixed64 num = (vector1.x * vector2.y) - (vector1.y * vector2.x);
            result.x = num3;
            result.y = num2;
            result.z = num;
        }
        #endregion

        /// <summary>
        /// Gets the hashcode of the vector.
        /// </summary>
        /// <returns>Returns the hashcode of the vector.</returns>
        #region public override int GetHashCode()
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }
        #endregion

        /// <summary>
        /// Inverses the direction of the vector.
        /// </summary>
        #region public static JVector Negate(JVector value)
        public void Negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
        }

        /// <summary>
        /// Inverses the direction of a vector.
        /// </summary>
        /// <param name="value">The vector to inverse.</param>
        /// <returns>The negated vector.</returns>
        public static FixVector3 Negate(FixVector3 value)
        {
            FixVector3 result;
            FixVector3.Negate(ref value,out result);
            return result;
        }

        /// <summary>
        /// Inverses the direction of a vector.
        /// </summary>
        /// <param name="value">The vector to inverse.</param>
        /// <param name="result">The negated vector.</param>
        public static void Negate(ref FixVector3 value, out FixVector3 result)
        {
            fixed64 num0 = -value.x;
            fixed64 num1 = -value.y;
            fixed64 num2 = -value.z;

            result.x = num0;
            result.y = num1;
            result.z = num2;
        }
        #endregion

        /// <summary>
        /// Normalizes the given vector.
        /// </summary>
        /// <param name="value">The vector which should be normalized.</param>
        /// <returns>A normalized vector.</returns>
        #region public static JVector Normalize(JVector value)
        public static FixVector3 Normalize(FixVector3 value)
        {
            FixVector3 result;
            FixVector3.Normalize(ref value, out result);
            return result;
        }

        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        public void Normalize()
        {
            fixed64 num2 = ((this.x * this.x) + (this.y * this.y)) + (this.z * this.z);
            fixed64 num = fixed64.One / fixed64.Sqrt(num2);
            this.x *= num;
            this.y *= num;
            this.z *= num;
        }

        /// <summary>
        /// Normalizes the given vector.
        /// </summary>
        /// <param name="value">The vector which should be normalized.</param>
        /// <param name="result">A normalized vector.</param>
        public static void Normalize(ref FixVector3 value, out FixVector3 result)
        {
            fixed64 num2 = ((value.x * value.x) + (value.y * value.y)) + (value.z * value.z);
            fixed64 num = fixed64.One / fixed64.Sqrt(num2);
            result.x = value.x * num;
            result.y = value.y * num;
            result.z = value.z * num;
        }
        #endregion

        #region public static void Swap(ref JVector vector1, ref JVector vector2)

        /// <summary>
        /// Swaps the components of both vectors.
        /// </summary>
        /// <param name="vector1">The first vector to swap with the second.</param>
        /// <param name="vector2">The second vector to swap with the first.</param>
        public static void Swap(ref FixVector3 vector1, ref FixVector3 vector2)
        {
            fixed64 temp;

            temp = vector1.x;
            vector1.x = vector2.x;
            vector2.x = temp;

            temp = vector1.y;
            vector1.y = vector2.y;
            vector2.y = temp;

            temp = vector1.z;
            vector1.z = vector2.z;
            vector2.z = temp;
        }
        #endregion

        /// <summary>
        /// Multiply a vector with a factor.
        /// </summary>
        /// <param name="value1">The vector to multiply.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>Returns the multiplied vector.</returns>
        #region public static JVector Multiply(JVector value1, fixed64 scaleFactor)
        public static FixVector3 Multiply(FixVector3 value1, fixed64 scaleFactor)
        {
            FixVector3 result;
            FixVector3.Multiply(ref value1, scaleFactor, out result);
            return result;
        }

        /// <summary>
        /// Multiply a vector with a factor.
        /// </summary>
        /// <param name="value1">The vector to multiply.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="result">Returns the multiplied vector.</param>
        public static void Multiply(ref FixVector3 value1, fixed64 scaleFactor, out FixVector3 result)
        {
            result.x = value1.x * scaleFactor;
            result.y = value1.y * scaleFactor;
            result.z = value1.z * scaleFactor;
        }
        #endregion

        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>Returns the cross product of both.</returns>
        #region public static JVector operator %(JVector value1, JVector value2)
        public static FixVector3 operator %(FixVector3 value1, FixVector3 value2)
        {
            FixVector3 result; FixVector3.Cross(ref value1, ref value2, out result);
            return result;
        }
        #endregion

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>Returns the dot product of both.</returns>
        #region public static fixed64 operator *(JVector value1, JVector value2)
        public static fixed64 operator *(FixVector3 value1, FixVector3 value2)
        {
            return FixVector3.Dot(ref value1, ref value2);
        }
        #endregion

        /// <summary>
        /// Multiplies a vector by a scale factor.
        /// </summary>
        /// <param name="value1">The vector to scale.</param>
        /// <param name="value2">The scale factor.</param>
        /// <returns>Returns the scaled vector.</returns>
        #region public static JVector operator *(JVector value1, fixed64 value2)
        public static FixVector3 operator *(FixVector3 value1, fixed64 value2)
        {
            FixVector3 result;
            FixVector3.Multiply(ref value1, value2,out result);
            return result;
        }
        #endregion

        /// <summary>
        /// Multiplies a vector by a scale factor.
        /// </summary>
        /// <param name="value2">The vector to scale.</param>
        /// <param name="value1">The scale factor.</param>
        /// <returns>Returns the scaled vector.</returns>
        #region public static JVector operator *(fixed64 value1, JVector value2)
        public static FixVector3 operator *(fixed64 value1, FixVector3 value2)
        {
            FixVector3 result;
            FixVector3.Multiply(ref value2, value1, out result);
            return result;
        }
        #endregion

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The difference of both vectors.</returns>
        #region public static JVector operator -(JVector value1, JVector value2)
        public static FixVector3 operator -(FixVector3 value1, FixVector3 value2)
        {
            FixVector3 result; FixVector3.Subtract(ref value1, ref value2, out result);
            return result;
        }
        #endregion

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The sum of both vectors.</returns>
        #region public static JVector operator +(JVector value1, JVector value2)
        public static FixVector3 operator +(FixVector3 value1, FixVector3 value2)
        {
            FixVector3 result; FixVector3.Add(ref value1, ref value2, out result);
            return result;
        }
        #endregion

        /// <summary>
        /// Divides a vector by a factor.
        /// </summary>
        /// <param name="value1">The vector to divide.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>Returns the scaled vector.</returns>
        public static FixVector3 operator /(FixVector3 value1, fixed64 value2) {
            FixVector3 result;
            FixVector3.Divide(ref value1, value2, out result);
            return result;
        }

        public FixVector2 ToFixVector2() {
            return new FixVector2(this.x, this.y);
        }

        public FixVector4 ToFixVector4()
        {
            return new FixVector4(this.x, this.y, this.z, fixed64.One);
        }

    }

}