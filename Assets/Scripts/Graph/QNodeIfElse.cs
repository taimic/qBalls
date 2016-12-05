using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Graph {
    class QNodeIfElse : QNode {
        private IqNode _elseChild;

        public string c2; // TODO: only for debugging

        public IqNode GetElseChild() {
            return _elseChild;
        }
        public void SetElseChild(IqNode elseChild) {
            _elseChild = elseChild;
            c2 = elseChild.GetType().Name;
        }
    }
}
