using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{

    public GameObject tile,arrow;
    public Node m_node;

    [Range(0, 0.5f)]
    public float borderSize = 0.15f;

    private Renderer arrowRender;
    public void Init(Node node)
    {
        if (tile != null)
        {
            gameObject.name = "Node" + node.xIndex + "#" + node.yIndex;
            gameObject.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f - borderSize, 1f - borderSize, 1f);
            m_node = node;
            arrowRender = arrow.GetComponent<Renderer>();
            EnableObj(arrow, false);
        }
      
    }

    public void ColorNode(Color color, GameObject go)
    {
        if (go != null)
        {
            var goRenderer = go.GetComponent<Renderer>();
            if (goRenderer != null)
            {
                goRenderer.material.color = color;
            }
        }
    }

    public void ColorNode(Color color)
    {
        ColorNode(color, tile);
    }

    private void EnableObj(GameObject go,bool state)
    {
        if (go!=null)
        {
            go.SetActive(state);
        }
    }

    public void ShowArrow(Color color)
    {
        if (m_node!=null&&arrow!=null&&m_node.previous!=null)
        {
            EnableObj(arrow, true);
            Vector3 dirToPrevious =( m_node.previous.position - m_node.position).normalized;
            arrow.transform.rotation = Quaternion.LookRotation(dirToPrevious);
            arrowRender.material.color = color;
        }
     }

}
