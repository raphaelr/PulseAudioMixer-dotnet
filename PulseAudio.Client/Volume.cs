using System;
using PulseAudio.Client.Ffi;

namespace PulseAudio.Client
{
    public struct Volume : IEquatable<Volume>
    {
        public const uint Maximum = uint.MaxValue/2;
        public const uint Normal = 10000;
        public const uint Muted = 0;

        public readonly uint Value;

        public Volume(uint volume)
        {
            Value = volume;
        }

        public double ToLinear()
        {
            return PaVolume.pa_sw_volume_to_linear(Value);
        }

        public static Volume FromLinear(double linear)
        {
            return new Volume(PaVolume.pa_sw_volume_from_linear(linear));
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Volume && Equals((Volume) obj);
        }

        public bool Equals(Volume other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return (int) Value;
        }

        public static bool operator ==(Volume left, Volume right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Volume left, Volume right)
        {
            return !left.Equals(right);
        }

        public static bool IsValid(uint volume)
        {
            return volume <= Maximum;
        }

        public static implicit operator uint(Volume v)
        {
            return v.Value;
        }

        public static explicit operator Volume(uint volume)
        {
            if (!IsValid(volume))
            {
                throw new ArgumentOutOfRangeException("volume", volume, "Volume must be between 0 and " + Maximum +
                                                                      " inclusive");
            }

            return new Volume(volume);
        }
    }
}