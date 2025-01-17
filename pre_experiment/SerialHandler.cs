﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
	public delegate void SerialDataReceivedEventHandler(string message);
	public event SerialDataReceivedEventHandler OnDataReceived;

	public string portName = "COM3"; // ポート名(Macだと/dev/tty.usbmodem1421など)
	public int baudRate = 9600;  // ボーレート(Arduinoに記述したものに合わせる)

	private SerialPort serialPort_;
	private Thread thread_;
	private bool isRunning_ = false;

	private string message_;
	private bool isNewMessageReceived_ = false;

	void Awake()
	{
		Debug.Log("aaaa");
		Open();
	}

	void Update()
	{
		if (isNewMessageReceived_)
		{
			OnDataReceived(message_);
		}
		isNewMessageReceived_ = false;

        if (Input.GetKeyDown(KeyCode.Delete))
        {
			Destroy(gameObject);
        }
	}

	void OnDestroy()
	{
		Close();
	}

	private void Open()
	{
		//serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
		//または
		serialPort_ = new SerialPort(portName, baudRate);
		Debug.Log("aaaa");

		serialPort_.Open();

		isRunning_ = true;

		thread_ = new Thread(Read);
		thread_.Start();
	}

	private void Close()
	{
		isNewMessageReceived_ = false;
		isRunning_ = false;

		if (thread_ != null && thread_.IsAlive)
		{
			thread_.Join();
		}

		if (serialPort_ != null && serialPort_.IsOpen)
		{
			serialPort_.Close();
			serialPort_.Dispose();
		}
	}

	private void Read()
	{
		while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
		{
			try
			{
				message_ = serialPort_.ReadLine();
				isNewMessageReceived_ = true;
			}
			catch (System.Exception e)
			{
				Debug.LogWarning(e.Message);
			}
		}
	}

	public void Write(string message)
	{
		try
		{
			serialPort_.Write(message);
		}
		catch (System.Exception e)
		{
			Debug.LogWarning(e.Message);
		}
	}
}
