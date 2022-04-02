using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

	public float VerticalTurn = 0f;
	public float HorizontalTurn = 0f;
    public float ClockTurn = 0f;
	public float VerticalMove = 0f;
	public float HorizontalMove = 0f;
    public float BackForwardMove = 0f;

	// Position Storage Variables
	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();


	// Use this for initialization
	void Start () 
	{
		
		// Store the starting position & rotation of the object
		posOffset = transform.position;


	}

	// Update is called once per frame
	void Update () {
		

		transform.Rotate(Vector3.right, VerticalTurn * 100 *  Time.deltaTime);
		transform.Rotate(Vector3.up, HorizontalTurn * 100 * Time.deltaTime);
        transform.Rotate(new Vector3(0,0,1), ClockTurn * 100 * Time.deltaTime);

        // Float up/down with a Sin()
        tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI ) * VerticalMove;
		tempPos.x += Mathf.Sin (Time.fixedTime * Mathf.PI ) * HorizontalMove;
        tempPos.z += Mathf.Sin(Time.fixedTime * Mathf.PI) * BackForwardMove;

        transform.position = tempPos;
	

	}
}
