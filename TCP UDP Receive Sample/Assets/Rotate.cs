using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	void Update () {
		transform.Rotate(new Vector3(0f, 90f * Time.deltaTime, 0f));
	}
}
