using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{

    public GameObject nodeViewPrefab;
    public NodeView[,] nodeViews;

    public Color baseColor = Color.white;
    public Color wallColor = Color.black;

    public void Init(Graph graph)
    {
        if (graph == null)
        {
            Debug.LogWarning("GraphView No graph to initialize!");
            return;
        }
        nodeViews = new NodeView[graph.Width, graph.Height];

        foreach (var n in graph.nodes)
        {
            //Debug.Log(n.position);
            GameObject instance = Instantiate<GameObject>(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();

            if (nodeView != null)
            {
                nodeView.Init(n);
                nodeViews[n.xIndex, n.yIndex] = nodeView;
                if (n.nodeType == NodeType.Blocked)
                {
                    nodeView.ColorNode(wallColor);
                }
                else
                {
                    nodeView.ColorNode(baseColor);
                }

            }
        }
    }


    public void ColorNodes(List<Node> nodes, Color color)
    {
        foreach (var n in nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
                nodeView.ColorNode(color);
            }
        }
    }

    internal void ColorNodes(Queue<Node> nodes, Color color)
    {
        foreach (var n in nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
                nodeView.ColorNode(color);
            }
        }
    }


    public void ShowNodeArrows(Node node, Color color)
    {
        if (node != null)
        {
            NodeView nodeView = nodeViews[node.xIndex, node.yIndex];
            if (nodeView != null)
            {
                nodeView.ShowArrow(color);
            }
        }
    }


    public void ShowNodeArrows(List<Node> nodes,Color color)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            ShowNodeArrows(nodes[i],color);
        }
    }

    public void ShowNodeArrows(Queue<Node> nodes,Color color)
    {
        foreach (var n in nodes)
        {
            if (n != null)
            {
                ShowNodeArrows(n,color);
            }
        }
    }
}
