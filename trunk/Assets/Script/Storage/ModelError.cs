using UnityEngine;
using System;
using System.Collections;

public class ModelError {

	public PlayerState currentState;
	public PlayerState lastState;

	public ErrorCode code;
	public string description;
	public DateTime time;

}

public enum ErrorCode {

}