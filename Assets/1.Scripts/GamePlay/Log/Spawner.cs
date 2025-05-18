using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _logPrefab;
    [SerializeField] private GameObject _logBranchPrefab;
    [SerializeField] private Transform _logParent;


    [Header("Settings")]
    [SerializeField] private int _startingLogAmount = 20;
    [SerializeField] private float _branchLogSpawnRate = 0.45f;


    [Header("Logs")]
    [SerializeField] private List<GameObject> _logs = new List<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < _startingLogAmount; i++)
        {
            SpawnLogOnTop();
        }

        PlayerHitting.OnPlayerHitting += ShiftLogsDown;
        Transition.OnFadeTransitionFinished += DestroyLogs;
    }
    private void OnDisable()
    {
        PlayerHitting.OnPlayerHitting -= ShiftLogsDown;
        Transition.OnFadeTransitionFinished -= DestroyLogs;
    }
    private void DestroyLogs()
    {
        foreach (var log in _logs)
        {
            if (log != null) Destroy(log);
        }
        _logs.Clear();
        for(int i = 0; i < _startingLogAmount; i++)
        {
            SpawnLogOnTop();
        }
    }
    private void SpawnLog()
    {
        Vector3 spawnPos;

        if (_logs.Count > 0)
        {
            float logHeight = _logs[_logs.Count - 1].GetComponent<Renderer>().bounds.size.y;
            spawnPos = _logs[_logs.Count - 1].transform.position + new Vector3(0, logHeight, 0);
        }
        else
            spawnPos = _logParent.position;

        GameObject newLog = Instantiate(_logPrefab, spawnPos, Quaternion.identity, _logParent);
        _logs.Add(newLog);
    }

    private void ShiftLogsDown()
    {
        if (_logs.Count == 0) return;

        GameObject bottomLog = _logs[0];
        _logs.RemoveAt(0);
        Destroy(bottomLog);

        Invoke(nameof(RePosLog), 0.1f);
    }
    private void RePosLog()
    {
        if (_logs.Count == 0) return;

        float logHeight = _logs[0].GetComponent<Renderer>().bounds.size.y;

        for (int i = 0; i < _logs.Count; i++)
        {
            Vector3 newPos = _logs[i].transform.position - new Vector3(0, logHeight, 0);
            _logs[i].transform.position = newPos;
        }

        SpawnLogOnTop();
    }

    private void SpawnLogOnTop()
    {
        if (_logs.Count == 0)
        {
            SpawnLog();
            return;
        }

        float logHeight = _logs[0].GetComponent<Renderer>().bounds.size.y;
        Vector3 topPos = _logs[_logs.Count - 1].transform.position + new Vector3(0, logHeight, 0);

        // %30 ihtimalle branchli log gelsin
        float chance = Random.value;
        GameObject prefabToSpawn;

        if (chance <= _branchLogSpawnRate)
        {
            prefabToSpawn = _logBranchPrefab;

            // Sağa veya sola rastgele çevir (y ekseninde 180 derece flip)
            float rotY = Random.value < 0.5f ? 0f : 180f;
            GameObject branchLog = Instantiate(prefabToSpawn, topPos, Quaternion.Euler(0, rotY, 0), _logParent);
            _logs.Add(branchLog);
        }
        else
        {
            prefabToSpawn = _logPrefab;
            GameObject plainLog = Instantiate(prefabToSpawn, topPos, Quaternion.identity, _logParent);
            _logs.Add(plainLog);
        }
    }
}
