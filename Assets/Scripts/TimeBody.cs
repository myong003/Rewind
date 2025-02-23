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


	public float recordTime = 5f;

	bool isRewinding = false;

	List<PointInTime> pointsInTime;
	Rigidbody2D rb;

	SpriteRenderer afterimage;
	public bool hasAfterimage = false;
	float afterimageFrequency = 1;
	float afterimageTimer;
	List<Vector3> positions;

	bool isPlayer = false;
	EntityCombat playerCombat;
	AudioSource rewindSound;

	public TrailRenderer trailRenderer;
	int latestPosition = 0;


	// Use this for initialization
	void Start()
	{
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody2D>();
		if (this.gameObject.tag == "Player") {
			playerCombat = GetComponent<EntityCombat>();
			rewindSound = GetComponent<AudioSource>();
			isPlayer = true;
		}
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
		}

		if (isPlayer) {
			playerCombat.isInvincible = true;
			rewindSound.Play();
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

		if (isPlayer) {
			playerCombat.isInvincible = false;
		}
	}
}