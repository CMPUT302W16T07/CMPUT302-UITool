using UnityEngine;
using System.Collections;
using TerrainGenerator;

public class TerrainPiece : MonoBehaviour {

	// Each terrain piece is defined by an X and Z position, a settings object, and Unity Terrain object. Also a noiseProvider object from the LibNoise library

	// Initialize and set X position
	public int X {
		get;
		private set;
	}

	// Initialize and set Z position
	public int Z {
		get;
		private set;
	}

	private float[,] heightmap { 
		get;
		set;
	}

	// Initialize and set terrain object
	private Terrain terrain {
		get;
		set;
	}

	private TerrainData Data {
		get;
		set;
	}

	// Initialize and set TerrainePieceSettings object
	private TerrainPieceSettings terrainPieceSettings {
		get; 
		set;
	}

	public Vector2i Position { 
		get; 
		private set; 
	}

	// Initialize and set a NoiseProvider objet
	private NoiseProvider NoiseProvider {
		get;
		set;
	}

	public TerrainPiece(TerrainPieceSettings settings, NoiseProvider noiseProvider, int x, int z)
	{

		terrainPieceSettings = settings;
		NoiseProvider = noiseProvider;

		Position = new Vector2i(x, z);
	}

	public void GenerateTerrain() {
		Data = new TerrainData();
		Data.heightmapResolution = terrainPieceSettings.HeightMapResolution;
		Data.alphamapResolution = terrainPieceSettings.AlphaMapResolution;
		Data.SetHeights(0, 0, heightmap);

		Data.size = new Vector3(terrainPieceSettings.Length, terrainPieceSettings.Height, terrainPieceSettings.Length);
		var newTerrainGameObject = Terrain.CreateTerrainGameObject(Data);
		newTerrainGameObject.transform.position = new Vector3(Position.X * terrainPieceSettings.Length, 0, Position.Z * terrainPieceSettings.Length);

		terrain = newTerrainGameObject.GetComponent<Terrain>();
		terrain.Flush();


	}

	object GetHeightMap () {
		var heightmap = new float[terrainPieceSettings.HeightMapResolution, terrainPieceSettings.HeightMapResolution];

		for (var zRes = 0; zRes < terrainPieceSettings.HeightMapResolution; zRes++)
		{
			for (var xRes = 0; xRes < terrainPieceSettings.HeightMapResolution; xRes++)
			{
				var xCoordinate = X + (float)xRes / (terrainPieceSettings.HeightMapResolution - 1);
				var zCoordinate = Z + (float)zRes / (terrainPieceSettings.HeightMapResolution - 1);

				heightmap[zRes, xRes] = NoiseProvider.GetValue(xCoordinate, zCoordinate);
			}
		}
		return heightmap;
	}
		
	// Use this for initialization
	void Start () {
		Update ();
		Test ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Test()
	{
		var settings = new TerrainPieceSettings(129, 129, 100, 20);
		var noiseProvider = new NoiseProvider();
		var terrain = new TerrainPiece(settings, noiseProvider, 0, 0);
		terrain.GenerateTerrain();
	}


}
