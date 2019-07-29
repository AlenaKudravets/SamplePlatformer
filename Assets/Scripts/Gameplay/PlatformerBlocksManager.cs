using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerBlocksManager : MonoBehaviour
{
    private Vector3 m_startBlockPos;
    [SerializeField] private GameObject m_startBlock;
    //[SerializeField] private List<GameObject> Blocks;
    [SerializeField] private List<string> BlocksNames;
    private Camera m_mainCamera;
    private Vector3 m_spawnPos;
    private float m_cameraWorldWidth;
    private List<PlatformerBlock> m_blockInstances = new List<PlatformerBlock>();

    private void Awake()
    {
        m_mainCamera = Camera.main;
        Vector3 startPoint = m_mainCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 endPoint = m_mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        m_cameraWorldWidth = endPoint.x - startPoint.x;
    }

    void Start()
    {
        m_spawnPos = CalculateFirstBlockSpawnPosition();
        SpawnBlock(m_startBlock);
    }

    void Update()
    {
        if (m_mainCamera.transform.position.x + m_cameraWorldWidth >= m_spawnPos.x)
        {
            SpawnRandomBlock();
        }
        ClearOldPlatforms();
    }

    private void ClearOldPlatforms()
    {
        for (var i = 0; i < m_blockInstances.Count; i++)
        {
            var block = m_blockInstances[i];

            if (m_mainCamera.transform.position.x - m_cameraWorldWidth > block.transform.position.x + block.BlockWidth)
            {
                Destroy(block.gameObject);
                m_blockInstances.RemoveAt(i);
                i--;
            }
            else
            {
                return;
            }
        }
    }

    private GameObject SpawnRandomBlock()
    {
        int blockIndex = Random.Range(0, BlocksNames.Count);
        return SpawnBlock(blockIndex);
    }

    private GameObject SpawnBlock(GameObject blockPrefab)
    {
        GameObject blockInstance = Instantiate(blockPrefab, m_spawnPos, Quaternion.identity);
        PlatformerBlock blockController = blockInstance.GetComponent<PlatformerBlock>();
        m_spawnPos += new Vector3(blockController.BlockWidth, 0, 0);
        m_blockInstances.Add(blockController);
        return blockInstance;
    }

    private GameObject SpawnBlock(int index)
    {
        if (index >= BlocksNames.Count)
        {
            Debug.LogError("Can't spawn block by index " + index);

            return null;
        }

        //var prefab = Blocks[index];
        var prefab = Resources.Load<GameObject>(BlocksNames[index]);

        return SpawnBlock(prefab);
    }

    private Vector3 CalculateFirstBlockSpawnPosition()
    {
        float heightOfCharacterCollider = GameObject.FindGameObjectWithTag("Character").GetComponent<CapsuleCollider2D>().size.y;
        float firstBlockSpawnPointY = PlatformerGameManager.Instance.CharacterSpawnPoint.y - (heightOfCharacterCollider * 2);
        Vector3 firstBlockSpawnPoint = new Vector3(PlatformerGameManager.Instance.CharacterSpawnPoint.x, firstBlockSpawnPointY, 0);
        return firstBlockSpawnPoint;
    }
}
