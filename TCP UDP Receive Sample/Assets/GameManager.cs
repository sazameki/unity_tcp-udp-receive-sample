using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject	tcpCube;
	Queue<string>       tcpMessageQueue = new Queue<string>();

	public GameObject	udpCube;
	Queue<string>       udpMessageQueue = new Queue<string>();

	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }

	void Start () {
		instance = this;
	}
	
	void Update () {
		// TCPからメッセージが来ていればキューブを回転
		string[] tcpMessages = PopAllTcpMessages();
		foreach (string message in tcpMessages) {
			Debug.Log("TCP: " + message);
			tcpCube.transform.Rotate(new Vector3(0f, 270f * Time.deltaTime, 0f));
		}

		// UDPからメッセージが来ていればキューブを回転
		string[] udpMessages = PopAllUdpMessages();
		foreach (string message in udpMessages) {
			Debug.Log("UDP: " + message);
			udpCube.transform.Rotate(new Vector3(0f, 270f * Time.deltaTime, 0f));
		}
	}

	// TCPから受信したメッセージの格納
	[MethodImpl(MethodImplOptions.Synchronized)]
	public void AddTcpMessage(string[] messages)
	{
		foreach (string s in messages) {
			tcpMessageQueue.Enqueue(s);
		}
	}

	// TCPから受信したメッセージの取り出し
	[MethodImpl(MethodImplOptions.Synchronized)]
	public string PopTcpMessage()
	{
		if (tcpMessageQueue.Count == 0) {
			return null;
		}
		return tcpMessageQueue.Dequeue();
	}

	// TCPから受信したすべてのメッセージの取り出し
	[MethodImpl(MethodImplOptions.Synchronized)]
	public string[] PopAllTcpMessages()
	{
		string[] ret = tcpMessageQueue.ToArray();
		tcpMessageQueue.Clear();
		return ret;
	}

	// UDPから受信したメッセージの格納
	[MethodImpl(MethodImplOptions.Synchronized)]
	public void AddUdpMessage(string message)
	{
		udpMessageQueue.Enqueue(message);
	}

	// UDPから受信したメッセージの取り出し
	[MethodImpl(MethodImplOptions.Synchronized)]
	public string PopUdpMessage()
	{
		if (udpMessageQueue.Count == 0) {
			return null;
		}
		return udpMessageQueue.Dequeue();
	}

	// UDPから受信したすべてのメッセージの取り出し
	[MethodImpl(MethodImplOptions.Synchronized)]
	public string[] PopAllUdpMessages()
	{
		string[] ret = udpMessageQueue.ToArray();
		udpMessageQueue.Clear();
		return ret;
	}
}
