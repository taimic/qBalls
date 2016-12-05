using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Graph {
    public interface IQInstruction {
        string GetCode();
        Value GetValue();
    }
}
