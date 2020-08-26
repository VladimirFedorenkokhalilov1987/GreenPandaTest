using System.Collections;
using System.Collections.Generic;
using GreenPandaAssets.Scripts;
using TMPro;
using UnityEngine;

public class TruckControl : MonoBehaviour
{

	public static TruckControl Instance;
	
	[SerializeField] private TextMeshProUGUI miningRatePerMinute;
	
	[SerializeField]
	Transform[] waypoints;
	
	private float _moveSpeed = 22;
	private float _timeLoadTruck = 4;
	private float _coinsMined = 1;

	public float MoveSpeed
	{
		get { return _moveSpeed; }
		set
		{
			_moveSpeed = value;
		}
	}

	public float TimeLoadTruck
	{
		get { return _timeLoadTruck; }
		set
		{
			_timeLoadTruck = value;
		}
	}
	public float CoinsMined
	{
		get { return _coinsMined; }
		set
		{
			_coinsMined = value;
		}
	}
	
	int waypointIndex = 0;

	private float timerTruckGo;
	private bool stayPause;
	
	private void Awake()
	{
		Instance = this;
	}
	
	void Start () {
		transform.position = waypoints [waypointIndex].transform.position;
	}

	void FixedUpdate () {
		if(!stayPause)
		Move ();
		float ratePerMin = (60 / (TimeLoadTruck + (MoveSpeed / 4.4f)))*CoinsMined;
		miningRatePerMinute.text = ratePerMin.ToString("F1") +"/min";
	}

	void Move()
	{
		timerTruckGo += Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position,
												waypoints[waypointIndex].transform.position,
												MoveSpeed * Time.deltaTime);
		transform.LookAt(waypoints[waypointIndex].transform);
		if (transform.position == waypoints [waypointIndex].transform.position) {
			waypointIndex ++;
		}

		if (waypointIndex == 5)
		{
			StartCoroutine(stay());
		}

		if (waypointIndex == waypoints.Length)
		{
			waypointIndex = 0;
			timerTruckGo = 0;
			TopUI.Instance.Coins+=CoinsMined;
			transform.position = waypoints [waypointIndex].transform.position;
		}
	}


	IEnumerator stay()
	{
		stayPause = true;
		yield return new WaitForSeconds(TimeLoadTruck);
		stayPause = false;
		waypointIndex ++;
		transform.position = Vector3.MoveTowards (transform.position,
			waypoints[waypointIndex].transform.position,
			MoveSpeed * Time.deltaTime);
	}
}
