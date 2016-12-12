using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;

public class TCP_Client {
    public delegate void PositionDelegate(float x, float y);
    public static event PositionDelegate FoundBall;

    public static void StartClient() {
        bool hasConnection = false;
        TcpClient client = null;
        BinaryReader binReader = null;
        while (!hasConnection) {
            // connect to server
            try {
                System.Net.IPAddress adress = System.Net.IPAddress.Parse("10.42.1.134");
                client = new TcpClient();
                client.Connect(adress, 4711);
            }
            catch {
                Debug.Log("No server found...");
            }

            if (client != null) {
                // read stream
                client.ReceiveBufferSize = 8;
                binReader = new BinaryReader(client.GetStream());
                hasConnection = true;
            }
            Debug.Log("Waiting for server...");
            Thread.Sleep(1000);
        }
        Debug.Log("Connection established.");
        bool loop = true;
        while (loop) {
            try {
                float x = binReader.ReadSingle();
                float y = binReader.ReadSingle();

                //Debug.Log("x " + x + " y " + y);
                // send event
                if (FoundBall != null)
                    FoundBall(x, y);
            }
            catch (Exception) {
                // stop loop when error occures
                loop = false;
            }
        }
        // close connection
        client.Close();
    }
}
