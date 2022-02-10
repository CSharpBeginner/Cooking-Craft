using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ClientGenerator : MonoBehaviour
{
    [SerializeField] private SquareOfSpawn _squareOfSpawn;
    [SerializeField] private List<GameObject> _tables;
    [SerializeField] private Client[] _prefabs;
    [SerializeField] private int _minClients;
    [SerializeField] private int _maxClients;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Generate(_tables);
        _audioSource.Play();
    }

    private void Generate(List<GameObject> tables)
    {
        foreach (GameObject table in tables)
        {
            Generate(table);
        }
    }

    private void Generate(GameObject table)
    {
        FinalContainer[] finalContainers = table.GetComponentsInChildren<FinalContainer>();
        Vector3[] finalPoints = new Vector3[finalContainers.Length];

        for (int i = 0; i < finalPoints.Length; i++)
        {
            finalPoints[i] = finalContainers[i].transform.position;
        }

        for (int i = 0; i < Random.Range(_minClients, _maxClients); i++)
        {
            Client prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            Vector3 position = _squareOfSpawn.GetRandomPosition();
            Client newClient = Instantiate(prefab, position, Quaternion.identity);
            Vector3 finalPoint = finalPoints[Random.Range(0, finalPoints.Length)];
            newClient.SetTarget(finalPoint);
        }
    }
}
