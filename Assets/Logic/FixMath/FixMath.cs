using System;
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

namespace Fix
{

    /// <summary>
    /// Contains common math operations.
    /// </summary>
    public sealed class FixMath {

        /// <summary>
        /// PI constant.
        /// </summary>
        public static fixed64 Pi = fixed64.Pi;

        /**
        *  @brief PI over 2 constant.
        **/
        public static fixed64 PiOver2 = fixed64.PiOver2;

        /// <summary>
        /// A small value often used to decide if numeric 
        /// results are zero.
        /// </summary>
		public static fixed64 Epsilon = fixed64.Epsilon;

        /**
        *  @brief Degree to radians constant.
        **/
        public static fixed64 Deg2Rad = fixed64.Deg2Rad;

        /**
        *  @brief Radians to degree constant.
        **/
        public static fixed64 Rad2Deg = fixed64.Rad2Deg;


        /**
         * @brief fixed64 infinity.
         * */
        public static fixed64 Infinity = fixed64.MaxValue;

        /// <summary>
        /// Gets the square root.
        /// </summary>
        /// <param name="number">The number to get the square root from.</param>
        /// <returns></returns>
        #region public static fixed64 Sqrt(fixed64 number)
        public static fixed64 Sqrt(fixed64 number) {
            return fixed64.Sqrt(number);
        }
        #endregion

        /// <summary>
        /// Gets the maximum number of two values.
        /// </summary>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <returns>Returns the largest value.</returns>
        #region public static fixed64 Max(fixed64 val1, fixed64 val2)
        public static fixed64 Max(fixed64 val1, fixed64 val2) {
            return (val1 > val2) ? val1 : val2;
        }
        #endregion

        /// <summary>
        /// Gets the minimum number of two values.
        /// </summary>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <returns>Returns the smallest value.</returns>
        #region public static fixed64 Min(fixed64 val1, fixed64 val2)
        public static fixed64 Min(fixed64 val1, fixed64 val2) {
            return (val1 < val2) ? val1 : val2;
        }
        #endregion

        /// <summary>
        /// Gets the maximum number of three values.
        /// </summary>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <param name="val3">The third value.</param>
        /// <returns>Returns the largest value.</returns>
        #region public static fixed64 Max(fixed64 val1, fixed64 val2,fixed64 val3)
        public static fixed64 Max(fixed64 val1, fixed64 val2, fixed64 val3) {
            fixed64 max12 = (val1 > val2) ? val1 : val2;
            return (max12 > val3) ? max12 : val3;
        }
        #endregion

