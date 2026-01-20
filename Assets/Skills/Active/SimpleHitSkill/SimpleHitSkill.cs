using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SimpleHitSkill : MonoBehaviour
{
    public Transform firePoint;
    public float range = 20f;
    public LineRenderer line;
    public float lineDuration = 0.05f;

    void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            Cast();
        }
    }

    void Cast()
    {
        Ray camRay = Camera.main.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        if (!Physics.Raycast(camRay, out RaycastHit mouseHit))
            return;

        Vector3 dir = mouseHit.point - firePoint.position;
        dir.y = 0f;
        dir.Normalize();

        Ray ray = new Ray(firePoint.position, dir);
        Vector3 endPoint = firePoint.position + dir * range;

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            endPoint = hit.point;

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeHit();
            }
        }

        StartCoroutine(ShowLine(endPoint));
    }


    IEnumerator ShowLine(Vector3 endPoint)
    {
        line.gameObject.SetActive(true);
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, endPoint);

        yield return new WaitForSeconds(lineDuration);

        line.gameObject.SetActive(false);
    }
}
