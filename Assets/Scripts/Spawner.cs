using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private float spawnRate = 2f;
    private float minHeight = -1f;
    private float maxHeight = 2f;
    private float minimumSpawnRate = 0.2f; // Giá trị spawnRate tối thiểu là 1
    private float decreaseInterval = 5f; // Khoảng thời gian giảm spawnRate

    private void Start()
    {
        StartCoroutine(AdjustSpawnRate());
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate); // Bắt đầu tạo "pipe" ngay lập tức
    }

    private IEnumerator AdjustSpawnRate()
    {
        while (spawnRate > minimumSpawnRate)
        {
            yield return new WaitForSeconds(decreaseInterval); // Đợi một khoảng thời gian nhất định
            spawnRate = Mathf.Max(minimumSpawnRate, spawnRate - 0.2f);
            CancelInvoke(nameof(Spawn)); // Hủy lệnh tạo "pipe" cũ
            InvokeRepeating(nameof(Spawn), spawnRate, spawnRate); // Tạo "pipe" mới với spawnRate đã được điều chỉnh
            Debug.Log("Spawn Rate is now: " + spawnRate); // In ra giá trị spawnRate hiện tại
        }
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * UnityEngine.Random.Range(minHeight, maxHeight);
        Debug.Log("Pipe spawned at: " + pipes.transform.position); // In ra vị trí của "pipe" mới
    }
}
