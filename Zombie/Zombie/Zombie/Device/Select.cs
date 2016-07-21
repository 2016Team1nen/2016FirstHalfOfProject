using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Zombie.Device
{
    class Select
    {
        private int select;
        private InputState input;

        public Select() { select = 0; input = new InputState(); }

        public void SelectT() {
            if (input.IsDown()) {
                select++;
                if (select > 1) { select = 1; }
            }

            if (input.IsUp()) {
                select--;
                if (select < 0) { select = 0; }
            }
        }

        public int GetSelect() { return select; }
    }
}
