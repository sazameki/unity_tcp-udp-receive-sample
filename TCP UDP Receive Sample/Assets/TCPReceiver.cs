using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPReceiver : MonoBehaviour
{
	public int portNum = 12345;

	static TcpListener tcpServer;
	Thread serverThread;
	static List<Thread>	clientThreads = new List<Thread>();
	static bool isRunning;
	static byte[] buffer = new byte[1024];

	void Start()
	{
		IPAddress addr = IPAddress.Parse("127.0.0.1");
		tcpServer = new TcpListener(addr, portNum);
		tcpServer.Start();
		serverThread = new Thread(new ThreadStart(ServerThreadProc));
		serverThread.Start();
	}

	void OnApplicationQuit()
	{
		serverThread.Abort();
		foreach (Thread t in clientThreads) {
			t.Abort();
		}
	}

	private static void ServerThreadProc()
	{
		while (true)
		{
			TcpClient client = tcpServer.AcceptTcpClient();
			Client c = new Client(client);
			Thread thread = new Thread(new ThreadStart(c.Work));
			clientThreads.Add(thread);
			thread.Start();
		}
	}

	class Client {
		TcpClient tcpClient;

		public Client(TcpClient tcpClient_) {
			tcpClient = tcpClient_;
		}

		public void Work()
		{
			NetworkStream stream = tcpClient.GetStream();
			while (true) {
				int len = stream.Read(buffer, 0, buffer.Length);
				if (len <= 0) {
					break;
				}
				string message = Encoding.ASCII.GetString(buffer, 0, len);
				string[] delimiters = { "\r", "\n" };
				string[] parts = message.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
				GameManager.Instance.AddTcpMessage(parts);
			}
			clientThreads.Remove(Thread.CurrentThread);
		}

	}
}

