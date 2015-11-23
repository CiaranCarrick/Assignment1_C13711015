//using UnityEngine;
//using System.Collections;
//
//public class EnemySpawner : Main {
//
//	public GameObject hazard;
//	public Vector3 spawnValues;
//	public int hazardCount;
//	public float spawnWait=1;
//	public float startWait=1;
//	public float waveWait=1;
//	public GameObject Target;
//	void Start ()
//	{
//		StartCoroutine (SpawnWaves ());
//	}
//
//	public IEnumerator SpawnWaves ()
//	{
//		yield return new WaitForSeconds (startWait);
//		while (true)
//		{
//			CreateEnemies();
//			for (int i = 0; i < EnemiesList.Count; i++)
//			{
//				Target = EnemiesList[i].gameObject;
//				if(Target!=null){
//				Enemies enemy = Target.GetComponent<Enemies>();// creates enemies instance that access referance that allows access to methods and variables within _Target
//				Vector3 spawnPosition = new Vector3 (2,2,0);
//				enemy.transform.position=spawnPosition;
//				}
//				yield return new WaitForSeconds (spawnWait);
//			}
//			yield return new WaitForSeconds (1);
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//}
