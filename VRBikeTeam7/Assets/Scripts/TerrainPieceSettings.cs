using UnityEngine;
using System.Collections;

public class TerrainPieceSettings : MonoBehaviour {

	// HeightMap and AlphaMap define how accurately the terrain will mesh in between terrain pieces. 
	// AlphaMap may not be needed.

	// initialize and set HeightMap
	public int HeightMapResolution {
		get;
		private set;
	}

	// initialize and set AlphaMap? May not be needed for the scope of this project
	public int AlphaMapResolution {
		get;
		private set;
	}

	// initialize and set length of map
	// the length defines where the borders of the map are for each seperate TerrainPiece. Defined in Unity units
	public int Length {
		get;
		private set;
	}

	// initialize and set height of map
	// the height definies the max height for a TerrainPiece. Defined in Unity units
	public int Height {
		get;
		private set;
	}


	public TerrainPieceSettings(int heightmapResolution, int alphamapResolution, int length, int height)
	{
		HeightMapResolution = heightmapResolution;
		AlphaMapResolution = alphamapResolution;
		Length = length;
		Height = height;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
