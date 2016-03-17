using LibNoise.Generator;

namespace TerrainGenerator
{
	public class NoiseProvider : INoiseProvider
	{
		private Perlin perlinNoiseGenerator;

		public NoiseProvider()
		{
			perlinNoiseGenerator = new Perlin();
		}

		public float GetValue(float x, float z)
		{
			return (float)(perlinNoiseGenerator.GetValue(x, 0, z) / 2f) + 0.5f;
		}
	}
}