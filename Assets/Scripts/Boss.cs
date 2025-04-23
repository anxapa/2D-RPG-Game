using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Attributes")]
    [SerializeField] private float[] orcballSpeed = { 2.5f, -2.5f };
    [SerializeField] private float _distance = 1f;
    [SerializeField] private Transform[] _orcball;

    private void Update()
    {
        for (int i = 0; i < _orcball.Length; i++)
        {
            _orcball[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * orcballSpeed[i]) * _distance,
                Mathf.Sin(Time.time * orcballSpeed[i]) * _distance, 0);
        }
    }
}
