using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Graph;
using System;

public enum Value {
    No,
    Low,
    Mid,
    High
}

public class FlowBuilder : MonoBehaviour {
    public delegate void FlowDelegate();
    public static event FlowDelegate EventAddedIfElseNode, EventAddedElseNode, EventAddedNode;

    private static FlowBuilder instance;
    public static FlowBuilder Instance {
        get { return instance; }
    }

    public RectTransform placeholderNode;
    public RectTransform flowPanelRect;
    public List<GameObject> instrucionPrefabs;

    private string currentInstruction;

    private List<QNode> instructions;
    public List<QNode> Instructions {
        get {
            return instructions;
        }
    }

    private float _spacing = 130;
    private int _currentLayer = 0;

    #region unity callbacks
    void Awake() {
        instance = this;
    }
    void Start() {
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();

        instructions = new List<QNode>();
        flowPanelRect = GetComponent<RectTransform>();

        CodeManager.EventBotReady += OnBotReady;
    }
    void OnDestroy() {
        CodeManager.EventBotReady -= OnBotReady;
    }

    #endregion

    #region public
    public string CurrentInstruction {
        get {
            return Instance.currentInstruction;
        }

        set {
            Instance.currentInstruction = value;
        }
    }

    public void AddInstruction(string name, Value value = Value.No) {
        foreach (GameObject item in instrucionPrefabs) {
            if (item.name == name) {
                AddNode(item, value);
            }
        }
    }
    #endregion

    #region private
    private void OnBotReady() {
        foreach (QNode item in instructions) {
            Destroy(item.gameObject);
        }
        instructions.Clear();
    }

    // Adds a new Node to the existing graph using wut
    private void AddNode(GameObject item, Value value) {
        QNode newNode = InstantiateInstruction(item, value);
        SetNodePosition(newNode);

        instructions.Add(newNode);

        if (EventAddedNode != null)
            EventAddedNode();
    }

    private QNode InstantiateInstruction(GameObject obj, Value value) {
        QNode node = obj.GetComponent<QNode>();
        if (node is QNodeIfElse) {
            node = Instantiate(obj, transform).GetComponent<QNodeIfElse>();
            node.Value = value;

            if (EventAddedIfElseNode != null)
                EventAddedIfElseNode();
        }
        else {
            node = Instantiate(obj, transform).GetComponent<QNode>();
            node.Value = value;
            // parenting
            if (instructions.Count > 1) {
                // set if child
                if (instructions[instructions.Count - 1] is QNodeIfElse) {
                    instructions[instructions.Count - 1].SetChild(node);
                    node.SetParent(instructions[instructions.Count - 1]);
                } // set else child
                else if (instructions[instructions.Count - 2] is QNodeIfElse) {
                    ((QNodeIfElse)instructions[instructions.Count - 2]).SetElseChild(node);
                    node.SetParent(instructions[instructions.Count - 2]);

                    if (EventAddedElseNode != null)
                        EventAddedElseNode();
                }
            }
            else if (instructions.Count > 0) {
                // set if child
                if (instructions[0] is QNodeIfElse) {
                    instructions[0].SetChild(node);
                    node.SetParent(instructions[0]);
                }
            }
        }

        node.name = obj.name;
        return node;
    }

    private void SetNodePosition(QNode node) {
        float halfWidth = flowPanelRect.rect.width / 2;

        node.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);

        if (node.GetParent() == null) {
            if (instructions.Count > 0) {
                node.GetComponent<RectTransform>().localPosition = new Vector2(halfWidth, instructions[instructions.Count - 1].GetComponent<RectTransform>().localPosition.y - _spacing);
            }
            else { // first entry
                node.GetComponent<RectTransform>().localPosition = new Vector2(halfWidth, 0);
            }
        }
        else {
            float parentNodeYPosition = ((QNode)node.GetParent()).GetComponent<RectTransform>().localPosition.y;
            if (node.GetParent().GetChild() == node) {
                node.GetComponent<RectTransform>().localPosition = new Vector2(halfWidth - _spacing, parentNodeYPosition - _spacing);
            }
            else {
                node.GetComponent<RectTransform>().localPosition = new Vector2(halfWidth + _spacing, parentNodeYPosition - _spacing);
            }
        }
    }
    #endregion
}