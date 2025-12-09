using System;
using System.Numerics;

namespace AdventOfCode_2025_day09;

class Program {

	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		long fpSolution = s.FirstPuzzle();
		// long spSolution = s.SecondPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");
		// Console.WriteLine($"SecondPuzzle: The answer is {spSolution}");

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
}


