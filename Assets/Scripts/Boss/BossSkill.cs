using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BossSkill : MonoBehaviour
{
	public GameObject target;
	public GameObject EnemyPrefebs;
	public GameObject Laser;

	public float angleRange = 60f;
	public float distance = 5f;
	public bool isCollision = false;


	Color _blue = new Color(0f, 0f, 1f, 0.2f);
	Color _red = new Color(1f, 0f, 0f, 0.2f);

	Vector3 direction;

	float dotValue = 0f;

	


	public void pull()
	{

		Debug.Log("1");

		dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
		direction = target.transform.position - transform.position;

		if (direction.magnitude < distance)
		{
			Debug.Log("범위안에 있다.");
			if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
			{
				Debug.Log("코루틴 시작.");
				StartCoroutine(pullAction(target, transform.position,0.5f));
			}
		}
	}

	public void LaserFire()
	{
		Debug.Log("5");

		GameObject laser = Instantiate(Laser);
		laser.transform.position = transform.position;

		laser.transform.LookAt(target.transform);

		

		StartCoroutine(LaserFireAction(laser,target.transform.position,3f));

	}

	public void SpeedDown()
	{
		Debug.Log("6");

		//지정범위내의 모든 플레이어를 찾은 후
		Collider[] colls = Physics.OverlapSphere(transform.position, 5f, 1 << 8);  //8번째 레이어 = Player

		if (colls.Length != 0)
		{
			StartCoroutine(SpeedDownAction(2f, colls));
		}
		else
			Debug.Log("주변의 플레이어가 없습니다.");
		
	}

	public void MonsterSummon()
	{

		Debug.Log("7");


		StartCoroutine(MonsterSummonAction(10,transform.position.x,transform.position.z));
	}

	



	IEnumerator pullAction(GameObject target, Vector3 currentPos,float endtime)
	{
		float time = 0f;
		while(true)
		{
			time += Time.deltaTime;

			if (time >= endtime)
				yield break;


			target.transform.position = Vector3.MoveTowards(target.transform.position, currentPos, 0.05f);

			yield return null;
		}
		
		
	}

	IEnumerator LaserFireAction(GameObject laser,Vector3 playerPos,float endTime)
	{
		float time = 0f;

		while(true)
		{
			time += Time.deltaTime;

			if(time> endTime)
			{
				DestroyObject(laser);
				yield break;
			}

			laser.gameObject.transform.position = Vector3.MoveTowards(laser.gameObject.transform.position, playerPos, 0.3f);

			yield return null;
		}

		
	}

	IEnumerator SpeedDownAction(float endtime, Collider[] colls)
	{
		float time = 0f;

		while (true)
		{

			time += Time.deltaTime;

			if (time >= endtime)
			{
				//지속시간이 끝나면 속도를 원래대로 해줌.
				for (int i = 0; i < colls.Length; i++)
				{
					colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = 8f;
				}
				yield break;
			}


			for (int i = 0; i < colls.Length; i++)
			{
				//float speed= colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed;
				//speed /= 2;
				//colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = speed;

				colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = 2f;
			}

			yield return null;
		}

	}


	IEnumerator MonsterSummonAction(int summonNum , float bossX, float bossZ)
	{

		GameObject[] monsters = new GameObject[summonNum];

		for (int i = 0; i < summonNum; i++)
		{
			float randX = Random.Range(bossX - 5f, bossX + 5f);
			float randZ = Random.Range(bossZ - 5f, bossZ + 5f);
			monsters[i] = Instantiate(EnemyPrefebs);
			monsters[i].transform.position = new Vector3(randX, 0, randZ);
		}

		yield return null;
	}

	// Start is called before the first frame update
	void Start()
    {

		

	}

	// Update is called once per frame
	void Update()
    {
		//디버그 확인용.
		dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
		direction = target.transform.position - transform.position;

		if (direction.magnitude < distance)
		{
			if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
				isCollision = true;
			else
				isCollision = false;
		}
		else
			isCollision = false;

		if (Input.GetKeyDown(KeyCode.Keypad2))
			pull();

		if (Input.GetKeyDown(KeyCode.Keypad5))
			LaserFire();

		if (Input.GetKeyDown(KeyCode.Keypad6))
			SpeedDown();

		if (Input.GetKeyDown(KeyCode.Keypad7))
			MonsterSummon();



	}


	//씬뷰에서 확인용
	private void OnDrawGizmos()
	{
		Handles.color = isCollision ? _red : _blue;
		Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, distance);
		Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);
	}


}
