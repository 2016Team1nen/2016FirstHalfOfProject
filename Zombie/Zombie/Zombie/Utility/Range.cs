using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombie.Utility
{
    class Range
    {
        private int first;
        private int end;

<<<<<<< HEAD
        public Range(int first, int end)
        {
=======
        public Range(int first, int end) {
>>>>>>> origin/you
            this.first = first;
            this.end = end;
        }

<<<<<<< HEAD
        public int First()
        {
            return first;
        }

        public int End()
        {
            return end;
        }

        public bool IsWishin(int num)
        {
            if (num < first)
            {
                return false;
            }

            if (num > end)
            {
                return false;
            }

            return true;
        }

        public bool IsOutOfRange()
        {
            return first >= end;
        }

        public bool IsOutOfRange(int num)
        {
            return !IsWishin(num);
        }
=======
        public int First() { return first; }
        public int End() { return end; }

        public bool IsWithin(int num) {
            if (num < first) { return false; }
            if (num > end) { return false; }
            return true;
        }

        public bool IsOutOfRange() { return first > end; }
        public bool IsOutOfRange(int num) { return !IsWithin(num); }
>>>>>>> origin/you
    }
}
