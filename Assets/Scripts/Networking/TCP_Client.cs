using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;
using System.Xml;

public class TCP_Client {
    public delegate void PositionDelegate(float x, float y);
    public static event PositionDelegate FoundBall;

    private static XmlDocument LoadConfig()
    {
        string fileName = "Config.xml";
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(fileName);
        }
        catch (Exception e)
        {
            if (e is FileNotFoundException)
                Console.WriteLine(fileName + " not found.");
            else if (e is XmlException)
                Console.WriteLine(fileName + " has invalid content.");

            Console.WriteLine("NO CONFIG LOADED - USING DEFAULT VALUES");
            return null;
        }
        return doc;
    }

    private static string ApplyConfig(XmlDocument doc)
    {
        if (doc == null)
            return "";

        if (doc.DocumentElement.ChildNodes.Count > 0)
        {
            Console.WriteLine("********* ReadingConfig... *********");

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "IP")
                {
                    return node.InnerText;
                }
            }
        }
        return "";
    }

    public static void StartClient() {
        bool hasConnection = false;
        TcpClient client = null;
        BinaryReader binReader = null;
        while (!hasConnection) {
            // connect to server
            try {
                System.Net.IPAddress adress = System.Net.IPAddress.Parse(ApplyConfig(LoadConfig()));
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
