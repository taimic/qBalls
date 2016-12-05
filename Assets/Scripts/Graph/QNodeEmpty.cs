using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Graph {
    class QNodeEmpty : IqNode {
        private IqNode _parent;
        public IqNode GetChild() {
            return null;
        }

        public IQInstruction GetInstrucion() {
            return new QInstruction("empty", Value.No);
        }

        public IqNode GetParent() {
            return _parent;
        }

        public void IsCurrent(bool value) {
            throw new NotImplementedException();
        }

        public void SetChild(IqNode child) {
            throw new NotImplementedException();
        }

        public void SetInstruction(IQInstruction instrucion) {
            throw new NotImplementedException();
        }

        public void SetParent(IqNode parent) {
            _parent = parent;
        }
    }
}