        /// <summary>
        /// Returns a number which is within [min,max]
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        #region public static fixed64 Clamp(fixed64 value, fixed64 min, fixed64 max)
        public static fixed64 Clamp(fixed64 value, fixed64 min, fixed64 max) {
            if (value < min)
            {
                value = min;
                return value;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }
        #endregion

        /// <summary>
        /// Returns a number which is within [fixed64.Zero, fixed64.One]
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>The clamped value.</returns>
        public static fixed64 Clamp01(fixed64 value)
        {
            if (value < fixed64.Zero)
                return fixed64.Zero;

            if (value > fixed64.One)
                return fixed64.One;

            return value;
        }

        /// <summary>
        /// Changes every sign of the matrix entry to '+'
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="result">The absolute matrix.</param>
        #region public static void Absolute(ref JMatrix matrix,out JMatrix result)
        public static void Absolute(ref FixMatrix3x3 matrix, out FixMatrix3x3 result) {
            result.M11 = fixed64.Abs(matrix.M11);
            result.M12 = fixed64.Abs(matrix.M12);
            result.M13 = fixed64.Abs(matrix.M13);
            result.M21 = fixed64.Abs(matrix.M21);
            result.M22 = fixed64.Abs(matrix.M22);
            result.M23 = fixed64.Abs(matrix.M23);
            result.M31 = fixed64.Abs(matrix.M31);
            result.M32 = fixed64.Abs(matrix.M32);
            result.M33 = fixed64.Abs(matrix.M33);
        }
        #endregion

        /// <summary>
        /// Returns the sine of value.
        /// </summary>
        public static fixed64 Sin(fixed64 value) {
            return fixed64.Sin(value);
        }

        /// <summary>
        /// Returns the cosine of value.
        /// </summary>
        public static fixed64 Cos(fixed64 value) {
            return fixed64.Cos(value);
        }

        /// <summary>
        /// Returns the tan of value.
        /// </summary>
        public static fixed64 Tan(fixed64 value) {
            return fixed64.Tan(value);
        }

        /// <summary>
        /// Returns the arc sine of value.
        /// </summary>
        public static fixed64 Asin(fixed64 value) {
            return fixed64.Asin(value);
        }

        /// <summary>
        /// Returns the arc cosine of value.
        /// </summary>
        public static fixed64 Acos(fixed64 value) {
            return fixed64.Acos(value);
        }

        /// <summary>
        /// Returns the arc tan of value.
        /// </summary>
        public static fixed64 Atan(fixed64 value) {
            return fixed64.Atan(value);
        }

        /// <summary>
        /// Returns the arc tan of coordinates x-y.
        /// </summary>
        public static fixed64 Atan2(fixed64 y, fixed64 x) {
            return fixed64.Atan2(y, x);
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified number.
        /// </summary>
        public static fixed64 Floor(fixed64 value) {
            return fixed64.Floor(value);
        }

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the specified number.
        /// </summary>
        public static fixed64 Ceiling(fixed64 value) {
            return value;
        }

        /// <summary>
        /// Rounds a value to the nearest integral value.
        /// If the value is halfway between an even and an uneven value, returns the even value.
        /// </summary>
        public static fixed64 Round(fixed64 value) {
            return fixed64.Round(value);
        }

        /// <summary>
        /// Returns a number indicating the sign of a Fix64 number.
        /// Returns 1 if the value is positive, 0 if is 0, and -1 if it is negative.
        /// </summary>
        public static int Sign(fixed64 value) {
            return fixed64.Sign(value);
        }

        /// <summary>
        /// Returns the absolute value of a Fix64 number.
        /// Note: Abs(Fix64.MinValue) == Fix64.MaxValue.
        /// </summary>
        public static fixed64 Abs(fixed64 value) {
            return fixed64.Abs(value);                
        }

        public static fixed64 Barycentric(fixed64 value1, fixed64 value2, fixed64 value3, fixed64 amount1, fixed64 amount2) {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

        public static fixed64 CatmullRom(fixed64 value1, fixed64 value2, fixed64 value3, fixed64 value4, fixed64 amount) {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using FPs not to lose precission
            fixed64 amountSquared = amount * amount;
            fixed64 amountCubed = amountSquared * amount;
            return (fixed64)(0.5 * (2.0 * value2 +
                                 (value3 - value1) * amount +
                                 (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                                 (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

        public static fixed64 Distance(fixed64 value1, fixed64 value2) {
            return fixed64.Abs(value1 - value2);
        }

        public static fixed64 Hermite(fixed64 value1, fixed64 tangent1, fixed64 value2, fixed64 tangent2, fixed64 amount) {
            // All transformed to fixed64 not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            fixed64 v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            fixed64 sCubed = s * s * s;
            fixed64 sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                         (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                         t1 * s +
                         v1;
            return (fixed64)result;
        }

        public static fixed64 Lerp(fixed64 value1, fixed64 value2, fixed64 amount) {
            return value1 + (value2 - value1) * Clamp01(amount);
        }

        public static fixed64 InverseLerp(fixed64 value1, fixed64 value2, fixed64 amount) {
            if (value1 != value2)
                return Clamp01((amount - value1) / (value2 - value1));
            return fixed64.Zero;
        }

        public static fixed64 SmoothStep(fixed64 value1, fixed64 value2, fixed64 amount) {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            fixed64 result = Clamp(amount, 0f, 1f);
            result = Hermite(value1, 0f, value2, 0f, result);
            return result;
        }


        /// <summary>
        /// Returns 2 raised to the specified power.
        /// Provides at least 6 decimals of accuracy.
        /// </summary>
        internal static fixed64 Pow2(fixed64 x)
        {
            if (x.RawValue == 0)
            {
                return fixed64.One;
            }

            // Avoid negative arguments by exploiting that exp(-x) = 1/exp(x).
            bool neg = x.RawValue < 0;
            if (neg)
            {
                x = -x;
            }

            if (x == fixed64.One)
            {
                return neg ? fixed64.One / (fixed64)2 : (fixed64)2;
            }
            if (x >= fixed64.Log2Max)
            {
                return neg ? fixed64.One / fixed64.MaxValue : fixed64.MaxValue;
            }
            if (x <= fixed64.Log2Min)
            {
                return neg ? fixed64.MaxValue : fixed64.Zero;
            }

            /* The algorithm is based on the power series for exp(x):
             * http://en.wikipedia.org/wiki/Exponential_function#Formal_definition
             * 
             * From term n, we get term n+1 by multiplying with x/n.
             * When the sum term drops to zero, we can stop summing.
             */

            int integerPart = (int)Floor(x);
            // Take fractional part of exponent
            x = fixed64.FromRaw(x.RawValue & 0x00000000FFFFFFFF);

            var result = fixed64.One;
            var term = fixed64.One;
            int i = 1;
            while (term.RawValue != 0)
            {
                term = fixed64.FastMul(fixed64.FastMul(x, term), fixed64.Ln2) / (fixed64)i;
                result += term;
                i++;
            }

            result = fixed64.FromRaw(result.RawValue << integerPart);
            if (neg)
            {
                result = fixed64.One / result;
            }

            return result;
        }

        /// <summary>
        /// Returns the base-2 logarithm of a specified number.
        /// Provides at least 9 decimals of accuracy.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument was non-positive
        /// </exception>
        internal static fixed64 Log2(fixed64 x)
        {
            if (x.RawValue <= 0)
            {
                throw new ArgumentOutOfRangeException("Non-positive value passed to Ln", "x");
            }

            // This implementation is based on Clay. S. Turner's fast binary logarithm
            // algorithm (C. S. Turner,  "A Fast Binary Logarithm Algorithm", IEEE Signal
            //     Processing Mag., pp. 124,140, Sep. 2010.)

            long b = 1U << (fixed64.FRACTIONAL_PLACES - 1);
            long y = 0;

            long rawX = x.RawValue;
            while (rawX < fixed64.ONE)
            {
                rawX <<= 1;
                y -= fixed64.ONE;
            }

            while (rawX >= (fixed64.ONE << 1))
            {
                rawX >>= 1;
                y += fixed64.ONE;
            }

            var z = fixed64.FromRaw(rawX);

            for (int i = 0; i < fixed64.FRACTIONAL_PLACES; i++)
            {
                z = fixed64.FastMul(z, z);
                if (z.RawValue >= (fixed64.ONE << 1))
                {
                    z = fixed64.FromRaw(z.RawValue >> 1);
                    y += b;
                }
                b >>= 1;
            }

            return fixed64.FromRaw(y);
        }

        /// <summary>
        /// Returns the natural logarithm of a specified number.
        /// Provides at least 7 decimals of accuracy.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument was non-positive
        /// </exception>
        public static fixed64 Ln(fixed64 x)
        {
            return fixed64.FastMul(Log2(x), fixed64.Ln2);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// Provides about 5 digits of accuracy for the result.
        /// </summary>
        /// <exception cref="DivideByZeroException">
        /// The base was zero, with a negative exponent
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The base was negative, with a non-zero exponent
        /// </exception>
        public static fixed64 Pow(fixed64 b, fixed64 exp)
        {
            if (b == fixed64.One)
            {
                return fixed64.One;
            }

            if (exp.RawValue == 0)
            {
                return fixed64.One;
            }

            if (b.RawValue == 0)
            {
                if (exp.RawValue < 0)
                {
                    //throw new DivideByZeroException();
                    return fixed64.MaxValue;
                }
                return fixed64.Zero;
            }

            fixed64 log2 = Log2(b);
            return Pow2(exp * log2);
        }

        public static fixed64 MoveTowards(fixed64 current, fixed64 target, fixed64 maxDelta)
        {
            if (Abs(target - current) <= maxDelta)
                return target;
            return (current + (Sign(target - current)) * maxDelta);
        }

        public static fixed64 Repeat(fixed64 t, fixed64 length)
        {
            return (t - (Floor(t / length) * length));
        }

        public static fixed64 DeltaAngle(fixed64 current, fixed64 target)
        {
            fixed64 num = Repeat(target - current, (fixed64)360f);
            if (num > (fixed64)180f)
            {
                num -= (fixed64)360f;
            }
            return num;
        }

        public static fixed64 MoveTowardsAngle(fixed64 current, fixed64 target, float maxDelta)
        {
            target = current + DeltaAngle(current, target);
            return MoveTowards(current, target, maxDelta);
        }

        public static fixed64 SmoothDamp(fixed64 current, fixed64 target, ref fixed64 currentVelocity, fixed64 smoothTime, fixed64 maxSpeed)
        {
            fixed64 deltaTime = fixed64.EN2;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static fixed64 SmoothDamp(fixed64 current, fixed64 target, ref fixed64 currentVelocity, fixed64 smoothTime)
        {
            fixed64 deltaTime = fixed64.EN2;
            fixed64 positiveInfinity = -fixed64.MaxValue;
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
        }

        public static fixed64 SmoothDamp(fixed64 current, fixed64 target, ref fixed64 currentVelocity, fixed64 smoothTime, fixed64 maxSpeed, fixed64 deltaTime)
        {
            smoothTime = Max(fixed64.EN4, smoothTime);
            fixed64 num = (fixed64)2f / smoothTime;
            fixed64 num2 = num * deltaTime;
            fixed64 num3 = fixed64.One / (((fixed64.One + num2) + (((fixed64)0.48f * num2) * num2)) + ((((fixed64)0.235f * num2) * num2) * num2));
            fixed64 num4 = current - target;
            fixed64 num5 = target;
            fixed64 max = maxSpeed * smoothTime;
            num4 = Clamp(num4, -max, max);
            target = current - num4;
            fixed64 num7 = (currentVelocity + (num * num4)) * deltaTime;
            currentVelocity = (currentVelocity - (num * num7)) * num3;
            fixed64 num8 = target + ((num4 + num7) * num3);
            if (((num5 - current) > fixed64.Zero) == (num8 > num5))
            {
                num8 = num5;
                currentVelocity = (num8 - num5) / deltaTime;
            }
            return num8;
        }
    }
}
