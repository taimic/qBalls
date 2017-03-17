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
        QInstrucionFactory.botName = "Q_" + SessionMaster.Instance.botName + "_" + DateTime.Now.Year + DateTime.Now.DayOfYear + DateTime.Now.Hour + DateTime.Now.Minute;
    }

    private void BotFinished() {
        CreateNewBotName();

        WriteFile();
        botCode = "";
        addedNodes = 0;

        if (EventBotReady != null)
            EventBotReady();

        SourceCodeOutput.Instance.Reset();
        UI_Manager.Instance.ToEndScreen();//SceneManager.LoadScene(2);// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void WriteFile() {
        string path = "bots/" + QInstrucionFactory.botName + ".m";
        System.IO.File.Delete(path);
        using (StreamWriter outputFile = new StreamWriter(path, true)) {
            outputFile.Write(botCode);
        }
    }
}