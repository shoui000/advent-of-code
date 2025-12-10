using System;
using System.Numerics;
using System.Collections.Generic;

namespace AdventOfCode_2025_day09;

class Program {

	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		long fpSolution = s.FirstPuzzle();
		long spSolution = s.SecondPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");
		Console.WriteLine($"SecondPuzzle: The answer is {spSolution}");

	}

}

class Solution {
	private string[] input;

	public Solution(int op) {
		switch (op) {
			case 0: 
				input = File.ReadAllLines("./input_test.txt");
				break;
			case 1:
				input = File.ReadAllLines("./input.txt");
				break;
			default:
				input = [];
				break;
		}
	}

	public long FirstPuzzle() {
		Vector2[] points = new Vector2[input.Length];

		for (int i = 0; i < points.Length; i++) {
			string[] coord = input[i].Split(",");
			Vector2 point = new Vector2(float.Parse(coord[0]), float.Parse(coord[1]));
			points[i] = point;
		}

		Rectangle[] rectangles = new Rectangle[(points.Length * (points.Length-1)) / 2];

		int x = 0;
		for (int i = 0; i < points.Length; i++) {
			for (int j = i+1; j < points.Length; j++) {
				rectangles[x] = new Rectangle(points[i], points[j]);
				x++;
			}
		}

		rectangles.Sort();

		return rectangles[0].Area;
	}

	public long SecondPuzzle() {
		Vector2[] points = new Vector2[input.Length];

		for (int i = 0; i < points.Length; i++) {
			string[] coord = input[i].Split(",");
			Vector2 point = new Vector2(float.Parse(coord[0]), float.Parse(coord[1]));
			points[i] = point;
		}

		List<Edge> edges = new List<Edge>();

		edges.Add(new Edge(points[0], points[^1]));

		for (int i = 1; i < points.Length; i++) {
			edges.Add(new Edge(points[i], points[i-1]));
		}

		Rectangle[] rectangles = new Rectangle[(points.Length * (points.Length-1)) / 2];

		int x = 0;
		for (int i = 0; i < points.Length; i++) {
			for (int j = i+1; j < points.Length; j++) {
				rectangles[x] = new Rectangle(points[i], points[j]);
				x++;
			}
		}

		Polygon p = new Polygon(edges);

		rectangles.Sort();

		x = 0;
		foreach (var r in rectangles) {
			if (p.IsInside(r))
				return r.Area;

			x++;
		}

		return 0;
	}
}

class Rectangle : IComparable<Rectangle> {
	public Vector2 FirstPoint { get; }
	public Vector2 SecondPoint { get; }
	public long Area { get; }

	public Rectangle(Vector2 fp, Vector2 sp) {
		this.FirstPoint = fp;
		this.SecondPoint = sp;

		this.Area = (Math.Abs((long)(fp.X - sp.X)) + 1) * (Math.Abs((long)(fp.Y - sp.Y)) + 1);
	}

	public int CompareTo(Rectangle? other) {
		if (other is null) return 1;

		return other.Area.CompareTo(this.Area);
	}

	public override int GetHashCode() {
		return base.GetHashCode();
	}

	public override string ToString() {
		return $"{this.FirstPoint} {this.SecondPoint} {this.Area}";
	}

	public Vector2 GetPointLeftUp() {
		return new Vector2(Math.Min(this.FirstPoint.X, this.SecondPoint.X),
				Math.Max(this.FirstPoint.Y, this.SecondPoint.Y));
	}

	public Vector2 GetPointRightUp() {
		return new Vector2(Math.Max(this.FirstPoint.X, this.SecondPoint.X),
				Math.Max(this.FirstPoint.Y, this.SecondPoint.Y));
	}

	public Vector2 GetPointLeftDown() {
		return new Vector2(Math.Min(this.FirstPoint.X, this.SecondPoint.X),
				Math.Min(this.FirstPoint.Y, this.SecondPoint.Y));
	}

	public Vector2 GetPointRightDown() {
		return new Vector2(Math.Max(this.FirstPoint.X, this.SecondPoint.X),
				Math.Min(this.FirstPoint.Y, this.SecondPoint.Y));
	}
}

class Edge {
	public Vector2 FirstPoint { get; }
	public Vector2 SecondPoint { get; }

	public Edge(Vector2 fp, Vector2 sp) {
		this.FirstPoint = fp;
		this.SecondPoint = sp;
	}

	public Vector2 GetDirectionVector() {
		return Vector2.Subtract(FirstPoint, SecondPoint);
	}
}

class Polygon {
	public List<Edge> Edges { get; }

	public Polygon(IEnumerable<Edge> edges) {
		this.Edges = edges.ToList();
	}

	public List<Vector2> GetPoints() {
		List<Vector2> ps = new List<Vector2>();

		foreach (Edge e in Edges) {
			if (!ps.Contains(e.FirstPoint))
				ps.Add(e.FirstPoint);

			if (!ps.Contains(e.SecondPoint))
				ps.Add(e.SecondPoint);
		}

		return ps;
	}

	private bool IsPointOnLineSegment(Vector2 a, Vector2 b, Vector2 p) {
		if (p.X < Math.Min(a.X, b.X) || p.X > Math.Max(a.X, b.X) ||
				p.Y < Math.Min(a.Y, b.Y) || p.Y > Math.Max(a.Y, b.Y))
			return false;

		float cross = (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X);
		return cross == 0;
	}

	public bool IsInside(Vector2 p) {
		bool inside = false;

		foreach (var edge in Edges) {
			Vector2 a = edge.FirstPoint;
			Vector2 b = edge.SecondPoint;

			if (IsPointOnLineSegment(a, b, p))
				return true;

			if (a.Y > b.Y) {
				Vector2 temp = a;
				a = b;
				b = temp;
			}

			if (p.Y > a.Y && p.Y <= b.Y) {
				if (Math.Abs(a.Y - b.Y) > 0) {
					float xIntersect = a.X + (p.Y - a.Y) * (b.X - a.X) / (b.Y - a.Y);
					if (xIntersect > p.X)
						inside = !inside;
				}
			}
		}

		return inside;
	}

	public bool IsInside(Rectangle r) {
		Vector2 p1 = r.GetPointLeftUp();
		Vector2 p2 = r.GetPointRightUp();
		Vector2 p3 = r.GetPointLeftDown();
		Vector2 p4 = r.GetPointRightDown();

		if (!(this.IsInside(p1) && this.IsInside(p2) && this.IsInside(p3) && this.IsInside(p4)))
			return false;

		for (int i = (int)(p1.X) + 1; i < (int)(p2.X); i++) {
			if (!this.IsInside(new Vector2((float)i, p1.Y)))
				return false;
		}

		for (int i = (int)(p3.X) + 1; i < (int)(p4.X); i++) {
			if (!this.IsInside(new Vector2((float)i, p1.Y)))
				return false;
		}

		for (int i = (int)(p3.Y) + 1; i < (int)(p1.Y); i++) {
			if (!this.IsInside(new Vector2(p3.X, (float)i)))
				return false;
		}

		for (int i = (int)(p2.Y) + 1; i < (int)(p4.Y); i++) {
			if (!this.IsInside(new Vector2(p2.X, (float)i)))
				return false;
		}

		return true;
	}
}
