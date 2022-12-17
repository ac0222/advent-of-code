// See https://aka.ms/new-console-template for more information
using System.Numerics;

const int NUM_KNOTS = 10; // 2 for part 1

string[] lines = File.ReadAllLines("input.txt");

Rope rope = new Rope(NUM_KNOTS);
foreach(string line in lines)
{
    char direction = line.Split(" ")[0][0];
    int distance = Convert.ToInt32(line.Split(" ")[1]);
    rope.MoveHead(direction, distance);
}

Console.WriteLine($"Tail visited {rope.GetNumberOfPositionsVisitedByKnot(NUM_KNOTS - 1)} positions");
Console.WriteLine("done");

public class Rope
{
    List<HashSet<Vector2>> _positionsVisited = new List<HashSet<Vector2>>();
    private List<Vector2> _knots = new List<Vector2>();

    public Rope (int numKnots)
    {
        for (int i = 0; i < numKnots; i++)
        {
            _knots.Add(Vector2.Zero);
            _positionsVisited.Add(new HashSet<Vector2> {Vector2.Zero});
        }
    }

    public int GetNumberOfPositionsVisitedByKnot(int knotId)
    {
        return _positionsVisited[knotId].Count();
    }

    public void MoveHead(char direction, int distance)
    {
        for (int i = 0; i < distance; i++)
        {
            Vector2 newHead = _knots[0];
            switch (direction)
            {
                case 'R':
                    newHead = _knots[0] + Vector2.UnitX;
                    break;
                case 'L':
                    newHead = _knots[0] + (-1*Vector2.UnitX);
                    break;
                case 'U':
                    newHead = _knots[0] + Vector2.UnitY;
                    break;
                case 'D':
                    newHead = _knots[0] + (-1*Vector2.UnitY);
                    break;
            }
            _positionsVisited[0].Add(newHead);
            _knots[0] = newHead;
            for (int knotId = 1; knotId < _knots.Count; knotId++)
            {
                AdjustKnot(knotId);
            }
        }
    }

    private void AdjustKnot(int knotId)
    {
        Vector2 knot = _knots[knotId];
        Vector2 knotInFront = _knots[knotId -1];
        Vector2 delta = knotInFront - knot;
        if (Math.Abs(delta.X) > 1 || Math.Abs(delta.Y) > 1)
        {
            int deltaX = delta.X == 0 ? 0 : (int)(delta.X/Math.Abs(delta.X));
            int deltaY = delta.Y == 0 ? 0 : (int)(delta.Y/Math.Abs(delta.Y));
            Vector2 moveDelta = new Vector2(deltaX, deltaY);
            Vector2 newKnotPosition = knot + moveDelta;
            _positionsVisited[knotId].Add(newKnotPosition);
            _knots[knotId] = newKnotPosition;
        }
    }
}




