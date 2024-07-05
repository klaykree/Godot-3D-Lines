using Godot;
using System;
using System.Collections.Generic;

public partial class LineDrawer : Node2D
{
    private class Line
    {
        public Vector3 Start;
        public Vector3 End;
        public Color LineColor;
        public float Time;

        public Line(Vector3 start, Vector3 end, Color lineColor, float time)
        {
            Start = start;
            End = end;
            LineColor = lineColor;
            Time = time;
        }
    }

    private List<Line> Lines = new List<Line>();
    private bool RemovedLine = false;

    public override void _Process(double delta)
    {
        for (int i = 0; i < Lines.Count; i++)
        {
            Lines[i].Time -= (float)delta;
        }

        if (Lines.Count > 0 || RemovedLine)
        {
            QueueRedraw(); // Calls _Draw
            RemovedLine = false;
        }
    }

    public override void _Draw()
    {
        Camera3D cam = GetViewport().GetCamera3D();
        for (int i = 0; i < Lines.Count; i++)
        {
            Vector2 screenPointStart = cam.UnprojectPosition(Lines[i].Start);
            Vector2 screenPointEnd = cam.UnprojectPosition(Lines[i].End);

            // Don't draw line if either start or end is considered behind the camera
            if (cam.IsPositionBehind(Lines[i].Start) || cam.IsPositionBehind(Lines[i].End))
                continue;

            DrawLine(screenPointStart, screenPointEnd, Lines[i].LineColor);
        }

        // Remove lines that have timed out
        for (int i = Lines.Count - 1; i >= 0; i--)
        {
            if (Lines[i].Time < 0.0f)
            {
                Lines.RemoveAt(i);
                RemovedLine = true;
            }
        }
    }

    public void DrawLine(Vector3 start, Vector3 end, Color lineColor, float time = 0.0f)
    {
        Lines.Add(new Line(start, end, lineColor, time));
    }

    public void DrawRay(Vector3 start, Vector3 ray, Color lineColor, float time = 0.0f)
    {
        Lines.Add(new Line(start, start + ray, lineColor, time));
    }

    public void DrawCube(Vector3 center, float halfExtents, Color lineColor, float time = 0.0f)
    {
        // Start at the 'top left'
        Vector3 linePointStart = center;
        linePointStart.X -= halfExtents;
        linePointStart.Y += halfExtents;
        linePointStart.Z -= halfExtents;

        // Draw top square
        Vector3 linePointEnd = linePointStart + new Vector3(0, 0, halfExtents * 2.0f);
        DrawLine(linePointStart, linePointEnd, lineColor, time);
        linePointStart = linePointEnd;
        linePointEnd = linePointStart + new Vector3(halfExtents * 2.0f, 0, 0);
        DrawLine(linePointStart, linePointEnd, lineColor, time);
        linePointStart = linePointEnd;
        linePointEnd = linePointStart + new Vector3(0, 0, -halfExtents * 2.0f);
        DrawLine(linePointStart, linePointEnd, lineColor, time);
        linePointStart = linePointEnd;
        linePointEnd = linePointStart + new Vector3(-halfExtents * 2.0f, 0, 0);
        DrawLine(linePointStart, linePointEnd, lineColor, time);

        // Draw bottom square
        linePointStart = linePointEnd + new Vector3(0, -halfExtents * 2.0f, 0);
        linePointEnd = linePointStart + new Vector3(0, 0, halfExtents * 2.0f);
        DrawLine(linePointStart, linePointEnd, lineColor, time);
        linePointStart = linePointEnd;
        linePointEnd = linePointStart + new Vector3(halfExtents * 2.0f, 0, 0);
        DrawLine(linePointStart, linePointEnd, lineColor, time);
        linePointStart = linePointEnd;
        linePointEnd = linePointStart + new Vector3(0, 0, -halfExtents * 2.0f);
        DrawLine(linePointStart, linePointEnd, lineColor, time);
        linePointStart = linePointEnd;
        linePointEnd = linePointStart + new Vector3(-halfExtents * 2.0f, 0, 0);
        DrawLine(linePointStart, linePointEnd, lineColor, time);

        // Draw vertical lines
        linePointStart = linePointEnd;
        DrawRay(linePointStart, new Vector3(0, halfExtents * 2.0f, 0), lineColor, time);
        linePointStart += new Vector3(0, 0, halfExtents * 2.0f);
        DrawRay(linePointStart, new Vector3(0, halfExtents * 2.0f, 0), lineColor, time);
        linePointStart += new Vector3(halfExtents * 2.0f, 0, 0);
        DrawRay(linePointStart, new Vector3(0, halfExtents * 2.0f, 0), lineColor, time);
        linePointStart += new Vector3(0, 0, -halfExtents * 2.0f);
        DrawRay(linePointStart, new Vector3(0, halfExtents * 2.0f, 0), lineColor, time);
    }
}
