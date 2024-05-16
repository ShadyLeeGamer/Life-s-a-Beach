using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlast : MonoBehaviour
{
    [SerializeField] LayerMask hitLayerMask;
    [SerializeField] float lifespan;

    LineRenderer blastRenderer;

    private void Awake()
    {
        blastRenderer = GetComponent<LineRenderer>();
        blastRenderer.positionCount = 2;
    }

    public void Init(Vector2 targetPos)
    {
        // Hit
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, targetPos, hitLayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
        }

        // Visual
        blastRenderer.enabled = true;
        blastRenderer.SetPosition(0, transform.position);
        blastRenderer.SetPosition(1, targetPos);

        Destroy(gameObject, lifespan);
    }
}
