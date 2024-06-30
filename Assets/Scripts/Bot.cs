using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.LookAt(_target);

        if (Vector3.Distance(_target.transform.position, transform.position) >= 2.5f)
            transform.position = Vector3.Lerp(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
}
