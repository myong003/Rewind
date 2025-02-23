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

	public bool hasAfterimage = false;
	float afterimageFrequency = 1;
	float afterimageTimer;

	public TrailRenderer trailRenderer;
	int latestPosition = 0;

	public float recordTime = 5f;

	List<PointInTime> pointsInTime;

	Rigidbody2D rb;

	SpriteRenderer afterimage;
	List<Vector3> positions;

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

		if (hasAfterimage) {
			if (afterimageTimer >= afterimageFrequency) {
				SpawnAfterimage();
				afterimageTimer = 0;
			}
			afterimageTimer += Time.deltaTime;
		}
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
			if (trailRenderer != null && positions.Count > 0) {
				Vector3[] arr = new Vector3[trailRenderer.positionCount];
				trailRenderer.GetPositions(arr);
				for (int i=arr.Length-1; i >= latestPosition; i--) {
					arr[i] = arr[latestPosition - 1];
				}
				trailRenderer.SetPositions(arr);
				positions.RemoveAt(latestPosition - 1);  
			}
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

	void SpawnAfterimage() {
		GameObject afterimage = Instantiate(new GameObject("Afterimage", typeof(SpriteRenderer), typeof(Afterimage)), this.transform.position, this.transform.rotation);
		afterimage.GetComponent<SpriteRenderer>().sprite = this.GetComponentInChildren<SpriteRenderer>().sprite;
		afterimage.transform.localScale = this.transform.localScale;
	}

	public void StartRewind()
	{
		isRewinding = true;
		rb.bodyType = RigidbodyType2D.Kinematic;
		if (trailRenderer != null) {
			trailRenderer.emitting = false;
			Vector3[] arr = new Vector3[trailRenderer.positionCount];
			latestPosition = trailRenderer.positionCount - 1;
			trailRenderer.GetPositions(arr);
			positions = new List<Vector3>(arr);

			// print(positions.Count);
			// foreach (Vector3 position in positions) {
			// 	print(position);
			// }
		}
	}

	public void StopRewind()
	{
		isRewinding = false;
		rb.bodyType = RigidbodyType2D.Dynamic;
		if (trailRenderer != null) {
			trailRenderer.emitting = true;
			positions.Clear();
		}
	}
}