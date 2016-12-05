using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Graph {
    public interface IqNode {
        void IsCurrent(bool value);

        IqNode GetChild();
        void SetChild(IqNode child);

        IqNode GetParent();
        void SetParent(IqNode parent);

        IQInstruction GetInstrucion();
        void SetInstruction(IQInstruction instrucion);
    }
}