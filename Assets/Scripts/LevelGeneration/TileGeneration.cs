using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TerrainType
{
    public string name;
    public float threshold;
    public Color color;
    public int index;

}

[System.Serializable]
public class Biome
{
    public string name;
    public Color color;
}
[System.Serializable]
public class BiomeRow
{
    public Biome[] biomes;
}

public class TileData
{
    public float[,] heightMap;
    public float[,] heatMap;
    public float[,] moistureMap;
    public TerrainType[,] chosenHeightTerrainTypes;
    public TerrainType[,] chosenHeatTerrainTypes;
    public TerrainType[,] chosenMoistureTerrainTypes;
    public Biome[,] chosenBiomes;
    public Mesh mesh;

    public TileData(float[,] heightMap, float[,] heatMap, float[,] moistureMap, TerrainType[,] chosenHeightTerrainTypes,
        TerrainType[,] chosenHeatTerrainTypes,
        TerrainType[,] chosenMoistureTerrainTypes, Biome[,] chosenBiomes, Mesh mesh)
    {
        this.heightMap = heightMap;
        this.heatMap = heatMap;
        this.moistureMap = moistureMap;
        this.chosenHeightTerrainTypes = chosenHeightTerrainTypes;
        this.chosenHeatTerrainTypes = chosenHeatTerrainTypes;
        this.chosenMoistureTerrainTypes = chosenMoistureTerrainTypes;
        this.chosenBiomes = chosenBiomes;
        this.mesh = mesh;
    }
}


enum VisualizationMode { height, heat, moisture, biome}
public class TileGeneration : MonoBehaviour
{
    [SerializeField]
    private float levelScale;
    [Header("Height Control")]
    [SerializeField]
    private TerrainType[] heightTerrainTypes;
    [SerializeField]
    private Wave[] heightWaves;
    [SerializeField]
    private AnimationCurve heightCurve;
    [SerializeField]
    private float heightMultiplier;
    [Header("Heat Control")]
    [SerializeField]
    private TerrainType[] heatTerrainTypes;
    [SerializeField]
    private Wave[] heatWaves;
    [SerializeField]
    private AnimationCurve heatCurve;
    [Header("Moisture Control")]
    [SerializeField]
    private TerrainType[] moistureTerrainTypes;
    [SerializeField]
    private Wave[] moistureWaves;
    [SerializeField]
    private AnimationCurve moistureCurve;
    [SerializeField]
    NoiseMapGeneration noiseMapGeneration;
    [Header("Biome Controls")]
    [SerializeField]
    private BiomeRow[] biomes;
    [SerializeField]
    private Color waterColor;
    [SerializeField]
    private MeshRenderer tileRenderer;
    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private MeshCollider meshCollider;
   
    [SerializeField]
    private VisualizationMode visualizationMode;

