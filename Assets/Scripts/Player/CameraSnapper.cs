using System.Collections;
using UnityEngine;

public class CameraSnapper : MonoBehaviour
{
    [SerializeField] Transform playerHead;
    Coroutine currentFlyCoroutine;
    Vector3 originalPosition;
    Quaternion originalRotation;
    const float MaxStepDistance = 0.1f;
    const float MinSnapDistance = 0.15f;
    void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        GameManager.OnGameStart.AddListener(FlyCameraToPlayer);
        GameManager.OnGameOver.AddListener(_ => FlyCameraToBase());
    }
    public void FlyCameraToPlayer()
    {
        if (currentFlyCoroutine != null)
            StopCoroutine(currentFlyCoroutine);
        currentFlyCoroutine = StartCoroutine(FlyCam(playerHead));
    }
    public void FlyCameraToBase()
    {
        if (currentFlyCoroutine != null)
            StopCoroutine(currentFlyCoroutine);
        currentFlyCoroutine = StartCoroutine(FlyCamToBase());
    }
    IEnumerator FlyCam(Transform newParent)
    {
        while (Vector3.Distance(transform.position, newParent.position) > MinSnapDistance)
        {
            if (newParent == null)
            {
                currentFlyCoroutine = null;
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position, newParent.position, MaxStepDistance);
            transform.rotation = Quaternion.LookRotation(newParent.position - transform.position);
            yield return null;
        }
        transform.SetParent(newParent);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        currentFlyCoroutine = null;
    }
    IEnumerator FlyCamToBase()
    {
        transform.SetParent(null);
        while (Vector3.Distance(transform.position, originalPosition) > MinSnapDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, MaxStepDistance);
            transform.rotation = Quaternion.LookRotation(transform.position - originalPosition);
            yield return null;
        }
        transform.SetPositionAndRotation(originalPosition, originalRotation);
        currentFlyCoroutine = null;
    }
}