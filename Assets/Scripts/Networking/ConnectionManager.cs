using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class ConnectionManager : MonoBehaviour {
    private delegate void RestartThread();

    private static ConnectionManager instance;
    private static bool runThread = false;
    private static bool doRestart = true;
    private static Thread clientThread;

    #region unity callbacks
    void Start() {
        instance = this;

        StartThread();

        TCP_Client.FoundBall += OnFoundBall;
    }

    void OnDestroy() {
        TCP_Client.FoundBall -= OnFoundBall;
        doRestart = false;
        clientThread.Abort();
    }
    #endregion

    #region private
    private void OnFoundBall(float x, float y) {
        print("Found Ball @ x " + x + " y " + y);
    }
    #endregion

    #region threading
    private static void StartThread() {
        runThread = true;
        clientThread = new Thread(Run);
        clientThread.Start();
        //clientThread.Join();
    }

    public static void Run() {
        while (runThread) {
            TCP_Client.StartClient();
            runThread = false;
        }
        // connection lost - restart thread
        if (doRestart)
            StartThread();
    }
    #endregion
}