using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : MonoBehaviour
{
    [SerializeField] private List<Vector3> waypoints;

    [SerializeField] private GameWinScreenController gameWinScreenController;

    [SerializeField] private float duration = 150;

    private Transform _transform;

    void Start()
    {
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        _transform = GetComponent<Transform>();

        if (waypoints.Count < 2)
        {
            Debug.LogError("O caminho precisa de pelo menos dois waypoints!");
            return;
        }

        _transform.DOPath(waypoints.ToArray(), duration , PathType.CatmullRom, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .SetLookAt(0.01f)
            .OnComplete(OnPathComplete); 
    }

    private void OnPathComplete()
    {
        // gameWinScreenController.EnableGameWinScreen();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (waypoints != null)
        {
            foreach (var waypoint in waypoints)
            {
                Gizmos.DrawCube(waypoint, Vector3.one * 0.2f);
            }
        }
    }
}
