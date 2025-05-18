using UnityEngine;

public class LogBreakingAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _logPrefab;
    [SerializeField] private Transform _logParent;
    [SerializeField] private Transform _playerTransform;


    [Header("Settings")]
    [SerializeField] private float _animDuration = 0.4f;
    [SerializeField] private Vector3 _breakingPos;
    [SerializeField] private Vector3 _breakingRot;

    private void Awake()
    {
        _logPrefab.GetComponent<BoxCollider2D>().enabled = false;
        PlayerHitting.OnPlayerHitting += AnimateLog;
    }
    private void OnDisable() => PlayerHitting.OnPlayerHitting -= AnimateLog;

    private void AnimateLog()
    {
        GameObject log = Instantiate(_logPrefab, _logParent.position, Quaternion.identity, _logParent);

        int direction = _playerTransform.position.x < 0 ? -1 : 1;

        Vector3 targetPos = new Vector3(_breakingPos.x * direction, _breakingPos.y, _breakingPos.z);
        Vector3 targetRot = new Vector3(_breakingRot.x * direction, _breakingRot.y * direction, _breakingRot.z * direction);

        LeanTween.move(log, targetPos, _animDuration);
        LeanTween.rotate(log, targetRot, _animDuration);
        LeanTween.alpha(log, 0, _animDuration).setOnComplete(() => { Destroy(log); });
    }
}
