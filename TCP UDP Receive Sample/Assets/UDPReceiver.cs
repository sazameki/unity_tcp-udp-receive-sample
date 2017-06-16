using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
	public int portNum = 22222;

	static UdpClient udpClient;
	Thread thread;

	void Start()
	{
		udpClient = new UdpClient(portNum);
		thread = new Thread(new ThreadStart(ThreadProc));
		thread.Start();
	}

	void OnApplicationQuit()
	{
		thread.Abort();
	}

	private static void ThreadProc()
	{
		while (true)
		{
			IPEndPoint remoteEP = null;
			byte[] data = udpClient.Receive(ref remoteEP);
			string message = Encoding.ASCII.GetString(data);
			GameManager.Instance.AddUdpMessage(message);
		}
	}
}

