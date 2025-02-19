using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
	private bool isRewinding = false;

	public float recordTime = 3f;

	private PointInTime[] pointsInTime = null;

	private int recordingPosition = 0;
	private int currentRewindPosition = 0;
	private int startingRewindPosition = 0;
	private float birthTime;
	private float timeSinceBirth;

	Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		pointsInTime = new PointInTime[(int)Mathf.Round(recordTime / Time.fixedDeltaTime)];
		rb = GetComponent<Rigidbody2D>();
		birthTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		timeSinceBirth = Time.time - birthTime;

		if (Input.GetKeyDown(KeyCode.R))
			StartRewind();
		if (Input.GetKeyUp(KeyCode.R))
			StopRewind();
	}

	void FixedUpdate()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind()
	{
		if (currentRewindPosition < 0)
		{ 
			currentRewindPosition = pointsInTime.Length - 1;
		}

		if (pointsInTime[currentRewindPosition] != null)
		{
			PointInTime pointInTime = pointsInTime[currentRewindPosition];
			transform.position = pointInTime.position;
			pointsInTime[currentRewindPosition] = null;
			currentRewindPosition--;
		}
		else if (timeSinceBirth <= recordTime)
		{
			//Object was spawned less than RecordTime seconds ago
			//Destroy Object
			Destroy(this.gameObject);
		}
		else
		{
			StopRewind();
		}
	}

	void Record()
	{
		if (recordingPosition >= pointsInTime.Length)
		{
			recordingPosition = 0;
		}

		// This is for manual Garbage Collection
		if (pointsInTime[recordingPosition] != null)
		{
			pointsInTime[recordingPosition] = null;
		}

		pointsInTime[recordingPosition] = new PointInTime(transform.position);

		startingRewindPosition = recordingPosition;
		recordingPosition += 1;
	}

	public void StartRewind()
	{
		currentRewindPosition = startingRewindPosition;
		isRewinding = true;
		rb.bodyType = RigidbodyType2D.Kinematic;
	}

	public void StopRewind()
	{
		startingRewindPosition = recordingPosition;
		currentRewindPosition = -1;
		isRewinding = false;
		rb.bodyType = RigidbodyType2D.Dynamic;
	}
}