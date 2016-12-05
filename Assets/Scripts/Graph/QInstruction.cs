using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Graph {
    class QInstruction : IQInstruction {
        private string _code;
        private Value _value;

        public QInstruction(string type, Value value) {
            _value = value;
            _code = QInstrucionFactory.GetCode(type, value);
        }

        public string GetCode() {
            return _code;
        }

        public Value GetValue() {
            return _value;
        }
    }
}