    private void UpdateMeshVerticies(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Vector3[] meshVerticies = this.meshFilter.mesh.vertices;

        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                float height = heightMap[zIndex, xIndex];
                Vector3 vertex = meshVerticies[vertexIndex];
                meshVerticies[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z); ;
                vertexIndex++;
            }
        }
        this.meshFilter.mesh.vertices = meshVerticies;
        this.meshFilter.mesh.RecalculateBounds();
        this.meshFilter.mesh.RecalculateNormals();

        this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }


    public void GenerateTile(float centerVertexZ, float maxDistanceZ)
    {
        Vector3[] meshVerticies = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVerticies.Length);
        int tileWidth = tileDepth;

        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;

        float[,] heightMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.heightWaves);

        Vector3 tileDimensions = this.meshFilter.mesh.bounds.size;
        float distanceBetweenVertecies = tileDimensions.z / (float)tileDepth;
        float vertexOffsetZ = this.gameObject.transform.position.z / distanceBetweenVertecies;

        float[,] uniformHeatMap = this.noiseMapGeneration.GenerateUniformNoiseMap(tileDepth, tileWidth, centerVertexZ, maxDistanceZ, vertexOffsetZ);
        float[,] randomHeatMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.heatWaves);
        float[,] heatMap = new float[tileDepth, tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                heatMap[zIndex, xIndex] = uniformHeatMap[zIndex, xIndex] * randomHeatMap[zIndex, xIndex];
                heatMap[zIndex, xIndex] += this.heatCurve.Evaluate(heightMap [zIndex, xIndex]) * heightMap[zIndex, xIndex];
            }
        }

        float[,] moistureMap = this.noiseMapGeneration.GeneratePerlinNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.moistureWaves);
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                moistureMap[zIndex, xIndex] -= this.moistureCurve.Evaluate(heightMap[zIndex, xIndex]) * heightMap[zIndex, xIndex];
            }
        }

        TerrainType[,] chosenHeightTerrainTypes = new TerrainType[tileDepth, tileWidth];
        Texture2D heightTexture = BuildTexture(heightMap, this.heightTerrainTypes, chosenHeightTerrainTypes);

        TerrainType[,] chosenHeatTerrainTypes = new TerrainType[tileDepth, tileWidth];
        Texture2D heatTexture = BuildTexture(heatMap, this.heatTerrainTypes, chosenHeatTerrainTypes);

        TerrainType[,] chosenMoistureTerrainTypes = new TerrainType[tileDepth, tileWidth];
        Texture2D moistureTexture = BuildTexture(moistureMap, this.moistureTerrainTypes, chosenMoistureTerrainTypes);

        Biome[,] chosenBiomes = new Biome[tileDepth, tileWidth];
        Texture2D biomeTexture = BuildBiomeTextures(chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes, chosenBiomes);



        switch (this.visualizationMode)
        {
            case VisualizationMode.height:
                this.tileRenderer.material.mainTexture = heightTexture;
                break;
            case VisualizationMode.heat:
                this.tileRenderer.material.mainTexture = heatTexture;
                break;
            case VisualizationMode.moisture:
                this.tileRenderer.material.mainTexture = moistureTexture;
                break;
            case VisualizationMode.biome:
                this.tileRenderer.material.mainTexture = biomeTexture;
                break;
        }
        UpdateMeshVerticies(heightMap);
    }

    private Texture2D BuildTexture(float[,] heightMap, TerrainType[] terrainTypes, TerrainType[,] chosenTerrainTypes)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Color[] colorMap = new Color[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;
                float height = heightMap[zIndex, xIndex];
                TerrainType terrainType = ChooseTerrainType(height, terrainTypes);

                colorMap[colorIndex] = terrainType.color;
                chosenTerrainTypes[zIndex, xIndex] = terrainType;
            }
        }
        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }

    private Texture2D BuildBiomeTextures(TerrainType[,] heightTerrainTypes, TerrainType[,] heatTerrainTypes, TerrainType[,] moistureTerrainTypes, Biome[,] chosenBiomes)
    {
        int tileDepth = heatTerrainTypes.GetLength(0);
        int tileWidth = heatTerrainTypes.GetLength(1);

        Color[] colorMap = new Color[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;
                TerrainType heightTerrainType = heightTerrainTypes[zIndex, xIndex];
                if (heightTerrainType.name != "water".ToLower())
                {
                    TerrainType heatTerrainType = heatTerrainTypes[zIndex, xIndex];
                    TerrainType moistureTerrainType = moistureTerrainTypes[zIndex, xIndex];

                    Biome biome = this.biomes[moistureTerrainType.index].biomes [heatTerrainType.index];
                    colorMap[colorIndex] = biome.color;
                }
                else
                {
                    colorMap[colorIndex] = this.waterColor;
                }
            }
        }
        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.filterMode = FilterMode.Point;
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }

    private TerrainType ChooseTerrainType(float noise, TerrainType[] terrainTypes)
    {
        foreach (TerrainType terrainType in terrainTypes)
        {
            if (noise < terrainType.threshold)
            {
                return terrainType;
            }
        }
        return terrainTypes[terrainTypes.Length - 1];
    }
}
