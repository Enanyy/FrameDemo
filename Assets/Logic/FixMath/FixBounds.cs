using System;
using System.Collections.Generic;

namespace Fix
{
    public struct FixBounds : IEquatable<FixBounds>
    {

        private FixVector3 m_Position;
        private FixVector3 m_Size;

        public fixed64 x
        {
            get { return m_Position.x; }
            set { m_Position.x = value; }
        }

        public fixed64 y
        {
            get { return m_Position.y; }
            set { m_Position.y = value; }
        }

        public fixed64 z
        {
            get { return m_Position.z; }
            set { m_Position.z = value; }
        }

        public FixVector3 center
        {
            get { return new FixVector3(x + m_Size.x / 2f, y + m_Size.y / 2f, z + m_Size.z / 2f); }
        }

        public FixVector3 min
        {
            get { return new FixVector3(xMin, yMin, zMin); }
            set
            {
                xMin = value.x;
                yMin = value.y;
                zMin = value.z;
            }
        }

        public FixVector3 max
        {
            get { return new FixVector3(xMax, yMax, zMax); }
            set
            {
                xMax = value.x;
                yMax = value.y;
                zMax = value.z;
            }
        }

        public fixed64 xMin
        {
            get { return FixMath.Min(m_Position.x, m_Position.x + m_Size.x); }
            set
            {
                fixed64 oldxmax = xMax;
                m_Position.x = value;
                m_Size.x = oldxmax - m_Position.x;
            }
        }

        public fixed64 yMin
        {
            get { return FixMath.Min(m_Position.y, m_Position.y + m_Size.y); }
            set
            {
                fixed64 oldymax = yMax;
                m_Position.y = value;
                m_Size.y = oldymax - m_Position.y;
            }
        }

        public fixed64 zMin
        {
            get { return FixMath.Min(m_Position.z, m_Position.z + m_Size.z); }
            set
            {
                fixed64 oldzmax = zMax;
                m_Position.z = value;
                m_Size.z = oldzmax - m_Position.z;
            }
        }

        public fixed64 xMax
        {
            get { return FixMath.Max(m_Position.x, m_Position.x + m_Size.x); }
            set { m_Size.x = value - m_Position.x; }
        }

        public fixed64 yMax
        {
            get { return FixMath.Max(m_Position.y, m_Position.y + m_Size.y); }
            set { m_Size.y = value - m_Position.y; }
        }

        public fixed64 zMax
        {
            get { return FixMath.Max(m_Position.z, m_Position.z + m_Size.z); }
            set { m_Size.z = value - m_Position.z; }
        }

        public FixVector3 position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public FixVector3 size
        {
            get { return m_Size; }
            set { m_Size = value; }
        }

        public FixBounds(fixed64 xMin, fixed64 yMin, fixed64 zMin, fixed64 sizeX, fixed64 sizeY, fixed64 sizeZ)
        {
            m_Position = new FixVector3(xMin, yMin, zMin);
            m_Size = new FixVector3(sizeX, sizeY, sizeZ);
        }

        public FixBounds(FixVector3 position, FixVector3 size)
        {
            m_Position = position;
            m_Size = size;
        }

        public void SetMinMax(FixVector3 minPosition, FixVector3 maxPosition)
        {
            min = minPosition;
            max = maxPosition;
        }

        public void ClampToBounds(FixBounds bounds)
        {
            position = new FixVector3(
                FixMath.Max(FixMath.Min(bounds.xMax, position.x), bounds.xMin),
                FixMath.Max(FixMath.Min(bounds.yMax, position.y), bounds.yMin),
                FixMath.Max(FixMath.Min(bounds.zMax, position.z), bounds.zMin)
            );
            size = new FixVector3(
                FixMath.Min(bounds.xMax - position.x, size.x),
                FixMath.Min(bounds.yMax - position.y, size.y),
                FixMath.Min(bounds.zMax - position.z, size.z)
            );
        }

        public bool Contains(FixVector3 position)
        {
            return position.x >= xMin
                   && position.y >= yMin
                   && position.z >= zMin
                   && position.x < xMax
                   && position.y < yMax
                   && position.z < zMax;
        }

        public override string ToString()
        {
            return string.Format("Position: {0}, Size: {1}", m_Position, m_Size);
        }

        public static bool operator ==(FixBounds lhs, FixBounds rhs)
        {
            return lhs.m_Position == rhs.m_Position && lhs.m_Size == rhs.m_Size;
        }

        public static bool operator !=(FixBounds lhs, FixBounds rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object other)
        {
            if (!(other is FixBounds)) return false;

            return Equals((FixBounds) other);
        }

        public bool Equals(FixBounds other)
        {
            return m_Position.Equals(other.m_Position) && m_Size.Equals(other.m_Size);
        }

        public override int GetHashCode()
        {
            return m_Position.GetHashCode() ^ (m_Size.GetHashCode() << 2);
        }
    }
}
