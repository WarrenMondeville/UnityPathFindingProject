using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PathFinder : MonoBehaviour
{

    Node m_startNode;
    Node m_goalNode;
    Graph m_graph;
    GraphView m_graphView;

    PriorityQueue<Node> m_frontierNodes;
    List<Node> m_exploreNodes;
    List<Node> m_pathNodes;

    public Color startColor = Color.green;
    public Color goalColor = Color.red;
    public Color frontierColor = Color.magenta;
    public Color exploredColor = Color.gray;
    public Color pathColor = Color.cyan;
    public Color arrowColor = new Color32(216, 216, 216, 255);
    public Color highlightColor = new Color32(255, 255, 128, 255);

    public bool showIterations = true;
    public bool showColors = true;
    public bool showArrows = true;
    public bool exitOnGoal = true;


    public bool isComplete = false;

    int m_itserations = 0;

    public enum Mode
    {
        BreadthFirstSerch = 0,
        Dijkstra = 1,

    }
    public Mode mode = Mode.BreadthFirstSerch;
    public void Init(Graph graph, GraphView graphView, Node start, Node goal)
    {
        if (start == null || goal == null || graph == null || graphView == null)
        {
            Debug.LogWarning("PATHFINDER Init error: missing component(s)!");
            return;
        }

        if (start.nodeType == NodeType.Blocked || goal.nodeType == NodeType.Blocked)
        {
            Debug.LogWarning("PATHFINDER Init error: start and goal nodes must be unbloacked!");
            return;
        }

        m_graph = graph;
        m_graphView = graphView;
        m_startNode = start;
        m_goalNode = goal;
        ShowColors(graphView, start, goal);

        m_frontierNodes = new PriorityQueue<Node>();
        m_frontierNodes.Enqueue(start);
        m_exploreNodes = new List<Node>();
        m_pathNodes = new List<Node>();
        for (int x = 0; x < m_graph.Width; x++)
        {
            for (int y = 0; y < m_graph.Height; y++)
            {
                m_graph.nodes[x, y].Reset();
            }
        }
        isComplete = false;
        m_itserations = 0;
        m_startNode.distanceTraveled = 0;
    }


    private void ShowColors()
    {
        ShowColors(m_graphView, m_startNode, m_goalNode);
    }

    private void ShowColors(GraphView graphView, Node start, Node goal)
    {

        if (start == null || goal == null || graphView == null)
        {
            Debug.LogWarning("PATHFINDER Init error: missing component(s)!");
            return;
        }

        if (m_frontierNodes != null)
        {
            graphView.ColorNodes(m_frontierNodes.ToList(), frontierColor);
        }

        if (m_exploreNodes != null)
        {
            graphView.ColorNodes(m_exploreNodes, exploredColor);
        }

        if (m_pathNodes != null && m_pathNodes.Count > 0)
        {
            graphView.ColorNodes(m_pathNodes, pathColor);
            graphView.ShowNodeArrows(m_pathNodes, highlightColor);
        }

        NodeView startNodeView = graphView.nodeViews[start.xIndex, start.yIndex];

        if (startNodeView != null)
        {
            startNodeView.ColorNode(startColor);
        }

        NodeView goalNodeView = graphView.nodeViews[goal.xIndex, goal.yIndex];
        if (goalNodeView != null)
        {
            goalNodeView.ColorNode(goalColor);
        }
    }

    public IEnumerator SearchRoutine(float timeStep = 0.1f)
    {
        float timeStart = Time.time;
        yield return null;

        while (!isComplete)
        {
            if (m_frontierNodes.Count > 0)
            {
                Node currentNode = m_frontierNodes.Dequeue();
                m_itserations++;
                if (!m_exploreNodes.Contains(currentNode))
                {
                    m_exploreNodes.Add(currentNode);
                }
                switch (mode)
                {
                    case Mode.BreadthFirstSerch:
                        ExpandFrontierBreadFirst(currentNode);
                        break;
                    case Mode.Dijkstra:
                        ExpandFrontierDijkstra(currentNode);
                        break;
                    default:
                        break;
                }


                if (m_frontierNodes.Contains(m_goalNode))
                {
                    m_pathNodes = GetPathNodes(m_goalNode);
                    if (exitOnGoal)
                    {
                        isComplete = true;
                        Debug.Log("PATHFINDER mode: "+mode.ToString()+"  path lenth ="+m_goalNode.distanceTraveled.ToString());
                    }

                }
                if (showIterations)
                {
                    ShowDiagnost();
                    yield return new WaitForSeconds(timeStep);
                }

            }
            else
            {
                isComplete = true;
            }
        }
        ShowDiagnost();
        Debug.Log("PATHFINDER SerchRoutine: elapse time =" + (Time.time - timeStart).ToString() + " seconds");
    }

    private void ShowDiagnost()
    {
        if (showColors)
        {
            ShowColors();
        }

        if (m_graphView != null && showArrows)
        {
            m_graphView.ShowNodeArrows(m_frontierNodes.ToList(), arrowColor);

        }
    }

    void ExpandFrontierBreadFirst(Node node)
    {
        if (node != null)
        {
            for (int i = 0; i < node.neighbors.Count; i++)
            {
                if (!m_exploreNodes.Contains(node.neighbors[i]) && !m_frontierNodes.Contains(node.neighbors[i]))
                {
                    float distanceToNeighbor = m_graph.GetNodeDistance(node, node.neighbors[i]);
                    float newDistanceTraveled = distanceToNeighbor + node.distanceTraveled;
                    node.neighbors[i].distanceTraveled = newDistanceTraveled;

                    node.neighbors[i].previous = node;
                     node.neighbors[i].priority = (int)node.neighbors[i].distanceTraveled;
                    //node.neighbors[i].priority = m_exploreNodes.Count;
                    m_frontierNodes.Enqueue(node.neighbors[i]);
                }
            }
        }
    }

    void ExpandFrontierDijkstra(Node node)
    {
        if (node != null)
        {
            for (int i = 0; i < node.neighbors.Count; i++)
            {
                if (!m_exploreNodes.Contains(node.neighbors[i]))
                {
                    float distanceToNeighbor = m_graph.GetNodeDistance(node, node.neighbors[i]);
                    float newDistanceTraveled = distanceToNeighbor + node.distanceTraveled;

                    if (float.IsPositiveInfinity(node.neighbors[i].distanceTraveled)
                        || newDistanceTraveled < node.neighbors[i].distanceTraveled)
                    {
                        node.neighbors[i].previous = node;
                        node.neighbors[i].distanceTraveled = newDistanceTraveled;
                    }
                    if (!m_frontierNodes.Contains(node.neighbors[i]))
                    {
                        node.neighbors[i].priority = node.neighbors[i].distanceTraveled;
                        m_frontierNodes.Enqueue(node.neighbors[i]);
                    }
                }
            }
        }
    }


    List<Node> GetPathNodes(Node endNode)
    {
        List<Node> path = new List<Node>();

        if (endNode == null)
        {
            return path;
        }
        path.Add(endNode);
        var currentNode = endNode.previous;
        while (currentNode != null)
        {
            path.Insert(0, currentNode);
            currentNode = currentNode.previous;
        }
        return path;
    }
}
