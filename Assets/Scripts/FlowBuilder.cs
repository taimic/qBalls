using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Graph;

public enum Value {
    No,
    Low,
    Mid,
    High
}

public class FlowBuilder : MonoBehaviour {
    private static FlowBuilder instance;
    public static FlowBuilder Instance
    {
        get { return instance; }
    }

    public RectTransform placeholderNode;
    public List<GameObject> instrucionPrefabs;

    private string currentInstruction;
    private List<GameObject> instructions;
    private IqNode headNode, currentNode;

    private float _spacing = 130;
    private int _currentLayer = 0;

    #region unity callbacks
    void Awake() {
        instance = this;
    }
    void Start() {
        instructions = new List<GameObject>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P))
            PrintSimpleGraph();
    }
    #endregion

    #region public
    public string CurrentInstruction
    {
        get
        {
            return Instance.currentInstruction;
        }

        set
        {
            Instance.currentInstruction = value;
        }
    }

    public void AddInstruction(string name, Value value = Value.No) {
        foreach (GameObject item in instrucionPrefabs) {
            if (item.name == name) {
                //AddToList(item);
                AddNode(item, value);
            }
        }
    }
    #endregion

    #region private
    private void AddNode(GameObject item, Value value) {
        IqNode newNode = item.GetComponent<IqNode>();
        if (newNode is QNodeIfElse) {
            newNode = (QNodeIfElse)Instantiate(item.GetComponent<QNodeIfElse>(), transform);
            newNode.SetChild(new QNodeEmpty());
            ((QNodeIfElse)newNode).SetElseChild(new QNodeEmpty());
        }
        else if (newNode is QNode) {
            newNode = (QNode)Instantiate(item.GetComponent<QNode>(), transform);
            newNode.SetChild(new QNodeEmpty());
        }

        RectTransform newRect = ((QNode)newNode).GetComponent<RectTransform>();
        newRect.localScale = new Vector3(0.1f, 0.1f);
        newRect.localPosition = Vector3.down * _spacing * _currentLayer;

        newNode.SetParent(currentNode);
        newNode.SetInstruction(new QInstruction(item.name, value));

        // first entry
        if (headNode == null) {
            headNode = currentNode = newNode;
            currentNode.IsCurrent(true);
            _currentLayer++;
        }
        else if (!HandleElseChild(newNode, newRect)) {
            if (!(currentNode is QNodeIfElse)) {
                _currentLayer++;
            }


            currentNode.IsCurrent(false);
            currentNode.SetChild(newNode);
            currentNode = newNode;
            currentNode.IsCurrent(true);
        }


        SetNextFreeNode(newNode);
    }

    private void SetNextFreeNode(IqNode newNode) {
        IqNode nextNode = GetNextFreeNodeBF(headNode);
        placeholderNode.GetComponent<QNode>().SetParent(nextNode.GetParent());
        placeholderNode.localPosition = ((QNode)nextNode.GetParent()).GetComponent<RectTransform>().localPosition + Vector3.down * _spacing;
    }

    // TODO: WIP - trying to traverse recursively
    private IqNode GetNextFreeNodeBF(IqNode node) {

        if (node is QNodeIfElse) { // IF/ELSE node
            QNodeIfElse ifNode = (QNodeIfElse)node;
            if (ifNode.GetElseChild() is QNodeEmpty) { // ELSE leaf
                ifNode.GetElseChild().SetParent(ifNode);
                return (ifNode.GetElseChild());
            }
            else {
                return GetNextFreeNodeBF(ifNode.GetElseChild()); // traverse down
            }
        }

        if (node.GetChild() is QNodeEmpty) { // most left leaf (can be an IF leaf)
            node.GetChild().SetParent(node);
            return node.GetChild();
        }

        print("never reached");
        return null; // SUX
    }

    private bool HandleElseChild(IqNode newNode, RectTransform newRect) {
        QNodeIfElse ifNode = null;
        if (currentNode.GetParent() is QNodeIfElse)
            ifNode = (QNodeIfElse)currentNode.GetParent();
        if (ifNode != null && (ifNode.GetElseChild() is QNodeEmpty)) {
            newNode.SetParent(ifNode);
            ifNode.SetElseChild(newNode);
            newRect.localPosition = ifNode.transform.localPosition
                + Vector3.down * _spacing
                + Vector3.right * 1.2f * _spacing;

            currentNode = ifNode.GetChild();
            _currentLayer++;
            return true; // handled ELSE
        }

        return false; // no ELSE
    }

    private void PrintSimpleGraph() {
        IqNode node = headNode;
        string output = "";

        while (node != null) {
            output += node.GetInstrucion().GetCode() + "\n";

            if (node.GetParent() != null)
                if (node.GetParent() is QNodeIfElse)
                    if (((QNodeIfElse)node.GetParent()).GetElseChild() != null)
                        output += "ELSE\n" + ((QNodeIfElse)node.GetParent()).GetElseChild().GetInstrucion().GetCode() + "\nEND\n";

            node = node.GetChild();
        }
        //print(output);
        SourceCodeOutput.Instance.SetText(output);
    }

    // [deprecated] just for testing
    private void AddToList(GameObject item) {
        GameObject newInstruction = (GameObject)Instantiate(item, transform);
        RectTransform newRect = newInstruction.GetComponent<RectTransform>();
        newRect.localScale = new Vector3(0.1f, 0.1f);
        newRect.localPosition = Vector3.down * _spacing * instructions.Count;
        instructions.Add(newInstruction);
    }
    #endregion
}

// not working with multiple if layers
//private void AddNode(GameObject item, Value value) {
//    IqNode newNode = item.GetComponent<IqNode>();
//    if (newNode is QNodeIfElse) {
//        newNode = (QNodeIfElse)Instantiate(item.GetComponent<QNodeIfElse>(), transform);
//    }
//    else if (newNode is QNode) {
//        newNode = (QNode)Instantiate(item.GetComponent<QNode>(), transform);
//    }

//    RectTransform newRect = ((QNode)newNode).GetComponent<RectTransform>();
//    newRect.localScale = new Vector3(0.1f, 0.1f);
//    newRect.localPosition = Vector3.down * _spacing * _currentLayer;

//    newNode.SetParent(currentNode);
//    newNode.SetInstruction(new QInstruction(item.name, value));

//    // first entry
//    if (headNode == null) {
//        headNode = currentNode = newNode;
//        currentNode.IsCurrent(true);
//        _currentLayer++;
//    }
//    else if (!HandleElseChild(newNode, newRect)) {
//        if (!(currentNode is QNodeIfElse)) {
//            _currentLayer++;
//        }
//        currentNode.IsCurrent(false);
//        currentNode.SetChild(newNode);
//        currentNode = newNode;
//        currentNode.IsCurrent(true);
//    }
//}