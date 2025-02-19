using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
	struct PointInTime
	{
		public Vector3 position;

		public PointInTime(Vector3 _position)
		{
			position = _position;
		}
	}

	bool isRewinding = false;

	public float recordTime = 5f;

	List<PointInTime> pointsInTime;

	Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
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
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
			transform.position = pointInTime.position;
			pointsInTime.RemoveAt(0);
		}
		else
		{
			StopRewind();
		}

	}

	void Record()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

		pointsInTime.Insert(0, new PointInTime(transform.position));
	}

	public void StartRewind()
	{
		isRewinding = true;
		rb.bodyType = RigidbodyType2D.Kinematic;
	}

	public void StopRewind()
	{
		isRewinding = false;
		rb.bodyType = RigidbodyType2D.Dynamic;
	}
}