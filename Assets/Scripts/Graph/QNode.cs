using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Graph {
    public class QNode : MonoBehaviour, IqNode {
        private IqNode _parent, _child;
        private IQInstruction _instruction;

        private Value value;
        public Value Value {
            get {
                return value;
            }

            set {
                this.value = value;
            }
        }

        public void IsCurrent(bool value) {
            GetComponent<Outline>().enabled = value;
        }

        public IqNode GetParent() {
            return _parent;
        }
        public IqNode GetChild() {
            return _child;
        }
        public IQInstruction GetInstrucion() {
            return _instruction;
        }       
        public void SetParent(IqNode parent) {
            _parent = parent;
        }
        public void SetChild(IqNode child) {
            _child = child;
        }
        public void SetInstruction(IQInstruction instrucion) {
            _instruction = instrucion;
        }
    }
}