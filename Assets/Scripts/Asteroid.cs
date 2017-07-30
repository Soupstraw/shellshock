using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public Transform level;

	public GameObject wallPrefab;
	public GameObject shellPrefab;
	public GameObject portalPrefab;
	public GameObject logPrefab;
	public Mineral[] mineralPrefabs;
	public float[] mineralProbabilities;
	[Space]
	public GameObject bigMommaPrefab;
	public int bigMommaCount = 1;
	public GameObject wormPrefab;
	public int wormCount = 2;
	public GameObject flyPrefab;
	public int flyCount = 1;
	public int fliesPerSpawn = 4;
	public GameObject wreckPrefab;
	public int wreckCount = 3;
	[Space]
	public float safeZoneRadius = 10f;
	public int monsterSpawnTries = 1000;
	[Space]
	public int width = 100, height = 100;
	public float maxSize = 100f;
	public float forkingChance = 0.05f;
	public float step = 1f;
	public float sharpness = 10f;
	public float mineralCluster = 10f;
	[Space]
	public int seed = 123456;

	private bool[,] grid;
	private float currentSize = 0f;

	GameData gameData;

	Vector2 size;

	// Use this for initialization
	void Start () {
		gameData = FindObjectOfType<GameData> ();
		size = wallPrefab.GetComponent<BoxCollider2D> ().size;
		Generate ();

		if (gameData.tutorial) {
			Instantiate (logPrefab, TileToWorldPos (new Vector2 (width / 2, height / 2)) + new Vector3 (0, 0, 1), Quaternion.Euler (new Vector3 (0, 0, Random.value * 360f))).transform.parent = level;
			Instantiate (logPrefab, new Vector3 (30, 40, 1), Quaternion.Euler (new Vector3 (0, 0, Random.value * 360f))).transform.parent = level;
			Instantiate (logPrefab, new Vector3 (32, 44, 1), Quaternion.Euler (new Vector3 (0, 0, Random.value * 360f))).transform.parent = level;
			Instantiate (logPrefab, new Vector3 (28, 46.5f, 1), Quaternion.Euler (new Vector3 (0, 0, Random.value * 360f))).transform.parent = level;
			Instantiate (logPrefab, new Vector3 (31.5f, 47f, 1), Quaternion.Euler (new Vector3 (0, 0, Random.value * 360f))).transform.parent = level;
			Instantiate (portalPrefab, new Vector3 (28, 46.5f, 1), Quaternion.Euler(new Vector3(0, 0, Random.value*360f))).transform.parent = level;
			Power pow = GameObject.FindGameObjectWithTag ("Player").GetComponent<Power> ();
			pow.maxPower = 100000f;
			pow.power = 5000f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Generate(){
		if (gameData.tutorial) {
			maxSize = 50f;
			Random.InitState (0);
		} else {
			Random.InitState (seed);
		}

		foreach (Transform t in level) {
			Destroy (t.gameObject);
		}
		grid = new bool[width, height];
		currentSize = 0;

		DoStep (new Vector2 (width/2, height/2), Random.value*360f);

		SpawnMonsters ();
		GenerateShell ();
		CreateWalls ();

		GameObject.FindGameObjectWithTag ("Player").transform.position = TileToWorldPos (new Vector2(width/2, height/2));
		if(!gameData.tutorial && !TrySpawnMonster (portalPrefab)) Generate();
	}

	void GenerateShell(){
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				float dist = (new Vector2 (x - width / 2, y - height / 2)).magnitude;
				if (dist >= width / 2 - 1.5f) {
					grid [x, y] = true;
				}else if(dist >= width / 2 - 3f){
					grid [x, y] = false;
				}
			}
		}
	}

	void ClearArea(Vector2 pos, float radius){
		for (int y = (int)(pos.y - radius); y < (int)(pos.y + radius); y++) {
			for (int x = (int)(pos.x - radius); x < (int)(pos.x + radius); x++) {
				if (y >= 1 && y < height-1 && x >= 1 && x < width-1) {
					grid [x, y] = true;
				}
			}
		}
	}

	void DoStep(Vector2 pos, float heading, int depth = 0){
		//ClearArea (pos, Random.Range (1, 4));
		if(depth > 100){
			Debug.LogWarning ("Max depth reached");
			return;
		}
		if((pos - new Vector2(width/2, height/2)).magnitude >= width/2-3f ){
			for (int i = 0; i < 1000; i++) {
				Vector2 newPos = new Vector2 (Random.Range (0, width), Random.Range (0, height));
				if (grid [(int)newPos.x, (int)newPos.y]) {
					DoStep (newPos, Random.value * 360f, depth + 1);
					return;
				}
			}
		}else{
			ClearArea (pos, 1);
			float rotateBy = (1 - 2 * Random.value) * sharpness;
			Vector2 stepVec = new Vector2 (Mathf.Cos(heading + rotateBy), Mathf.Sin(heading + rotateBy));

			currentSize += step;
			if (currentSize < maxSize) {
				DoStep (pos + stepVec, heading + rotateBy, depth);
			}
		}
	}

	void CreateWalls(){
		Vector2[] seeds = new Vector2[mineralPrefabs.Length];
		for (int i = 0; i < seeds.Length; i++) {
			seeds [i] = Random.insideUnitCircle * 10000f;
		}

		for (int y = 1; y < height-1; y++) {
			for (int x = 1; x < width-1; x++) {
				float dist = (new Vector2 (x - width / 2, y - height / 2)).magnitude;
				if (!grid [x, y]) {
					GameObject prefab;
					if (!gameData.tutorial) {
						prefab = wallPrefab;
					} else {
						prefab = shellPrefab;
					}

					int mineralType = -1;
					float val = -1;
					for (int i = 0; i < seeds.Length; i++) {
						float perlin = Mathf.PerlinNoise(seeds[i].x + x/mineralCluster, seeds[i].y + y/mineralCluster);
						if (val < perlin) {
							val = perlin;
							mineralType = i;
						}
					}
					if (val < mineralProbabilities [mineralType]) {
						prefab = mineralPrefabs[mineralType].gameObject;
					}

					if (dist >= width / 2 - 3f) {
						prefab = shellPrefab;
					}

					GameObject wall = Instantiate (prefab, transform);
					wall.transform.parent = level;
					wall.transform.localPosition = new Vector2 (x*size.x, y*size.y);

					int bitMask = 0;
					bitMask += grid[x,y+1] ? 0: 1;
					bitMask += grid[x-1,y] ? 0: 2;
					bitMask += grid[x+1,y] ? 0: 4;
					bitMask += grid[x,y-1] ? 0: 8;
					wall.GetComponent<Tiling> ().SetBitMask (bitMask);
				}else if (gameData.tutorial && dist > 15 && dist < 17) {
					GameObject wall = Instantiate (mineralPrefabs[0].gameObject, transform);
					wall.transform.parent = level;
					wall.transform.localPosition = new Vector2 (x*size.x, y*size.y);

					int bitMask = 0;
					bitMask += grid[x,y+1] ? 0: 1;
					bitMask += grid[x-1,y] ? 0: 2;
					bitMask += grid[x+1,y] ? 0: 4;
					bitMask += grid[x,y-1] ? 0: 8;
					wall.GetComponent<Tiling> ().SetBitMask (bitMask);
				}
			}
		}
	}

	void SpawnMonsters(){
		for(int i = 0; i < bigMommaCount; i++){
			TrySpawnMonster (bigMommaPrefab);
		}
		for(int i = 0; i < wreckCount; i++){
			TrySpawnMonster (wreckPrefab);
		}
		for(int i = 0; i < wormCount; i++){
			TrySpawnMonster (wormPrefab);
		}
		for(int i = 0; i < flyCount; i++){
			TrySpawnMonster (flyPrefab, fliesPerSpawn);
		}
	}

	bool TrySpawnMonster(GameObject prefab, int count = 1){
		for (int i = 0; i < monsterSpawnTries; i++) {
			int x = Random.Range (0, width), y = Random.Range (0, height);
			Vector2 spawnPos = new Vector2 (x, y);
			float dist = Vector2.Distance (new Vector2 (width / 2, height / 2), spawnPos);
			if (grid [x, y] && dist > safeZoneRadius && dist < width/2 - 5f) {
				ClearArea (spawnPos, 3);
				for(int j = 0; j < count; j++){
					Instantiate(prefab, TileToWorldPos(spawnPos), Quaternion.Euler(new Vector3(0, 0, Random.value * 360f))).transform.parent = level;
				}
				return true;
				break;
			}
		}
		return false;
	}

	public Vector3 TileToWorldPos(Vector2 tile){
		return new Vector3 (tile.x*size.x-transform.position.x, tile.y*size.y-transform.position.y, 0);
	}
}
