using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Models.Map
{
    internal class TetrisMap
    {
        private List<List<FieldValue>> _map;

        public List<FieldValue> this[int index]
        {
            get => _map[index];
            set => _map[index] = value;
        }

        public TetrisMap()
        {
            _map = Enumerable.Range(0, 10)
               .Select(i => new List<FieldValue>(Enumerable.Range(0, 22).Select(x => FieldValue.None)))
               .ToList();
        }
    }
}
