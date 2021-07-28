using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform obstraclePrefab;
    public Vector2 mapSize;

    [Range(0, 1)]
    public float outlinePercent;
    [Range(0, 1)]
    public float obstraclePercent;

    public int seed = 10;

    List<Coord> allTileCoord;
    Queue<Coord> shuffledTileCroods;
    private Coord mapCentre;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        allTileCoord = new List<Coord>();
        for (int x = 0; x < mapSize.x; ++x)
        {
            for (int y = 0; y < mapSize.y; ++y)
            {
                allTileCoord.Add(new Coord(x, y));
            }
        }
        shuffledTileCroods = new Queue<Coord>(Utility.ShuffleArray<Coord>(allTileCoord.ToArray(), seed));
        mapCentre = new Coord((int)mapSize.x / 2, (int)mapSize.y / 2);

        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; ++x)
        {
            for (int y = 0; y < mapSize.y; ++y)
            {
                Vector3 tileposition = CoordToVector3(x, y);
                Transform newTile = Instantiate(tilePrefab, tileposition, Quaternion.Euler(Vector3.right*90));
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }


        bool[,] obstracleMap = new bool[(int)mapSize.x, (int)mapSize.y];
        int obstracleCount = (int)(mapSize.x * mapSize.y * obstraclePercent);
        int currentObstracleCount = 0;

        for (int i = 0; i < obstracleCount; ++i)
        {
            Coord randomCoord = GetRandomCoord();
            obstracleMap[randomCoord.x, randomCoord.y] = true;
            currentObstracleCount++;

            if (randomCoord != mapCentre && MapIsFullyAccessible(obstracleMap, currentObstracleCount))
            {
                Vector3 obstraclePosition = CoordToVector3(randomCoord.x, randomCoord.y); 
                Transform newObstracle = Instantiate(obstraclePrefab, obstraclePosition + Vector3.up * 0.5f, Quaternion.identity);
                newObstracle.parent = mapHolder;
            }
            else
            {
                obstracleMap[randomCoord.x, randomCoord.y] = false;
                currentObstracleCount --;
            }
        }
    }

    bool MapIsFullyAccessible(bool[,] obstracleMap, int currentObstracleCount)
    {
        // ��¼�õ������Ƿ���
        bool[,] mapFlags = new bool[obstracleMap.GetLength(0), obstracleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        // ���м���������Ϊ��������ĵ�
        queue.Enqueue(mapCentre);
        mapFlags[mapCentre.x, mapCentre.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; ++x)
            {
                for(int y = -1; y <= 1; ++y)
                {
                    int neighbourX = tile.x + x;
                    int neighBourY = tile.y + y;
                    // �����Խ���
                    if (x == 0 || y == 0)
                    {
                        // �ų������������������
                        if (neighbourX >= 0 && neighbourX < obstracleMap.GetLength(0) && neighBourY >= 0 && neighBourY < obstracleMap.GetLength(1))
                        {
                            // ����õ�û�м��������ҿ�������
                            if(!mapFlags[neighbourX, neighBourY] && !obstracleMap[neighbourX, neighBourY])
                            {
                                // ��¼
                                mapFlags[neighbourX, neighBourY] = true;
                                // �������
                                queue.Enqueue(new Coord(neighbourX, neighBourY));
                                // �������ߵ�������һ
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }
        print(accessibleTileCount);
        int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstracleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

    private Vector3 CoordToVector3(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCroods.Dequeue();
        shuffledTileCroods.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord (int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        // ������������ڽṹ����ж�
        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
        
    }
}
