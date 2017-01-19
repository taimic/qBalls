using Assets.Scripts.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodeManager : MonoBehaviour {
    public delegate void CodeManagerDelegate();
    public static event CodeManagerDelegate EventBotReady;

    private List<QNode> instrucitons;

    public int addedNodes = 0;
    private int nodesToAdd = 8;

    private string botCode;

    void Start() {
        FlowBuilder.EventAddedNode += OnAddedNode;

        CreateNewBotName();
    }

    void OnDestroy() {
        FlowBuilder.EventAddedNode -= OnAddedNode;
    }

    private void OnAddedNode() {
        if (instrucitons == null)
            instrucitons = FlowBuilder.Instance.Instructions;

        botCode = QInstrucionFactory.GetHeader();

        foreach (QNode item in instrucitons) {
            if (item is QNodeIfElse) {
                // Empty IfElse Node
                if (item.GetChild() == null && ((QNodeIfElse)item).GetElseChild() == null) {
                    botCode += ("\n" + QInstrucionFactory.GetCode(item.name, item.Value));
                    botCode += ("\n" + QInstrucionFactory.GetEndBlock());
                }
                else {
                    botCode += ("\n" + QInstrucionFactory.GetCode(item.name, item.Value));
                }
            }
            else {
                if (item.GetParent() != null) {
                    if (item == item.GetParent().GetChild() && ((QNodeIfElse)item.GetParent()).GetElseChild() == null) {
                        botCode += ("\n" + QInstrucionFactory.GetCode(item.name, item.Value));
                        botCode += ("\n" + QInstrucionFactory.GetEndBlock());
                    }
                    else if (((QNodeIfElse)item.GetParent()).GetElseChild() == item) {
                        botCode += ("\n" + QInstrucionFactory.GetElse());
                        botCode += ("\n" + QInstrucionFactory.GetCode(item.name, item.Value));
                        botCode += ("\n" + QInstrucionFactory.GetEndBlock());
                    }
                    else {
                        botCode += ("\n" + QInstrucionFactory.GetCode(item.name, item.Value));
                    }
                }
                else {
                    botCode += ("\n" + QInstrucionFactory.GetCode(item.name, item.Value));
                }
            }
        }

        botCode += QInstrucionFactory.GetFooter();

        SourceCodeOutput.Instance.SetText(botCode);

        addedNodes++;
        if (addedNodes >= nodesToAdd)
            BotFinished();
    }

    private void CreateNewBotName() {
        QInstrucionFactory.botName = "Bot_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Ticks;
    }

    private void BotFinished() {
        WriteFile();
        botCode = "";
        addedNodes = 0;

        CreateNewBotName();

        if (EventBotReady != null)
            EventBotReady();

        SourceCodeOutput.Instance.Reset();
        //SceneManager.LoadScene(1);// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void WriteFile() {
        using (StreamWriter outputFile = new StreamWriter(QInstrucionFactory.botName + ".m", true)) {
            outputFile.Write(botCode);
        }
    }
}